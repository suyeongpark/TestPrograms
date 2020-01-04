using System;
using System.Collections.Generic;

namespace Test.Docker.Type
{
    [Serializable]
    public struct UserInfo
    {
        public UserInfo(uint id, string userID, string passwordCrypted, string name)
        {
            this.ID = id;
            this.UserID = userID;
            this.PasswordCrypted = passwordCrypted;
            this.Name = name;
        }

        public uint ID { get; private set; }
        public string UserID { get; private set; }
        public string PasswordCrypted { get; private set; }
        public string Name { get; private set; }
    }

    [Serializable]
    public class UserInfoCollection : List<UserInfo>
    {
        public UserInfoCollection()
        {

        }
    }
}
