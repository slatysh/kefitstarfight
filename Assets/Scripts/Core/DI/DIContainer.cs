using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, object> _registrations = new();
        private List<IRunnableService> _runnableServices = new List<IRunnableService>();

        public void Register<TService>(TService impl)
        {
            _registrations[typeof(TService)] = impl;
        }

        public TService Resolve<TService>()
        {
            return (TService)_registrations[typeof(TService)];
        }

        public void InjectDependencies(object target)
        {
            var targetType = target.GetType();
            var method = targetType.GetMethod("Construct");

            if (method == null || !Attribute.IsDefined(method, typeof(InjectAttribute)))
            {
                return;
            }

            var resolveMethodInfo = typeof(DIContainer).GetMethod("Resolve");
            var parameters = method.GetParameters();
            var parameterValues = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var resolveMethod = resolveMethodInfo.MakeGenericMethod(parameter.ParameterType);
                var service = resolveMethod.Invoke(this, null);

                parameterValues[i] = service;
            }

            method.Invoke(target, parameterValues);
        }

        public void Init()
        {
            var targets = _registrations.Values;
            foreach (var target in targets)
            {
                InjectDependencies(target);

                if (target is IRunnableService runnableService)
                {
                    _runnableServices.Add(runnableService);
                }
            }
        }

        public void Run()
        {
            foreach (var service in _runnableServices)
            {
                service.Run();
            }
        }
    }
}
