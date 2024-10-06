using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Playwright;

namespace PlaywrightTest.Models
{
    public class GmailHomePage(IPage page)
    {
        private readonly IPage _page = page;
        public const string Url = "https://gmail.com";
        private readonly ILocator _signInButton = page.Locator("[data-action=\"sign in\"]");

        public async Task GotoAsync()
        {
            await _page.GotoAsync(Url);
        }

        public async Task ClickSignIn()
        {
            await _signInButton.ClickAsync();
        }
    }
}
