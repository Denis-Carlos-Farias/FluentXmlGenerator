namespace FluentXmlGenerator.Interfaces;

public interface IForGenerateXml: IForFirstStage, IForSecondStage
{
    public string Build();
}
