using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SGR.DAL.DataAccess
{
    public class SecurityDBContext : IdentityDbContext
    {
        public SecurityDBContext(DbContextOptions<SecurityDBContext> options)
            : base(options)
        {
        }
    }
}
