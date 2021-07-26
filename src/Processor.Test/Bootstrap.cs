using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor.Test
{
  public static class Bootstrap
  {
    private readonly static Container _container;

    static Bootstrap()
    {
      _container = new Container();
    }

    public static Container Container => _container;

    public static void Strap()
    {
      _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
      _container.Register<IFileProcessor, FileProcessor>();
      _container.Verify();
    }
  }
}
