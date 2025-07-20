using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DLA.Data;

public class ToDoContextFactory : IDesignTimeDbContextFactory<ToDoContext>
{
    public ToDoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ToDoContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Test;User Id=SA;Password=O435762198_Ok;TrustServerCertificate=True;");

        return new ToDoContext(optionsBuilder.Options);
    }
}