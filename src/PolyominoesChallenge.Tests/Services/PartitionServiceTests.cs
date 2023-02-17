using PolyominoesChallenge.Services;

namespace PolyominoesChallenge.Tests.Services;

public class PartitionServiceTests
{
    private readonly PartitionService _subject;

    private int _integer;
    private int[][]? _result;

    public PartitionServiceTests()
    {
        _subject = new PartitionService();
    }
    
    [Theory]
    [InlineData(4, new[]{4}, new[]{3,1}, new[]{2,2}, new[]{2,1,1}, new[]{1,1,1,1})]
    [InlineData(5, new[]{5}, new[]{4,1}, new[]{3,2}, new[]{3,1,1}, new[]{2,2,1}, new[]{2,1,1,1}, new[]{1,1,1,1,1})]
    public void ItShouldFindAllUniquePartitionsOfAnInteger(int integer, params int[][] partitions)
    {
        this.Given(x => GivenAnInteger(integer))
            .When(x => WhenFindingPartitions())
            .Then(x => ThenItShouldReturnAllUniquePartitions(partitions))
            .BDDfy();
    }

    private void GivenAnInteger(int integer)
    {
        _integer = integer;
    }

    private void WhenFindingPartitions()
    {
        _result = _subject.GetPartitionsOfNumber(_integer);
    }

    private void ThenItShouldReturnAllUniquePartitions(int[][] partitions)
    {
        _result.ShouldNotBeNull();
        _result.Length.ShouldBe(partitions.Length);
        for (var i = 0; i < _result.Length; i++)
        {
            _result[i].ShouldBeEquivalentTo(partitions[i]);
        }
    }
}