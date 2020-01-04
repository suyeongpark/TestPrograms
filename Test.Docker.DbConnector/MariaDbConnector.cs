using System;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Suyeong.Core.DB.MySql;

namespace Test.Docker.Database
{
    public class MariaDbConnector
    {
        string conStr;

        public MariaDbConnector(string serverIP, string databaseName, string uid, string password)
        {
            this.conStr = MySqlDB.GetDbConStr(serverIP: serverIP, databaseName: databaseName, uid: uid, password: password);
        }


        //async public Task<uint> GetLastID()
        //{
        //    uint result = 0;

        //    try
        //    {
        //        object obj = await MySqlDB.GetDataSingleAsync(conStr: this.conStr, query: Query.SELECT_LAST_INSERT_ID);
        //        result = ConvertUtil.StringToUint(obj?.ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return result;
        //}

        //async public Task<bool> AddUser(UserInfo userInfo, int state)
        //{
        //    bool result = false;

        //    try
        //    {
        //        uint authorityID = DataConverter.GetTypeIdFromGroupDic(type: ColumnName.AUTHORITY, name: userInfo.Authority.ToString(), dataTypeGroupDic: this.DataTypeGroupDic);
        //        uint departmentID = DataConverter.GetTypeIdFromGroupDic(type: ColumnName.DEPARTMENT, name: userInfo.Department.ToString(), dataTypeGroupDic: this.DataTypeGroupDic);
        //        uint classID = DataConverter.GetTypeIdFromGroupDic(type: ColumnName.CLASS, name: userInfo.UserClass.ToString(), dataTypeGroupDic: this.DataTypeGroupDic);
        //        uint roleID = DataConverter.GetTypeIdFromGroupDic(type: ColumnName.ROLE, name: userInfo.Role.ToString(), dataTypeGroupDic: this.DataTypeGroupDic);

        //        MySqlParameter[] parameters = new MySqlParameter[]
        //        {
        //            new MySqlParameter(ParameterName.AUTHORITY_ID, authorityID),
        //            new MySqlParameter(ParameterName.DEPARTMENT_ID, departmentID),
        //            new MySqlParameter(ParameterName.CLASS_ID, classID),
        //            new MySqlParameter(ParameterName.ROLE_ID, roleID),
        //            new MySqlParameter(ParameterName.USER_ID, userInfo.UserID),
        //            new MySqlParameter(ParameterName.NAME, userInfo.Name),
        //            new MySqlParameter(ParameterName.PASSWORD, userInfo.CryptedPassword),
        //            new MySqlParameter(ParameterName.EMAIL, userInfo.Email),
        //            new MySqlParameter(ParameterName.STATE, state),
        //        };

        //        result = await MySqlDB.SetQueryAsync(conStr: this.conStr, query: Query.INSERT_USER, parameters: parameters);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return result;
        //}
    }
}
