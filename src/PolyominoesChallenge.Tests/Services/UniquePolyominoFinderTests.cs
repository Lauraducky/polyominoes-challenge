using PolyominoesChallenge.Models;
using PolyominoesChallenge.Services;
using PolyominoesChallenge.Tests.Helpers;

namespace PolyominoesChallenge.Tests.Services;

public class UniquePolyominoFinderTests
{
    private readonly IShapeEquivalenceComparer _shapeEquivalenceComparer;
    private readonly IShapeManipulator _shapeManipulator;
    
    private readonly UniquePolyominoFinder _subject;

    private Polyomino[]? _input;
    private bool _allowFlippedShapes;
    private Polyomino[]? _output;

    private readonly Polyomino _a = TestHelpers.GeneratePolyomino("111010", 2);
    private readonly Polyomino _b = TestHelpers.GeneratePolyomino("100111", 3);
    private readonly Polyomino _c = TestHelpers.GeneratePolyomino("011110", 2);
    private readonly Polyomino _d = TestHelpers.GeneratePolyomino("011110", 3);

    public UniquePolyominoFinderTests()
    {
        _shapeEquivalenceComparer = Substitute.For<IShapeEquivalenceComparer>();
        _shapeManipulator = Substitute.For<IShapeManipulator>();
        _subject = new UniquePolyominoFinder(_shapeEquivalenceComparer, _shapeManipulator);
    }

    [Theory]
    [InlineData(false, 2)]
    [InlineData(true, 3)]
    public void ItShouldRemoveNonUniqueInstancesOfShapes(bool allowFlippedShapes, int expectedOutputLength)
    {
        this.Given(x => GivenAListOfShapes())
            .And(x => GivenFlippedShapesAreAllowed(allowFlippedShapes))
            .And(x => GivenShapeEquivalenceComparerEvaluatesShapes())
            .And(x => GivenShapeManipulatorReturnsStandardRotations())
            .When(x => WhenGettingUniquePolyominoes())
            .Then(x => ThenItShouldReturnAListOfLength(expectedOutputLength))
            .BDDfy();
    }

    private void GivenAListOfShapes()
    {
        _input = new[] {
            _a, _b, _c, _d, _a
        };
    }

    private void GivenFlippedShapesAreAllowed(bool allowFlippedShapes)
    {
        _allowFlippedShapes = allowFlippedShapes;
    }

    private void GivenShapeEquivalenceComparerEvaluatesShapes()
    {
        _shapeEquivalenceComparer.AreShapesEquivalent(_a, _a, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(_a, _b, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(_b, _b, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(_b, _a, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(_c, _c, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(_a, _c, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_c, _a, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_b, _c, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_c, _b, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_d, _d, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(_a, _d, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_d, _a, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_b, _d, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_d, _b, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_c, _d, false).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(_d, _c, false).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(_c, _d, true).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(_d, _c, true).Returns(false);
    }

    private void GivenShapeManipulatorReturnsStandardRotations()
    {
        _shapeManipulator.GetStandardShapeRotation(Arg.Any<Polyomino>()).Returns(x => x.ArgAt<Polyomino>(0));
    }

    private void WhenGettingUniquePolyominoes()
    {
        _output = _subject.GetUniquePolyominoes(_input!, _allowFlippedShapes);
    }

    private void ThenItShouldReturnAListOfLength(int length)
    {
        _output.ShouldNotBeNull();
        _output.Length.ShouldBe(length);
    }
}