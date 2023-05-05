using Microsoft.AspNetCore.Mvc;
using SGR.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TBBlazorApp.Data
{
    public partial class UserModel : User
    {
        public string ConfirmPassword { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserModel()
        {

        }

        public UserModel(User user)
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
