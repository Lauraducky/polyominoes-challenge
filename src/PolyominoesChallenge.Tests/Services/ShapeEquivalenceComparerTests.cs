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
    [InlineData("111001", 3, "111001", 3, false, "001111", 3, true)]
    // flipped shapes
    [InlineData("110011", 3, "101101", 2, false, "110011", 3, true)]
    // not equivalent
    [InlineData("111001", 3, "101101", 2, false, "110011", 3, false)]
    // not equivalent flipped allowed
    [InlineData("110011", 3, "101101", 2, true, "110011", 3, false)]
    // non standard rotation
    [InlineData("010111010", 3, "010111010", 3, false, "010111010", 3, true)]
    // non standard rotation, requires flip
    [InlineData("0100011111010100", 4, "0110001011110100", 4, false, "0110010011110010", 4, true)]
    // non standard rotation, flipped allowed
    [InlineData("0100011111010100", 4, "0110001011110100", 4, true, "0110010011110010", 4, false)]
    public void ItShouldTestShapeEquivalence(string a, int aWidth, string b, int bWidth, bool allowFlippedShapes,
        string bFlipped, int bFlippedWidth, bool equivalent)
    {
        this.Given(x => GivenAShape(a, aWidth))
            .And(x => GivenAnotherShape(b, bWidth))
            .And(x => GivenAllowFlippedShapes(allowFlippedShapes))
            .And(x => GivenFlipShapeHorizontallyReturns(b, bWidth, bFlipped, bFlippedWidth))
            .And(x => GivenGetShapeRotationsReturnsRotations())
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

    private void GivenFlipShapeHorizontallyReturns(string input, int inputWidth, string output, int outputWidth)
    {
        _shapeManipulator.FlipShapeHorizontally(TestHelpers.GeneratePolyomino(input, inputWidth))
            .Returns(TestHelpers.GeneratePolyomino(output, outputWidth));
    }
    
    private void GivenGetShapeRotationsReturnsRotations()
    {
        var a = TestHelpers.GeneratePolyomino("111001", 3);
        var a1 = TestHelpers.GeneratePolyomino("010111", 2);
        var a2 = TestHelpers.GeneratePolyomino("100111", 3);
        var a3 = TestHelpers.GeneratePolyomino("111010", 2);
        var aRotations = new[] { a, a1, a2, a3 };
        _shapeManipulator.GetAllShapeRotations(a).Returns(aRotations);
        _shapeManipulator.GetAllShapeRotations(a1).Returns(aRotations);
        _shapeManipulator.GetAllShapeRotations(a2).Returns(aRotations);
        _shapeManipulator.GetAllShapeRotations(a3).Returns(aRotations);

        var b = TestHelpers.GeneratePolyomino("110011", 3);
        var b1 = TestHelpers.GeneratePolyomino("011110", 2);
        var bRotations = new[] { b, b1 };
        _shapeManipulator.GetAllShapeRotations(b).Returns(bRotations);
        _shapeManipulator.GetAllShapeRotations(b1).Returns(bRotations);

        var c = TestHelpers.GeneratePolyomino("101101", 2);
        var c1 = TestHelpers.GeneratePolyomino("011110", 3);
        var cRotations = new[] { c, c1 };
        _shapeManipulator.GetAllShapeRotations(c).Returns(cRotations);
        _shapeManipulator.GetAllShapeRotations(c1).Returns(cRotations);
        
        var nonStandardA = TestHelpers.GeneratePolyomino("010111010", 3);
        _shapeManipulator.GetAllShapeRotations(nonStandardA).Returns(new[] { nonStandardA });
        
        var nonStandardB = TestHelpers.GeneratePolyomino("0100011111010100", 4);
        var nonStandardB1 = TestHelpers.GeneratePolyomino("0100111100100110", 4);
        var nonStandardB2 = TestHelpers.GeneratePolyomino("0010101111100010", 4);
        var nonStandardB3 = TestHelpers.GeneratePolyomino("0110010011110010", 4);
        var nonStandardBRotations = new[] { nonStandardB, nonStandardB1, nonStandardB2, nonStandardB3 };
        _shapeManipulator.GetAllShapeRotations(nonStandardB).Returns(nonStandardBRotations);
        _shapeManipulator.GetAllShapeRotations(nonStandardB1).Returns(nonStandardBRotations);
        _shapeManipulator.GetAllShapeRotations(nonStandardB2).Returns(nonStandardBRotations);
        _shapeManipulator.GetAllShapeRotations(nonStandardB3).Returns(nonStandardBRotations);
        
        var nonStandardC = TestHelpers.GeneratePolyomino("0010111010110010", 4);
        var nonStandardC1 = TestHelpers.GeneratePolyomino("0110001011110100", 4);
        var nonStandardC2 = TestHelpers.GeneratePolyomino("0100110101110100", 4);
        var nonStandardC3 = TestHelpers.GeneratePolyomino("0010111101000110", 4);
        var nonStandardCRotations = new[] { nonStandardC, nonStandardC1, nonStandardC2, nonStandardC3 };
        _shapeManipulator.GetAllShapeRotations(nonStandardC).Returns(nonStandardCRotations);
        _shapeManipulator.GetAllShapeRotations(nonStandardC1).Returns(nonStandardCRotations);
        _shapeManipulator.GetAllShapeRotations(nonStandardC2).Returns(nonStandardCRotations);
        _shapeManipulator.GetAllShapeRotations(nonStandardC3).Returns(nonStandardCRotations);
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