using System;
using System.Net.Http.Json;

namespace AqsCore;

public class AqsService
{
    private readonly HttpClient httpClient;

    public AqsService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri(AqsConstants.AqsApiUrl);
    }

    public async Task<List<AqsOrderResponseDto>> GetOrders()
    {
        //var response =  await httpClient.GetFromJsonAsync<AqsOrderResponse>("/getorders");
        var response =  await httpClient.GetAsync("/getorders");
        response.EnsureSuccessStatusCode();
        var str = await response.Content.ReadAsStringAsync();
        var result = await response.Content.ReadFromJsonAsync<AqsOrderResponse>();
        if(result is null) return [];
        return [];
    }
}
