using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core
{
    public interface IFinancialDbContext
    {
        DbSet<Asset> Assets { get; }
        DbSet<AssetSetting> AssetSettings { get; }
        DbSet<AssetRelationship> AssetRelationships { get; }
        DbSet<AssetTransaction> AssetTransactions { get; }
        DbSet<AssetType> AssetTypes { get; }
        DbSet<AssetTypeSettingType> AssetTypesSettingTypes { get; }
        DbSet<AssetTypeRelationshipType> AssetTypesRelationshipTypes { get; }
        DbSet<ParentChildRelationshipType> ParentChildRelationshipTypes { get; }
        DbSet<RelationshipType> RelationshipTypes { get; }
        DbSet<SettingType> SettingTypes { get; }
        DbSet<TransactionCategory> TransactionCategories { get; }
        DbSet<TransactionDescription> TransactionDescriptions { get; }
        DbSet<TransactionType> TransactionTypes { get; }
    }
}
