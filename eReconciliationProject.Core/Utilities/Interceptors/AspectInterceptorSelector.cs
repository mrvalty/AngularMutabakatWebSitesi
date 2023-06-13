using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector :IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type,MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x=>x.Priority).ToArray();
        }
    }
}
