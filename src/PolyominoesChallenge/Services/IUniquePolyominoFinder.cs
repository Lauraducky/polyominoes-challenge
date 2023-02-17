using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IUniquePolyominoFinder
{
    Polyomino[] GetUniquePolyominoes(Polyomino[] input, bool allowFlippedShapes = false);
}