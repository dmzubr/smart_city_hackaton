using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("OuterMetricCityValue")]
    public class OuterMetricCityValue
    {
        public OuterMetricCityValue()
        {
        }

        [ExplicitKey]
        public long OuterMetricCityValueId { get; set; }
        
        public long OuterMetricId { get; set; }
        
        public int CityId { get; set; }
        
        public decimal Value { get; set; }
        
        public DateTime CalcDate { get; set; }
        
        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }
    }
}
