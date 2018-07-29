using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Data.RepositoryInterfaces;

namespace Financial.Business.Tests.Fakes
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private bool _trans;

        public InMemoryUnitOfWork()
        {
            Committed = false;
        }

        public bool Committed { get; set; }

        public IAssetSettingRepository AssetSettings { get; set; }
        public IAssetRelationshipRepository AssetRelationships { get; set; }
        public IAssetRepository Assets { get; set; }
        public IAssetTransactionRepository AssetTransactions { get; set; }
        public IAssetTypeSettingTypeRepository AssetTypeSettingTypes { get; set; }
        public IAssetTypeRelationshipTypeRepository AssetTypeRelationshipTypes { get; set; }
        public IAssetTypeRepository AssetTypes { get; set; }
        public IParentChildRelationshipTypeRepository ParentChildRelationshipTypes { get; set; }
        public IRelationshipTypeRepository RelationshipTypes { get; set; }
        public ISettingTypeRepository SettingTypes { get; set; }
        public ITransactionCategoryRepository TransactionCategories { get; set; }
        public ITransactionDescriptionRepository TransactionDescriptions { get; set; }
        public ITransactionTypeRepository TransactionTypes { get; set; }


        public void BeginTrans()
        {
        }
        public void CommitTrans()
        {
            Complete();
        }
        public void RollBackTrans()
        {
        }
        public void Complete()
        {
            if (!_trans)
            {
                //_context.SaveChanges();
                Committed = true;
            }
        }
        public void Dispose()
        {
            //_context.Dispose();
        }
    }
}
