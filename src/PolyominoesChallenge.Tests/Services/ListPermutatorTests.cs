using PolyominoesChallenge.Models;
using PolyominoesChallenge.Services;

namespace PolyominoesChallenge.Tests.Services;

public class ListPermutatorTests
{
    private readonly ListPermutator _subject;

    private int[]? _partition;
    private int[][]? _partitionResult;

    private PolyominoRow? _row;
    private PolyominoRow[]? _rowResult;

    public ListPermutatorTests()
    {
        _subject = new ListPermutator(new IntegerArrayComparer());
    }

    [Theory]
    [InlineData(new[] {1,2,3}, new[] {1,2,3}, new[] {1,3,2}, new[] {2,1,3}, new[] {2,3,1}, new[] {3,1,2}, new[] {3,2,1})]
    [InlineData(new[] {2,1,1}, new[] {2,1,1}, new[] {1,2,1}, new[] {1,1,2})]
    public void ItShouldReturnAllPermutationsOfAPartition(int[] list, params int[][] permutations)
    {
        this.Given(x => GivenAPartition(list))
            .When(x => WhenPermutatingPartition())
            .Then(x => ThenItShouldReturnAllUniquePermutationsOfThePartition(permutations))
            .BDDfy();
    }

    [Theory]
    [InlineData(new[] {true,false,false}, new[] {true,false,false}, new[] {false,true,false}, new[] {false,false,true})]
    [InlineData(new[] {false, true}, new[] {false, true}, new[] {true, false})]
    public void ItShouldReturnAllPermutationsOfARow(bool[] row, params bool[][] permutations)
    {
        this.Given(x => GivenARow(row))
            .When(x => WhenPermutatingARow())
            .Then(x => ThenItShouldReturnAllUniquePermutationsOfTheRow(permutations))
            .BDDfy();
    }

    private void GivenAPartition(int[] list)
    {
        _partition = list;
    }

    private void GivenARow(bool[] row)
    {
        _row = new PolyominoRow(row);
    }

    private void WhenPermutatingPartition()
    {
        _partitionResult = _subject.GetAllPartitionPermutations(_partition!);
    }

    private void WhenPermutatingARow()
    {
        _rowResult = _subject.GetAllRowPermutations(_row!);
    }

    private void ThenItShouldReturnAllUniquePermutationsOfThePartition(int[][] permutations)
    {
        _partitionResult.ShouldNotBeNull();
        _partitionResult.Length.ShouldBe(permutations.Length);
        foreach (var permutation in permutations)
        {
            _partitionResult.Count(x => x.SequenceEqual(permutation)).ShouldBe(1);
        }
    }

    private void ThenItShouldReturnAllUniquePermutationsOfTheRow(bool[][] rows)
    {
        _rowResult.ShouldNotBeNull();
        _rowResult.Length.ShouldBe(rows.Length);
        var expected = rows.Select(x => new PolyominoRow(x));
        foreach (var row in expected)
        {
            _rowResult.Count(x => x.Equals(row)).ShouldBe(1);
        }
    }
}