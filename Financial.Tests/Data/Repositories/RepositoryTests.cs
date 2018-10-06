using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core;
using NUnit.Framework;
using Moq;
using Financial.Data.Repositories;
using System.Data.Entity;
using Financial.Core.Models;
using Financial.Tests.Mocks;

namespace Financial.Tests.Data.Repositories
{
    [TestFixture]
    public class RepositoryTests
    {
        private BaseEntity _baseEntity;
        private DbSet<BaseEntity> _mockDbSet;
        private Mock<FinancialDbContext> _mockDbContext;
        private FinancialDbContext _fakeDbContext;
        private Repository<BaseEntity> _repository;



        [SetUp]
        public void SetUp()
        {
            // setup fake db
            var fakeAssets = new List<BaseEntity>();

            SetupMockWithNewFakeDb(fakeAssets);
        }

        [TearDown]
        public void TearDown()
        {

        }



        [Test]
        public void Get_WhenIdIsValid_ReturnEntity_Test()
        {
            // Arrange
            var fakeEntity = new BaseEntity {Id = 1, IsActive = true};

            var fakeEntities = new List<BaseEntity>
            {
                fakeEntity,
            };

            SetupMockWithNewFakeDb(fakeEntities);

            // Act
            var result = _repository.Get(id: 1);

            // Assert
            Assert.That(result, Is.EqualTo(fakeEntity));
        }

        [Test]
        public void Get_WhenIdIsNotValid_ReturnNull_Test()
        {
            // Arrange
            var fakeEntity = new BaseEntity { Id = 1, IsActive = true };

            var fakeEntities = new List<BaseEntity>
            {
                fakeEntity,
            };

            SetupMockWithNewFakeDb(fakeEntities);

            // Act
            var result = _repository.Get(id: 2);

            // Assert
            Assert.That(result, Is.EqualTo(null));
        }



        [Test]
        public void GetActive_WhenEntityIsActiveEqualsTrue_ReturnEntity_Test()
        {
            // Arrange
            var fakeEntity = new BaseEntity { Id = 1, IsActive = true };

            var fakeEntities = new List<BaseEntity>
            {
                fakeEntity,
            };

            SetupMockWithNewFakeDb(fakeEntities);

            // Act
            var result = _repository.GetActive(id: 1);

            // Assert
            Assert.That(result, Is.EqualTo(fakeEntity));
        }

        [Test]
        public void GetActive_WhenEntityIsActiveEqualsFalse_ReturnNull_Test()
        {
            // Arrange
            var fakeEntity = new BaseEntity { Id = 1, IsActive = false };

            var fakeEntities = new List<BaseEntity>
            {
                fakeEntity,
            };

            SetupMockWithNewFakeDb(fakeEntities);

            // Act
            var result = _repository.GetActive(id: 1);

            // Assert
            Assert.That(result, Is.EqualTo(null));
        }



        [Test]
        public void GetAll_WhenCalled_ReturnAllEntities_Test()
        {
            // Arrange
            var fakeEntities = new List<BaseEntity>
            {
                new BaseEntity { Id = 1, IsActive = true },
                new BaseEntity { Id = 2, IsActive = true }
            };

            SetupMockWithNewFakeDb(fakeEntities);

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }



        [Test]
        public void GetAllActive_WhenCalled_ReturnEntitiesThatIsActiveEqualsTrue_Test()
        {
            // Arrange
            var fakeEntities = new List<BaseEntity>
            {
                new BaseEntity { Id = 1, IsActive = true },
                new BaseEntity { Id = 2, IsActive = false }
            };

            SetupMockWithNewFakeDb(fakeEntities);

            // Act
            var result = _repository.GetAllActive();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
        }



