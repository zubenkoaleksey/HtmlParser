using StructureMap;


namespace HtmlParser
{
    public class MyRegystryContainer
    {
        public IContainer Container;

        public MyRegystryContainer()
        {
            Container = new Container(x => x.Scan(s =>
            {
                s.TheCallingAssembly();
                s.AssembliesFromPath(".");
                s.WithDefaultConventions();
                s.LookForRegistries();
            }));
            //{
            //    x.For<ILinkRepository>().Use<LinkRepository>();
            //    x.For<ICssRepository>().Use<CssRepository>();
            //    x.For<IImageRepository>().Use<ImageRepository>();
            //});
        }
    }
}
