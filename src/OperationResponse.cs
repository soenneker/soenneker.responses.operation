using Newtonsoft.Json;
using Soenneker.Attributes.PublicOpenApiObject;
using Soenneker.Dtos.ProblemDetails;
using System.Diagnostics.Contracts;
using System.Net;
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
public sealed class OperationResponse
{
    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    [JsonPropertyName("succeeded")]
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    /// <summary>
    /// Gets the HTTP status code associated with the operation result.
    /// This value reflects the outcome of the operation, such as 200 for success or 400 for a client error.
    /// </summary>
    [JsonPropertyName("statusCode")]
    [JsonProperty("statusCode")]
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets the value returned when the operation succeeds.
    /// This property is <see langword="null"/> when the operation fails.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonProperty("value")]
    public object? Value { get; set; }

    /// <summary>
    /// Gets the problem details describing the error when the operation fails.
    /// This property is <see langword="null"/> when the operation succeeds.
    /// </summary>
    [JsonPropertyName("problem")]
    [JsonProperty("problem")]
    public ProblemDetailsDto? Problem { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public bool Failed => !Succeeded;

    [Pure]
    public static OperationResponse<TResponse> Success<TResponse>(TResponse value, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new OperationResponse<TResponse>
        {
            Succeeded = true,
            Value = value,
            StatusCode = (int)statusCode
        };
    }

    [Pure]
    public static OperationResponse<TResponse> Fail<TResponse>(string title, string detail, HttpStatusCode statusCode)
    {
        return new OperationResponse<TResponse>
        {
            Succeeded = false,
            StatusCode = (int)statusCode,
            Problem = new ProblemDetailsDto
            {
                Title = title,
                Detail = detail,
                Status = (int)statusCode
            }
        };
    }
}