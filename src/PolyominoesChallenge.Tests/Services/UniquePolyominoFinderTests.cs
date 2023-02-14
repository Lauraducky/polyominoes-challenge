using PolyominoesChallenge.Models;
using PolyominoesChallenge.Services;
using PolyominoesChallenge.Tests.Helpers;

namespace PolyominoesChallenge.Tests.Services;

public class UniquePolyominoFinderTests
{
    private readonly IShapeEquivalenceComparer _shapeEquivalenceComparer;
    
    private readonly UniquePolyominoFinder _subject;

    private Polyomino[]? _input;
    private bool _allowFlippedShapes;
    private Polyomino[]? _output;

    private Polyomino a = TestHelpers.GeneratePolyomino("111010", 2);
    private Polyomino b = TestHelpers.GeneratePolyomino("100111", 3);
    private Polyomino c = TestHelpers.GeneratePolyomino("011110", 2);
    private Polyomino d = TestHelpers.GeneratePolyomino("011110", 3);

    public UniquePolyominoFinderTests()
    {
        _shapeEquivalenceComparer = Substitute.For<IShapeEquivalenceComparer>();
        _subject = new UniquePolyominoFinder(_shapeEquivalenceComparer);
    }

    [Theory]
    [InlineData(false, 2)]
    [InlineData(true, 3)]
    public void ItShouldRemoveNonUniqueInstancesOfShapes(bool allowFlippedShapes, int expectedOutputLength)
    {
        this.Given(x => GivenAListOfShapes())
            .And(x => GivenFlippedShapesAreAllowed(allowFlippedShapes))
            .And(x => GivenShapeEquivalenceComparerEvaluatesShapes())
            .When(x => WhenGettingUniquePolyominoes())
            .Then(x => ThenItShouldReturnAListOfLength(expectedOutputLength))
            .BDDfy();
    }

    private void GivenAListOfShapes()
    {
        _input = new Polyomino[] {
            a, b, c, d, a
        };
    }

    private void GivenFlippedShapesAreAllowed(bool allowFlippedShapes)
    {
        _allowFlippedShapes = allowFlippedShapes;
    }

    private void GivenShapeEquivalenceComparerEvaluatesShapes()
    {
        _shapeEquivalenceComparer.AreShapesEquivalent(a, a, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(a, b, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(b, b, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(b, a, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(c, c, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(a, c, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(c, a, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(b, c, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(c, b, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(d, d, _allowFlippedShapes).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(a, d, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(d, a, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(b, d, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(d, b, _allowFlippedShapes).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(c, d, false).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(d, c, false).Returns(true);
        _shapeEquivalenceComparer.AreShapesEquivalent(c, d, true).Returns(false);
        _shapeEquivalenceComparer.AreShapesEquivalent(d, c, true).Returns(false);
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