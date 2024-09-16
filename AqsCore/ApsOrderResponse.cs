using System;
using System.Text.Json.Serialization;

namespace AqsCore;

public class AqsOrderResponse
{
    [JsonPropertyName("result")]
    public AqsApiResult Data { get; set; } = new();
}

public class AqsApiResult
{
}