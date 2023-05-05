using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGI_WebApi_Pauny.Models
{
    public class UserWithToken : User
    {
        
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.EmailAddress = user.EmailAddress;            
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.HireDate = user.HireDate;
            this.IdDealer = user.IdDealer;
            this.IdDealerNavigation = user.IdDealerNavigation;
            this.IdRoleNavigation = user.IdRoleNavigation;
        }
    }
}
