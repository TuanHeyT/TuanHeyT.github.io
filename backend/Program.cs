using Microsoft.EntityFrameworkCore;

string Example07JSDomain = "_Example07JSDomain";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddDbContext<Example07Context>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceConnectString"));
});
builder.Services.AddCors(options => {
    options.AddPolicy(
        name: Example07JSDomain,
        policy => policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()
    );
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthorizationBuilder();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(Example07JSDomain);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
