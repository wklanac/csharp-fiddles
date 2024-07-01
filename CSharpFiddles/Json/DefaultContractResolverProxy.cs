using Newtonsoft.Json.Serialization;

namespace CSharpFiddles.Json;

///<summary>
/// Acts as a proxy to the default contract resolver provided by Newtonsoft JSON.
/// This is useful for situations where it is desirable to use the default resolver
/// for most of your inputs, but you have inputs which require specific resolution
/// overrides.
/// </summary>
/// <param name="contractOverrides">Map of types to the contract overrides desired for each.</param>
public class DefaultContractResolverProxy
    (Dictionary<Type, DefaultContractResolverProxy.SupportedContracts> contractOverrides)
    : DefaultContractResolver
{
    public enum SupportedContracts
    {
        Object = 0
    }
    
    protected override JsonContract CreateContract(Type objectType)
    {
        if (contractOverrides.TryGetValue(objectType, out var contractOverride))
        {
            return contractOverride switch
            {
                SupportedContracts.Object => CreateObjectContract(objectType),
                _ => throw new NotSupportedException("Unsupported contract type configured.")
            };
        }

        return base.CreateContract(objectType);
    }
}