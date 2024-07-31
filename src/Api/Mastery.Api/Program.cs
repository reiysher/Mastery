WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication([Mastery.Modules.Career.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddCareerModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapCareerModuleEndpoints();

await app.RunAsync();
