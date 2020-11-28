using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    public enum CityTypeEnum
    {
        ADMIN_CENTER = 1,
        BIG = 2,
        BIGGER = 3,
        MOST_BIG = 4
    }

    [Table("CityType")]
    public class CityType
    {
        public CityType()
        {
        }

        [ExplicitKey]
        public int CityTypeId { get; set; }
        
        public string Name { get; set; }
    }
}
