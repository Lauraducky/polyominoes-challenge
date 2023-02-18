using PolyominoesChallenge.Models;
using PolyominoesChallenge.Services;
using PolyominoesChallenge.Tests.Helpers;

namespace PolyominoesChallenge.Tests.Services;

public class PolyominoValidatorTests
{
    private readonly PolyominoValidator _subject;

    private Polyomino? _polyomino;
    private int _size;
    
    private bool _isValid;

    public PolyominoValidatorTests()
    {
        _subject = new PolyominoValidator(new IntegerArrayComparer());
    }

    [Theory]
    [InlineData("110000000011110011100", 7, 9, false)]
    [InlineData("110101001", 3, 5, false)]
    [InlineData("11110110011100", 7, 9, false)]
    public void ItShouldTestIfAPolyominoIsValid(string input, int inputWidth, int size, bool isValid)
    {
        this.Given(x => GivenAPolyomino(input, inputWidth))
            .And(x => GivenASize(size))
            .When(x => WhenTestingIfPolyominoIsValid())
            .Then(x => ThenItShouldReturn(isValid))
            .BDDfy();
    }

    private void GivenAPolyomino(string input, int inputWidth)
    {
        _polyomino = TestHelpers.GeneratePolyomino(input, inputWidth);
    }

    private void GivenASize(int size)
    {
        _size = size;
    }

    private void WhenTestingIfPolyominoIsValid()
    {
        _isValid = _subject.IsValidPolyomino(_polyomino!, _size);
    }

    private void ThenItShouldReturn(bool isValid)
    {
        _isValid.ShouldBe(isValid);
    }
}