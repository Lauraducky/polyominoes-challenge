using PolyominoesChallenge.Models;
using PolyominoesChallenge.Services;
using PolyominoesChallenge.Tests.Helpers;

namespace PolyominoesChallenge.Tests.Services;

public class ShapeManipulatorTests
{
    private readonly ShapeManipulator _subject;

    private Polyomino? _shape;
    private Polyomino? _resultShape;
    private Polyomino[]? _rotations;

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
    [InlineData("1111", 1, 2)]
    [InlineData("011110", 2, 2)]
    [InlineData("010111010", 3, 1)]
    [InlineData("111010", 2, 4)]
    public void ItShouldReturnAllShapeRotations(string input, int inputWidth, int numRotations)
    {
        this.Given(x => GivenAShape(input, inputWidth))
            .When(x => WhenGettingAllShapeRotations())
            .Then(x => ThenItShouldReturnRotations(numRotations))
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

    private void WhenGettingAllShapeRotations()
    {
        _rotations = _subject.GetAllShapeRotations(_shape!);
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

    private void ThenItShouldReturnRotations(int numRotations)
    {
        _rotations.ShouldNotBeNull();
        _rotations.Length.ShouldBe(numRotations);
    }
}