namespace Cirrus.Node.Enrichment
{
    public interface ISmartContractEnrichmentFactory
    {
        ISmartContractLogEnrichment GetLogEnrichment(string typeName);
    }
}
