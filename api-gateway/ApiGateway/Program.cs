using ApiGateway.Configs;
using ApiGateway.Controllers.Endpoint.v1;
using ApiGateway.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<ApiGatewayMiddleware>();
builder.Services.AddHttpClient();

// DI Application layer
builder.Services.AddRouteFeature(); // Add the RouteFeature service
builder.Services.AddRouterProvider(); // Add the RouterProvider service

// DI Infrastructure layer
builder.Services.AddEFRepository(); // Add the EFRepository service
builder.Services.AddEFDB(builder.Configuration);

// Helper
builder.Services.AddSwagger();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// ExceptionHandlingMiddleware nên được đặt đầu tiên
// để bắt tất cả các exception từ các middleware khác
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Minimal API V1");
        c.RoutePrefix = string.Empty; // Optional: Serve Swagger UI at the root URL
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiGatewayMiddleware>();

// Pipeline
app.MapGatewayEndpoints();

app.Run();