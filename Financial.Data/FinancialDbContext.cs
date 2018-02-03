using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Financial.Data
{
    public class FinancialDbContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetSetting> AssetAttributes { get; set; }
        public DbSet<AssetRelationship> AssetRelationships { get; set; }
        public DbSet<AssetTransaction> AssetTransactions { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<AssetTypeSettingType> AssetTypesSettingTypes { get; set; }
        public DbSet<AssetTypeRelationshipType> AssetTypesRelationshipTypes { get; set; }
        public DbSet<ParentChildRelationshipType> ParentChildRelationshipTypes { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<SettingType> SettingTypes { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<TransactionDescription> TransactionDescriptions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
    }
}
