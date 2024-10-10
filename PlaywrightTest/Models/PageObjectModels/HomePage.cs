using Microsoft.Playwright;

namespace PlaywrightTest.Models.PageObjectModels
{
    internal class HomePage(IPage page) : PageObject(page)
    {
        public const string Url = "https://www.google.com/intl/en-US/gmail/about/";
        public readonly ILocator signInButton = page.GetByText("Sign in");

        public static async Task<HomePage> GotoAsync(IPage page)
        {
            await page.GotoAsync(Url);
            return new HomePage(page);
        }

        public async Task<LoginPage> ClickSignIn()
        {
            await signInButton.ClickAsync();
            return new LoginPage(_page);
        }
    }
}
