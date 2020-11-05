using System;
using System.Collections.Generic;

namespace SGRBlazorApp.Data
{
    public partial class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid? IdUser { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
