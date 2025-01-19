WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(static x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
});

WebApplication app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
