using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IShapeManipulator
{
    Polyomino FlipShapeHorizontally(Polyomino input);
    Polyomino[] GetAllShapeRotations(Polyomino input);
    Polyomino RotateShapeClockwise(Polyomino input);
}
