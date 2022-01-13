namespace Cirrus.Node
{
    public interface ISmartContractEnrichmentFactory
    {
        ISmartContractLogEnrichment GetLogEnrichment(string typeName);
    }
}
