using SGI.ApplicationCore.Interfaces;
using SGI.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.UnitOfWorks
{
    public class SGIAppUnitOfWork : IUnitOfWork
    {
        private readonly SGIApplicationDataContext _context;
        public SGIAppUnitOfWork(SGIApplicationDataContext context)
        {
            _context = context;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
