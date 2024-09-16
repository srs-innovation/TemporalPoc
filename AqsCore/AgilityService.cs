using System.Net.Http.Json;

public class AgilityService
{
    private readonly HttpClient httpClient;

    public AgilityService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri(AqsConstants.AgilityApiUrl);
    }

    public async Task<AgilityOrder> CreateOrder()
    {
        try
        {
            var response = await httpClient.GetAsync("/createorder");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<AgilityOrder>();
            if (result is null) return new AgilityOrder();
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> GetShipping()
    {
        var response = await httpClient.GetStringAsync("/getshipping");
        if (response is null) return string.Empty;
        return response;
    }
    public async Task<string> GetInvoice()
    {
        var response = await httpClient.GetStringAsync("/getinvoice");
        if (response is null) return string.Empty;
        return response;
    }
    public async Task<AgilityOrder> GetOrder()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<AgilityOrder>("/getorder");
            if (response is null) return new();
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }
}