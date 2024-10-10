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

        [Test]
        public async Task LoginWithNoUserName()
        {
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.EnterUsername(""))
                .Then(loginPage => loginPage.ContinueToPasswordInput());

            await Expect(Page.GetByText("Enter an email or phone number")).ToBeVisibleAsync();
        }

        [Test]
        public async Task MultipleLoginWithIncorrectUserName_ExpectCaptcha()
        {
            var user = TestUsers.WrongUser;
            var login = await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.EnterUsername(user.UserName))
                .Then(loginPage => loginPage.ContinueToPasswordInput());

            int counterUntilCaptcha = 1;

            while(await Page.GetByText("Type the text you hear or see").IsHiddenAsync())
            {
                await login.ContinueToPasswordInput();
                counterUntilCaptcha++;
            }
            Assert.That(counterUntilCaptcha, Is.GreaterThan(1));
            await Expect(Page.GetByText("Type the text you hear or see")).ToBeVisibleAsync();
        }
    }
}
