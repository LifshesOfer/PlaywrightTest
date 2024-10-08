﻿using Microsoft.Playwright;
using PlaywrightTest.Models.PageObjectModels.Menus.CreateAccount;

namespace PlaywrightTest.Models.PageObjectModels
{
    internal class LoginPage(IPage page) : PageObject(page)
    {
        public readonly ILocator titleText = page.GetByText("to continue to Gmail");
        public readonly ILocator userInput = page.Locator("#identifierId");
        public readonly ILocator passInput = page.Locator("input[type='password']");
        public readonly ILocator nextButton = page.GetByText("Next");
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

        public async Task<LoginPage> ContinueToPasswordInput()
        {
            await ClickNext();
            return this;
        }

        public async Task<InboxPage> ContinueToInboxPage()
        {
            await ClickNext();
            return new InboxPage(_page);
        }

        public async Task<InboxPage> Login(string userName, string password)
        {
            await EnterUsername(userName);
            await ContinueToPasswordInput();
            await EnterPassword(password);
            return await ContinueToInboxPage();
        }

        public async Task<CreateAccountMenu> ClickCreate()
        {
            var createAccountMenu = new CreateAccountMenu(_page);
            await createAccountMenu.DropdownList.WaitForAsync(new() { State = WaitForSelectorState.Attached });

            await createButton.ClickAsync();
            return new CreateAccountMenu(_page);
        }
    }
}
