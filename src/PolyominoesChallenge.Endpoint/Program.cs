using Microsoft.Extensions.DependencyInjection;
using PolyominoesChallenge.Endpoint;
using PolyominoesChallenge.Services;

var size = int.Parse(Environment.GetCommandLineArgs()[1]);
var allowFlippedShapes = bool.Parse(Environment.GetCommandLineArgs()[2]);

IServiceCollection services = new ServiceCollection();
var startup = new Startup();
startup.ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();

var shapeGenerator = serviceProvider.GetRequiredService<IShapeGenerator>();
var shapes = shapeGenerator.GenerateShapes(size, allowFlippedShapes);
Console.WriteLine("done");