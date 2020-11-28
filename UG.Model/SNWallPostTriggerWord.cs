using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("SNWallPostTriggerWord")]
    public class SNWallPostTriggerWord
    {
        public SNWallPostTriggerWord()
        {
        }

        [ExplicitKey]
        public long SNWallPostTriggerWordId { get; set; }
        
        public long SNWallPostId { get; set; }
        
        public long TriggerWordId { get; set; }
    }
}
