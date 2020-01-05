using System;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Suyeong.Core.DB.MySql;
using Suyeong.Core.Util;
using Test.Docker.Type;
using Test.Docker.Variable;

namespace Test.Docker.Database
{
    public class MariaDbConnector
    {
        string conStr;

        public MariaDbConnector(string serverIP, string databaseName, string uid, string password)
        {
            this.conStr = MySqlDB.GetDbConStr(serverIP: serverIP, databaseName: databaseName, uid: uid, password: password);
        }

        async public Task<bool> AddUser(string userID, string passwordCrypted, string userName)
        {
            bool result = false;

            try
            {
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter(ParameterName.USER_ID, userID?.ToUpperInvariant()),
                    new MySqlParameter(ParameterName.PASSWORD, passwordCrypted),
                    new MySqlParameter(ParameterName.NAME, userName),
                };

                result = await MySqlDB.SetQueryAsync(conStr: this.conStr, query: Query.INSERT_USER, parameters: parameters).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        async public Task<UserInfo> GetUserInfo(string userID, string passwordCrypted, int state)
        {
            UserInfo userInfo = new UserInfo();

            try
            {
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter(ParameterName.USER_ID, userID?.ToUpperInvariant()),
                    new MySqlParameter(ParameterName.PASSWORD, passwordCrypted),
                };

                DataTable table = await MySqlDB.GetDataTableAsync(conStr: this.conStr, query: Query.SELECT_USER_INFO, parameters: parameters).ConfigureAwait(false);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    uint id = ConvertUtil.StringToUint(row[ColumnName.ID].ToString());
                    string name = row[ColumnName.NAME].ToString();

                    userInfo = new UserInfo(id: id, userID: userID, passwordCrypted: passwordCrypted, name: name);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return userInfo;
        }
    }
}
