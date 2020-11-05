using System;
using System.Collections.Generic;

namespace SGRBlazorApp.Data
{
    public partial class User
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? HireDate { get; set; }
        public Guid? IdRole { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid? IdDealer { get; set; }

        public virtual Dealer IdDealerNavigation { get; set; }
        public virtual Role IdRoleNavigation { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public string ConfirmPassword { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
