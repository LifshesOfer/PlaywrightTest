using Microsoft.Playwright.NUnit;

using PlaywrightTest.Models;
using PlaywrightTest.Models.PageObjectModels;

namespace PlaywrightTest.Extensions
{
    internal static class PageTestExtensions
    {
        public static async Task<InboxPage> FullLogin(this PageTest page, User user)
        {
            return await page.GoToLogin()
                .Then(loginPage => loginPage.Login(user.UserName, user.Password));
        }

        public static async Task<LoginPage> GoToLogin(this PageTest page)
        {
            return await HomePage.GotoAsync(page.Page)
                .Then(homePage => homePage.ClickSignIn());
        }
    }
}
