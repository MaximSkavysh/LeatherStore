﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Domain.Entities;

namespace Store.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Thing> Things{get; set;}


    }
}