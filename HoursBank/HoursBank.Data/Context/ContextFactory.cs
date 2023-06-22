using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HoursBank.Data.Context
{
    class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=hoursbank.c0tllc9aptlh.sa-east-1.rds.amazonaws.com;Database=HoursBank;Uid=root;Pwd=*Bh2023db";
            //var connectionString = "Server=localhost\\SQLEXPRESS;Database=HoursBank;Uid=root;Pwd=Asdfg123";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
