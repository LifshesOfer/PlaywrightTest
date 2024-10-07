using Microsoft.Playwright;

namespace PlaywrightTest.Models.PageObjectModels
{
    internal abstract class PageObject(IPage page)
    {
        protected readonly IPage _page = page;
    }
}
