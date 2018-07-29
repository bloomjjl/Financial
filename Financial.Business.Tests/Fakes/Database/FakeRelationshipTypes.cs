using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Tests.Fakes.Database
{
    public class FakeRelationshipTypes
    {
        public static IEnumerable<RelationshipType> InitialFakeRelationshipTypes()
        {
            yield return new RelationshipType() { Id = 1, Name = "RelationshipTypeName1", IsActive = true };
            yield return new RelationshipType() { Id = 2, Name = "RelationshipTypeName2", IsActive = true };
            yield return new RelationshipType() { Id = 3, Name = "RelationshipTypeName3", IsActive = false };
            yield return new RelationshipType() { Id = 4, Name = "RelationshipTypeName4", IsActive = true };
            yield return new RelationshipType() { Id = 5, Name = "RelationshipTypeName5", IsActive = true };
        }
    }
}
