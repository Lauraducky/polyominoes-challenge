using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ShapeEquivalenceComparer : IShapeEquivalenceComparer
{
    private readonly IShapeManipulator _shapeManipulator;

    public ShapeEquivalenceComparer(IShapeManipulator shapeManipulator)
    {
        _shapeManipulator = shapeManipulator;
    }

    public bool AreShapesEquivalent(Polyomino a, Polyomino b, bool allowFlippedShapes = false) 
    {
        if (CompareShapeRotations(a, b))
        {
            return true;
        }

        if (allowFlippedShapes)
        {
            return false;
        }

        var flippedB = _shapeManipulator.FlipShapeHorizontally(b);

        return CompareShapeRotations(a, flippedB);
    }

    public bool CompareShapeRotations(Polyomino a, Polyomino b)
    {
        var aRotations = _shapeManipulator.GetAllShapeRotations(a);
        var bRotations = _shapeManipulator.GetAllShapeRotations(b);

        return aRotations.Any(x => bRotations.Contains(x));
    }
}