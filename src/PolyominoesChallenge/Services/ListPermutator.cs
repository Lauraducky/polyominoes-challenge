using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ListPermutator : IListPermutator
{
    private readonly IEqualityComparer<int[]> _integerArrayComparer;

    private static readonly Dictionary<PolyominoRow, PolyominoRow[]> AllRowPermutations = new();

    public ListPermutator(IEqualityComparer<int[]> integerArrayComparer)
    {
        _integerArrayComparer = integerArrayComparer;
    }
    
    public int[][] GetAllPartitionPermutations(int[] sequence)
    {
        var list = new List<int>(sequence);
        var permutations = Permutate(list, sequence.Length).Select(x => x.ToArray()).Distinct(_integerArrayComparer);
        return permutations.ToArray();
    }

    public PolyominoRow[] GetAllRowPermutations(PolyominoRow row)
    {
        if (AllRowPermutations.ContainsKey(row))
        {
            return AllRowPermutations[row];
        }

        var list = new List<bool>(row.Columns);
        var permutations = Permutate(list, list.Count)
            .Select(x => new PolyominoRow(x.ToArray()))
            .Distinct()
            .ToArray();
        
        AllRowPermutations[row] = permutations;
        return permutations;
    }

    private IEnumerable<IList<T>> Permutate<T>(IList<T> sequence, int count)
    {
        if (count == 1)
        {
            yield return sequence;
        }
        else
        {
            for (var i = 0; i < count; i++)
            {
                foreach (var permutation in Permutate(sequence, count - 1))
                {
                    yield return permutation;
                }

                RotateRight(sequence, count);
            }
        }
    }

    private void RotateRight<T>(IList<T> sequence, int count)
    {
        var tmp = sequence[count - 1];
        sequence.RemoveAt(count - 1);
        sequence.Insert(0, tmp);
    }
}