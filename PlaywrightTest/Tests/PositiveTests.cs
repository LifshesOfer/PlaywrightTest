using System.Text.RegularExpressions;

using Microsoft.Playwright.NUnit;
using PlaywrightTest.Models.PageObjectModels;
using PlaywrightTest.TestData;

namespace PlaywrightTest.Tests
{
    //[Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class PositiveTests : PageTest
    {
        [Test]
        public async Task GoToLoginPage()
        {
            var homePage = await HomePage.GotoAsync(Page);
            var loginPage = await homePage.ClickSignIn();

            await Expect(loginPage.titleText).ToBeVisibleAsync();
        }

        [Test]
        public async Task Login()
        {
            var user = TestUsers.TrueUser;
            var inboxPage = await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.Login(user.UserName, user.Password));

            await Expect(Page).ToHaveURLAsync(new Regex(InboxPage.Url));
        }
    }
}