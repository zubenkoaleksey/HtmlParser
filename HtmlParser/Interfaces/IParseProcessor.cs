namespace HtmlParser.Interfaces
{
    public interface IParseProcessor
    {
        void MultipleThreadProcess();
        void ThreadProcess();
        void StartParse(string startUrl, int breakDepth, bool isParseExternal, int threadNumber = 10);
        bool IsExternal(string source);
    }
}
