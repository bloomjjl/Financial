﻿using Financial.Core;
using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data
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
        public IAssetTypeSettingTypeRepository AssetTypesSettingTypes { get; set; }
        public IAssetTypeRelationshipTypeRepository AssetTypesRelationshipTypes { get; set; }
        public IAssetTypeRepository AssetTypes { get; set; }
        public ISettingTypeRepository SettingTypes { get; set; }
        public IRelationshipTypeRepository RelationshipTypes { get; set; }
        public ITransactionTypeRepository TransactionTypes { get; set; }

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