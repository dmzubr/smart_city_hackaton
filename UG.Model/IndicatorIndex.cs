using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("IndicatorIndex")]
    public class IndicatorIndex
    {
        public IndicatorIndex()
        {
        }

        [ExplicitKey]
        public long IndicatorIndexId { get; set; }

        public long IndicatorId { get; set; }

        public string Name { get; set; }
        
        public string Number { get; set; }
        
        public string Description { get; set; }
        
        public string Type { get; set; }
    }
}
