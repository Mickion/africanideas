using afi.university.application;
using afi.university.infrastructure;
using afi.university.api;
using afi.university.application.Common.Mappings;
using afi.university.application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Add Custom Services to the container
builder.Logging.AddLogging(builder.Configuration);
builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Add application & infrastructure layers
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSeedInMemoryDb(app.Services.GetRequiredService<IPasswordHasher>());  //seed test data
}

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials()
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