        [Test]
        public void Exists_WhenIdIsValid_ReturnTrue_Test()
        {
            // Arrange
            var fakeEntity = new BaseEntity { Id = 1, IsActive = true };

            var fakeEntities = new List<BaseEntity>
            {
                fakeEntity,
            };

            SetupMockWithNewFakeDb(fakeEntities);

            // Act
            var result = _repository.Exists(id: 1);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void Exists_WhenIdIsNotValid_ReturnFalse_Test()
        {
            // Arrange
            var fakeEntity = new BaseEntity { Id = 1, IsActive = true };

            var fakeEntities = new List<BaseEntity>
            {
                fakeEntity,
            };

            SetupMockWithNewFakeDb(fakeEntities);

            // Act
            var result = _repository.Exists(id: 2);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }



        [Test]
        public void Add_WhenEntityProvided_CallDbContextAddProperty_Test()
        {
            // Arrange
            var newEntity = new BaseEntity { /*Id = 1,*/ IsActive = true };
            var count = 0;
            _mockDbContext.Setup(a => a.Set<BaseEntity>().Add(It.IsAny<BaseEntity>()))
                .Callback(() => count++);

            // Act
            _repository.Add(newEntity);

            // Assert
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Add_WhenEntityProvided_UpdateDbContextWithEntity_Test()
        {
            // Arrange
            var newEntity = new BaseEntity { /*Id = 1,*/ IsActive = true };
            var count = 0;
            _mockDbContext.Setup(a => a.Set<BaseEntity>().Add(It.IsAny<BaseEntity>()))
                .Callback(() => count++);

            // Act
            _repository.Add(newEntity);

            // Assert
            Assert.That(count, Is.EqualTo(1));
        }



        [Test]
        public void AddRange_WhenEntitiesProvided_CallDbContextAddRangeProperty_Test()
        {
            // Arrange
            var newEntities = new List<BaseEntity>
            {
                new BaseEntity { /*Id = 1,*/ IsActive = true },
                new BaseEntity { /*Id = 2,*/ IsActive = true },
            };
            var count = 0;
            _mockDbContext.Setup(a => a.Set<BaseEntity>().AddRange(It.IsAny<List<BaseEntity>>()))
                .Callback(() => count++);

            // Act
            _repository.AddRange(newEntities);

            // Assert
            Assert.That(count, Is.EqualTo(1));
        }



        [Test]
        public void Remove_WhenEntityProvided_CallDbContextRemoveProperty_Test()
        {
            // Arrange
            var newEntity = new BaseEntity { /*Id = 1,*/ IsActive = true };
            var count = 0;
            _mockDbContext.Setup(a => a.Set<BaseEntity>().Remove(It.IsAny<BaseEntity>()))
                .Callback(() => count++);

            // Act
            _repository.Remove(newEntity);

            // Assert
            Assert.That(count, Is.EqualTo(1));
        }



        [Test]
        public void RemoveRange_WhenEntitiesProvided_CallDbContextRemoveProperty_Test()
        {
            // Arrange
            var newEntities = new List<BaseEntity>
            {
                new BaseEntity { /*Id = 1,*/ IsActive = true },
                new BaseEntity { /*Id = 2,*/ IsActive = true },
            };
            var count = 0;
            _mockDbContext.Setup(a => a.Set<BaseEntity>().RemoveRange(It.IsAny<List<BaseEntity>>()))
                .Callback(() => count++);

            // Act
            _repository.RemoveRange(newEntities);

            // Assert
            Assert.That(count, Is.EqualTo(1));
        }





        // private methods



        private void SetupMockWithNewFakeDb(List<BaseEntity> fakeBaseEntities)
        {
            // setup DbSet
            _mockDbSet = MockDbSet.Create<BaseEntity>(fakeBaseEntities);

            // setup DbContext
            _mockDbContext = new Mock<FinancialDbContext>();
            _mockDbContext.Setup(c => c.Set<BaseEntity>())
                .Returns(_mockDbSet);

            // set up repository
            _repository = new Repository<BaseEntity>(_mockDbContext.Object);
        }

    }
}
