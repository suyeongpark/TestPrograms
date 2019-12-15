using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Suyeong.Lib.Net.Lib;
using Suyeong.Lib.Net.Tcp;
using Newtonsoft.Json;

namespace TestTcpClientConcurrency
{
    class Program
    {
        const int SERVER_PORT = 10003;
        const string SERVER_IP = "220.76.209.32";

        static void Main(string[] args)
        {
            string ip = ClientIP;
            string msg;
            PacketValue receivePacket, sendPacket;
            PacketJson sendPacketJson;
            int stageID = 0;

            Task task1 = Task.Run(async () =>
            {
                TcpClientConcurrencyAsync tcpClinet = new TcpClientConcurrencyAsync(serverIP: SERVER_IP, serverPort: SERVER_PORT, callback: async (IPacket packet) =>
                {
                    receivePacket = packet as PacketValue;
                    Console.WriteLine($"[receive] {receivePacket.Protocol}: {receivePacket.Value}");
                });

                await tcpClinet.StartAsync(stageID: stageID.ToString(), userID: ip);

                Dictionary<string, string> dic;

                while (true)
                {
                    msg = Console.ReadLine();

                    if (string.Equals(msg, "0"))
                    {
                        dic = new Dictionary<string, string>();
                        dic.Add("OldStageID", stageID++.ToString());
                        dic.Add("NewStageID", stageID.ToString());
                        dic.Add("UserID", ip);

                        sendPacketJson = new PacketJson(protocol: msg, json: JsonConvert.SerializeObject(dic));
                        await tcpClinet.SendAsync(sendPacketJson);
                    }
                    else if (string.Equals(msg, "-1"))
                    {
                        dic = new Dictionary<string, string>();
                        dic.Add("StageID", stageID.ToString());
                        dic.Add("UserID", ip);

                        sendPacketJson = new PacketJson(protocol: msg, json: JsonConvert.SerializeObject(dic));
                        await tcpClinet.SendAsync(sendPacketJson);
                    }
                    else
                    {
                        sendPacket = new PacketValue(protocol: ip, value: msg);
                        await tcpClinet.SendAsync(sendPacket);
                    }
                }
            });

            task1.Wait();


            //try
            //{
            //    TcpClientConcurrencySync tcpClinet = new TcpClientConcurrencySync(serverIP: SERVER_IP, serverPort: SERVER_PORT, callback: (IPacket packet) =>
            //    {
            //        receivePacket = packet as PacketValue;
            //        Console.WriteLine($"[receive] {receivePacket.Protocol}: {receivePacket.Value}");
            //    });

            //    tcpClinet.Start(stageID: stageID.ToString(), userID: ip);

            //    Dictionary<string, string> dic;

            //    while (true)
            //    {
            //        msg = Console.ReadLine();

            //        if (string.Equals(msg, "0"))
            //        {
            //            dic = new Dictionary<string, string>();
            //            dic.Add("OldStageID", stageID++.ToString());
            //            dic.Add("NewStageID", stageID.ToString());
            //            dic.Add("UserID", ip);

            //            sendPacketJson = new PacketJson(protocol: msg, json: JsonConvert.SerializeObject(dic));
            //            tcpClinet.Send(sendPacketJson);
            //        }
            //        else if (string.Equals(msg, "-1"))
            //        {
            //            dic = new Dictionary<string, string>();
            //            dic.Add("StageID", stageID.ToString());
            //            dic.Add("UserID", ip);

            //            sendPacketJson = new PacketJson(protocol: msg, json: JsonConvert.SerializeObject(dic));
            //            tcpClinet.Send(sendPacketJson);
            //        }
            //        else
            //        {
            //            sendPacket = new PacketValue(protocol: ip, value: msg);
            //            tcpClinet.Send(sendPacket);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        static string ClientIP
        {
            get
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                for (int i = 0; i < host.AddressList.Length; i++)
                {
                    if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return host.AddressList[i].ToString();
                    }
                }
                return string.Empty;
            }
        }
    }
}
