using System.Reflection;

namespace CSharpFiddles.Knowledge.Reflection.Resolver;

/// <summary>
/// Resolves a single constructor to use out of at most 2 public constructors.
/// Favours built in constructors, and if this is not present, looks for a parameterized constructor.
/// The implementation is not context-sensitive, so it can NOT handle classes with
/// more than one parameterized constructor.
/// </summary>
public class DefaultConstructorResolver : IConstructorResolver
{
    public ConstructorInfo Get(IEnumerable<ConstructorInfo> constructorInfo)
    {
        var publicConstructors =
            constructorInfo
                .Where(c =>  c.IsPublic)
                .ToList();
        
        if (!publicConstructors.Any())
        {
            throw new ArgumentException("Provided type does not have any public constructors.");
        }
        
        var publicBuiltInConstructor =
            publicConstructors
                .ToList()
                .FirstOrDefault(x => x.GetParameters().Length == 0);
        if (publicBuiltInConstructor != null)
        {
            return publicBuiltInConstructor;
        }

        var publicParameterizedConstructors =
            publicConstructors
                .ToList()
                .Where(x => x.GetParameters().Length != 0);

        var parameterizedConstructors = publicParameterizedConstructors.ToList();
        if (parameterizedConstructors.Count > 1)
        {
            throw new ArgumentException(
                """
                Type supplied has multiple parameterized constructors.
                The default constructor resolver does not use context.
                It is unable to decide between one or more parameterized constructors to use.
                """);
        }

        if (!parameterizedConstructors.Any())
        {
            throw new ArgumentException("Type supplied does not have a public built in or parameterized constructor.");
        }

        return parameterizedConstructors.First();
    }
}