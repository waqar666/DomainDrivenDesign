using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Domain;
using Marketplace.Framework;
using Marketplace.Contracts;

namespace Marketplace.Api
{
    public class ClassifiedAdsApplicationService : IApplicationService
    {
        IClassifiedAdsRepository _repository; ICurrencyLookup _currencyLookup; 
        public ClassifiedAdsApplicationService(IClassifiedAdsRepository repository, ICurrencyLookup currencyLookup)
        {
            _repository = repository; _currencyLookup = currencyLookup;
        }
       public async Task Handle(object command)
        {
            switch(command)
            {
                case ClassifiedAds.V1.Create cmd:
                    if (await _repository.Exists(cmd.Id.ToString()))
                        throw new InvalidOperationException($"Entity with id {cmd.Id} already exists");
                    var classifiedAd = new ClassifiedAd(
                        new ClassifiedAdId(cmd.Id),
                        new UserId(cmd.OwnerId));
                    await _repository.Save(classifiedAd);
                    break;
            }
        }
    }
}
