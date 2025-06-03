var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("mastery-db");

builder.AddProject<Projects.Mastery_Api>("mastery-api")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.Build().Run();