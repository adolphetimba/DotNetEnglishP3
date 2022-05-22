using Microsoft.EntityFrameworkCore;
using P3AddNewFunctionalityDotNetCore.Data;
using System;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class InMemoryDbContextFixture : IDisposable
    {
        public P3Referential DbContext { get; private set; }

        public InMemoryDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseInMemoryDatabase(databaseName: "In_memory_db")
                .Options;

            DbContext = new P3Referential(options);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
