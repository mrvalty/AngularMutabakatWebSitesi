using Castle.DynamicProxy;
using eReconciliationProject.Core.Extensions;
using eReconciliationProject.Core.Utilities.Interceptors;
using eReconciliationProject.Core.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.BusinessAspect
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
       private IHttpContextAccessor _httpContextAccessor;


        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.Identities;
            foreach (var role in _roles)
            {
                if (roleClaims.Any())
                {
                    return;
                }
            }
            throw new Exception("İşlem yapmaya yetkiniz yok.");
        }
    }

    
}
