using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Test.Docker.Variable;

namespace Test.Docker.Server.MainServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + Pathes.SETUP);
            Dictionary<string, object> setupDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(file);

            Dictionary<string, object> addressDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(setupDic[ColumnNames.ADDRESS].ToString());
            string databaseIP = addressDic[ColumnNames.DATABASE_IP].ToString();
            string vaultDrive = addressDic[ColumnNames.VAULT_DRIVE].ToString();

            DatabaseManager.Init(databaseIP: databaseIP, databaseName: Connections.DB_NAME, uid: Connections.DB_ID, password: Connections.DB_PASSWORD);
            FileManager.Init(vaultDrive: vaultDrive);
            ClientListener.Init(portNum: Connections.PORT_NUM_SERVER_MAIN);

            Task tcp = Task.Run(ClientListener.Listening);
            tcp.Wait();
        }
    }
}
