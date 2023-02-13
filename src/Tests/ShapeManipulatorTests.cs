using System.Diagnostics;
using PolyominoesChallenge.Helpers;
using PolyominoesChallenge.Models;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace PolyominoesChallenge.Tests;

public class ShapeManipulatorTests
{
    private readonly ShapeManipulator _subject;

    private Polyomino? _shape;
    private Polyomino? _resultShape;

    public ShapeManipulatorTests()
    {
        _subject = new ShapeManipulator(new ShapeComparer());
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
    public void ItShouldReturnTheStandardRotation(string input, int inputWidth, string output, int outputWidth)
    {
        this.Given(x => GivenAShape(input, inputWidth))
            .When(x => WhenGettingTheStandardRotation())
            .Then(x => ThenItShouldReturnAShape(output, outputWidth))
            .BDDfy();
    }

    private void GivenAShape(string input, int inputWidth)
    {
        _shape = GeneratePolyomino(input, inputWidth);
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
        var expectedPolyomino = GeneratePolyomino(output, outputWidth);
        _resultShape.Rows.Length.ShouldBe(expectedPolyomino.Rows.Length);

        for (var i = 0; i < _resultShape.Rows.Length; i++)
        {
            _resultShape.Rows[i].Columns.ShouldBe(expectedPolyomino.Rows[i].Columns);
        }
    }

    private Polyomino GeneratePolyomino(string input, int inputWidth)
    {
        var rows = Enumerable.Range(0, input.Length / inputWidth)
            .Select(i => input.Substring(i * inputWidth, inputWidth))
            .Select(x => new PolyominoRow(Enumerable.Range(0, inputWidth).Select(y => x[y] == '1').ToArray()))
            .ToArray();
        return new Polyomino(rows);
    }
}