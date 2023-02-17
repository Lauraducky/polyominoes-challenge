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

        if (!standardA.Rows[0].Columns[0] || !standardB.Rows[0].Columns[0])
        {
            if (CompareShapeRotations(a, b))
            {
                return true;
            }
        }
        else if (standardA.Equals(standardB))
        {
            return true;
        }

        if (allowFlippedShapes)
        {
            return false;
        }

        var flippedB = _shapeManipulator.FlipShapeHorizontally(b);
        var standardFlippedB = _shapeManipulator.GetStandardShapeRotation(flippedB);
        
        if (!standardA.Rows[0].Columns[0] || !standardFlippedB.Rows[0].Columns[0])
        {
            return CompareShapeRotations(standardA, standardFlippedB);
        }

        return standardA.Equals(standardFlippedB);
    }

    public bool CompareShapeRotations(Polyomino a, Polyomino b)
    {
        var aRotations = _shapeManipulator.GetAllShapeRotations(a);
        var bRotations = _shapeManipulator.GetAllShapeRotations(b);

        return aRotations.Any(x => bRotations.Contains(x));
    }
}