using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using HtmlParser.DAL.Repositories;
using HtmlParser.Interfaces;


namespace HtmlParser
{
    public class ParseProcessor : IParseProcessor
    {
        private readonly IParser _parseLinks;
        private readonly IDownloader _download;
        private readonly ImageRepository _imageRepository;
        private readonly CssRepository _cssRepository;
        private readonly LinkRepository _linkRepository;
        private readonly SiteRepository _siteRepository;

        public ParseProcessor(IParser parser, IDownloader downloader, ImageRepository imageRepository, CssRepository cssRepository, 
            LinkRepository linkRepository, SiteRepository siteRepository)
        {
            _parseLinks = parser;
            _download = downloader;
            _imageRepository = imageRepository;
            _cssRepository = cssRepository;
            _linkRepository = linkRepository;
            _siteRepository = siteRepository;
        }

        private readonly List<string> _allLinks = new List<string>(); // collection for finded links
        private readonly ConcurrentQueue<Link> _queueLinks = new ConcurrentQueue<Link>();
        private readonly ConcurrentDictionary<int, bool> _threadDictionary = new ConcurrentDictionary<int, bool>();

        private static readonly object MLock = new object();
        private Uri _startUrl; // save our start url
        private int _breakDepth;
        private bool _isParseExternal; // show there are we need to parse external links or not
        private int _threadNumber;
        private string _html;

        //...
        public void MultipleThreadProcess()
        {
            // main thread collect links list into Queue
            // then we need to start threads and them try take link from queue

            Task[] task = new Task[_threadNumber];

            for (int i = 0; i < task.Length; i++)
            {
                task[i] = Task.Factory.StartNew(ThreadProcess);
            }
            Task.WaitAll();
        }

        public void ThreadProcess()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            bool stop = false;

            while (!stop)
            {
                if (_queueLinks.Count != 0)
                {
                    Link found;
                    lock (MLock)
                    {
                        _queueLinks.TryDequeue(out found);
                        _threadDictionary[threadId] = true; // add thread id and thread state
                    }

                    if (found != null)
                    {
                        RunParse(found.Url, found.Depth);
                    }
                }
                else
                {
                    lock (MLock)
                    {
                        _threadDictionary[threadId] = false; // mark then thread aren't work 
                    }
                }

                lock (MLock)
                {
                    stop = _threadDictionary.Values.All(x => x.Equals(false)) && _queueLinks.Count == 0;
                }
            }
        }


        public void StartParse(string startUrl, int breakDepth, bool isParseExternal, int threadNumber = 10)
        {
            if (startUrl == string.Empty)
            {
                return;
            }

            _breakDepth = breakDepth;
            _isParseExternal = isParseExternal;
            _threadNumber = threadNumber;
            _startUrl = new Uri(startUrl, UriKind.Absolute);

            _queueLinks.Enqueue(new Link { Url = startUrl, Depth = 0 });

            MultipleThreadProcess(); // start multiple thread method

        }

        // ...
        private void RunParse(string urlToParse, int currentDepth)
        {
            try
            {
                // don't try to parse the mailto links
                if (urlToParse.StartsWith("mailto:") || urlToParse.ToLower().EndsWith(".pdf"))
                {
                    return;
                }
                // download and measure time
                var ts = Download(urlToParse);
                // counting html size
                var size = Encoding.Unicode.GetByteCount(_html);

                // doing parse
                var rawLinks = _parseLinks.ParseLinks(_html); // raw parsed links
                var images = _parseLinks.ParseImage(_html);
                var css = _parseLinks.ParseCss(_html);

                Console.WriteLine("Parsing link " + urlToParse);

                lock (MLock)
                {
                    _allLinks.Add(urlToParse); // store all link which we parser

                    var cleanedChildLinks = CleanedChildLinks(rawLinks);

                    EnqueueFindedLinks(cleanedChildLinks, currentDepth);

                    // Store to DB here!!!
                    // .....
                    
                    _imageRepository.ImageStore(images);
                    _cssRepository.CssStore(css);
                    _linkRepository.LinkStore(urlToParse, _startUrl.ToString(), currentDepth, ts,size);
                    _siteRepository.SiteStore(_startUrl.ToString());

                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                // Remember failed link to prevent parsing it
                lock (MLock)
                {
                    _allLinks.Add(urlToParse);
                }
            }
        }

        private TimeSpan Download(string urlToParse)
        {
            Stopwatch loadingTime = Stopwatch.StartNew();
            // download html
            _html = _download.GetResponse(urlToParse);

            if (_html == string.Empty)
            {
                return new TimeSpan(0); // skip if we can't download
            }
            loadingTime.Stop();
            // measure loading time
            TimeSpan ts = loadingTime.Elapsed;

            return ts;
        }

        private SortedSet<string> CleanedChildLinks(SortedSet<string> rawLinks)
        {
            SortedSet<string> cleanedChildLinks = CleanLinks(rawLinks); // clean raw links and try to fix it

            foreach (var rawLink in rawLinks)
            {
                // check for external link 
                // and if we don't need to parse it remove it from list
                bool isExternal = IsExternal(rawLink);
                if (isExternal && !_isParseExternal)
                {
                    cleanedChildLinks.Remove(rawLink);
                }
            }
            return cleanedChildLinks;
        }

        private void EnqueueFindedLinks(SortedSet<string> cleanedChildLinks, int currentDepth)
        {
            // add to Queue finded link
            foreach (var curLink in cleanedChildLinks)
            {
                // check for _allLinks contain currLink
                // check for currentDepth < _breakDepth
                // and check _queueLinks don't contain curLink
                var newLink = new Link { Depth = currentDepth + 1, Url = curLink };
                if (_allLinks.Contains(curLink) || currentDepth >= _breakDepth || _queueLinks.Contains(newLink))
                {
                    continue;
                }
                _queueLinks.Enqueue(newLink);
            }
        }

        // get raw link and clean it
        private SortedSet<string> CleanLinks(SortedSet<string> chilLinks)
        {
            var rez = new SortedSet<string>();
            foreach (var chilLink in chilLinks)
            {
                try
                {
                    string fixedUrl = _parseLinks.CleanAndFixUrl(_startUrl, chilLink); // try to get fixed link
                    rez.Add(fixedUrl);
                }
                catch
                {
                    Console.WriteLine($"Unable to parse url {chilLink}");
                }
            }

            return rez;
        }

        public bool IsExternal(string source)
        {
            Uri uri;
            return Uri.TryCreate(source, UriKind.Absolute, out uri)
                    && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

    }

}
