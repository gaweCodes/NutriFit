var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.NutriFit_Web_Angular_BackendForFrontend>("nutrifit-web-angular-backendforfrontend");
builder.Build().Run();
