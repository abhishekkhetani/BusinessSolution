using BusinessSolution.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusinessSolution
{
    public class EFCodeFirstContext : DbContext
    {
        public EFCodeFirstContext()
            : base()
        {
        }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using Fluent API here

            base.OnModelCreating(modelBuilder);
        }
    }
}