using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    /// <summary>
    /// This is a universal record for both social network wall post and comment
    /// </summary>
    public class CommonSocialRecordModel
    {
        public long SNWallPostId { get; set; }
        
        public long SNcommentId { get; set; }
        
        public string Text { get; set; }
        
        public DateTime? PublishDateTime { get; set; }
        
        public string Url { get; set; }
        
        public int? EmotionMark { get; set; }

        public int? LikesQuantity { get; set; }
        
        public int? RepostQuantity { get; set; }
        
        public int? CommentQuantity { get; set; }
        
        public int? ViewsQuantity { get; set; }
    }

    public interface IIndicatorSocialVerificationService : ISimpleCRUDService<IndicatorSocialVerification>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        Task<IEnumerable<IndicatorSocialVerificationExtendedModel>> GetListByYear(int year);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indicatorId"></param>
        /// <returns></returns>
        Task<IEnumerable<CommonSocialRecordModel>> GetCommonSocialRecordsList(long indicatorId);
    }
}
