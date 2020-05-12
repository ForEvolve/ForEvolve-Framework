# ForEvolve.EntityFrameworkCore

Adds EF Core utilities, like easy data seeding, and value conversion.

## How to install?

```bash
dotnet add package ForEvolve.EntityFrameworkCore
```

## Easy Data Seeding

The idea behind this is to create one or more data seeding classes, then scan one or more assemblies to find those data seeder classes, then execute them against one or more `DbContext`.

It allows to encapsulate data seeding into cohesive units of code, like `CategorySeeder`, `ProductSeeder`, and `WhateverSeeder`. Moreover, it removes the need to register every single seeder manually, automating that process.

There is no magic seeding mechanism, but an extension method called `Seed`. So all in all, the process is in three steps:

1. Create one or more seeders that implement the `ISeeder<TDbContext>` interface.
1. Register those seeders (or scan one or more assemblies).
1. Start the seeding process when you need it.

### Create a seeder

Each seeder must implement the `ISeeder<TDbContext>` interface.

```csharp
public class ProductSeeder : ISeeder<ProductContext>
{
    public void Seed(ProductContext db)
    {
        db.Products.Add(new Product
        {
            Id = 1,
            Name = "Banana",
            QuantityInStock = 50
        });
        db.Products.Add(new Product
        {
            Id = 2,
            Name = "Apple",
            QuantityInStock = 20
        });
        db.Products.Add(new Product
        {
            Id = 3,
            Name = "Habanero Pepper",
            QuantityInStock = 10
        });
        db.SaveChanges();
    }
}
```

### Register your seeders

In the `Startup.ConfigureServices` method:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddForEvolveSeeders().Scan<Startup>();
    //...
}
```

### Execute the seeders

To execute all of the registered seeders for a given `DbContext`, in `Startup.Configure`:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // ...
    app.Seed<ProductContext>();
    // ...
}
```

You can call `app.Seed<TDbContext>()` on as many `DbContext` as you need.

## Convert objects to JSON

If you want to save some complex properties into a single field, as JSON, there's two value converters for you:

1. `ObjectToJsonConverter<TObject>` that serialize an object to JSON.
1. `DictionaryToJsonConverter` that serialize a `Dictionary<string, object>` to JSON.

### Setup your DbContext

To setup your `DbContext`, you must:

1. Create an instance of the converter that you want to use (globally or locally)
2. Tell EF Core about that converter

```csharp
public class SeederDbContext : DbContext
{
    // Create converters (each are reusable for multiple properties)
    private readonly ObjectToJsonConverter<object> _objectToJsonConverter = new ObjectToJsonConverter<object>();
    private readonly ObjectToJsonConverter<SubEntity> _subEntityToJsonConverter = new ObjectToJsonConverter<SubEntity>();
    private readonly DictionaryToJsonConverter _dictionaryToJsonConverter = new DictionaryToJsonConverter();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tell EF Core that `TestEntity.Object` needs the `_objectToJsonConverter`
        modelBuilder
            .Entity<TestEntity>()
            .Property(e => e.Object)
            .HasConversion(_objectToJsonConverter);

        // Tell EF Core that `TestEntity.SubEntity` needs the `_subEntityToJsonConverter`
        modelBuilder
            .Entity<TestEntity>()
            .Property(e => e.SubEntity)
            .HasConversion(_subEntityToJsonConverter);

        // Tell EF Core that `TestEntity.Dictionary` needs the `_dictionaryToJsonConverter`
        modelBuilder
            .Entity<TestEntity>()
            .Property(e => e.Dictionary)
            .HasConversion(_dictionaryToJsonConverter);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TestEntity> TestEntities { get; set; }
}

public class TestEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public object Object { get; set; }
    public SubEntity SubEntity { get; set; }
    public Dictionary<string, object> Dictionary { get; set; }
}

public class SubEntity { }
```

That's it, the `TestEntity` should be saved in a single table, serializing three of its properties.
