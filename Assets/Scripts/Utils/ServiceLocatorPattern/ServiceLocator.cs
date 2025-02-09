using System;
using System.Collections.Generic;

namespace Utils.ServiceLocatorPattern
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object>
            Services = new Dictionary<Type, object>();

        public static void Register<T>(object serviceInstance)
        {
            Services[typeof(T)] = serviceInstance;
        }

        public static void Unregister<T>()
        {
            Services.Remove(typeof(T));
        }

        public static T Resolve<T>()
        {
            return (T) Services[typeof(T)];
        }

        public static void Reset()
        {
            Services.Clear();
        }
    }
}
