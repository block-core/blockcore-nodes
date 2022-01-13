using System;
using Cirrus.Node.Enrichment;
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
            fullNodeBuilder.ConfigureServices((Action<IServiceCollection>) (service =>
            {
                //TODO check if add singleton is better in this case
                service.AddTransient<ISmartContractLogEnrichment, StandardTokenLogEnrichment>();
                service.AddTransient<ISmartContractLogEnrichment, NonFungibleTokenLogEnrichment>();
                service.AddTransient<ISmartContractEnrichmentFactory, SmartContractEnrichmentFactory>();
            }));

            return fullNodeBuilder;
        }
    }
}
