using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Docker.Database;
using Test.Docker.Type;

namespace Test.Docker.Server.Login
{
    public static class DbManager
    {
        static MariaDbConnector _database;

        public static void Init(string serverIP, string databaseName, string uid, string password)
        {
            _database = new MariaDbConnector(serverIP: serverIP, databaseName: databaseName, uid: uid, password: password);
        }

        public static Task<UserInfo> GetUserInfo(string userID, string cryptedPassword, int state)
        {
            return _database.GetUserInfo(userID: userID, cryptedPassword: cryptedPassword, state: state);
        }
    }
}
