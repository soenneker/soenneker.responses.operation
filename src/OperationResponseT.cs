using Newtonsoft.Json;
using Soenneker.Attributes.PublicOpenApiObject;
using Soenneker.Dtos.ProblemDetails;
using System.Text.Json.Serialization;

namespace Soenneker.Responses.Operation;

/// <summary>
/// Represents the standardized outcome of an operation, containing either a successful result value
/// or detailed error information in the form of a <see cref="ProblemDetailsDto"/>.
/// </summary>
/// <typeparam name="T">
/// The type of the successful result value returned by the operation.
/// </typeparam>
[PublicOpenApiObject]
public sealed class OperationResponse<T> : OperationResponse
{
    /// <summary>
    /// Gets the value returned when the operation succeeds.
    /// This property is <see langword="null"/> when the operation fails.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonProperty("value")]
    public new T? Value { get; set; }
}