using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakBank.Repository;

namespace TweakBank.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTransactionManager(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
