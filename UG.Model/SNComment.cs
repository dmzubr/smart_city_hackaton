using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("SNComment")]
    public class SNComment
    {
        public SNComment()
        {
        }

        [ExplicitKey]
        public long SNCommentId { get; set; }
        
        public long OuterId { get; set; }
        
        public DateTime PublishDateTime { get; set; }
        
        public string Text { get; set; }
        
        public long WallPostId { get; set; }
        
        public long? OuterAuthorId { get; set; }
        
        public int? LikesQuantity { get; set; }
        
        public int? CommentsQuantity { get; set; }
        
        public DateTime LastModifiedDateTime { get; set; }
        
        public int? EmotionMark { get; set; }
        
        public int? IsTarget { get; set; }
    }
}
