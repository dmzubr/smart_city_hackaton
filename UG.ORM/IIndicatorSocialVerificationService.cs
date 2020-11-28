using System.Collections.Generic;
using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IIndicatorSocialVerificationService : ISimpleCRUDService<IndicatorSocialVerification>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        Task<IEnumerable<IndicatorSocialVerificationExtendedModel>> GetListByYear(int year);
    }
}
