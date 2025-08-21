using ToDoList.Application;
using ToDoList.Infrastructure;
using ToDoList.Web.Api;
using ToDoList.Web.Api.Extensions;
using ToDoList.Web.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // ✅ Permite cualquier origen (frontend en cualquier puerto)
              .AllowAnyMethod()    // ✅ GET, POST, PUT, DELETE, etc.
              .AllowAnyHeader();   // ✅ Cualquier encabezado (Content-Type, Authorization, etc.)
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddPresentation(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.CreateInitialUser();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GloblalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();