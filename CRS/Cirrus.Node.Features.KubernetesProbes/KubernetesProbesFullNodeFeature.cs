using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Builder.Feature;

namespace Cirrus.Node.Features.KubernetesProbes
{
    public class KubernetesProbesFullNodeFeature : FullNodeFeature
    {
        public override Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }

    public static class KubernetesProbesFeatureExtension
    {
        public static IFullNodeBuilder UseKubernetesProbes(this IFullNodeBuilder fullNodeBuilder)
        {
            fullNodeBuilder.ConfigureFeature(features =>
            {
                features.AddFeature<KubernetesProbesFullNodeFeature>()
                        .FeatureServices(services =>
                        {

                            services.AddScoped<KubernetesProbesController>();
                        })
                        .EnsureDependencies(features.FeatureRegistrations);
            });

            return fullNodeBuilder;
        }
    }
}
