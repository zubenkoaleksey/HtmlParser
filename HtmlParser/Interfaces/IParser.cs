using System;
using System.Collections.Generic;

namespace HtmlParser.Interfaces
{
    public interface IParser
    {
        IEnumerable<string> Parse(string html, string selectNode, string selectAttribute);
        SortedSet<string> ParseLinks(string html);
        SortedSet<string> ParseImage(string html);
        SortedSet<string> ParseCss(string html);
        string CleanAndFixUrl(Uri startUrl, string childLink);
    }
}
