﻿using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAssetSettingRepository AssetSettings { get; }
        IAssetRelationshipRepository AssetRelationships { get; }
        IAssetRepository Assets { get; }
        IAssetTransactionRepository AssetTransactions { get; }
        IAssetTypeSettingTypeRepository AssetTypesSettingTypes { get; }
        IAssetTypeRelationshipTypeRepository AssetTypesRelationshipTypes { get; }
        IAssetTypeRepository AssetTypes { get; }
        ISettingTypeRepository SettingTypes { get; }
        IRelationshipTypeRepository RelationshipTypes { get; }
        ITransactionTypeRepository TransactionTypes { get; }

        void BeginTrans();
        void CommitTrans();
        void RollBackTrans();
        void Complete();
    }
}