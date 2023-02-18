using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IShapeManipulator
{
    Polyomino[] GetAllEquivalentShapes(Polyomino input, bool allowFlippedShapes);
    Polyomino FlipShapeHorizontally(Polyomino input);
    Polyomino RotateShapeClockwise(Polyomino input);
}
