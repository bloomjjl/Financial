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

namespace Financial.Core
{
    public class FinancialDbContext : DbContext, IFinancialDbContext
    {
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetSetting> AssetSettings { get; set; }
        public virtual DbSet<AssetRelationship> AssetRelationships { get; set; }
        public virtual DbSet<AssetTransaction> AssetTransactions { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<AssetTypeSettingType> AssetTypesSettingTypes { get; set; }
        public virtual DbSet<AssetTypeRelationshipType> AssetTypesRelationshipTypes { get; set; }
        public virtual DbSet<ParentChildRelationshipType> ParentChildRelationshipTypes { get; set; }
        public virtual DbSet<RelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<SettingType> SettingTypes { get; set; }
        public virtual DbSet<TransactionCategory> TransactionCategories { get; set; }
        public virtual DbSet<TransactionDescription> TransactionDescriptions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    }
}
