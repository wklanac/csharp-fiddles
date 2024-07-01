using System.Reflection;
using System.Text;
using CSharpFiddles.Caching;
using CSharpFiddles.Knowledge.Reflection.Model;

namespace CSharpFiddles.Knowledge.Reflection.Templater;

/// <summary>
/// Templater class which produces mustache templates to use for source generation
/// from creational metadata.
/// </summary>
/// <param name="constructorResolver">Constructor resolver to use for templating.</param>
public class CreationalMetadataMustacheTemplater(IConstructorResolver constructorResolver)
    : ITypeMetadataTemplater<CreationalMetadata>
{
    private readonly ConcurrentCache<Type, CreationalMetadata> creationalMetadataCache = new();
    private readonly ConcurrentCache<IEnumerable<ConstructorInfo>, ConstructorInfo> constructorResolverCache = new();
        
    
    public string GetTemplate(ITypeMetadataProvider<CreationalMetadata> typeMetadataProvider, Type type)
    {
        var typeMetadata = creationalMetadataCache.Get(type, typeMetadataProvider.Get);
        var constructorToUse = constructorResolverCache.Get(
            typeMetadata.Constructors,
            constructorResolver.Get);
        
        StringBuilder templateBuilder = new StringBuilder();

        templateBuilder.Append($"var {{name}}{type.Name} = new {type.Name}(");

        var parameters = constructorToUse.GetParameters();
        
        if (parameters.Length > 0)
        {
            templateBuilder.AppendLine();
            
            for (var parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                var parameter = parameters[parameterIndex];
                var finalParameter = parameterIndex == parameters.Length - 1;
                var trailingCharacter = finalParameter ? "" : ",";
                templateBuilder.AppendLine($"    {parameter.Name}: {{{parameter.Name}}}{trailingCharacter}");
            }
        }
        
        templateBuilder.AppendLine(");");

        foreach (var writeableProperty in typeMetadata.WriteableProperties)
        {
            templateBuilder.AppendLine($"{{name}}{type.Name}.{writeableProperty.Name} = {{{writeableProperty.Name}}};");
        }

        return templateBuilder.ToString();
    }
}