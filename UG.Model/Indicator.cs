using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("Indicator")]
    public class Indicator
    {
        public Indicator()
        {
        }

        [ExplicitKey]
        public long IndicatorId { get; set; }

        public int SubIndexId { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }
        
        public string CalculationDescription { get; set; }
    }

    public class IndicatorViewModel : Indicator
    {
        public IndicatorIndex[] indicatorIndexes { get; set; }
    }
}
