using System.Text.RegularExpressions;

using PlaywrightTest.Extensions;
using PlaywrightTest.Models.PageObjectModels;
using PlaywrightTest.Models.PageObjectModels.Menus.CreateAccount;
using PlaywrightTest.TestData;

namespace PlaywrightTest.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class PositiveTests : BaseTest
    {
        [Test]
        public async Task GoToLoginPage()
        {
            var loginPage = await this.GoToLogin();

            await Expect(loginPage.userInput).ToBeVisibleAsync();
        }

        [Test]
        public async Task Login()
        {
            var user = TestUsers.TrueUser;

            await this.FullLogin(user);

            await Expect(Page).ToHaveURLAsync(new Regex(InboxPage.Url));
        }

        [Test]
        public async Task ContinueToPasswordInput()
        {
            var userName = TestUsers.TrueUser.UserName;

            var login = await this.GoToLogin()
                .Then(loginPage => loginPage.EnterUserAndContinue(userName));


            await Expect(login.passInput).ToBeVisibleAsync();
        }

        [Test]
        public async Task LoginThenNavigateToHomePage_ExpectAccountChoiceList()
        {
            //initial login
            var user = TestUsers.TrueUser;
            await this.FullLogin(user);

            // await full inbox page load
            await Page.WaitForURLAsync(new Regex("#inbox$"));

            // navigate back to home page
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn());

            await Expect(Page.Locator($"[data-email='{user.UserName}']")).ToBeVisibleAsync();
        }

        [Test]
        public async Task CheckCreateOptions()
        {
            var createMenu = await this.GoToLogin()
                .Then(loginPage => loginPage.ClickCreate());

            await createMenu.DropdownList.IsVisibleAsync();
            var menuList = await createMenu.MenuItems.AllInnerTextsAsync();
            var expectedList = new List<string> 
            { 
                CreateAccountOptions.ForMyself,
                CreateAccountOptions.ForBusiness,
                CreateAccountOptions.ForChild
            };
            Assert.That(menuList, Is.EquivalentTo(expectedList));
        }
    }
}