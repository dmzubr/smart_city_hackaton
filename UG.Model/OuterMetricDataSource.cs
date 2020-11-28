using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("OuterMetricDataSource")]
    public class OuterMetricDataSource
    {
        public OuterMetricDataSource()
        {
        }

        [ExplicitKey]
        public long OuterMetricDataSourceId { get; set; }

        public long OuterMetricId { get; set; }
        
        public string Name { get; set; }
        
        public string HandlerUrl { get; set; }
    }

    public class OuterMetricDataSourceViewModel : OuterMetricDataSource
    {
        public string MetricName { get; set; }
    }
}
