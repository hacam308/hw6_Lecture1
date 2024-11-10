using Lecture1.Data;
using Lecture1.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void SeedData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Check if the database has been seeded already
    if (!context.Customers.Any() && !context.Products.Any())
    {
        // Seed customers
        var customers = new List<Customer>
        {
            new Customer { Name = "John Doe", Id = 1 },
            new Customer { Name = "Jane Smith", Id = 2 }
        };

        context.Customers.AddRange(customers);

        // Seed products
        var products = new List<Product>
        {
            new Product { ProductName = "Product A", Id = 1 },
            new Product { ProductName = "Product B", Id = 2 }
        };

        context.Products.AddRange(products);

        // Save changes to the database
        context.SaveChanges();
    }
}