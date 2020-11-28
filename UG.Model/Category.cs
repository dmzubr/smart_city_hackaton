using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
        }

        [ExplicitKey]
        public int CategoryId { get; set; }
        
        public int Name { get; set; }

        public long IndicatorId { get; set; }
    }
}
