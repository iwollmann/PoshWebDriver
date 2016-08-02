using System.Diagnostics;
using System.IO;
using System.Management.Automation;

namespace PoshWebDriver.Commands
{
    [Cmdlet(VerbsCommon.Show, "Image")]
    public class ShowImage : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public Stream Stream { get; set; }

        override protected void ProcessRecord()
        {
            base.ProcessRecord();

            if ((Stream != null) && (Stream is MemoryStream))
            {
                var temp = Path.GetTempFileName() + ".png";
                var mem = Stream as MemoryStream;
                File.WriteAllBytes(temp, mem.ToArray());
                Process.Start(temp);
            }
            else
            {
                throw new PSNotSupportedException();
            }
        }
    }
}
