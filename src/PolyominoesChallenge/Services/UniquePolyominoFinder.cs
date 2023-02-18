using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class UniquePolyominoFinder : IUniquePolyominoFinder
{
    private readonly IShapeEquivalenceComparer _shapeEquivalenceComparer;
    private readonly IShapeManipulator _shapeManipulator;

    public UniquePolyominoFinder(IShapeEquivalenceComparer shapeEquivalenceComparer, IShapeManipulator shapeManipulator)
    {
        _shapeEquivalenceComparer = shapeEquivalenceComparer;
        _shapeManipulator = shapeManipulator;
    }
    
    public Polyomino[] GetUniquePolyominoes(Polyomino[] input, bool allowFlippedShapes = false)
    {
        var output = input.Select(x => _shapeManipulator.GetStandardShapeRotation(x)).ToList();
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
