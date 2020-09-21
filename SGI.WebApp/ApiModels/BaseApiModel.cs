using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGI.WebApp.ApiModels
{
    public abstract class BaseApiModel
    {
        public Guid? Id { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
