using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("Region")]
    public class Region
    {
        public Region()
        {
        }

        [ExplicitKey]
        public int RegionId { get; set; }
        
        public string Name { get; set; }        
    }
}
