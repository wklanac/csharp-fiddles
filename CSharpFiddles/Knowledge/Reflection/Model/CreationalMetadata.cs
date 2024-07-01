using System.Reflection;

namespace CSharpFiddles.Knowledge.Reflection.Model;

public record CreationalMetadata(IEnumerable<PropertyInfo> WriteableProperties, IEnumerable<ConstructorInfo> Constructors)
{
    public IEnumerable<PropertyInfo> WriteableProperties { get; } = WriteableProperties;

    public IEnumerable<ConstructorInfo> Constructors { get; } = Constructors;
};