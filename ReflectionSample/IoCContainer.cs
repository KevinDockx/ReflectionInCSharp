using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionSample
{
    public class IoCContainer
    {
        Dictionary<Type, Type> _map = new Dictionary<Type, Type>();
        MethodInfo _resolveMethod;

        public void Register<TContract, TImplementation>()
        {
            // register in the mapping dictionary
            if (!_map.ContainsKey(typeof(TContract)))
            {
                _map.Add(typeof(TContract), typeof(TImplementation));
            }
        }

        public void Register(Type contract, Type implementation)
        {
            // register in the mapping dictionary
            if (!_map.ContainsKey(contract))
            {
                _map.Add(contract, implementation);
            }
        }

        public TContract Resolve<TContract>()
        {
            // check whether we're trying to resolve a generic type
            if (typeof(TContract).IsGenericType &&
                  _map.ContainsKey(typeof(TContract).GetGenericTypeDefinition()))
            {
                var openImplementation = _map[typeof(TContract).GetGenericTypeDefinition()];
                var closedImplementation = openImplementation
                    .MakeGenericType(typeof(TContract).GenericTypeArguments);
                return Create<TContract>(closedImplementation);
            }

            if (!_map.ContainsKey(typeof(TContract)))
            {
                throw new ArgumentException($"No registration found for {typeof(TContract)}");
            }

            // create an instance and return it 
            return Create<TContract>(_map[typeof(TContract)]);
        }

        private TContract Create<TContract>(Type implementationType)
        {
            // get the resolve method. 
            if (_resolveMethod == null)
            {
                _resolveMethod = typeof(IoCContainer).GetMethod("Resolve");
            }

            var constructorParameters = implementationType.GetConstructors()
                    .OrderByDescending(c => c.GetParameters().Length)
                    .First()
                    .GetParameters()
                    .Select(p =>
                      {
                          // make the resolve method generic and invoke it
                          var genericResolveMethod = _resolveMethod
                               .MakeGenericMethod(p.ParameterType);
                          return genericResolveMethod.Invoke(this, null);
                      })
                    .ToArray();

            return (TContract)Activator.CreateInstance(
                implementationType, constructorParameters);
        }
    }
}
