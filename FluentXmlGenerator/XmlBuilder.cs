using FluentXmlGenerator.Interfaces;
using System.Xml;

namespace FluentXmlGenerator;

public class XmlBuilder: IForFirstStage, IForSecondStage, IForGenerateXml
{
    private readonly XmlDocument _xmlDocument;
    private XmlElement _currentElement;

    private XmlBuilder()
    {
        _xmlDocument = new XmlDocument();
    }

    /// <summary>
    /// Metodo de configuração inicial
    /// </summary>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    public static IForFirstStage Configure()
    {
        return new XmlBuilder();
    }

    /// <summary>
    /// Metodo que cria elementos baseado nas propriedades de uma classe
    /// </summary>
    /// <typeparam name="T">Classe a ser utilizada</typeparam>
    /// <param name="obj">Objeto com as propriedades a serem transformadas</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    IForSecondStage IForFirstStage.AddElement<T>(T obj) where T : class
    {
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj)?.ToString();

            var element = _xmlDocument.CreateElement(property.Name);

            if (!string.IsNullOrEmpty(value))
            {
                element.InnerText = value;
            }
            if (_currentElement == null)
            {
                _xmlDocument.AppendChild(element);
            }
            else
            {
                _currentElement.AppendChild(element);
            }
        }

        return this;
    }

    /// <summary>
    /// Metodo que cria elementos baseado nas propriedades de uma classe
    /// </summary>
    /// <typeparam name="T">Classe a ser utilizada</typeparam>
    /// <param name="obj">Objeto com as propriedades a serem transformadas</param>
    /// <param name="namespacePrefix">Prefixo do namespace a ser atribuido</param>
    /// <param name="attribute">Nome do atributo</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    IForSecondStage IForFirstStage.AddElement<T>(T obj, string namespacePrefix, string attribute) where T : class
    {
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj)?.ToString();
            var element = _xmlDocument.CreateElement(property.Name);
            _currentElement?.AppendChild(element);

            if (!string.IsNullOrEmpty(namespacePrefix))
            {
                var attrNamespaceUri = _currentElement.GetNamespaceOfPrefix(namespacePrefix);
                var attr = _xmlDocument.CreateAttribute(namespacePrefix, attribute, attrNamespaceUri);
                attr.Value = value?.GetType()?.Name ?? "string";
                _currentElement?.LastChild?.Attributes?.Append(attr);
            }

            if (!string.IsNullOrEmpty(value))
            {
                element.InnerText = value;
            }
            else
            {
                var attrNamespaceUri = _currentElement.GetNamespaceOfPrefix(namespacePrefix);
                var attr = _xmlDocument.CreateAttribute(namespacePrefix, "nil", attrNamespaceUri);
                attr.Value = "true";
                _currentElement?.LastChild?.Attributes?.Append(attr);
            }
        }

        return this;
    }

    /// <summary>
    /// Metodo que cria elementos baseado nas propriedades de uma classe
    /// </summary>
    /// <typeparam name="T">Classe a ser utilizada</typeparam>
    /// <param name="obj">Objeto com as propriedades a serem transformadas</param>
    /// <param name="namespacePrefixattribute">Prefixo do namespace a ser atribuido no atributo</param>
    /// <param name="namespacePrefixattributeValue">Prefixo do namespace a ser atribuido no valor do atributo, padrão soap</param>
    /// <param name="attribute">Nome do atributo</param>
    /// <param name="defaultAttributeValue">Campo opcional para adicionar o valor padrão no atributo.
    /// valor default: string</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    IForSecondStage IForFirstStage.AddElement<T>(T obj, string namespacePrefixattribute, string namespacePrefixattributeValue, string attribute, bool defaultAttributeValue) where T : class
    {
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj)?.ToString();
            var element = _xmlDocument.CreateElement(property.Name);
            _currentElement?.AppendChild(element);

            if (!string.IsNullOrEmpty(namespacePrefixattribute))
            {
                var attrNamespaceUri = _currentElement.GetNamespaceOfPrefix(namespacePrefixattribute);
                var attr = _xmlDocument.CreateAttribute(namespacePrefixattribute, attribute, attrNamespaceUri);

                attr.Value = $"{namespacePrefixattributeValue}:{(defaultAttributeValue ? "string" : property.PropertyType.Name.ToLower())}";
                _currentElement?.LastChild?.Attributes?.Append(attr);

                if (!string.IsNullOrEmpty(value))
                {
                    element.InnerText = value;
                }
                else
                {
                    var attrubute = _xmlDocument.CreateAttribute(namespacePrefixattribute, "nil", attrNamespaceUri);
                    attrubute.Value = "true";
                    _currentElement?.LastChild?.Attributes?.Append(attrubute);
                }
            }
        }

        return this;
    }

    /// <summary>
    /// Metodo para criar um elemento em xml.
    /// </summary>
    /// <param name="elementName">Nome do elemento</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    public IForSecondStage AddElement(string elementName)
    {
        var element = _xmlDocument.CreateElement(elementName);
        if (_currentElement == null)
        {
            _xmlDocument.AppendChild(element);
        }
        else
        {
            _currentElement.AppendChild(element);
        }
        _currentElement = element;
        return this;
    }

    /// <summary>
    /// Metodo para criar um elemento com o namespace herdado
    /// </summary>
    /// <param name="elementName">Nome do elemento</param>
    /// <param name="namespacePrefix">Nome do namespace a ser herdado</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    public IForSecondStage AddElement(string elementName, string namespacePrefix = null)
    {
        XmlElement element;
        if (!string.IsNullOrEmpty(namespacePrefix))
        {
            var namespaceUri = _currentElement.GetNamespaceOfPrefix(namespacePrefix);
            element = _xmlDocument.CreateElement(namespacePrefix, elementName, namespaceUri);
        }
        else
        {
            element = _xmlDocument.CreateElement(elementName);
        }

        _currentElement?.AppendChild(element);
        _currentElement = element;
        return this;
    }

    /// <summary>
    /// Metodo para criar um elemento com o namespace atrelado, padrao soap.
    /// </summary>
    /// <param name="elementName">Nome do elemento</param>
    /// <param name="namespacePrefix">Prefixo do namespace</param>
    /// <param name="namespaceUri">Uri do respectivo namespace</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    public IForSecondStage AddElement(string elementName, string namespacePrefix = null, string namespaceUri = null)
    {
        XmlElement element;
        if (namespacePrefix != null && namespaceUri != null)
        {
            var prefix = _xmlDocument.CreateAttribute("xmlns", namespacePrefix, "http://www.w3.org/2000/xmlns/");
            prefix.Value = namespaceUri;
            _xmlDocument.DocumentElement?.SetAttributeNode(prefix);

            element = _xmlDocument.CreateElement(namespacePrefix, elementName, namespaceUri);
            if (_currentElement == null)
            {
                _xmlDocument.AppendChild(element);
            }
            else
            {
                _currentElement.AppendChild(element);
            }
        }
        else
        {
            element = _xmlDocument.CreateElement(elementName);
            if (_currentElement == null)
            {
                _xmlDocument.AppendChild(element);
            }
            else
            {
                _currentElement.AppendChild(element);
            }
        }

        _currentElement = element;
        return this;
    }

    /// <summary>
    /// Metodo que adiciona um namespace no elemento correspondente
    /// </summary>
    /// <param name="prefix">Prefixo do namespace</param>
    /// <param name="uri">Uri do respectivo namespace</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    public IForGenerateXml WithNamespace(string prefix, string uri)
    {
        if (_currentElement != null)
        {
            var xmlns = _xmlDocument.CreateAttribute("xmlns", prefix, "http://www.w3.org/2000/xmlns/");
            xmlns.Value = uri;
            _currentElement.Attributes.Append(xmlns);
        }
        return this;
    }

    /// <summary>
    /// Metodo que adiciona atributos no elemento correspondente
    /// </summary>
    /// <param name="attributeName">Nome do atributo</param>
    /// <param name="attributeValue">Valor do atributo</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    public IForGenerateXml WithAttribute(string attributeName, string attributeValue)
    {
        if (_currentElement != null)
        {
            var attribute = _xmlDocument.CreateAttribute(attributeName);
            attribute.Value = attributeValue;
            _currentElement.Attributes.Append(attribute);
        }
        return this;
    }

    /// <summary>
    /// Metodo que adiciona atributos com namespaces no elemento correspondente
    /// </summary>
    /// <param name="namespacePrefix">Nome do prefixo</param>
    /// <param name="attributeName">Nome do atributo</param>
    /// <param name="attributeValue">Valor do atributo</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    public IForGenerateXml WithAttribute(string namespacePrefix, string attributeName, string attributeValue)
    {
        if (_currentElement != null)
        {
            XmlAttribute attribute;
            if (namespacePrefix == "xmlns")
            {
                attribute = _xmlDocument.CreateAttribute(namespacePrefix, attributeName, "http://www.w3.org/2001/XMLSchema");
            }
            else
            {
                string namespaceUri = _currentElement.GetNamespaceOfPrefix(namespacePrefix);
                attribute = _xmlDocument.CreateAttribute(namespacePrefix, attributeName, namespaceUri);
            }
            attribute.Value = attributeValue;
            _currentElement.SetAttributeNode(attribute);
        }
        return this;
    }

    /// <summary>
    /// Metodo para adicionar valore ao elemento correspondente
    /// </summary>
    /// <param name="value">Valor a ser incluido no elemento</param>
    /// <returns>XmlBuilder.XmlBuilder</returns>
    public IForGenerateXml SetValue(string value)
    {
        if (_currentElement != null)
        {
            _currentElement.InnerText = value;
        }
        return this;
    }

    /// <summary>
    /// Metodo para construir o xml, baseado nas configurações feitas
    /// </summary>
    /// <returns>string</returns>
    string IForGenerateXml.Build()
    {
        return _xmlDocument.OuterXml;
    }
}