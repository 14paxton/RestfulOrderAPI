using RestfulOrderAPI.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<OrderContext>("Data Source=RestfulOrderAPI.db");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.CreateDbIfNotExists();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();