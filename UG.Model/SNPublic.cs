using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("SNPublic")]
    public class SNPublic
    {
        public SNPublic()
        {
        }

        [ExplicitKey]
        public long SNPublicId { get; set; }
        
        public int SocialNetworkId { get; set; }
        
        public long OuterId { get; set; }
        
        public string Name { get; set; }
        
        public string URL { get; set; }
        
        public int? FolowersQuntity { get; set; }       
        
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
