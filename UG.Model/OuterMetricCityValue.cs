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

    public class OuterMetricValueViewModel : OuterMetricCityValue
    {
        public string CityName { get; set; }
        
        public string MetricName { get; set; }

        public int Year { get { return this.PeriodStart.Year; } }
    }
}
