using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Playwright.NUnit;

namespace PlaywrightTest.Tests
{
    public abstract class BaseTest : PageTest
    {
        public static readonly string dateTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        [SetUp]
        public async Task Setup()
        {
            await Context.Tracing.StartAsync(new()
            {
                Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
                Screenshots = true,
                //Snapshots = true,
                //Sources = true
            });

            await OnSetup();
        }

        [TearDown]
        public async Task TearDown()
        {
            await OnTeardown();
            var failed = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
           || TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;

            await Context.Tracing.StopAsync(new()
            {
                Path = failed ? Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "../../../../playwright-traces",
                    dateTime,
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                ) : null,
            });
        }

        public virtual async Task OnTeardown()
        {

        }

        public virtual async Task OnSetup()
        {

        }
    }
}
