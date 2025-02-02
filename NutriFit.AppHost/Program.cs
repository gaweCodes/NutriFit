var builder = DistributedApplication.CreateBuilder(args);

var postgresDbServer = builder.AddPostgres("postgres")
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);
var nutriFitReadDatabase = postgresDbServer.AddDatabase("nutrifit-read", "nutrifit-read");
var nutriFitWriteDatabase = postgresDbServer.AddDatabase("nutrifit-write", "nutriFit-write");

var migrator = builder.AddProject<Projects.NutriFit_MigrationService>("nutrifit-migration-service")
    .WithReference(nutriFitReadDatabase)
    .WithReference(nutriFitWriteDatabase)
    .WaitFor(nutriFitReadDatabase)
    .WaitFor(nutriFitWriteDatabase)
    .WaitFor(postgresDbServer); 

var nutriFitCore = builder.AddProject<Projects.NutriFit_Core>("nutrifit-core")
    .WithReference(nutriFitReadDatabase)
    .WithReference(nutriFitWriteDatabase)
    .WaitForCompletion(migrator);

builder.AddProject<Projects.NutriFit_Web_Angular_BackendForFrontend>("nutrifit-web-angular-backendforfrontend")
    .WaitFor(nutriFitCore);

builder.Build().Run();
