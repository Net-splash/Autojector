using Autojector;
using Autojector.Abstractions;
using Sample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutojector();
var app = builder.Build();

app.MapGet("/simple-injected-service", (ISimpleInjectedService simpleInjectedService) =>
{
    return simpleInjectedService.GetData();
});

app.MapGet("/simple-injected-by-attribute", (ISimpleInjectedByAttribute simpleInjectedByAttribute) =>
{
    return simpleInjectedByAttribute.GetData();
});

app.MapGet("/factory-injected-service1", (IFactoryInjectedService1 factoryInjectedService1) =>
{
    return factoryInjectedService1.GetData();
});

app.MapGet("/factory-injected-service2", (IFactoryInjectedService2 factoryInjectedService2) =>
{
    return factoryInjectedService2.GetData();
});

app.MapGet("/simple-injected-decorated-by-attribute", (ISimpleInjectedDecoratedByAttributeService simpleInjectedDecoratedByAttributeService) =>
{
    return simpleInjectedDecoratedByAttributeService.GetData();
});

app.MapGet("/async-factory-injected-service", async (IAsyncDependency<IAsyncFactoryInjectedService> asyncFactoryInjectedServiceDependency) =>
{
    var asyncFactoryInjectedService = await asyncFactoryInjectedServiceDependency.ServiceAsync;
    return asyncFactoryInjectedService.GetData();
});

app.MapGet("/decorated-simple-injected-service", (ISimpleInjectedDecoratedService simpleInjectedDecoratedService) =>
{
    return simpleInjectedDecoratedService.GetData();
});

app.MapGet("/config-service", (MyConfig myConfig) =>
{
    return myConfig.Data;
});

app.MapGet("/unimplemented-configs", (IUnimplmentedConfig myConfig) =>
{
    return myConfig.Data;
});

app.MapGet("/chains-services", (IChain<MyRequest, MyResponse> chain) =>
 {
     var response = chain.Handle(new MyRequest());
     return response.Data;
 });

app.Run();
