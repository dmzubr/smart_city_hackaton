using System;

using Dapper.Contrib.Extensions;

namespace UG.Model
{
    [Table("SNUser")]
    public class SNUser
    {
        public SNUser()
        {
        }

        [ExplicitKey]
        public long SNUserId { get; set; }
        
        public int SocialNetworkId { get; set; }
        
        public long OuterId { get; set; }
        
        public long NameAlias { get; set; }
        
        public string UserPageURL { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string City { get; set; }

        public string Phone { get; set; }
        
        public int? FriendsQuantity { get; set; }

        public int? FollowersQuantity { get; set; }

        public DateTime LastModifiedDateTime { get; set; }
    }
}
