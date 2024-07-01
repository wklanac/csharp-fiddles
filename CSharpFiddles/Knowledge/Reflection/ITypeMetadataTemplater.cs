namespace CSharpFiddles.Knowledge.Reflection;

/// <summary>
/// Templater interface which takes in a type and a metadata provider and provides a code template based on the inputs.
/// </summary>
/// <typeparam name="T">Metadata type</typeparam>
public interface ITypeMetadataTemplater<in T>
{
    public string GetTemplate(ITypeMetadataProvider<T> typeMetadataProvider, Type type);
}