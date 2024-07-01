namespace CSharpFiddles.Knowledge.Reflection;

/// <summary>
/// Generic metadata provider for type knowledge
/// </summary>
/// <typeparam name="T">Output metadata type.</typeparam>
public interface ITypeMetadataProvider<out T>
{
    public T Get(Type type);
}