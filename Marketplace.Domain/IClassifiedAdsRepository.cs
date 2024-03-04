using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
 public   interface IClassifiedAdsRepository
    {
        Task<bool> Exists(ClassifiedAdId id);

       Task<ClassifiedAd> Load(ClassifiedAdId id);

        Task Save(ClassifiedAd entity);

    }
}
