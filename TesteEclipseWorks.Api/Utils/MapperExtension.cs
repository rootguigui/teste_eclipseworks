using System.Diagnostics.CodeAnalysis;
using Mapster;

namespace TesteEclipseWorks.Api.Utils;

[ExcludeFromCodeCoverage]
public static class MapperExtension
{
    public static TOutput? ToMap<TInput, TOutput>(this TInput? input)
    {
        return input is null ? default : input.Adapt<TOutput>();
    }

    public static TOutput? ToMap<TInput, TOutput>(this TInput? input, TypeAdapterConfig config)
    {
        return input is null ? default : input.Adapt<TOutput>(config);
    }

    public static string ToCamelCase(this string str)
    {
        return string.IsNullOrEmpty(str) ? str : str.Length == 1 ? str.ToLower() : char.ToLowerInvariant(str[0]) + str[1..];
    }
}