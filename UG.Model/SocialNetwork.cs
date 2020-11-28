using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("SocialNetwork")]
    public class SocialNetwork
    {
        public SocialNetwork()
        {
        }

        [ExplicitKey]
        public int SocialNetworkId { get; set; }
        
        public string Name { get; set; }
    }
}
