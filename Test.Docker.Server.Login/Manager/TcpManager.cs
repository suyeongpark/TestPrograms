using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Suyeong.Core.Net.Lib;
using Suyeong.Core.Net.Tcp;
using Suyeong.Core.Util;
using Test.Docker.Type;
using Test.Docker.Variable;

namespace Test.Docker.Server.Login
{
    public static class TcpManager
    {
        static TcpListenerSimpleAsync _listener;

        public static void Init(int portNum)
        {
            _listener = new TcpListenerSimpleAsync(portNum: portNum);
        }

        async public static Task Listening()
        {
            await _listener.StartAsync(callback: ReturnRequest).ConfigureAwait(false);
        }

        async static Task<IPacket> ReturnRequest(IPacket packet)
        {
            Console.WriteLine($"{packet.Protocol} /{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");

            switch (packet.Protocol)
            {
                case Protocol.CREATE_USER:
                    return await CreateUser(packet: packet as PacketJson).ConfigureAwait(false);

                case Protocol.GET_USER:
                    return await GetUser(packet: packet as PacketJson).ConfigureAwait(false);

                default:
                    return new PacketValue(protocol: string.Empty, value: 0);
            }
        }

        async static Task<PacketValue> CreateUser(PacketJson packet)
        {
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Json);
            string userID = dic[ColumnName.ID].ToString();
            string cryptedPassword = dic[ColumnName.PASSWORD].ToString();
            string userName = dic[ColumnName.NAME].ToString();

            bool result = await DbManager.AddUser(userID: userID, cryptedPassword: cryptedPassword, userName: userName).ConfigureAwait(false);

            return new PacketValue(protocol: packet.Protocol, value: result ? 1 : 0);
        }

        async static Task<PacketSerialized> GetUser(PacketJson packet)
        {
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Json);
            string userID = dic[ColumnName.ID].ToString();
            string passwordCrypted = dic[ColumnName.PASSWORD].ToString();

            UserInfo result = await DbManager.GetUserInfo(userID: userID, passwordCrypted: passwordCrypted, state: (int)DbState.Enable).ConfigureAwait(false);

            return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(result));
        }
    }
}
