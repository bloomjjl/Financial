using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Fakes
{
    public class FakeTransactionCategories
    {
        public static IEnumerable<TransactionCategory> InitialFakeTransactionCategories()
        {
            yield return new TransactionCategory() { Id = 1, Name = "TransactionCategory1", IsActive = true };
            yield return new TransactionCategory() { Id = 2, Name = "TransactionCategory2", IsActive = true };
            yield return new TransactionCategory() { Id = 3, Name = "TransactionCategory3", IsActive = false };
            yield return new TransactionCategory() { Id = 4, Name = "TransactionCategory4", IsActive = true };
            yield return new TransactionCategory() { Id = 5, Name = "TransactionCategory5", IsActive = true };
        }
    }
}
