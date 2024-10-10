using System.Text.RegularExpressions;

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
            var homePage = await HomePage.GotoAsync(Page);
            var loginPage = await homePage.ClickSignIn();

            await Expect(loginPage.userInput).ToBeVisibleAsync();
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

        [Test]
        public async Task CheckCreateOptions()
        {
            var createMenu = await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
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