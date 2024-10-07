using PlaywrightTest.Models;

namespace PlaywrightTest.TestData
{
    public static class TestUsers
    {
        public static readonly User TrueUser = new User("mctestersontester554@gmail.com", "!@#123QWEqwe");
        public static readonly User UserBadPass = new User("mctestersontester554@gmail.com", "12345678");
        public static readonly User WrongUser = new User("donatester@gmail.com", "12345678");

    }
}
