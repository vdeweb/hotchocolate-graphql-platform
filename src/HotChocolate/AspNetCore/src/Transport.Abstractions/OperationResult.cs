using System;
using System.Text.Json;
using static HotChocolate.Transport.Serialization.Utf8GraphQLResultProperties;

namespace HotChocolate.Transport;

/// <summary>
/// Represents the result of a GraphQL operation.
/// </summary>
public sealed class OperationResult : IDisposable
{
    private readonly IDisposable? _memoryOwner;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationResult"/> class with the
    /// specified JSON document and optional data, errors, and extensions.
    /// </summary>
    /// <param name="memoryOwner">
    /// The memory owner of the json elements.
    /// operation.
    /// </param>
    /// <param name="data">
    /// A <see cref="JsonElement"/> object representing the data returned by the operation.
    /// </param>
    /// <param name="errors">
    /// A <see cref="JsonElement"/> object representing any errors that occurred during
    /// the operation.
    /// </param>
    /// <param name="extensions">
    /// A <see cref="JsonElement"/> object representing any extensions returned by the
    /// operation.
    /// </param>
    public OperationResult(
        IDisposable? memoryOwner = default,
        JsonElement data = default,
        JsonElement errors = default,
        JsonElement extensions = default)
    {
        _memoryOwner = memoryOwner;
        Data = data;
        Errors = errors;
        Extensions = extensions;
    }

    /// <summary>
    /// Gets the <see cref="JsonElement"/> object representing the data returned by
    /// the operation.
    /// </summary>
    public JsonElement Data { get; }

    /// <summary>
    /// Gets the <see cref="JsonElement"/> object representing any errors that occurred
    /// during the operation.
    /// </summary>
    public JsonElement Errors { get; }

    /// <summary>
    /// Gets the <see cref="JsonElement"/> object representing any extensions returned
    /// by the operation.
    /// </summary>
    public JsonElement Extensions { get; }

    /// <summary>
    /// Releases all resources used by the <see cref="OperationResult"/> object.
    /// </summary>
    public void Dispose()
        => _memoryOwner?.Dispose();

    public static OperationResult Parse(JsonDocument document)
    {
        if (document == null)
        {
            throw new ArgumentNullException(nameof(document));
        }
        
        var root = document.RootElement;

        return new OperationResult(
            document,
            root.TryGetProperty(DataProp, out var data) ? data : default,
            root.TryGetProperty(ErrorsProp, out var errors) ? errors : default,
            root.TryGetProperty(ExtensionsProp, out var extensions) ? extensions : default);
    }
}
