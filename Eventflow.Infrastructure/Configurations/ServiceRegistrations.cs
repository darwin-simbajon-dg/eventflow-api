using Eventflow.Domain.Interfaces;
using Eventflow.Infrastructure.Data;
using Eventflow.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Configurations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<DapperDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
