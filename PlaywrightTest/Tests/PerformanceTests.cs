using System.Diagnostics;
using System.Text.RegularExpressions;

using Microsoft.Playwright;

using PlaywrightTest.Models.PageObjectModels;
using PlaywrightTest.Models.PageObjectModels.Menus.CreateAccount;
using PlaywrightTest.TestData;

namespace PlaywrightTest.Tests
{

    /// <summary>
    /// Apparently Playwright doesn't 'performance' implementations in .NET
    /// so these tests are placeholders as external measurements (assuming no usage of DevTools)
    /// are affected by too many factors to be reliable.
    /// </summary>
    [NonParallelizable]   
    [TestFixture]
    public class PerformanceTests : BaseTest
    {
        //sample for usage, could be used for additional setup/teardown functions within a test fixture
        public override async Task OnTeardown()
        {
            Console.WriteLine("Performance test finished.");
        }

        public override async Task OnSetup()
        {
            Console.WriteLine("Starting Performance test.");
        }

        // time measurement function
        private static async Task<TimeSpan> Time(Func<Task> action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        [Test]
        public async Task GoToPasswordInput_ExpectLessThan5Seconds()
        {
            //setup
            var user = TestUsers.TrueUser;
            var loginPage = await HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.EnterUsername(user.UserName));

            //measure
            var elapsed = await Time(() => 
                    loginPage.ContinueToPasswordInput()
                        .Then(loginPage => loginPage.passInput.IsEditableAsync()));

            Console.WriteLine(elapsed);
            Assert.That(elapsed, Is.LessThan(TimeSpan.FromSeconds(5)));
        }

        [Test]
        public async Task FullLogin_ExpectLessThan5Seconds()
        {
            var user = TestUsers.TrueUser;
            
            //measure
            var elapsed = await Time(() =>
                    HomePage.GotoAsync(Page)
                .Then(homePage => homePage.ClickSignIn())
                .Then(loginPage => loginPage.Login(user.UserName, user.Password)));

            Console.WriteLine(elapsed);
            Assert.That(elapsed, Is.LessThan(TimeSpan.FromSeconds(5)));
        }
    }
}
