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
        var output = new List<Polyomino>(input);
        for (var i = output.Count - 1; i > 0; i--)
        {
            var current = output[i];
            if (output.Take(i).Any(x => _shapeEquivalenceComparer.AreShapesEquivalent(x, current, allowFlippedShapes)))
            {
                output.RemoveAt(i);
            }
        }

        return output.ToArray();
    }
}
