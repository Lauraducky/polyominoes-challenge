using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Helpers;

public class ShapeEquivalenceComparer {
    private readonly ShapeManipulator _shapeManipulator;
    private readonly ShapeComparer _shapeComparer;

    public ShapeEquivalenceComparer(ShapeManipulator shapeManipulator, ShapeComparer shapeComparer)
    {
        _shapeManipulator = shapeManipulator;
        _shapeComparer = shapeComparer;
    }

    public bool AreShapesEquivalent(Polyomino a, Polyomino b, bool allowFlippedShapes = false) 
    {
        var standardA = _shapeManipulator.GetStandardShapeRotation(a);
        var standardB = _shapeManipulator.GetStandardShapeRotation(b);

        if (_shapeComparer.AreShapesEqual(standardA, standardB))
        {
            return true;
        }

        if (allowFlippedShapes)
        {
            return false;
        }

        var flippedB = _shapeManipulator.FlipShapeHorizontally(b);
        var standardFlippedB = _shapeManipulator.GetStandardShapeRotation(flippedB);

        return _shapeComparer.AreShapesEqual(standardA, standardFlippedB);
    }
}