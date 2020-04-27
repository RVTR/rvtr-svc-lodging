using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext.Databases;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
    public class UnitOfWork_Test
    {
        /// <summary>
        /// Test Unit Of Work Commit functionality by attempting to commit
        /// a change in the database
        /// </summary>
        [Fact]
        public void Test_CommitMethod()
        {
            var options = new DbContextOptionsBuilder<LodgingDbContext>()
                      .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                      .Options;
          BedroomModel _bm = new BedroomModel() { Id = 1, BedType = "King", Count = 1 };

            using (var ldb = new LodgingDbContext(options))
            {
                var sut = new UnitOfWork(ldb);

                Assert.True(sut.BedroomRepository.Insert(_bm));
                sut.Commit();
                Assert.True(sut.BedroomRepository.Select().ToList().Count == 1);
            }
        }
    }
}
