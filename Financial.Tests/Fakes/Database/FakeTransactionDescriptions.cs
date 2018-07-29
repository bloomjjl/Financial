using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Tests.Fakes.Database
{
    public class FakeTransactionDescriptions
    {
        public static IEnumerable<TransactionDescription> InitialFakeTransactionDescriptions()
        {
            yield return new TransactionDescription() { Id = 1, Name = "TransactionDescription1", IsActive = true };
            yield return new TransactionDescription() { Id = 2, Name = "TransactionDescription2", IsActive = true };
            yield return new TransactionDescription() { Id = 3, Name = "TransactionDescription3", IsActive = false };
            yield return new TransactionDescription() { Id = 4, Name = "TransactionDescription4", IsActive = true };
            yield return new TransactionDescription() { Id = 5, Name = "TransactionDescription5", IsActive = true };
        }
    }
}
