using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolyominoesChallenge.Services;

namespace PolyominoesChallenge.Endpoint;

internal class Startup
{
    IConfigurationRoot Configuration { get; }

    public Startup()
    {
        var builder = new ConfigurationBuilder();
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IShapeManipulator, ShapeManipulator>();
        services.AddTransient<IShapeEquivalenceComparer, ShapeEquivalenceComparer>();
        services.AddTransient<IPartitionService, PartitionService>();
        services.AddTransient<IListPermutator, ListPermutator>();
        services.AddTransient<IEqualityComparer<int[]>, IntegerArrayComparer>();
        services.AddTransient<IPolyominoValidator, PolyominoValidator>();
        services.AddTransient<IShapeGenerator, ShapeGenerator>();
        services.AddTransient<IPolyominoPrinter, PolyominoPrinter>();
    }
}