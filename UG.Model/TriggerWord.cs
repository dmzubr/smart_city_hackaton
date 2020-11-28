using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("TriggerWord")]
    public class TriggerWord
    {
        public TriggerWord()
        {
        }

        [ExplicitKey]
        public long TriggerWordId { get; set; }
        
        public string Name { get; set; }
        
        public bool IsTarget { get; set; }
    }
}
