using Bteste.Data;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(Options=>
    {Options.SuppressModelStateInvalidFilter = true;});
    
builder.Services.AddDbContext<BlogDataContext>();

var app = builder.Build();
app.MapControllers();

app.Run();
