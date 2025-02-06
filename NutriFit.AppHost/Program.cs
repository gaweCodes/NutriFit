var builder = DistributedApplication.CreateBuilder(args);

var postgresDbServer = builder.AddPostgres("postgres")
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);
var nutritionReadDatabase = postgresDbServer.AddDatabase("nutrition-read", "nutrition-read");
var nutritionWriteDatabase = postgresDbServer.AddDatabase("nutrition-write", "nutrition-write");

var migrator = builder.AddProject<Projects.NutriFit_MigrationService>("nutrifit-migration-service")
    .WithReference(nutritionReadDatabase)
    .WithReference(nutritionWriteDatabase)
    .WaitFor(nutritionReadDatabase)
    .WaitFor(nutritionWriteDatabase)
    .WaitFor(postgresDbServer);

var nutritionApi = builder.AddProject<Projects.Nutrition_RestApi>("nutrition-rest-api")
    .WithReference(nutritionReadDatabase)
    .WithReference(nutritionWriteDatabase)
    .WaitForCompletion(migrator);

builder.AddProject<Projects.NutriFit_Web_Angular_BackendForFrontend>("nutrifit-web-angular-backendforfrontend")
    .WithReference(nutritionApi)
    .WaitFor(nutritionApi);

builder.Build().Run();
