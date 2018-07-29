using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Tests.Fakes.Database
{
    public class FakeParentChildRelationshipTypes
    {
        public static IEnumerable<ParentChildRelationshipType> InitialFakeParentChildRelationshipTypes()
        {
            yield return new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 2, ChildRelationshipTypeId = 4, IsActive = true };
            yield return new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 4, ChildRelationshipTypeId = 5, IsActive = true };
            yield return new ParentChildRelationshipType() { Id = 3, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 3, IsActive = false };
            yield return new ParentChildRelationshipType() { Id = 4, ParentRelationshipTypeId = 5, ChildRelationshipTypeId = 1, IsActive = true };
            yield return new ParentChildRelationshipType() { Id = 5, ParentRelationshipTypeId = 1, ChildRelationshipTypeId = 2, IsActive = true };
        }
    }
}
