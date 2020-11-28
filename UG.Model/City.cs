using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("City")]
    public class City
    {
        public City()
        {
        }

        [ExplicitKey]
        public int CityId { get; set; }
        
        public int RegionId { get; set; }

        public int CityTypeId { get; set; }

        public string Name { get; set; }
        
        public string OKTMO { get; set; }
    }
}
