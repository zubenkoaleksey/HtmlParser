using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using HtmlParser.Interfaces;

namespace HtmlParser
{
    public class Parser : IParser
    {
        public IEnumerable<string> Parse(string html, string selectNode, string selectAttribute)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            // for some link throw null reference exception
            var nodes = htmlDocument.DocumentNode.SelectNodes(selectNode);

            if (nodes != null)
            {
                foreach (HtmlNode link in nodes)
                {
                    if (link?.Attributes[selectAttribute] != null)
                    {
                        yield return link.Attributes[selectAttribute].Value;
                    }
                }
            }
        }

        // parse links
        public SortedSet<string> ParseLinks(string html)
        {
            SortedSet<string> linksSet = new SortedSet<string>();

            foreach (var link in Parse(html, "//a[@href]", "href"))
                linksSet.Add(link);

            return linksSet;
        }

        // parse images
        public SortedSet<string> ParseImage(string html)
        {
            SortedSet<string> imagesSet = new SortedSet<string>();

            foreach (var image in Parse(html, "//img[@src]", "src"))
                imagesSet.Add(image);

            return imagesSet;
        }

        // parse css
        public SortedSet<string> ParseCss(string html)
        {
            SortedSet<string> cssSet = new SortedSet<string>();

            foreach (var css in Parse(html, "//link[@rel=\"stylesheet\"]", "href"))
            {
                cssSet.Add(css);
            }

            return cssSet;
        }

        public string CleanAndFixUrl(Uri startUrl, string childLink)
        {
            if (childLink.StartsWith("//"))
            {
                childLink = startUrl.Scheme + ":" + childLink;
            }

            Uri curLink;
            bool isValid = Uri.TryCreate(childLink, UriKind.Absolute, out curLink);

            // if link absolute return it
            if (isValid)
            {
                return childLink;
            }

            // Try as relative
            isValid = Uri.TryCreate(startUrl, childLink, out curLink);

            if (isValid)
            {
                return curLink.ToString();
            }

            Uri fullUrl = new Uri(startUrl, childLink);

            return fullUrl.ToString();

        }
    }
}

