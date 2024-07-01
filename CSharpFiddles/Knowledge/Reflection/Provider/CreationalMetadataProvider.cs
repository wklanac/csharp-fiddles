using CSharpFiddles.Knowledge.Reflection.Model;

namespace CSharpFiddles.Knowledge.Reflection.Provider;


/// <summary>
/// Gets creational metadata for type using Reflection APIs.
/// </summary>
public class CreationalMetadataProvider: ITypeMetadataProvider<CreationalMetadata>
{
    public CreationalMetadata Get(Type type)
    {
        var constructors = type.GetConstructors();
        var writeableProperties = type.GetProperties().Where(p => p.CanWrite);

        return new CreationalMetadata(writeableProperties, constructors);
    }
}