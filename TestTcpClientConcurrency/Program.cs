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


        static TcpClient client;

        static void Main(string[] args)
        {
            string ip = ClientIP;
            string msg;
            PacketValue receivePacket, sendPacket;
            PacketJson sendPacketJson;
            int stageID = 0;

            try
            {
                TcpClientConcurrencySync tcpClinet = new TcpClientConcurrencySync(serverIP: SERVER_IP, serverPort: SERVER_PORT, callback: (IPacket packet) =>
                {
                    receivePacket = packet as PacketValue;
                    Console.WriteLine($"[receive] {receivePacket.Protocol}: {receivePacket.Value}");
                });

                tcpClinet.Start(stageID: stageID.ToString(), userID: ip);

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
                        tcpClinet.Send(sendPacketJson);
                    }
                    else if (string.Equals(msg, "-1"))
                    {
                        dic = new Dictionary<string, string>();
                        dic.Add("StageID", stageID.ToString());
                        dic.Add("UserID", ip);

                        sendPacketJson = new PacketJson(protocol: msg, json: JsonConvert.SerializeObject(dic));
                        tcpClinet.Send(sendPacketJson);
                    }
                    else
                    {
                        sendPacket = new PacketValue(protocol: ip, value: msg);
                        tcpClinet.Send(sendPacket);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            //client = new TcpClient(hostname: SERVER_IP, port: SERVER_PORT);

            //Thread t1 = new Thread(Start);
            //Thread t2 = new Thread(Wait);
            //t1.Start();
            //t2.Start();
        }



        //public void Dispose()
        //{
        //    if (client.Connected)
        //    {
        //        client.Close();
        //    }
        //}

        //public void Start(string stageID, string userID)
        //{
        //    NetworkStream stream;
        //    IPacket received;
        //    byte[] receiveHeader, receiveData, decompressData;
        //    int nbytes, receiveDataLength;

        //    // 1. 접속을 생성한다.
        //    client = new TcpClient(hostname: SERVER_IP, port: SERVER_PORT);

        //    // 2. 접속할 사용자 정보를 보낸다.
        //    // protocol에 stage id를 넣고, value에 user id를 넣는다.
        //    PacketValue packet = new PacketValue(protocol: stageID, value: userID);
        //    Send(packet: packet);

        //    // 그 후에 서버에서 오는 메세지를 듣기 위해 별도의 쓰레드를 돌린다.
        //    Thread thread = new Thread(() =>
        //    {
        //        while (client.Connected)
        //        {
        //            try
        //            {
        //                stream = client.GetStream();

        //                // 1. 결과의 헤더를 받는다.
        //                receiveHeader = new byte[Consts.SIZE_HEADER];
        //                nbytes = stream.Read(buffer: receiveHeader, offset: 0, size: receiveHeader.Length);

        //                // 2. 결과의 데이터를 받는다.
        //                receiveDataLength = BitConverter.ToInt32(value: receiveHeader, startIndex: 0);
        //                receiveData = TcpUtil.ReceiveData(networkStream: stream, dataLength: receiveDataLength);

        //                stream.Flush();

        //                // 3. 결과는 압축되어 있으므로 푼다.
        //                decompressData = NetUtil.Decompress(data: receiveData);
        //                received = NetUtil.DeserializeObject(data: decompressData) as IPacket;

        //                callback(received);
        //            }
        //            catch (SocketException ex)
        //            {
        //                Dispose();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex);
        //                Dispose();
        //            }
        //        }
        //    });

        //    thread.IsBackground = true;
        //    thread.Start();
        //}

        //public void Send(IPacket packet)
        //{
        //    try
        //    {
        //        NetworkStream stream = client.GetStream();

        //        // 1. 보낼 데이터를 압축한다.
        //        byte[] sendData = NetUtil.SerializeObject(data: packet);
        //        byte[] compressData = NetUtil.Compress(data: sendData);

        //        // 2. 요청의 헤더를 보낸다.
        //        int sendDataLength = compressData.Length;
        //        byte[] sendHeader = BitConverter.GetBytes(value: sendDataLength);
        //        stream.Write(buffer: sendHeader, offset: 0, size: sendHeader.Length);

        //        // 3. 요청을 보낸다.
        //        TcpUtil.SendData(networkStream: stream, data: compressData, dataLength: sendDataLength);

        //        stream.Flush();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}

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
