﻿using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.RepositoryInterfaces
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Asset GetActive(int assetId);
        IEnumerable<Asset> GetAllActiveOrderedByName();
    }
}