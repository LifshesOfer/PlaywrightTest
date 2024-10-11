namespace PlaywrightTest.TestData
{
    public static class SqlInjections
    {
        public static readonly string SingleQuote = "'";
        public static readonly string TrueStatement = "'1=1";
        public static readonly string FullQuery = $"SELECT * FROM users WHERE username='{TestUsers.TrueUser.UserName}' AND password='{TestUsers.TrueUser.Password}'";
    }
}
