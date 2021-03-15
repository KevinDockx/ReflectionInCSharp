using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionSample
{
    public class CoffeeService : ICoffeeService
    {
        public CoffeeService(IWaterService waterService)
        {
        }

        public CoffeeService(IWaterService waterService,
            IBeanService<Catimor> beanService)
        {
        }
    }

    public interface ICoffeeService
    { }

    public class TapWaterService : IWaterService
    { }

    public interface IWaterService
    { }

    public class ArabicaBeanService<T> : IBeanService<T>
    { }

    public interface IBeanService<T>
    { }

    /// <summary>
    /// Variety of the Arabica coffee bean
    /// </summary>
    public class Catimor
    { }

}
