namespace Test.Docker.Database
{
    public static class Query
    {
        public const string INSERT_USER = "insert into TEST_USER (User_ID, Password, Name) values (@User_ID, @Password, @Name)";
        public const string SELECT_USER_INFO = "select ID, Name from TEST_USER where User_ID = @User_ID and Password = @Password";
    }
}
