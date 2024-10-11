using PlaywrightTest.Models.PageObjectModels;

namespace PlaywrightTest.Extensions
{
    internal static class LoginPageExtensions
    {
        public static async Task<InboxPage> Login(this LoginPage loginPage, string userName, string password)
        {
            await loginPage.EnterUsername(userName);
            await loginPage.ContinueToPasswordInput();
            await loginPage.EnterPassword(password);
            return await loginPage.ContinueToInboxPage();
        }

        public static async Task<LoginPage> EnterUserAndContinue(this LoginPage loginPage, string userName)
        {
            await loginPage.EnterUsername(userName);
            return await loginPage.ContinueToPasswordInput();
        }
    }
}
