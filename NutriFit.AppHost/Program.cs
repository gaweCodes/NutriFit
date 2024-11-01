var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var postgresDbServer = builder.AddPostgres("postgres")
    .WithPgWeb();
var nutriFitWriteDatabase = postgresDbServer.AddDatabase("nutriFitWrite", "NutriFitWrite");
var nutriFitReadDatabase = postgresDbServer.AddDatabase("nutriFitRead", "NutriFitRead");
var apiService = builder.AddProject<Projects.NutriFit_ApiService>("apiservice")
    .WithReference(nutriFitWriteDatabase)
    .WithReference(nutriFitReadDatabase);

var webBff = builder.AddProject<Projects.NutriFit_Web_BackendForFrontend>("webBff")
    .WithReference(apiService);

builder.AddProject<Projects.NutriFit_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(webBff);

builder.AddProject<Projects.NutriFit_MigrationsService>("migrationservice")
    .WithReference(nutriFitWriteDatabase)
    .WithReference(nutriFitReadDatabase);

builder.Build().Run();
