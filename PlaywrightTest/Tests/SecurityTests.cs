using PlaywrightTest.Models.PageObjectModels;
using PlaywrightTest.TestData;

namespace PlaywrightTest.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class SecurityTests : BaseTest
    {
        
        public static object[] UserSqlInjectionData = 
        {
            new object[] { SqlInjections.SingleQuote, ErrorMessages.IncorrectUserName },
            new object[] { SqlInjections.TrueStatement, ErrorMessages.IncorrectUserName },
            new object[] { SqlInjections.FullQuery, ErrorMessages.InvalidUserName }
        };

        public static object[] PasswordSqlInjectionData = {

            new object[] { SqlInjections.SingleQuote, ErrorMessages.IncorrectPassword },
            new object[] { SqlInjections.TrueStatement, ErrorMessages.IncorrectPassword },
            new object[] { SqlInjections.FullQuery, ErrorMessages.IncorrectPassword }
        };

        [Test]
        [TestCaseSource(nameof(UserSqlInjectionData))]
        public async Task CheckSqlInUserName_ExpectErrorMessage(string sql, string expected)
        {
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.EnterUsername(sql))
                .Then(loginPage => loginPage.ContinueToPasswordInput());

            await Expect(Page.GetByText(expected)).ToBeVisibleAsync();
        }

        [Test]
        [TestCaseSource(nameof(PasswordSqlInjectionData))]
        public async Task CheckSqlInPassword_ExpectErrorMessage(string sql, string expected)
        {
            var userName = TestUsers.TrueUser.UserName;
            await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(LoginPage => LoginPage.Login(userName, sql));

            await Expect(Page.GetByText(expected)).ToBeVisibleAsync();
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
            var captchaText = "Type the text you hear or see";
            while (await Page.GetByText(captchaText).IsHiddenAsync())
            {
                await login.ContinueToPasswordInput();
                counterUntilCaptcha++;
            }
            Assert.That(counterUntilCaptcha, Is.GreaterThan(1));
            await Expect(Page.GetByText(captchaText)).ToBeVisibleAsync();
        }
    }
}
