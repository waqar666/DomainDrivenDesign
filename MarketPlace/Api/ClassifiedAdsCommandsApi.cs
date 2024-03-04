using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api
{
    public class ClassifiedAdsCommandsApi
    {
        ClassifiedAdsApplicationService _classifiedAdsApplicationService;
        public ClassifiedAdsCommandsApi(ClassifiedAdsApplicationService classifiedAdsApplicationService)
        {
            _classifiedAdsApplicationService = classifiedAdsApplicationService;
        }
        [HttpPost]
        public Task<IActionResult> post(Contracts.ClassifiedAds.V1.Create request)
        {

        }
    }
}