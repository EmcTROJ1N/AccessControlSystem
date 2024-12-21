using API;
using API.Middlewares;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProgramDependencies(builder.Configuration);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DbAccessSystemContext>();
    dbContext.Database.Migrate();
}

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
