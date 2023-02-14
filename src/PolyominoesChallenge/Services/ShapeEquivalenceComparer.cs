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
        var standardA = _shapeManipulator.GetStandardShapeRotation(a);
        var standardB = _shapeManipulator.GetStandardShapeRotation(b);

        if (standardA.Equals(standardB))
        {
            return true;
        }

        if (allowFlippedShapes)
        {
            return false;
        }

        var flippedB = _shapeManipulator.FlipShapeHorizontally(b);
        var standardFlippedB = _shapeManipulator.GetStandardShapeRotation(flippedB);

        return standardA.Equals(standardFlippedB);
    }
}