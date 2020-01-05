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

        public static Task<bool> AddUser(string userID, string cryptedPassword, string userName)
        {
            return _database.AddUser(userID: userID, passwordCrypted: cryptedPassword, userName: userName);
        }

        public static Task<UserInfo> GetUserInfo(string userID, string passwordCrypted, int state)
        {
            return _database.GetUserInfo(userID: userID, passwordCrypted: passwordCrypted, state: state);
        }
    }
}
