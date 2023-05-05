﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SGI_WebApi_Pauny.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? Deleted { get; set; }
    }
}