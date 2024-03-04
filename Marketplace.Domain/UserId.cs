using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
 public   class UserId
    {
        private Guid Value { get; set; }
        public UserId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "User Id cannot be Empty");
            Value = value;
        }
        public static implicit operator Guid(UserId self) => self.Value;

    }
}
