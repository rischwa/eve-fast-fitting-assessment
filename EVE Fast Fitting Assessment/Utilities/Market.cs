using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CrestSharp;
using CrestSharp.Model;
using EVE_Fast_Fitting_Assessment.Properties;

namespace EVE_Fast_Fitting_Assessment
{
    public static class DoubleExtensions
    {
        public static string ToStringInIskFormat(this double iskValue)
        {
            const string MILLION_ISK_FORMAT = "0.0M";
            const string BILLION_ISK_FORMAT = "0.##B";
            const double _1_BILLION = 1000000000;
            const double _1_MILLION = 1000000;

            return iskValue > _1_BILLION
                       ? (iskValue / _1_BILLION).ToString(BILLION_ISK_FORMAT)
                       : (iskValue / _1_MILLION).ToString(MILLION_ISK_FORMAT);
        }
    }

    class FittingTypeEqualityComparer : EqualityComparer<FittingType>
    {
        public override bool Equals(FittingType x, FittingType y)
        {
            return x.Id == y.Id;
        }

        public override int GetHashCode(FittingType obj)
        {
            return obj?.Id.GetHashCode() ?? 0;
        }
    }

    public class Market
    {
        private static readonly ConcurrentDictionary<int, ConcurrentDictionary<int, double>> PRICE_FOR_TYPE_ID_CACHE =
            new ConcurrentDictionary<int, ConcurrentDictionary<int, double>>();

        private readonly ICrest _crest;
        private static readonly IEqualityComparer<FittingType> ID_COMPARER = new FittingTypeEqualityComparer();

        public Market(ICrest crest)
        {
            _crest = crest;
        }

        public IDictionary<int, double> GetPricesForTypeIdsInSystem(int solarSystemId, string namePrefix, Fitting fitting)
        {
            return new[] {fitting.Ship}.Concat(fitting.Items.Select(x => x.Type))
                .Distinct(ID_COMPARER)
                .AsParallel()
                .WithDegreeOfParallelism(Settings.Default.MaxParallelDegree)
                .ToDictionary(x => x.Id, x => GetMinSellPrice(solarSystemId, namePrefix, x));
        }

        private double GetMinSellPrice(int solarSystemId, string namePrefix, FittingType x)
        {
            return PRICE_FOR_TYPE_ID_CACHE.GetOrAdd(solarSystemId, y => new ConcurrentDictionary<int, double>())
                .GetOrAdd(
                          x.Id,
                          item => _crest.Market.GetSellItems(solarSystemId, x.AsCrestType())
                                      .Where(i => i.Location.Name.StartsWith(namePrefix)).Select(m=>m.Price)
                                      .DefaultIfEmpty()
                                      .Min());
        }
    }
}
