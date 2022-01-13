using System;
using Microsoft.Extensions.DependencyInjection;
using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Features.Api;

namespace Cirrus.Node
{
    public static class IoCRegistrations
    {
        public static IFullNodeBuilder AddBlockcoreRegistrations(this IFullNodeBuilder fullNodeBuilder,
            Action<ApiFeatureOptions> optionsAction = null)
        {
            fullNodeBuilder.Services.AddTransient<ISmartContractLogEnrichment, StandardTokenLogEnrichment>();
            fullNodeBuilder.Services.AddTransient<ISmartContractEnrichmentFactory, SmartContractEnrichmentFactory>();

            return fullNodeBuilder;
        }
    }
}
