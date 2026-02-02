using ISKI.IBKS.Infrastructure;
using ISKI.IBKS.Persistence;
using ISKI.IBKS.WebAPI.Services;
using Wolverine;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(ISKI.IBKS.Application.Common.Features.Operations.StartSample.StartSampleCommand).Assembly);

    opts.Discovery.IncludeType<ISKI.IBKS.Application.Common.Features.Operations.StartSample.StartSampleHandler>();
    opts.Discovery.IncludeType<ISKI.IBKS.Application.Common.Features.Operations.StartSample.TriggerPlcSampleHandler>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapWolverineEndpoints();

app.MapGrpcService<StationStreamService>();

app.MapControllers();

app.Run();
