namespace FluentXmlGenerator.Interfaces;

public interface IForSecondStage : IForFirstStage
{
    public IForGenerateXml WithNamespace(string prefix, string uri);
    public IForGenerateXml WithAttribute(string attributeName, string attributeValue);
    public IForGenerateXml WithAttribute(string namespacePrefix, string attributeName, string attributeValue);
    public IForGenerateXml SetValue(string value);
}
