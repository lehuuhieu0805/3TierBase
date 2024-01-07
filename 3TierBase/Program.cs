using _3TierBase.API.Configurations;
using _3TierBase.API.Middleware.ErrorHandling;
using _3TierBase.Business;
using _3TierBase.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod();
    });
});

// add swagger config
builder.Services.RegisterSwaggerModule();
builder.Services.RegisterData();
builder.Services.RegisterBusiness();
// add authentication config
builder.Services.RegisterSecurityModule(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors();
app.UseApplicationSwagger();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
