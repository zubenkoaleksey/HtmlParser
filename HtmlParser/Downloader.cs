using System;
using System.Net;
using HtmlParser.Interfaces;


namespace HtmlParser
{
    public class Downloader : IDownloader
    {
        public string GetResponse(string uri)
        {
            try
            {
                // 1st implementation many errors
                //HtmlWeb web = new HtmlWeb();
                //HtmlDocument doc = web.Load(uri);
                //return doc.DocumentNode.OuterHtml;

                // 2nd non many but able error
                //WebRequest request = WebRequest.Create(uri);
                //WebResponse response = request.GetResponse();
                //Stream dataStream = response.GetResponseStream();
                //StreamReader reader = new StreamReader(dataStream);
                //return reader.ReadToEnd();

                // 3rd how 2nd but less code
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(uri);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to download url " + uri, ex);
            }
        }
    }
}
