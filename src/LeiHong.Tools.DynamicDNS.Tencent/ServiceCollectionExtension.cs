using System;
using LeiHong.Tools.DynamicDNS.Core;
using Microsoft.Extensions.DependencyInjection;

namespace LeiHong.Tools.DynamicDNS.TencentCloud
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTencentCloudDynamicDnsService(this IServiceCollection services, Action<TencentCloudDynamicDnsOptionsBuilder> options)
        {
            services.AddScoped<IDynamicDNS, TencentCloudDynamicDns>();
            var data = new TencentCloudDynamicDnsOptionsBuilder();
            options.Invoke(data);
            AppConsts.SecretKey = data.SecretKey;
            AppConsts.SecretId = data.SecretId;
            return services;
        }
    }
}
