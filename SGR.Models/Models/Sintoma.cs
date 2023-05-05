﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Sintoma
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }
    }
}