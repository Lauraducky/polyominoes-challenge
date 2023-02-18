using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class UniquePolyominoFinder : IUniquePolyominoFinder
{
    private readonly IShapeEquivalenceComparer _shapeEquivalenceComparer;

    public UniquePolyominoFinder(IShapeEquivalenceComparer shapeEquivalenceComparer)
    {
        _shapeEquivalenceComparer = shapeEquivalenceComparer;
    }
    
    public Polyomino[] GetUniquePolyominoes(Polyomino[] input, bool allowFlippedShapes = false)
    {
        var output = new List<Polyomino>(input);
        var indicesToRemove = new List<int>();
        for (var i = 0; i < output.Count; i++)
        {
            var current = output[i];
            foreach (var polyomino in output.Select((value, index) => new{index, value}).Skip(i + 1))
            {
                if (_shapeEquivalenceComparer.AreShapesEquivalent(current, polyomino.value, allowFlippedShapes))
                {
                    indicesToRemove.Add(polyomino.index);
                }
            }

            for (var j = indicesToRemove.Count - 1; j >= 0; j--)
            {
                output.RemoveAt(indicesToRemove[j]);
            }
            indicesToRemove.Clear();
        }

        return output.ToArray();
    }
}
