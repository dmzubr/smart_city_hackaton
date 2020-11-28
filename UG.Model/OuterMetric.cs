using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("OuterMetric")]
    public class OuterMetric
    {
        public OuterMetric()
        {
        }

        [ExplicitKey]
        public long OuterMetricId { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public string Description { get; set; }
    }
}
