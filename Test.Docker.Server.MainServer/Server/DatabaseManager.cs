using System;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Suyeong.Core.DB.MySql;
using Suyeong.Core.Util;
using Test.Docker.Type;
using Test.Docker.Variable;

namespace Test.Docker.Server.MainServer
{
    public static class DatabaseManager
    {
        const string INSERT_FILE = "insert into TEST_FILE (Name, Type) values (@Name, @Type)";
        const string SELECT_FILE_LIST = "select ID, Name, Type from TEST_FILE";

        static string _conStr;

        public static void Init(string databaseIP, string databaseName, string uid, string password)
        {
            _conStr = MySqlDB.GetDbConStr(serverIP: databaseIP, databaseName: databaseName, uid: uid, password: password);
        }

        async public static Task<bool> AddFile(string fileName, string fileType)
        {
            bool result = false;

            try
            {
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter(ParameterNames.NAME, fileName),
                    new MySqlParameter(ParameterNames.TYPE, fileType),
                };

                result = await MySqlDB.SetQueryAsync(conStr: _conStr, query: INSERT_FILE, parameters: parameters).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        async public static Task<FileInfoCollection> GetFileList()
        {
            FileInfoCollection fileInfos = new FileInfoCollection();

            try
            {
                DataTable table = await MySqlDB.GetDataTableAsync(conStr: _conStr, query: SELECT_FILE_LIST).ConfigureAwait(false);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        uint id = ConvertUtil.StringToUint(row[ColumnNames.ID].ToString());
                        string fileName = row[ColumnNames.NAME].ToString();
                        string fileType = row[ColumnNames.TYPE].ToString();

                        fileInfos.Add(new FileInfo(id: id, fileName: fileName, fileType: fileType));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fileInfos;
        }
    }
}
