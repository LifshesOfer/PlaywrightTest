using Microsoft.Playwright.NUnit;

using PlaywrightTest.PageObjectModels;

namespace PlaywrightTest.Tests
{
    //[Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class ExampleTest : PageTest
    {
        [Test]
        public async Task HasTitle()
        {
            var homePage = await HomePage.GotoAsync(Page);
            var loginPage = await homePage.ClickSignIn();

            // Expect a title "to contain" a substring.
            await Expect(loginPage.titleText).ToBeVisibleAsync();
        }

        //[Test]
        //public async Task GetStartedLink()
        //{
        //    await Page.GotoAsync("https://playwright.dev");

        //    // Click the get started link.
        //    await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

        //    // Expects page to have a heading with the name of Installation.
        //    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
        //}
    }
}