using PoshWebDriver.Helpers;
using System.Management.Automation;

namespace PoshWebDriver.Commands
{
    [Cmdlet(VerbsCommon.Set, "WebDriverSessionUrl")]
    public class SetWebDriverSessionUrl : PSCmdlet
    {
        [Parameter]
        public WebDriverSession Session { get; set; }

        [Parameter(Mandatory = true)]
        public string Url { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var s = Session ?? WebDriverSession.Instance;

            AsyncHelpers.RunSync(() => s.GotoUrl(Url));
        }
    }
}
