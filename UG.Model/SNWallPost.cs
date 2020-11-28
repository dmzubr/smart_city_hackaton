using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("SNWallPost")]
    public class SNWallPost
    {
        public SNWallPost()
        {
        }

        [ExplicitKey]
        public long SNWallPostId { get; set; }
        
        public long OuterId { get; set; }
        
        public DateTime? PublishDateTime { get; set; }
        
        public string Text { get; set; }
        
        public long? WallOwnerOuterId { get; set; }
        
        public long? OuterAuthorId { get; set; }
        
        public string WallPostURL { get; set; }
        
        public int? LikesQuantity { get; set; }
        
        public int? RepostQuantity { get; set; }
                
        public int? CommentQuantity { get; set; }
        
        public int? ViewsQuantity { get; set; }
        
        public DateTime LastModifiedDateTime { get; set; }
        
        public int EmotionMark { get; set; }
        
        public bool? IsTarget { get; set; }
    }
}
