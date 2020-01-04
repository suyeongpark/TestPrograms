using System;
using System.Threading.Tasks;
using Test.Docker.Variable;

namespace Test.Docker.Server.Chatting
{
    class Program
    {
        static void Main(string[] args)
        {
            string databaseIP = "";

            DbManager.Init(serverIP: databaseIP, databaseName: Connection.DB_NAME, uid: Connection.DB_NAME, password: Connection.DB_PASSWORD);
            TcpManager.Init(portNum: Connection.PORT_NUM_SERVER_CHATTING);

            Task tcp = Task.Run(TcpManager.Listening);

            tcp.Wait();
        }
    }
}
