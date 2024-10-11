using PlaywrightTest.Extensions;

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
            await this.FullLogin(user);

            await Expect(Page.GetByText(ErrorMessages.IncorrectPassword)).ToBeVisibleAsync();
        }

        [Test]
        public async Task LoginWithIncorrectUserName()
        {
            var userName = TestUsers.WrongUser.UserName;
            await this.GoToLogin()
                .Then(loginPage => loginPage.EnterUserAndContinue(userName));

            await Expect(Page.GetByText(ErrorMessages.IncorrectUserName)).ToBeVisibleAsync();
        }

        [Test]
        public async Task LoginWithNoUserName()
        {
            var userName = "";
            await this.GoToLogin()
                .Then(loginPage => loginPage.EnterUserAndContinue(userName));

            await Expect(Page.GetByText(ErrorMessages.NoUserName)).ToBeVisibleAsync();
        }

        
    }
}
