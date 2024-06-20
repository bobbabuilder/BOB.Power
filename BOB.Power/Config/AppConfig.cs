namespace BOB.Power.Config;

public class AppConfig
{
    public static readonly string PowerPriceApiUrl = "https://www.hvakosterstrommen.no/api/v1/prices/";
    public static Dictionary<string, (int, int)> PoNumberEnergyAreas = new Dictionary<string, (int, int)>
    {
        { "NO1", (0001, 3999) },  
        { "NO2", (4000, 4999) },
        { "NO5", (5000, 5999) },
        { "NO3", (6000, 7999) },
        { "NO4", (8000, 9999) }
    };
}