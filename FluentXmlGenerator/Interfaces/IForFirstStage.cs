namespace FluentXmlGenerator.Interfaces;

public interface IForFirstStage
{
    public IForSecondStage AddElement<T>(T obj) where T : class;
    public IForSecondStage AddElement<T>(T obj, string namespacePrefix, string attribute) where T : class;
    public IForSecondStage AddElement<T>(T obj, string namespacePrefixattribute, string namespacePrefixattributeValue, string attribute, bool defaultAttributeValue = false) where T : class;
    public IForSecondStage AddElement(string elementName);
    public IForSecondStage AddElement(string elementName, string namespacePrefix = null);
    public IForSecondStage AddElement(string elementName, string namespacePrefix = null, string namespaceUri = null);
}
