using System;
using System.Threading.Tasks;
using Test.Docker.Variable;

namespace Test.Docker.Server.Login
{
    class Program
    {
        static void Main(string[] args)
        {
            string databaseIP = "localhost";

            DbManager.Init(serverIP: databaseIP, databaseName: Connection.DB_NAME, uid: Connection.DB_ID, password: Connection.DB_PASSWORD);
            TcpManager.Init(portNum: Connection.PORT_NUM_SERVER_LOGIN);

            Task tcp = Task.Run(TcpManager.Listening);
            tcp.Wait();
        }
    }
}
