namespace PolyominoesChallenge.Services;

public class ListPermutator : IListPermutator
{
    private readonly IEqualityComparer<int[]> _integerArrayComparer;

    public ListPermutator(IEqualityComparer<int[]> integerArrayComparer)
    {
        _integerArrayComparer = integerArrayComparer;
    }
    
    public int[][] GetAllListPermutations(int[] sequence)
    {
        var list = new List<int>(sequence);
        var permutations = Permutate(list, sequence.Length).Select(x => x.ToArray()).Distinct(_integerArrayComparer);
        return permutations.ToArray();
    }

    private IEnumerable<IList<int>> Permutate(IList<int> sequence, int count)
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

    private void RotateRight(IList<int> sequence, int count)
    {
        var tmp = sequence[count - 1];
        sequence.RemoveAt(count - 1);
        sequence.Insert(0, tmp);
    }
}