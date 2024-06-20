using BOB.Power.Config;
using BOB.Power.Converters;
using BOB.Power.Models;
using BOB.Power.Models.ProxyModels;
using Newtonsoft.Json;

namespace BOB.Power.Services;

public interface IPowerService
{
    IntervalPricing GetSpotPriceNow(Decimal poNumber);
}

public class PowerService : IPowerService
{
    private readonly HttpClient _httpClient;
    private readonly IntervalMapper _mapper;

    public PowerService()
    {
        _httpClient = new HttpClient();
        _mapper = new IntervalMapper();
    }
    
    public IntervalPricing GetSpotPriceNow(Decimal poNumber)
    {
        // Example url: https://www.hvakosterstrommen.no/api/v1/prices/2024/06-18_NO5.json
        
        //Extract year, dayOfMonth and hour from DateTime.Now   
        DateTime now = DateTime.Now;
        int year = now.Year;
        string month = now.Month.ToString("D2");
        string dayOfMonth = now.Day.ToString("D2");
        string hour = now.Hour.ToString("D2");
        string area= GetEnergyArea(poNumber);
        
        
        string requestUrl = $"{year}/{month}-{dayOfMonth}_{area}.json";
        
        _httpClient.BaseAddress = new Uri(AppConfig.PowerPriceApiUrl);
        var response = _httpClient.GetAsync(requestUrl).Result;
        response.EnsureSuccessStatusCode();
        var responseContent = response.Content.ReadAsStringAsync().Result;

        var proxyPriceInterval = ExtractIntervalPricing(responseContent, hour);
        var priceInterval = _mapper.ToBOB(proxyPriceInterval);
        priceInterval.area= area;

        return priceInterval;
    }
    
    private Proxy_PriceInterval ExtractIntervalPricing(string dayPricing, string fromHour)
    {
        // Deserialize the responseContent to a list of IntervalPricing objects
        List<Proxy_PriceInterval> priceIntervals = JsonConvert.DeserializeObject<List<Proxy_PriceInterval>>(dayPricing);

        foreach (var interval in priceIntervals)
        {
            if (interval.time_start.Hour.ToString("D2").Equals(fromHour))
            {
                return interval;
            }
        }
        return null;
    }
    

    // Method to get price area based on postnummer
    public string GetEnergyArea(Decimal poNumber)
    {
        foreach (var area in AppConfig.PoNumberEnergyAreas)
        {
            if (poNumber >= area.Value.Item1 && poNumber <= area.Value.Item2)
            {
                return area.Key;
            }
        }
        return "Unknown";
    }
    
    
}

