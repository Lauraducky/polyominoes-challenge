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
    [InlineData("111001", 3, "111001", 3, false, true)]
    // flipped shapes
    [InlineData("110011", 3, "101101", 2, false, true)]
    // not equivalent
    [InlineData("111001", 3, "101101", 2, false, false)]
    // not equivalent flipped allowed
    [InlineData("110011", 3, "101101", 2, true, false)]
    // non standard rotation
    [InlineData("010111010", 3, "010111010", 3, false, true)]
    // non standard rotation, requires flip
    [InlineData("0100011111010100", 4, "0110001011110100", 4, false, true)]
    // non standard rotation, flipped allowed
    [InlineData("0100011111010100", 4, "0110001011110100", 4, true, false)]
    public void ItShouldTestShapeEquivalence(string a, int aWidth, string b, int bWidth, bool allowFlippedShapes, bool equivalent)
    {
        this.Given(x => GivenAShape(a, aWidth))
            .And(x => GivenAnotherShape(b, bWidth))
            .And(x => GivenAllowFlippedShapes(allowFlippedShapes))
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
    
    private void GivenGetShapeRotationsReturnsRotations()
    {
        var a = TestHelpers.GeneratePolyomino("111001", 3);
        var a1 = TestHelpers.GeneratePolyomino("010111", 2);
        var a2 = TestHelpers.GeneratePolyomino("100111", 3);
        var a3 = TestHelpers.GeneratePolyomino("111010", 2);
        var aRotations = new[] { a, a1, a2, a3 };
        _shapeManipulator.GetAllEquivalentShapes(a, true).Returns(aRotations);
        _shapeManipulator.GetAllEquivalentShapes(a1, true).Returns(aRotations);
        _shapeManipulator.GetAllEquivalentShapes(a2, true).Returns(aRotations);
        _shapeManipulator.GetAllEquivalentShapes(a3, true).Returns(aRotations);

        var aa = TestHelpers.GeneratePolyomino("111100", 3);
        var aa1 = TestHelpers.GeneratePolyomino("110101", 2);
        var aa2 = TestHelpers.GeneratePolyomino("001111", 3);
        var aa3 = TestHelpers.GeneratePolyomino("101011", 2);
        var aaRotations = new[] { aa, aa1, aa2, aa3 };
        _shapeManipulator.GetAllEquivalentShapes(aa, true).Returns(aaRotations);
        _shapeManipulator.GetAllEquivalentShapes(aa1, true).Returns(aaRotations);
        _shapeManipulator.GetAllEquivalentShapes(aa2, true).Returns(aaRotations);
        _shapeManipulator.GetAllEquivalentShapes(aa3, true).Returns(aaRotations);

        var aAaRotations = new[] { a, a1, a2, a3, aa, aa1, aa2, aa3 };
        _shapeManipulator.GetAllEquivalentShapes(a, false).Returns(aAaRotations);
        _shapeManipulator.GetAllEquivalentShapes(a1, false).Returns(aAaRotations);
        _shapeManipulator.GetAllEquivalentShapes(a2, false).Returns(aAaRotations);
        _shapeManipulator.GetAllEquivalentShapes(a3, false).Returns(aAaRotations);
        _shapeManipulator.GetAllEquivalentShapes(aa, false).Returns(aAaRotations);
        _shapeManipulator.GetAllEquivalentShapes(aa1, false).Returns(aAaRotations);
        _shapeManipulator.GetAllEquivalentShapes(aa2, false).Returns(aAaRotations);
        _shapeManipulator.GetAllEquivalentShapes(aa3, false).Returns(aAaRotations);

        var b = TestHelpers.GeneratePolyomino("110011", 3);
        var b1 = TestHelpers.GeneratePolyomino("011110", 2);
        var bRotations = new[] { b, b1 };
        _shapeManipulator.GetAllEquivalentShapes(b, true).Returns(bRotations);
        _shapeManipulator.GetAllEquivalentShapes(b1, true).Returns(bRotations);

        var c = TestHelpers.GeneratePolyomino("101101", 2);
        var c1 = TestHelpers.GeneratePolyomino("011110", 3);
        var cRotations = new[] { c, c1 };
        _shapeManipulator.GetAllEquivalentShapes(c, true).Returns(cRotations);
        _shapeManipulator.GetAllEquivalentShapes(c1, true).Returns(cRotations);

        var bcRotations = new[] { b, b1, c, c1 };
        _shapeManipulator.GetAllEquivalentShapes(b, false).Returns(bcRotations);
        _shapeManipulator.GetAllEquivalentShapes(b1, false).Returns(bcRotations);
        _shapeManipulator.GetAllEquivalentShapes(c, false).Returns(bcRotations);
        _shapeManipulator.GetAllEquivalentShapes(c1, false).Returns(bcRotations);
        
        var nonStandardA = TestHelpers.GeneratePolyomino("010111010", 3);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardA, Arg.Any<bool>()).Returns(new[] { nonStandardA });
        
        var nonStandardB = TestHelpers.GeneratePolyomino("0100011111010100", 4);
        var nonStandardB1 = TestHelpers.GeneratePolyomino("0100111100100110", 4);
        var nonStandardB2 = TestHelpers.GeneratePolyomino("0010101111100010", 4);
        var nonStandardB3 = TestHelpers.GeneratePolyomino("0110010011110010", 4);
        var nonStandardBRotations = new[] { nonStandardB, nonStandardB1, nonStandardB2, nonStandardB3 };
        _shapeManipulator.GetAllEquivalentShapes(nonStandardB, true).Returns(nonStandardBRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardB1, true).Returns(nonStandardBRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardB2, true).Returns(nonStandardBRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardB3, true).Returns(nonStandardBRotations);
        
        var nonStandardC = TestHelpers.GeneratePolyomino("0010111010110010", 4);
        var nonStandardC1 = TestHelpers.GeneratePolyomino("0110001011110100", 4);
        var nonStandardC2 = TestHelpers.GeneratePolyomino("0100110101110100", 4);
        var nonStandardC3 = TestHelpers.GeneratePolyomino("0010111101000110", 4);
        var nonStandardCRotations = new[] { nonStandardC, nonStandardC1, nonStandardC2, nonStandardC3 };
        _shapeManipulator.GetAllEquivalentShapes(nonStandardC, true).Returns(nonStandardCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardC1, true).Returns(nonStandardCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardC2, true).Returns(nonStandardCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardC3, true).Returns(nonStandardCRotations);

        var nonStandardBCRotations = new[] { nonStandardB, nonStandardB1, nonStandardB2, nonStandardB3, nonStandardC, nonStandardC1, nonStandardC2, nonStandardC3 };
        _shapeManipulator.GetAllEquivalentShapes(nonStandardB, false).Returns(nonStandardBCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardB1, false).Returns(nonStandardBCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardB2, false).Returns(nonStandardBCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardB3, false).Returns(nonStandardBCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardC, false).Returns(nonStandardBCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardC1, false).Returns(nonStandardBCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardC2, false).Returns(nonStandardBCRotations);
        _shapeManipulator.GetAllEquivalentShapes(nonStandardC3, false).Returns(nonStandardBCRotations);
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