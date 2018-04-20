using Financial.Core;
using Financial.Data.RepositoryInterfaces;
using Financial.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinancialDbContext _context;
        private bool _trans;

        public UnitOfWork()
        {
            _context = new FinancialDbContext();
            SetRepositories();
        }

        public UnitOfWork(FinancialDbContext context)
        {
            _context = context;
            SetRepositories();
        }

        private void SetRepositories()
        {
            AssetSettings = new AssetSettingRepository(_context);
            AssetRelationships = new AssetRelationshipRepository(_context);
            Assets = new AssetRepository(_context);
            AssetTransactions = new AssetTransactionRepository(_context);
            AssetTypesSettingTypes = new AssetTypeSettingTypeRepository(_context);
            AssetTypesRelationshipTypes = new AssetTypeRelationshipTypeRepository(_context);
            AssetTypes = new AssetTypeRepository(_context);
            ParentChildRelationshipTypes = new ParentChildRelationshipTypeRepository(_context);
            RelationshipTypes = new RelationshipTypeRepository(_context);
            SettingTypes = new SettingTypeRepository(_context);
            TransactionCategories = new TransactionCategoryRepository(_context);
            TransactionDescriptions = new TransactionDescriptionRepository(_context);
            TransactionTypes = new TransactionTypeRepository(_context);
        }

        public IAssetSettingRepository AssetSettings { get; private set; }
        public IAssetRelationshipRepository AssetRelationships { get; private set; }
        public IAssetRepository Assets { get; private set; }
        public IAssetTransactionRepository AssetTransactions { get; private set; }
        public IAssetTypeSettingTypeRepository AssetTypesSettingTypes { get; private set; }
        public IAssetTypeRelationshipTypeRepository AssetTypesRelationshipTypes { get; private set; }
        public IAssetTypeRepository AssetTypes { get; private set; }
        public IParentChildRelationshipTypeRepository ParentChildRelationshipTypes { get; set; }
        public IRelationshipTypeRepository RelationshipTypes { get; private set; }
        public ISettingTypeRepository SettingTypes { get; private set; }
        public ITransactionCategoryRepository TransactionCategories { get; private set; }
        public ITransactionDescriptionRepository TransactionDescriptions { get; private set; }
        public ITransactionTypeRepository TransactionTypes { get; private set; }

        public void BeginTrans()
        {
            _trans = true;
        }
        public void CommitTrans()
        {
            _trans = false;
            Complete();
        }
        public void RollBackTrans()
        {
            _trans = false;
        }
        public void Complete()
        {
            if(!_trans)
            {
                _context.SaveChanges();
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
