using PoshWebDriver.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshWebDriver.Commands
{
    [Cmdlet(VerbsCommon.Set, "WebDriverSessionSendKeys")]
    public class SetWebDriverSessionSendKeys : PSCmdlet
    {
        [Parameter]
        public WebDriverSession Session { get; set; }

        [Parameter(Mandatory = true)]
        public string Element { get; set; }

        [Parameter(Mandatory = true)]
        public object[] Keys { get; set; }
        
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var s = Session ?? WebDriverSession.Instance;

            var elementId = AsyncHelpers.RunSync(() => s.FindElement(Element));
            AsyncHelpers.RunSync(() => s.SendKeys(elementId, Keys));
        }
    }
}
