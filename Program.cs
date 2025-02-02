using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using to_do_list_api.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

// builder.Services.AddMvcCore().AddJsonOptions(
//     options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
// );
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
});

// Add CORS services
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowLocalhost",
//         policy =>
//         {
//             policy.WithOrigins("http://127.0.0.1:5500") // Add your frontend origin here
//                   .AllowAnyHeader()                     // Allow any headers (e.g., Content-Type, Authorization)
//                   .AllowAnyMethod();                    // Allow any HTTP methods (GET, POST, PUT, etc.)
//         });
// });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();