using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("SubIndex")]
    public class SubIndex
    {
        public SubIndex()
        {
        }

        [ExplicitKey]
        public int SubIndexId { get; set; }
        
        public string Name { get; set; }
    }

    public class SubIndexViewModel : SubIndex
    {
        public IndicatorViewModel[] indicatorsList { get; set; }
    }
}
