using PlaywrightTest.Models.PageObjectModels;

using PlaywrightTest.TestData;

namespace PlaywrightTest.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class NegativeTests : BaseTest
    {
        

        [Test]
        public async Task LoginWithIncorrectPassword()
        {
            var user = TestUsers.UserBadPass;
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.Login(user.UserName, user.Password));

            await Expect(Page.GetByText(ErrorMessages.IncorrectPassword)).ToBeVisibleAsync();
        }

        [Test]
        public async Task LoginWithIncorrectUserName()
        {
            var user = TestUsers.WrongUser;
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.EnterUsername(user.UserName))
                .Then(loginPage => loginPage.ContinueToPasswordInput());

            await Expect(Page.GetByText(ErrorMessages.IncorrectUserName)).ToBeVisibleAsync();
        }

        [Test]
        public async Task LoginWithNoUserName()
        {
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.EnterUsername(""))
                .Then(loginPage => loginPage.ContinueToPasswordInput());

            await Expect(Page.GetByText(ErrorMessages.NoUserName)).ToBeVisibleAsync();
        }

        
    }
}
