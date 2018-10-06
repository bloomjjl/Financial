using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using System.Data.Entity.Infrastructure;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Moq.Language.Flow;
using Moq.Language;
using Moq;

namespace Financial.Tests.Mocks
{
    public static class MockDbSet
    {
        public static DbSet<T> Create<T>(IEnumerable<T> data = null) where T : class
        {
            // create a mock DbSet exposing both DbSet and IQueryable interfaces for setup
            var mockDbSet = Substitute.For<DbSet<T>, IQueryable<T>>();

            if (data != null)
            {
                // convert list to queryable
                var qData = data.AsQueryable();

                // setup all IQueryable methods using what you have from "data"
                ((IQueryable<T>)mockDbSet).Provider.Returns(qData.Provider);
                ((IQueryable<T>)mockDbSet).Expression.Returns(qData.Expression);
                ((IQueryable<T>)mockDbSet).ElementType.Returns(qData.ElementType);
                ((IQueryable<T>)mockDbSet).GetEnumerator().Returns(qData.GetEnumerator());
            }

            // bypass include
            mockDbSet.Include(Arg.Any<string>())
                .Returns(mockDbSet);

            return mockDbSet;
        }
    }
}
