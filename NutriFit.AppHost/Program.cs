var builder = DistributedApplication.CreateBuilder(args);

var postgresDbServer = builder.AddPostgres("postgres")
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);
var nutritionReadDatabase = postgresDbServer.AddDatabase("nutrition-read", "nutrition-read");
var nutritionEventDb = postgresDbServer.AddDatabase("nutrition-events", "nutrition-events");

var migrator = builder.AddProject<Projects.NutriFit_MigrationService>("nutrifit-migration-service")
    .WithReference(nutritionReadDatabase)
    .WithReference(nutritionEventDb)
    .WaitFor(nutritionReadDatabase)
    .WaitFor(nutritionEventDb)
    .WaitFor(postgresDbServer);

var nutritionApi = builder.AddProject<Projects.Nutrition_Api>("nutrition-api")
    .WithReference(nutritionReadDatabase)
    .WithReference(nutritionEventDb)
    .WaitForCompletion(migrator);

builder.AddProject<Projects.NutriFit_Web_Angular_BackendForFrontend>("nutrifit-web-angular-backendforfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(nutritionApi)
    .WaitFor(nutritionApi);

await builder.Build().RunAsync();
