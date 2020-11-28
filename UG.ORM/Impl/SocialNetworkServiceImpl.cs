using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class SocialNetworkServiceImpl : BaseCRUDService<SocialNetwork>, ISocialNetworkService
    {
        public SocialNetworkServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }
    }
}
