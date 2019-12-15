using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyeong.Lib.Net.Lib;
using Suyeong.Lib.Net.Udp;

namespace TestUdpServer
{
    class Program
    {
        const int SERVER_PORT = 10001;

        static void Main(string[] args)
        {
            int count = 0;
            UdpListenerSimpleSync listener = new UdpListenerSimpleSync(SERVER_PORT);
            listener.ListenerStart(callback: (IPacket packet) =>
            {
                PacketValue value = packet as PacketValue;
                Console.WriteLine($"receive: {value.Protocol}, {value.Value}");
                return new PacketValue(protocol: listener.LocalEndPoint.ToString(), count++);
            });
        }
    }
}
