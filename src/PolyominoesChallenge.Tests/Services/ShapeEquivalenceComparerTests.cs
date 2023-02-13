using PolyominoesChallenge.Models;
using PolyominoesChallenge.Services;
using PolyominoesChallenge.Tests.Helpers;

namespace PolyominoesChallenge.Tests.Services;

public class ShapeEquivalenceComparerTests
{
    private readonly IShapeManipulator _shapeManipulator;

    private readonly ShapeEquivalenceComparer _subject;

    private Polyomino? _a;
    private Polyomino? _b;
    private bool _allowFlippedShapes;
    private bool? _result;

    public ShapeEquivalenceComparerTests()
    {
        _shapeManipulator = Substitute.For<IShapeManipulator>();
        _subject = new ShapeEquivalenceComparer(_shapeManipulator);
    }

    [Theory]
    // rotated shapes
    [InlineData("111010", 2, "100111", 3, false, "111001", 3, "111001", 3, "001111", 3, "111100", 3, true)]
    // flipped shapes
    [InlineData("011110", 2, "011110", 3, false, "110011", 3, "101101", 2, "110011", 3, "110011", 3, true)]
    // not equivalent
    // [InlineData("011110", 2, "110011", 3)]
    // not equivalent flipped allowed
    public void ItShouldTestShapeEquivalence(string a, int aWidth, string b, int bWidth, bool allowFlippedShapes,
        string aStandard, int aStandardWidth, string bStandard, int bStandardWidth, 
        string bFlipped, int bFlippedWidth, string bFlippedStandard, int bFlippedStandardWidth, 
        bool equivalent)
    {
        this.Given(x => GivenAShape(a, aWidth))
            .And(x => GivenAnotherShape(b, bWidth))
            .And(x => GivenAllowFlippedShapes(allowFlippedShapes))
            .And(x => GivenGetStandardRotationReturns(a, aWidth, aStandard, aStandardWidth))
            .And(x => GivenGetStandardRotationReturns(b, bWidth, bStandard, bStandardWidth))
            .And(x => GivenFlipShapeHorizontallyReturns(b, bWidth, bFlipped, bFlippedWidth))
            .And(x => GivenGetStandardRotationReturns(bFlipped, bFlippedWidth, bFlippedStandard, bFlippedStandardWidth))
            .When(x => WhenGettingShapeEquivalence())
            .Then(x => ThenItShouldReturn(equivalent))
            .BDDfy();
    }

    private void GivenAShape(string a, int aWidth)
    {
        _a = TestHelpers.GeneratePolyomino(a, aWidth);
    }

    private void GivenAnotherShape(string b, int bWidth)
    {
        _b = TestHelpers.GeneratePolyomino(b, bWidth);
    }

    private void GivenAllowFlippedShapes(bool allowFlippedShapes)
    {
        _allowFlippedShapes = allowFlippedShapes;
    }

    private void GivenGetStandardRotationReturns(string input, int inputWidth, string output, int outputWidth)
    {
        _shapeManipulator.GetStandardShapeRotation(TestHelpers.GeneratePolyomino(input, inputWidth))
            .Returns(TestHelpers.GeneratePolyomino(output, outputWidth));
    }

    private void GivenFlipShapeHorizontallyReturns(string input, int inputWidth, string output, int outputWidth)
    {
        _shapeManipulator.FlipShapeHorizontally(TestHelpers.GeneratePolyomino(input, inputWidth))
            .Returns(TestHelpers.GeneratePolyomino(output, outputWidth));
    }

    private void WhenGettingShapeEquivalence()
    {
        _result = _subject.AreShapesEquivalent(_a!, _b!, _allowFlippedShapes);
    }

    private void ThenItShouldReturn(bool equivalent)
    {
        _result.ShouldNotBeNull();
        _result.ShouldBe(equivalent);
    }
}