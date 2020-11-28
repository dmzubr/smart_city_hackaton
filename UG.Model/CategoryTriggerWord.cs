using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("CategoryTriggerWord")]
    public class CategoryTriggerWord
    {
        public CategoryTriggerWord()
        {
        }

        [ExplicitKey]
        public long CategoryTriggerWordId { get; set; }
        
        public int CategoryId { get; set; }
        
        public long TriggerWordId { get; set; }
    }
}
