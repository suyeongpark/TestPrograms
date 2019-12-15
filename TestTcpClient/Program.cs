﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Suyeong.Lib.Net.Lib;
using Suyeong.Lib.Net.Tcp;

namespace TestTcpClient
{
    class Program
    {
        const int SERVER_PORT = 10000;
        const string SERVER_IP = "220.76.209.32";

        static void Main(string[] args)
        {
            int index = 0;
            string ip = Client_IP;
            TcpClientSimpleSync client = new TcpClientSimpleSync(SERVER_IP, SERVER_PORT);
            PacketValue packet;

            while (true)
            {
                packet = client.Send(new PacketValue(ip, index++)) as PacketValue;

                Console.WriteLine($"receive: {packet.Value}");
                Thread.Sleep(2000);
            }
        }

        static string Client_IP
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
