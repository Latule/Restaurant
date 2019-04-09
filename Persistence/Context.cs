using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Entities.Invoice;
using Data.Entities.Menu;
using Repositories.Dtos;

namespace Persistence
{
   public class Context : DbContext
    {
        public Context (DbContextOptions options) : base(options) { }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<Command> Commands { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<StoreIngredient> Stores { get; set; }
    }
}
