using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoshWebDriver
{
    public class WebDriverSession
    {
        public static WebDriverSession Instance { get; set; }

        public static async Task<WebDriverSession> Start(string url)
        {
            var session = new WebDriverSession(url);
            await session.NewSession();

            Instance = session;

            return session;
        }

        string BrowserUrl;
        HttpClient Http;

        string SessionId;

        public WebDriverSession(string browserUrl)
        {
            BrowserUrl = browserUrl.Trim('/', '\\');
            Http = new HttpClient();
        }

        private async Task NewSession()
        {
            var url = BrowserUrl + "/session";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent("{\"desiredCapabilities\":{}}");

            var response = await Http.SendAsync(request);
            var r = await response.Content.ReadAsAsync<NewSessionResponse>();

            SessionId = r.sessionId;
        }

        public class NewSessionResponse
        {
            public string sessionId { get; set; }
        }

        public async Task GotoUrl(string gotourl)
        {
            var url = BrowserUrl + "/session/" + SessionId + "/url";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent("{\"url\":\"" + gotourl + "\"}");

            await Http.SendAsync(request);
        }

        public async Task<Stream> GetScreenshot()
        {
            var url = BrowserUrl + "/session/" + SessionId + "/screenshot";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await Http.SendAsync(request);

            var r = await response.Content.ReadAsAsync<ScreenshotResponse>();

            return r.GetImageStream();
        }

        public class ScreenshotResponse
        {
            public string value { get; set; }

            internal Stream GetImageStream()
            {
                var buffer = Convert.FromBase64String(value);
                return new MemoryStream(buffer);
            }
        }
    }

}
