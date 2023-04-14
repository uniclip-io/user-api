using Bugsnag;
using UserApi.Middlewares;
using UserApi.Repositories;
using UserApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IClient>(_ => new Client(builder.Configuration["ApiKeys:Bugsnag"]));
builder.Services.AddSingleton(_ => new UserRepository(builder.Configuration["ConnectionStrings:MongoDb"]!));
builder.Services.AddSingleton(_ => new SettingsRepository(builder.Configuration["ConnectionStrings:MongoDb"]!));
builder.Services.AddScoped<UserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HttpExceptionMiddleware>();
app.UseMiddleware<JwtAuthorizationMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();