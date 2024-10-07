﻿using Microsoft.Playwright;

using PlaywrightTest.PageObjectModels.Menus.CreateAccount;

namespace PlaywrightTest.PageObjectModels
{
    internal class LoginPage(IPage page) : PageObject(page)
    {
        public readonly ILocator titleText = page.Locator("//*[@id='headingText']/span[text()='Sign in']");
        public readonly ILocator userInput = page.Locator("//input[@type='email']");
        public readonly ILocator passInput = page.Locator("//input[@type='password']");
        public readonly ILocator nextButton = page.Locator("//button/span[text()='Next']");
        public readonly ILocator createButton = page.Locator("//button/span[text()='Create account']");

        public async Task<LoginPage> EnterUsername(string userName)
        {
            await userInput.FillAsync(userName);
            return this;
        }

        public async Task<LoginPage> EnterPassword(string password)
        {
            await passInput.FillAsync(password);
            return this;
        }

        public async Task ClickNext()
        {
            await nextButton.ClickAsync();
        }

        public async Task Login(string userName, string password)
        {
            await EnterUsername(userName);
            await ClickNext();
            await EnterPassword(password);
            await ClickNext();
        }

        public async Task<CreateAccountMenu> ClickCreate()
        {
            await createButton.ClickAsync();
            return new CreateAccountMenu(_page);
        }
    }
}
