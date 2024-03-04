using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
   public  class ClassifiedAd:Entity<ClassifiedAdId>
   {
       
        public ClassifiedAd(ClassifiedAdId id, UserId ownerid)
        {
            Id = id;
            OwnerId = ownerid;
            State = ClassifiedAdState.Inactive;
            EnsureValidState();
            Apply(new Events.ClassifiedAdCreated
            {
                Id = id,
                OwnerId = ownerid
            }
                );
        }      

       public void SetTitle(ClassifiedAdTitle title)
        {
            Title = title;
            EnsureValidState();
            Apply(new Events.ClassifiedAdTitleChanged
            {
                Id = Id,
                Title = title
            }
                );
        }
       public void SetText(ClassifiedAdText text)
        {
            Text = text;
            EnsureValidState();
            Apply(new Events.ClassifiedAdTextUpdated
            {
                Id = Id,
                AdText = text
            }
                );
        }
       public void UpdatePrice(Price price)
        {
            Price1 = price;
            EnsureValidState();
            Apply(new Events.ClassifiedAdPriceUpdated
            {
                Id = Id,
                Price = Price1.Amount,
                CurrencyCode = Price1.Currency.CurrencyCode
            }
                );
        }
        public void RequestToPublish()
        {
            State = ClassifiedAdState.PendingReview;
            EnsureValidState();
            Apply(new Events.ClassifiedAdSentForReview
            {
                Id = Id
            }
                );
        }
        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.ClassifiedAdCreated e:
                        Id = new ClassifiedAdId(e.Id);
                        OwnerId = new UserId(e.OwnerId);
                        State = ClassifiedAdState.Inactive;
                        break;
                case Events.ClassifiedAdTextUpdated e:
                        Text = new ClassifiedAdText(e.AdText);
                        break;
                case Events.ClassifiedAdTitleChanged e:
                        Title = new ClassifiedAdTitle(e.Title);
                        break;
                case Events.ClassifiedAdPriceUpdated e:
                        Price1 = new Price(e.Price, e.CurrencyCode);
                        break;
                case Events.ClassifiedAdSentForReview e:
                        State = ClassifiedAdState.PendingReview;
                        break;
                    }
            }

        
        protected override void EnsureValidState()
        {
            var valid =
                Id != null && OwnerId != null &&
                (State switch
                {
                    ClassifiedAdState.PendingReview =>
                    Title != null
                    && Text != null
                    && Price1?.Amount > 0,
                    ClassifiedAdState.Active => 
                    Title != null
                    && Text != null
                    && Price1?.Amount > 0
                    && ApprovedBy != null,
                    _ => true
                });
            if (!valid)
                throw new InvalidEntityStateException(
                    this, $"Post-checks failed in state {State}");
            
        }
        public ClassifiedAdId Id { get; private set; }
        public UserId OwnerId { get; private set; }
       public ClassifiedAdTitle Title { get; private set; }
       public ClassifiedAdText Text { get; private set; }
       public Price Price1 { get; private set; }
        public ClassifiedAdState State { get; private set; }
        public UserId ApprovedBy { get; private set; }
        public enum ClassifiedAdState
        {
            PendingReview,
            Active,
            Inactive,
            MarkedAsSold
        }
   }
}
