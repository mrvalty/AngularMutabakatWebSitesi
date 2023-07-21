using eReconciliationProject.Core.CrossCuttingConcerns.Caching;
using eReconciliationProject.Core.CrossCuttingConcerns.Caching.Microsoft;
using eReconciliationProject.Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Core.DependencyRevolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            
        }
    }
}
