# FluentXmlGenerator

O FluentXmlGenerator é um componente que oferece métodos para criar e configurar elementos XML, tambem cria elementos e propriedades baseado em uma classe no C#.

## Método Configure

### Descrição
Método de configuração inicial.

### Uso
```csharp
XmlBuilder.Configure();
```

## Método AddElement\<T\>

### Descrição
Método que cria elementos com base nas propriedades de uma classe.

### Parâmetros
- `obj` (obrigatório): Objeto com as propriedades a serem transformadas.

### Uso
```csharp
xmlBuilderInstance.AddElement(obj);
```

## Método AddElement\<T\>

### Descrição
Método que cria elementos com base nas propriedades de uma classe e atribui um namespace e um atributo.

### Parâmetros
- `obj` (obrigatório): Objeto com as propriedades a serem transformadas.
- `namespacePrefix` (opcional): Prefixo do namespace a ser atribuído.
- `attribute` (opcional): Nome do atributo.

### Uso
```csharp
xmlBuilderInstance.AddElement(obj, namespacePrefix, attribute);
```

## Método AddElement\<T\>

### Descrição
Método que cria elementos com base nas propriedades de uma classe e atribui namespaces tanto ao atributo quanto ao valor do atributo.

### Parâmetros
- `obj` (obrigatório): Objeto com as propriedades a serem transformadas.
- `namespacePrefixattribute` (opcional): Prefixo do namespace a ser atribuído ao atributo.
- `namespacePrefixattributeValue` (opcional): Prefixo do namespace a ser atribuído ao valor do atributo.
- `attribute` (obrigatório): Nome do atributo.
- `defaultAttributeValue` (opcional): Valor padrão para o atributo.

### Uso
```csharp
xmlBuilderInstance.AddElement(obj, namespacePrefixattribute, namespacePrefixattributeValue, attribute, defaultAttributeValue);
```

---

## Método AddElement

### Descrição
Método para criar um elemento XML.

### Parâmetros
- `elementName` (obrigatório): Nome do elemento.

### Uso
```csharp
xmlBuilderInstance.AddElement(elementName);
```

---

## Método AddElement

### Descrição
Método para criar um elemento com um namespace herdado.

### Parâmetros
- `elementName` (obrigatório): Nome do elemento.
- `namespacePrefix` (opcional): Nome do namespace a ser herdado.

### Uso
```csharp
xmlBuilderInstance.AddElement(elementName, namespacePrefix);
```

---

## Método AddElement

### Descrição
Método para criar um elemento com um namespace específico.

### Parâmetros
- `elementName` (obrigatório): Nome do elemento.
- `namespacePrefix` (opcional): Prefixo do namespace.
- `namespaceUri` (opcional): URI do respectivo namespace.

### Uso
```csharp
xmlBuilderInstance.AddElement(elementName, namespacePrefix, namespaceUri);
```

---

## Método Parent

### Descrição
Método para retornar para o elemento pai em primeiro nível.

### Uso
```csharp
xmlBuilderInstance.Parent();
```

---

## Método Parent

### Descrição
Método para retornar para o elemento pai baseado no nome do elemento passado.

### Parâmetros
- `parentElementName` (obrigatório): Nome do elemento pai.

### Uso
```csharp
xmlBuilderInstance.Parent(parentElementName);
```

---

## Método Parent

### Descrição
Método para retornar para o elemento pai baseado no nome do elemento e do namespace passados.

### Parâmetros
- `parentElementName` (obrigatório): Nome do elemento pai.
- `namespaceElementName` (obrigatório): Nome do namespace vinculado ao elemento pai.

### Uso
```csharp
xmlBuilderInstance.Parent(parentElementName, namespaceElementName);
```

---

## Método WithNamespace

### Descrição
Método para adicionar um namespace ao elemento correspondente.

### Parâmetros
- `prefix` (obrigatório): Prefixo do namespace.
- `uri` (obrigatório): URI do respectivo namespace.

### Uso
```csharp
xmlBuilderInstance.WithNamespace(prefix, uri);
```

---

## Método WithAttribute

### Descrição
Método para adicionar atributos ao elemento correspondente.

### Parâmetros
- `attributeName` (obrigatório): Nome do atributo.
- `attributeValue` (obrigatório): Valor do atributo.

### Uso
```csharp
xmlBuilderInstance.WithAttribute(attributeName, attributeValue);
```

---

## Método WithAttribute

### Descrição
Método para adicionar atributos com namespaces ao elemento correspondente.

### Parâmetros
- `namespacePrefix` (obrigatório): Nome do prefixo.
- `attributeName` (obrigatório): Nome do atributo.
- `attributeValue` (obrigatório): Valor do atributo.

### Uso
```csharp
xmlBuilderInstance.WithAttribute(namespacePrefix, attributeName, attributeValue);
```

---

## Método SetValue

### Descrição
Método para adicionar um valor ao elemento correspondente.

### Parâmetros
- `value` (obrigatório): Valor a ser incluído no elemento.

### Uso
```csharp
xmlBuilderInstance.SetValue(value);
```

---

## Método Build

### Descrição
Método para construir o XML baseado nas configurações feitas.

### Uso
```csharp
xmlBuilderInstance.Build();
```