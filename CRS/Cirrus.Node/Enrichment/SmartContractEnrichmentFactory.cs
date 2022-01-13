using System.Collections.Generic;
using System.Linq;

namespace Cirrus.Node.Enrichment
{
    public class SmartContractEnrichmentFactory : ISmartContractEnrichmentFactory
    {
        private readonly IList<ISmartContractLogEnrichment> enrichmentList;

        public SmartContractEnrichmentFactory(IEnumerable<ISmartContractLogEnrichment> enrichmentList)
        {
            this.enrichmentList = enrichmentList.ToList();
        }

        public ISmartContractLogEnrichment GetLogEnrichment(string typeName)
        {
            return this.enrichmentList.FirstOrDefault(_ => _.IsSmartContractSupported(typeName));
        }
    }
}
