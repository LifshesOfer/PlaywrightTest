using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Playwright.NUnit;

using PlaywrightTest.Models.PageObjectModels;

using PlaywrightTest.TestData;

namespace PlaywrightTest.Tests
{
    [TestFixture]
    public class NegativeTests : PageTest
    {
        [Test]
        public async Task LoginWithIncorrectPassword()
        {
            var user = TestUsers.UserBadPass;
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.Login(user.UserName, user.Password));

            await Expect(Page.GetByText("Wrong password. Try again or click Forgot password to reset it.")).ToBeVisibleAsync();
        }

        [Test]
        public async Task LoginWithIncorrectUserName()
        {
            var user = TestUsers.WrongUser;
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.EnterUsername(user.UserName))
                .Then(loginPage => loginPage.ContinueToPasswordInput());

            await Expect(Page.GetByText("Couldn’t find your Google Account")).ToBeVisibleAsync();
        }
    }
}
