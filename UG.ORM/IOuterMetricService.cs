using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IOuterMetricService : ISimpleCRUDService<OuterMetric>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="metricCode"></param>
        /// <returns></returns>
        Task<OuterMetric> GetByCode(string metricCode);
    }
}
