using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class UniquePolyominoFinder
{
    private readonly IShapeEquivalenceComparer _shapeEquivalenceComparer;

    public UniquePolyominoFinder(IShapeEquivalenceComparer shapeEquivalenceComparer)
    {
        _shapeEquivalenceComparer = shapeEquivalenceComparer;
    }
    
    public Polyomino[] GetUniquePolyominoes(Polyomino[] input)
    {
        var output = new List<Polyomino>(input);
        for (var i = output.Count - 1; i > 0; i--)
        {
            var current = output[i];
            if (output.Take(i).Any(x => _shapeEquivalenceComparer.AreShapesEquivalent(x, current)))
            {
                output.RemoveAt(i);
            }
        }

        return output.ToArray();
    }
}