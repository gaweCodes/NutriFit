using Nutrition.RestApi;

var app = WebApplication.CreateBuilder(args).Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();
