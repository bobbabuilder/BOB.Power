namespace BOB.Power.Models.ProxyModels;
public class Proxy_PriceInterval
{
    public decimal NOK_per_kWh { get; set; }
    public decimal EUR_per_kWh { get; set; }
    public decimal EXR { get; set; }
    public DateTime time_start { get; set; }
    public DateTime time_end { get; set; }
}
