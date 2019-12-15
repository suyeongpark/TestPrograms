using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyeong.Lib.Net.Lib;
using Suyeong.Lib.Net.Tcp;

namespace TestTcpServer
{
    class Program
    {
        const int SERVER_PORT = 10000;

        static void Main(string[] args)
        {
            int count = 0;
            TcpListenerSimpleSync listener = new TcpListenerSimpleSync(SERVER_PORT);
            listener.Start(callback: (IPacket packet) =>
            {
                PacketValue value = packet as PacketValue;
                Console.WriteLine($"receive: {value.Protocol}, {value.Value}");
                return new PacketValue(protocol: value.Protocol, count++);
            });
        }
    }
}
