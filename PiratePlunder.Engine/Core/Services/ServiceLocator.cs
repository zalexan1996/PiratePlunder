using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Core.Services;

public class ServiceLocator
{
    private IDictionary<Type, object> _services;

    public void Register<T>(T instance) where T : class
    {
        Unregister<T>();

        _services.Add(typeof(T), instance);
    }

    public void Unregister<T>() where T : class
    {
        if (_services.ContainsKey(typeof(T)))
        {
            _services.Remove(typeof(T));
        }
    }

    public T Get<T>() where T : class
    {
        return _services[typeof(T)] as T;
    }
}
