using System;

namespace Test.Docker.Variable
{
    public static class Connection
    {
        public static byte[] CRYPT_KEY = { 185, 229, 88, 200, 15, 6, 49, 236, 2, 151, 71, 229, 68, 43, 149, 99, };
        public static byte[] CRYPT_IV = { 14, 12, 128, 96, 21, 109, 153, 5, 50, 16, 208, 114, 34, 25, 239, 138, };

        public const string DB_NAME = "Test.Docker";
        public const string DB_ID = "suyeongpark";
        public const string DB_PASSWORD = "dBb%fU(5W-zWMrd";

        public const int PORT_NUM_SERVER_LOGIN = 32000;
        public const int PORT_NUM_SERVER_CHATTING = 32001;
    }
}
