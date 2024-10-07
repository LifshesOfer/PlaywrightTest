using Microsoft.Playwright;

namespace PlaywrightTest.Models.PageObjectModels
{
    internal class InboxPage(IPage page) : PageObject(page)
    {
        public const string Url = "mail.google.com";
    }
}
