using System;
using HtmlParser.Interfaces;

namespace HtmlParser
{
    class Program
    {
        static void Main()
        {
            string url = "http://medeanalytics.com";
            int depth = 2;
            bool external = true;

            var container = new MyRegystryContainer().Container;
            var process = container.GetInstance<IParseProcessor>();
            process.StartParse(url, depth, external);

            

            Console.ReadLine();
        }
    }
}
