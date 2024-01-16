using FluentValidation;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using odata.models;
using odata.repository;
using odata.repository.Repositories;
using odata.Validators.V1;

SqliteConnection? sqliteConnection = null;

var builder = WebApplication.CreateBuilder(args);

if (builder.Configuration.GetValue<bool>("UseSqllite"))
{
    sqliteConnection = new SqliteConnection("DataSource=:memory:");
    sqliteConnection.Open();
}

builder.Services.AddDbContext<EducationContext>(options =>
{
    options.EnableDetailedErrors(true);
    options.EnableSensitiveDataLogging(true);

    if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("MSSQL")))
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
    }
    else if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("Postgres")))
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
    }
    else if (builder.Configuration.GetValue<bool>("UseSqllite"))
    {
        options.UseSqlite(sqliteConnection);
    }
    else
    {
        options.UseInMemoryDatabase("In-Memory-DB");
    }
});

builder.Services.AddControllers(options => options.ModelValidatorProviders.Clear())
    .AddOData((odataOptions, serviceProvider) =>
    {
        odataOptions.AddRouteComponents("api/v1", EdmService.GetEdmModel(), new DefaultODataBatchHandler());

        odataOptions.EnableQueryFeatures(builder.Configuration.GetValue<int>("Limit-Top-MaxValue"));
    });

builder.Services.AddTransient<IRepository<Degree>, Repository<Degree>>();
builder.Services.AddTransient<IRepository<EducationClass>, Repository<EducationClass>>();

builder.Services.AddValidatorsFromAssemblyContaining<DegreeValidator>(includeInternalTypes: true);
builder.Services.AddValidatorsFromAssemblyContaining<EducationClassValidator>(includeInternalTypes: true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EducationContext>();

    await context.Database.EnsureCreatedAsync();

    if (!string.IsNullOrWhiteSpace(app.Configuration.GetConnectionString("MSSQL"))
        || !string.IsNullOrWhiteSpace(app.Configuration.GetConnectionString("Postgres")) 
        || app.Configuration.GetValue<bool>("UseSqllite"))
    {
        await context.Database.MigrateAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseODataRouteDebug();

    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseODataBatching();

app.UseRouting();

//app.UseAuthorization();
//app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.RunAsync();
