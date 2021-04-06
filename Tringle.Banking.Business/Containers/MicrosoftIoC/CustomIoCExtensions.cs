using Microsoft.Extensions.DependencyInjection;
using Tringle.Banking.Business.Concrete;
using Tringle.Banking.Business.Interfaces;
using Tringle.Banking.DataAccess.Concrete.Context;
using Tringle.Banking.DataAccess.Concrete.Repositories;
using Tringle.Banking.DataAccess.Interfaces;

namespace Tringle.Banking.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<RedisContext>();
            services.AddSingleton(typeof(IRedisDAL<>), typeof(RedisRepository<>));
            services.AddSingleton(typeof(IRedisService<>), typeof(RedisManager<>));


            services.AddScoped<ITransferDAL, TransferRepository>();
            services.AddScoped<IAccountDAL, AccountRepository>();
            services.AddScoped<ITransferService, TransferManager>();
            services.AddScoped<IAccountService, AccountManager>();
        }
    }
}
