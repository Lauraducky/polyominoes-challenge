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

if (Environment.GetCommandLineArgs().Length > 3)
{
    var filePath = Environment.GetCommandLineArgs()[3];
    var polyominoPrinter = serviceProvider.GetRequiredService<IPolyominoPrinter>();
    polyominoPrinter.SavePolyominoesToFile(shapes, filePath);
}
else
{
    for (var i = 0; i < shapes.Length; i++)
    {
        Console.WriteLine("Shape " + i);
        Console.WriteLine(shapes[i].ToString());
    }
}