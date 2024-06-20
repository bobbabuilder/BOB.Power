using BOB.Power.Models;
using BOB.Power.Models.ProxyModels;
using Riok.Mapperly.Abstractions;

namespace BOB.Power.Converters;

[Mapper]
public partial class IntervalMapper
{
    
    [MapProperty(nameof(@proxyPriceInterval.NOK_per_kWh), nameof(IntervalPricing.price))]
    public partial IntervalPricing ToBOB(Proxy_PriceInterval proxyPriceInterval);

}