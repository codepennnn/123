namespace WorkOrderExemtionApi.Middleware
{
    public class SecretKeyAuthorization
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private const string APIKEY = "ApiKey";
        public SecretKeyAuthorization(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Enter ApiKey");
                return;
            }
            var expectedApiKey = _configuration.GetValue<string>("ApiKey");
            if (string.IsNullOrEmpty(expectedApiKey) || !expectedApiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);
        }
    }

}
---------------------

using WorkOrderExemtionApi.DataAcess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var Config = builder.Configuration;
var connectionString = Config.GetConnectionString("Dbcs");
var environment = builder.Environment;
builder.Services.AddSingleton(new WorkOrderExemptionDataAcess(connectionString));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
