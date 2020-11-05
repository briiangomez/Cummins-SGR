using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRBlazorApp.Data
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
