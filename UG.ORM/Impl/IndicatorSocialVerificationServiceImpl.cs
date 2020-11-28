using System.Threading.Tasks;
using System.Linq;

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
    }
}
