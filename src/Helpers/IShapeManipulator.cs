using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Helpers;

public interface IShapeManipulator
{
    Polyomino FlipShapeHorizontally(Polyomino input);
    Polyomino GetStandardShapeRotation(Polyomino input);
    Polyomino RotateShapeClockwise(Polyomino input);
}
