using PolyominoesChallenge.Services;

namespace PolyominoesChallenge.Tests.Services;

public class ListPermutatorTests
{
    private readonly ListPermutator _subject;

    private int[]? _input;
    private int[][]? _result;

    public ListPermutatorTests()
    {
        _subject = new ListPermutator(new IntegerArrayComparer());
    }

    [Theory]
    [InlineData(new[] {1,2,3}, new[] {1,2,3}, new[] {1,3,2}, new[] {2,1,3}, new[] {2,3,1}, new[] {3,1,2}, new[] {3,2,1})]
    [InlineData(new[] {2,1,1}, new[] {2,1,1}, new[] {1,2,1}, new[] {1,1,2})]
    public void ItShouldReturnAllPermutationsOfAList(int[] list, params int[][] permutations)
    {
        this.Given(x => GivenAList(list))
            .When(x => WhenPermutatingList())
            .Then(x => ThenItShouldReturnAllUniquePermutationsOfTheList(permutations))
            .BDDfy();
    }

    private void GivenAList(int[] list)
    {
        _input = list;
    }

    private void WhenPermutatingList()
    {
        _result = _subject.GetAllListPermutations(_input!);
    }

    private void ThenItShouldReturnAllUniquePermutationsOfTheList(int[][] permutations)
    {
        _result.ShouldNotBeNull();
        _result.Length.ShouldBe(permutations.Length);
        foreach (var permutation in permutations)
        {
            _result.Count(x => x.SequenceEqual(permutation)).ShouldBe(1);
        }
    }
}