using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class UniquePolyominoFinder : IUniquePolyominoFinder
{
    private readonly IShapeEquivalenceComparer _shapeEquivalenceComparer;

    public UniquePolyominoFinder(IShapeEquivalenceComparer shapeEquivalenceComparer)
    {
        _shapeEquivalenceComparer = shapeEquivalenceComparer;
    }
    
    public Polyomino[] GetUniquePolyominoes(Polyomino[] input, bool allowFlippedShapes = false)
    {
        var output = new List<Polyomino>();
        foreach (var polyomino in input)
        {
            if (!output.Any(x => _shapeEquivalenceComparer.AreShapesEquivalent(x, polyomino)))
            {
                output.Add(polyomino);
            }
        }

        return output.ToArray();
    }
}
