using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("IndicatorIndexCityValue")]
    public class IndicatorIndexCityValue
    {
        public IndicatorIndexCityValue()
        {
        }

        [ExplicitKey]
        public long IndicatorIndexCityValueId { get; set; }
        
        public long IndicatorIndexId { get; set; }
        
        public int CityId { get; set; }
        
        public decimal Value { get; set; }
        
        public DateTime CalcDate { get; set; }
        
        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }
    }

    public class IndicatorIndexValueModel
    {
        public long IndicatorIndexId { get; set; }

        public int CityId { get; set; }

        public decimal Value { get; set; }

        public int Year { get; set; }
    }
}
