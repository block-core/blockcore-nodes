using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cirrus.Node.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Builder.Feature;
using Stratis.Bitcoin.Features.Api;

namespace Cirrus.Node
{
    public static class IndexerApiFeatureExtension
    {
        public static IFullNodeBuilder UseIndexerApi(
            this IFullNodeBuilder fullNodeBuilder)
        {

            fullNodeBuilder.ConfigureFeature(features =>
            {
                features.AddFeature<IndexerApiFeature>()
                    .FeatureServices(services =>
                    {
                        services.AddScoped<IndexerContractsController>();
                    });
            });

            return fullNodeBuilder;
        }
    }

    public class IndexerApiFeature : FullNodeFeature
    {
        public override Task InitializeAsync()
        {
            // We only use the feature to intercept the services and inject controllers.

            return Task.CompletedTask;
        }
    }

}
