using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;
using System.Collections.Generic;

namespace UG.ORM.Impl
{
    public class IndicatorSocialVerificationServiceImpl : BaseCRUDService<IndicatorSocialVerification>, IIndicatorSocialVerificationService
    {
        public IndicatorSocialVerificationServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }

        public async Task<IEnumerable<IndicatorSocialVerificationExtendedModel>> GetListByYear(int year)
        {
            var srcList = await this.GetList();
            srcList = srcList.Where(X => X.PeriodStart.Year == year);
            var res = srcList.Select(X => new IndicatorSocialVerificationExtendedModel
            {
                CalcDate = X.CalcDate,
                CityId = X.CityId,
                IndicatorId = X.IndicatorId,
                IndicatorSocialVerificationId = X.IndicatorSocialVerificationId,
                PeriodEnd = X.PeriodEnd,
                PeriodStart = X.PeriodStart,
                Value = X.Value
            });
            return res;
        }

        private async Task<IEnumerable<CommonSocialRecordModel>> GetcommonSocialRecordsFromWallPosts(long indicatorId)
        {
            var conn = GetMySqlConnection();
            var sql = @"
SELECT
	P.SNWallPostId,        
	P.Text,
	P.PublishDateTime,
	P.WallPostUrl as Url,
	P.EmotionMark,
	P.LikesQuantity,
	P.RepostQuantity,
	P.CommentQuantity,
	P.ViewsQuantity    

FROM 
    `SNWallPost` P INNER JOIN
    `SNWallPostTriggerWord` PT ON PT.SNWallPostId=P.SNWallPostId INNER JOIN
    `TriggerWord` T ON PT.TriggerWordId=T.TriggerWordId INNER JOIN
    `CategoryTriggerWord` CT ON CT.TriggerWordId=T.TriggerWordId INNER JOIN
    `Category` Cat ON CT.CategoryId=Cat.CategoryId
WHERE Cat.IndicatorId=@IndicatorId;";
            var res = await conn.QueryAndCloseConnAsync<CommonSocialRecordModel>(sql, new { IndicatorId = indicatorId });
            return res;
        }

        private async Task<IEnumerable<CommonSocialRecordModel>> GetcommonSocialRecordsFromComments(long indicatorId)
        {
            var conn = GetMySqlConnection();
            var sql = @"
SELECT
	Comm.SNCommentId,        
	Comm.Text,
	Comm.PublishDateTime,	
	Comm.EmotionMark,
	Comm.LikesQuantity,	
	Comm.CommentsQuantity
FROM 
    `SNComment` Comm INNER JOIN
    `SNCommentTriggerWord` CommTrigger ON CommTrigger.SNCommentId=Comm.SNCommentId INNER JOIN
    `TriggerWord` T ON CommTrigger.TriggerWordId=T.TriggerWordId INNER JOIN
    `CategoryTriggerWord` CT ON CT.TriggerWordId=T.TriggerWordId INNER JOIN
    `Category` Cat ON CT.CategoryId=Cat.CategoryId
WHERE Cat.IndicatorId=@IndicatorId;";
            var res = await conn.QueryAndCloseConnAsync<CommonSocialRecordModel>(sql, new { IndicatorId = indicatorId });
            return res;
        }

        public async Task<IEnumerable<CommonSocialRecordModel>> GetCommonSocialRecordsList(long indicatorId)
        {
            var wallPostRecords = await this.GetcommonSocialRecordsFromWallPosts(indicatorId);
            var wallPostRecordsList = wallPostRecords.ToList();
            var commentrecords = await this.GetcommonSocialRecordsFromComments(indicatorId);
            wallPostRecordsList.AddRange(commentrecords.ToList());
            return wallPostRecordsList;
        }
    }
}
