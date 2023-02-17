using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ShapeGenerator : IShapeGenerator
{
    private readonly IPartitionService _partitionService;
    private readonly IListPermutator _listPermutator;
    private readonly IUniquePolyominoFinder _uniquePolyominoFinder;
    private readonly IPolyominoValidator _polyominoValidator;

    public ShapeGenerator(IPartitionService partitionService, IListPermutator listPermutator, 
        IUniquePolyominoFinder uniquePolyominoFinder, IPolyominoValidator polyominoValidator)
    {
        _partitionService = partitionService;
        _listPermutator = listPermutator;
        _uniquePolyominoFinder = uniquePolyominoFinder;
        _polyominoValidator = polyominoValidator;
    }
    
    public Polyomino[] GenerateShapes(int size, bool allowFlippedShapes)
    {
        var partitions = _partitionService.GetPartitionsOfNumber(size);
        var allPartitions = new List<int[]>();
        foreach (var partition in partitions)
        {
            allPartitions.AddRange(_listPermutator.GetAllPartitionPermutations(partition));
        }
        
        var shapes = GetAllVariations(allPartitions.ToArray(), size);
        shapes = _polyominoValidator.RemoveInvalidPolyominoes(shapes);
        
        return _uniquePolyominoFinder.GetUniquePolyominoes(shapes.ToArray(), allowFlippedShapes);
    }

    private Polyomino[] GetAllVariations(int[][] input, int size)
    {
        var response = new List<Polyomino>();

        foreach (var partition in input)
        {
            if (partition.Length == 1 || partition.All(x => x == 1))
            {
                // there are no variations on lines
                var width = partition.Max();
                response.Add(new Polyomino(partition.Select(x => GenerateRow(x, width)).ToArray()));
                continue;
            }

            var maxWidth = size - (partition.Length - 1);
            var minWidth = partition.Max();
            for (var i = minWidth; i <= maxWidth; i++)
            {
                response.AddRange(GenerateVariationsOfWidth(partition, i));
            }
        }

        return response.ToArray();
    }

    private Polyomino[] GenerateVariationsOfWidth(int[] partition, int width)
    {
        var rows = new List<PolyominoRow[]>();
        foreach (var row in partition)
        {
            var initialRow = GenerateRow(row, width);
            rows.Add(_listPermutator.GetAllRowPermutations(initialRow));
        }

        var polyominoes = GetAllRowCombinations(rows.ToArray());
        return polyominoes;
    }

    private PolyominoRow GenerateRow(int filledCells, int width)
    {
        var row = new bool[width];
        var i = 0;
        var remaining = filledCells;
        while (remaining > 0)
        {
            row[i] = true;
            i++;
            remaining--;
        }

        return new PolyominoRow(row);
    }

    private Polyomino[] GetAllRowCombinations(PolyominoRow[][] rows)
    {
        var combinations = new List<List<PolyominoRow>>();
        foreach (var row in rows)
        {
            if (combinations.Count == 0)
            {
                combinations.Add(new List<PolyominoRow>());
            }
            
            var current = combinations.ToArray();
            combinations.Clear();
            
            foreach (var rowPermutation in row)
            {
                combinations.AddRange(current.Select(combination => new List<PolyominoRow>(combination)
                    { rowPermutation }));
            }
        }

        return combinations.Select(x => new Polyomino(x.ToArray())).ToArray();
    }
}