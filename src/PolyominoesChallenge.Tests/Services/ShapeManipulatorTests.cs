using PolyominoesChallenge.Models;
using PolyominoesChallenge.Services;
using PolyominoesChallenge.Tests.Helpers;

namespace PolyominoesChallenge.Tests.Services;

public class ShapeManipulatorTests
{
    private readonly ShapeManipulator _subject;

    private Polyomino? _shape;
    private Polyomino? _resultShape;

    public ShapeManipulatorTests()
    {
        _subject = new ShapeManipulator();
    }

    [Theory]
    [InlineData("111100", 3, "110101", 2)]
    [InlineData("1111", 1, "1111", 4)]
    [InlineData("110011", 3, "011110", 2)]
    public void ItShouldRotateAShapeCorrectly(string input, int inputWidth, string output, int outputWidth)
    {
        this.Given(x => GivenAShape(input, inputWidth))
            .When(x => WhenRotatingAShape())
            .Then(x => ThenItShouldReturnAShape(output, outputWidth))
            .BDDfy();
    }

    [Theory]
    [InlineData("111100", 3, "111001", 3)]
    [InlineData("110011", 3, "011110", 3)]
    public void ItShouldFlipAShapeCorrectly(string input, int inputWidth, string output, int outputWidth)
    {
        this.Given(x => GivenAShape(input, inputWidth))
            .When(x => WhenFlippingAShape())
            .Then(x => ThenItShouldReturnAShape(output, outputWidth))
            .BDDfy();
    }

    [Theory]
    [InlineData("110101", 2, "111100", 3)]
    [InlineData("011110", 3, "101101", 2)]
    [InlineData("011110", 2, "110011", 3)]
    [InlineData("1111", 1, "1111", 4)]
    public void ItShouldReturnTheStandardRotation(string input, int inputWidth, string output, int outputWidth)
    {
        this.Given(x => GivenAShape(input, inputWidth))
            .When(x => WhenGettingTheStandardRotation())
            .Then(x => ThenItShouldReturnAShape(output, outputWidth))
            .BDDfy();
    }

    private void GivenAShape(string input, int inputWidth)
    {
        _shape = TestHelpers.GeneratePolyomino(input, inputWidth);
    }

    private void WhenRotatingAShape()
    {
        _resultShape = _subject.RotateShapeClockwise(_shape!);
    }

    private void WhenFlippingAShape()
    {
        _resultShape = _subject.FlipShapeHorizontally(_shape!);
    }

    private void WhenGettingTheStandardRotation()
    {
        _resultShape = _subject.GetStandardShapeRotation(_shape!);
    }

    private void ThenItShouldReturnAShape(string output, int outputWidth)
    {
        _resultShape.ShouldNotBeNull();
        var expectedPolyomino = TestHelpers.GeneratePolyomino(output, outputWidth);
        _resultShape.Rows.Length.ShouldBe(expectedPolyomino.Rows.Length);

        for (var i = 0; i < _resultShape.Rows.Length; i++)
        {
            _resultShape.Rows[i].Columns.ShouldBe(expectedPolyomino.Rows[i].Columns);
        }
    }
}