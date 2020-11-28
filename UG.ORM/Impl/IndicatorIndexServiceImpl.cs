﻿using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class IndicatorIndexServiceImpl : BaseCRUDService<IndicatorIndex>, IIndicatorIndexService
    {
        public IndicatorIndexServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }
    }
}
