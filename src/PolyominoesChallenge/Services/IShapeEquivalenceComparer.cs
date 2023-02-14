using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IShapeEquivalenceComparer
{
    bool AreShapesEquivalent(Polyomino a, Polyomino b, bool allowFlippedShapes = false);
}