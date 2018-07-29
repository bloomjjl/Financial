using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Tests.Fakes.Database
{
    public class FakeTransactionTypes
    {
        public static IEnumerable<TransactionType> InitialFakeTransactionTypes()
        {
            yield return new TransactionType() { Id = 1, Name = "TransactionType1", IsActive = true };
            yield return new TransactionType() { Id = 2, Name = "TransactionType2", IsActive = true };
            yield return new TransactionType() { Id = 3, Name = "TransactionType3", IsActive = false };
            yield return new TransactionType() { Id = 4, Name = "TransactionType4", IsActive = true };
            yield return new TransactionType() { Id = 5, Name = "TransactionType5", IsActive = true };
        }
    }
}
