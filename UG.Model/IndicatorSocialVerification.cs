using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("IndicatorSocialVerification")]
    public class IndicatorSocialVerification
    {
        public IndicatorSocialVerification()
        {
        }

        [ExplicitKey]
        public long IndicatorSocialVerificationId { get; set; }

        public long IndicatorId { get; set; }
        
        public int CityId { get; set; }
        
        public decimal Value { get; set; }
        
        public DateTime CalcDate { get; set; }
        
        public DateTime PeriodStart { get; set; }
        
        public DateTime PeriodEnd { get; set; }
    }
}
