using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IShapeGenerator
{
    Polyomino[] GenerateShapes(int size, bool allowFlippedShapes);
}