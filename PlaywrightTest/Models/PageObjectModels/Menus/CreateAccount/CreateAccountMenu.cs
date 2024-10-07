using Microsoft.Playwright;
using PlaywrightTest.Models.PageObjectModels;

namespace PlaywrightTest.Models.PageObjectModels.Menus.CreateAccount
{
    internal class CreateAccountMenu(IPage page) : PageObject(page)
    {
        public readonly ILocator DropdownList = page.Locator("//ul[@role='menu'][@aria-label='Create account']");
        public readonly ILocator MenuItems = page.Locator("role='menuitem'");
        public ILocator MenuItemOption(string optionText) => MenuItems.Locator($"//li[text()='{optionText}']");
        public async Task SelectOption(string optionText)
        {
            await MenuItemOption(optionText).ClickAsync();
        }
    }
}
