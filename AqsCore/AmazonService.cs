using System.Net.Http.Json;
using AqsCore;

public class AmazonService
{
    private readonly HttpClient httpClient;

    public AmazonService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri(AqsConstants.AmazonApiUrl);
    }

    public async Task<List<AmazonOrder>> GetOrders()
    {
        var response =  await httpClient.GetFromJsonAsync<List<AmazonOrder>>("/getorders");
        if(response is null) return [];
        return response;
    }
}