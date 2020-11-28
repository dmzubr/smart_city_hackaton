using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("IndicatorCityValue")]
    public class IndicatorCityValue
    {
        public IndicatorCityValue()
        {
        }

        [ExplicitKey]
        public long IndicatorCityValueId { get; set; }

        public long IndicatorId { get; set; }

        public int CityId { get; set; }

        public decimal Value { get; set; }

        public DateTime CalcDate { get; set; }

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }
    }

    public class IndicatorValueModel
    {
        public long IndicatorId { get; set; }

        public int CityId { get; set; }

        public decimal Value { get; set; }

        public int Year { get; set; }
    }

    public class IndicatorValuesContainer
    {
        public IList<IndicatorValueModel> indicatorValuesList { get; set; }

        public IList<IndicatorIndexValueModel> indicatorIndexesValuesList { get; set; }        
    }
}
