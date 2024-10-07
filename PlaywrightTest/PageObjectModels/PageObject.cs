using Microsoft.Playwright;

namespace PlaywrightTest.PageObjectModels
{
    internal abstract class PageObject(IPage page)
    {
        protected readonly IPage _page = page;
    }
}
