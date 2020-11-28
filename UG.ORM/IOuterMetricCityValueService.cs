using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public class MetricValueSubmitBodyModel
    {
        public MetricValueImportRecordModel[] values { get; set; }
    }

    public class MetricValueImportRecordModel
    {
        public string oktmo { get; set; }

        public string metric_code { get; set; }

        public decimal value { get; set; }

        public DateTime period_start { get; set; }

        public DateTime period_end { get; set; }
    }

    public interface IOuterMetricCityValueService : ISimpleCRUDService<OuterMetricCityValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerMetricId"></param>
        /// <returns></returns>
        Task<IEnumerable<OuterMetricValueViewModel>> GetVMListByMetric(long outerMetricId);
    }
}
