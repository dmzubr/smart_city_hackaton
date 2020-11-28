using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("SNCommentTriggerWord")]
    public class SNCommentTriggerWord
    {
        public SNCommentTriggerWord()
        {
        }

        [ExplicitKey]
        public long SNCommentTriggerWordId { get; set; }
        
        public long SNCommentId { get; set; }
        
        public long TriggerWordId { get; set; }
    }
}
