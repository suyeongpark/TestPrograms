using System;
using System.IO;
using System.Threading.Tasks;
using Suyeong.Core.Net.Lib;
using Suyeong.Core.Net.Tcp;
using Suyeong.Core.Util;
using Test.Docker.Type;
using Test.Docker.Variable;

namespace Test.Docker.Server.MainServer
{
    public static class ClientListener
    {
        static TcpListenerSimpleAsync _listener;

        public static void Init(int portNum)
        {
            _listener = new TcpListenerSimpleAsync(portNum: portNum);
        }

        async public static Task Listening()
        {
            try
            {
                await _listener.StartAsync(callback: ReturnRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        async static Task<IPacket> ReturnRequest(IPacket packet)
        {
            Console.WriteLine($"{packet.Protocol} /{DateTime.Now.ToString(Formats.DATE_LOG)}");

            switch (packet.Protocol)
            {
                case Protocols.UPLOAD_FILE:
                    return await UploadFile(packet: packet as PacketFile);

                case Protocols.GET_FILE_LIST:
                    return await GetFileList(packet: packet as PacketValue);

                default:
                    return new PacketValue(protocol: string.Empty, value: 0);
            }
        }

        async static Task<PacketSerialized> UploadFile(PacketFile packet)
        {
            FileManager.SetFileToWaitingPath(fileName: packet.Desc, fileData: packet.FileData);

            string fileName = Path.GetFileNameWithoutExtension(packet.Desc);
            string fileType = Path.GetExtension(packet.Desc);
            bool result = await DatabaseManager.AddFile(fileName: fileName, fileType: fileType);

            FileInfoCollection files = await DatabaseManager.GetFileList();
            return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(files));
        }

        async static Task<PacketSerialized> GetFileList(PacketValue packet)
        {
            FileInfoCollection files = await DatabaseManager.GetFileList();
            return new PacketSerialized(protocol: packet.Protocol, serializedData: StreamUtil.SerializeObject(files));
        }
    }
}
