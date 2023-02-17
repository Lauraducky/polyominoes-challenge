using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ShapeGenerator
{
    private readonly IPartitionService _partitionService;
    private readonly IListPermutator _listPermutator;
    private readonly IUniquePolyominoFinder _uniquePolyominoFinder;

    public ShapeGenerator(IPartitionService partitionService, IListPermutator listPermutator, IUniquePolyominoFinder uniquePolyominoFinder)
    {
        _partitionService = partitionService;
        _listPermutator = listPermutator;
        _uniquePolyominoFinder = uniquePolyominoFinder;
    }
    
    public Polyomino[] GenerateShapes(int size, bool allowFlippedShapes)
    {
        var partitions = _partitionService.GetPartitionsOfNumber(size);
        var allPartitions = new List<int[]>();
        foreach (var partition in partitions)
        {
            allPartitions.AddRange(_listPermutator.GetAllListPermutations(partition));
        }
        
        var shapes = allPartitions.Select(PartitionToPolyomino).ToList();
        shapes.AddRange(GetLegalVariations(partitions));
        return _uniquePolyominoFinder.GetUniquePolyominoes(shapes.ToArray(), allowFlippedShapes);
    }

    private Polyomino PartitionToPolyomino(int[] partition)
    {
        var width = partition[0];
        var rows = new List<PolyominoRow>();
        foreach (var number in partition)
        {
            var currentRow = new bool[width];
            var i = 0;
            var remaining = number;
            while (remaining > 0)
            {
                currentRow[i] = true;
                i++;
                remaining--;
            }
            rows.Add(new PolyominoRow(currentRow));
        }

        return new Polyomino(rows.ToArray());
    }

    private Polyomino[] GetLegalVariations(int[][] input)
    {
        var response = new List<Polyomino>();

        foreach (var partition in input)
        {
            if (partition.Length == 1 || partition.All(x => x == 1))
            {
                // there are no variations on lines
                continue;
            }

            // todo vary up each partition
        }

        return response.ToArray();
    }
}