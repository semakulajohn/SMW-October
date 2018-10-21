﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace SMW.EF.Context
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        DbContextConfiguration Configuration { get; }
        int SaveChanges();
        void Dispose();
    }
}

