var builder = DistributedApplication.CreateBuilder(args);

var eventStore = builder.AddEventStore("event-store")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);
var postgresDbServer = builder.AddPostgres("postgres")
    .WithPgWeb()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);
var nutritionReadDatabase = postgresDbServer.AddDatabase("nutrition-read", "nutrition-read");

var migrator = builder.AddProject<Projects.NutriFit_MigrationService>("nutrifit-migration-service")
    .WithReference(nutritionReadDatabase)
    .WaitFor(nutritionReadDatabase)
    .WaitFor(postgresDbServer);

var nutritionApi = builder.AddProject<Projects.Nutrition_Api>("nutrition-api")
    .WithReference(eventStore)
    .WithReference(nutritionReadDatabase)
    .WaitFor(eventStore)
    .WaitForCompletion(migrator);

builder.AddProject<Projects.NutriFit_Web_Angular_BackendForFrontend>("nutrifit-web-angular-backendforfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(nutritionApi)
    .WaitFor(nutritionApi);

await builder.Build().RunAsync();
