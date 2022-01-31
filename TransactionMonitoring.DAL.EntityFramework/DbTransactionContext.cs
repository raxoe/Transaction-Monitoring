using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TransactionMonitoring.EntityModels;

namespace TransactionMonitoring.DAL.EntityFramework
{
    public class DbTransactionContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public DbTransactionContext(DbContextOptions<DbTransactionContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

    }
}
