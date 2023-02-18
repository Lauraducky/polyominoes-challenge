using System.Diagnostics;
using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ShapeGenerator : IShapeGenerator
{
    private readonly IPartitionService _partitionService;
    private readonly IListPermutator _listPermutator;
    private readonly IPolyominoValidator _polyominoValidator;
    private readonly IShapeEquivalenceComparer _shapeEquivalenceComparer;

    public ShapeGenerator(IPartitionService partitionService, IListPermutator listPermutator, 
        IPolyominoValidator polyominoValidator, IShapeEquivalenceComparer shapeEquivalenceComparer)
    {
        _partitionService = partitionService;
        _listPermutator = listPermutator;
        _polyominoValidator = polyominoValidator;
        _shapeEquivalenceComparer = shapeEquivalenceComparer;
    }
    
    public Polyomino[] GenerateShapes(int size, bool allowFlippedShapes)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var partitions = _partitionService.GetPartitionsOfNumber(size);
        Console.WriteLine($"{stopwatch.Elapsed} passed while generating {partitions.Length} partitions.");
        stopwatch.Reset();
        stopwatch.Start();
        var allPartitions = new List<int[]>();
        foreach (var partition in partitions)
        {
            allPartitions.AddRange(_listPermutator.GetAllPartitionPermutations(partition));
        }
        Console.WriteLine($"{stopwatch.Elapsed} passed while generating {allPartitions.Count} partition permutations.");
        stopwatch.Reset();
        stopwatch.Start();
        var shapes = GetAllVariations(allPartitions, size, allowFlippedShapes).ToArray();
        Console.WriteLine($"{stopwatch.Elapsed} passed while generating {shapes.Length} shapes.");
        var totalShapes = shapes.Length;
        
        return shapes;
    }

    private IEnumerable<Polyomino> GetAllVariations(IEnumerable<int[]> input, int size, bool allowFlippedShapes = false)
    {
        var response = new List<Polyomino>();
        var inputIndex = 0;
        foreach (var partition in input.Where(x => !x.All(y => y == 1)))
        {
            if (partition.Length == 1)
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
                response.AddRange(GenerateVariationsOfWidth(partition, i)
                    .Where(x => _polyominoValidator.IsValidPolyomino(x, size) && 
                        !response.Any(y => _shapeEquivalenceComparer.AreShapesEquivalent(x, y, allowFlippedShapes))));
            }
            if (inputIndex % 50 == 0){
                Console.WriteLine($"On loop {inputIndex}, generated {response.Count} shapes so far.");
            }
            inputIndex++;
        }

        return response;
    }

    private IEnumerable<Polyomino> GenerateVariationsOfWidth(int[] partition, int width)
    {
        var rows = new List<PolyominoRow[]>();
        foreach (var row in partition)
        {
            var initialRow = GenerateRow(row, width);
            rows.Add(_listPermutator.GetAllRowPermutations(initialRow));
        }

        var polyominoes = GetAllRowCombinations(rows);
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

    private IEnumerable<Polyomino> GetAllRowCombinations(IEnumerable<IEnumerable<PolyominoRow>> rows)
    {
        var combinations = new List<List<PolyominoRow>> { new() };
        foreach (var row in rows)
        {
            var current = new List<List<PolyominoRow>>(combinations);
            combinations.Clear();
            
            foreach (var rowPermutation in row)
            {
                combinations.AddRange(current.Select(combination => new List<PolyominoRow>(combination)
                    { rowPermutation }));
            }
        }

        return combinations.Select(x => new Polyomino(x.ToArray()));
    }
}