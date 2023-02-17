using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IListPermutator
{
    int[][] GetAllPartitionPermutations(int[] sequence);
    PolyominoRow[] GetAllRowPermutations(PolyominoRow row);
}