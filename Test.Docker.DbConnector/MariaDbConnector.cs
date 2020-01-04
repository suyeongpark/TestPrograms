using System;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Suyeong.Core.DB.MySql;
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

        async public Task<UserInfo> GetUserInfo(string userID, string cryptedPassword, int state)
        {
            UserInfo userInfo = new UserInfo();

            try
            {
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter(ParameterName.USER_ID, userID.ToLowerInvariant()),
                    new MySqlParameter(ParameterName.PASSWORD, cryptedPassword),
                    new MySqlParameter(ParameterName.STATE, state),
                };

                DataTable table = await MySqlDB.GetDataTableAsync(conStr: this.conStr, query: Query.SELECT_USER_INFO, parameters: parameters).ConfigureAwait(false);

                if (table.Rows.Count > 0)
                {
                    //userInfo = DataConverter.ConvertUserInfo(row: table.Rows[0], dbTypeIdDic: DatabaseTypeIdDic, userID: userID, cryptedPassword: cryptedPassword);
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
