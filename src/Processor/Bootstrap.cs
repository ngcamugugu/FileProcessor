using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;

namespace Processor
{
  public static class Bootstrap
  {
    private readonly static Container _container;

    static Bootstrap()
    {
      _container = new Container();
    }

    public static Container Container => _container;

    public static void Strap(HttpConfiguration config)
    {
      _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
      _container.Register<IFileProcessor, FileProcessor>();
      _container.RegisterWebApiControllers(config);
      _container.Verify();
      config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);
    }
  }
}
