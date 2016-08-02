using PoshWebDriver.Helpers;
using System.Management.Automation;

namespace PoshWebDriver.Commands
{
    [Cmdlet(VerbsCommon.New, "WebDriverSession")]
    public class NewWebDriverSession : PSCmdlet
    {
        [Parameter]
        public string Url { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var session = AsyncHelpers.RunSync(() => WebDriverSession.Start(Url));

            WriteObject(session);
        }
    }
}
