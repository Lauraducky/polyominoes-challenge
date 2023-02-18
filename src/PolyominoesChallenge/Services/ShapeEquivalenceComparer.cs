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
        var equivalentShapes = _shapeManipulator.GetAllEquivalentShapes(a, allowFlippedShapes);
        return equivalentShapes.Any(x => x.Equals(b));
    }
}