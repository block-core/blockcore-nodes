using System.Collections.Generic;
using System.Linq;

namespace Cirrus.Node
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
            return this.enrichmentList.First(_ => _.IsSmartContractSupported(typeName));
        }
    }
}
