using System.Reflection;

namespace CSharpFiddles.Knowledge.Reflection;

/// <summary>
/// Non-contextual resolver which takes in a collection of constructor information
/// and returns information for a single constructor for use.
/// </summary>
public interface IConstructorResolver
{
    public ConstructorInfo Get(IEnumerable<ConstructorInfo> constructorInfo);
}