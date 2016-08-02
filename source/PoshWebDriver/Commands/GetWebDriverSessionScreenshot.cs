using PoshWebDriver.Helpers;
using System.Management.Automation;

namespace PoshWebDriver.Commands
{
    [Cmdlet(VerbsCommon.Get, "WebDriverSessionScreenshot")]
    public class GetWebDriverSessionScreenshot : PSCmdlet
    {
        [Parameter]
        public WebDriverSession Session { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var s = Session ?? WebDriverSession.Instance;

            var screenshot = AsyncHelpers.RunSync(() => s.GetScreenshot());

            WriteObject(screenshot);
        }
    }
}
