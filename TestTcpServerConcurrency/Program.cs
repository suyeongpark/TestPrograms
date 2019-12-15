using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Suyeong.Lib.Net.Lib;
using Suyeong.Lib.Net.Tcp;
using Newtonsoft.Json;

namespace TestTcpServerConcurrency
{
    class Program
    {
        const int SERVER_PORT = 10003;
        static TcpListenerConcurrencySync listener;

        static void Main(string[] args)
        {
            listener = new TcpListenerConcurrencySync(portNum: SERVER_PORT, userEnterCallback: UserEnter, userExitCallback: UserExit, responseCallbak: Receive);
            listener.Start();
        }

        static IPacket UserEnter(string stageID, string userID)
        {
            string protocol = "User Enter";
            string value = $"{userID} 입장: {stageID}";
            Console.WriteLine($"[{protocol}] {value}");
            return new PacketValue(protocol: protocol, value: value);
        }

        static IPacket UserExit(string stageID, string userID)
        {
            string protocol = "User Exit";
            string value = $"{userID} 퇴장: {stageID}";
            Console.WriteLine($"[{protocol}] {value}");
            return new PacketValue(protocol: protocol, value: value);
        }

        static IPacket Receive(IPacket packet)
        {
            if (string.Equals(packet.Protocol, "0"))
            {
                PacketJson json = packet as PacketJson;

                Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json.Json);
                string oldStageID = dic["OldStageID"];
                string newStageID = dic["NewStageID"];
                string userID = dic["UserID"];

                Console.WriteLine($"[{json.Protocol}] oldStageID: {oldStageID}, newStageID: {newStageID}, userID: {userID}");

                listener.MoveStage(oldStageID: oldStageID, newStageID: newStageID, userID: userID);

                return null;
            }
            else if (string.Equals(packet.Protocol, "-1"))
            {
                PacketJson json = packet as PacketJson;

                Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json.Json);
                string stageID = dic["StageID"];
                string userID = dic["UserID"];

                Console.WriteLine($"[{json.Protocol}] StageID: {stageID}, userID: {userID}");

                listener.Disconnect(stageID: stageID, userID: userID);

                return null;
            }
            else
            {
                PacketValue receive = packet as PacketValue;
                Console.WriteLine($"[{receive.Protocol}] {receive.Value}");
                return new PacketValue(protocol: receive.Protocol, value: receive.Value);
            }
        }
    }
}
