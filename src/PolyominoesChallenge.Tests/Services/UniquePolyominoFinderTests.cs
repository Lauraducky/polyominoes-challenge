using PolyominoesChallenge.Services;

namespace PolyominoesChallenge.Tests.Services;

public class UniquePolyominoFinderTests
{
    private readonly IShapeEquivalenceComparer _shapeEquivalenceComparer;
    
    private readonly UniquePolyominoFinder _subject;

    public UniquePolyominoFinderTests()
    {
        _shapeEquivalenceComparer = Substitute.For<IShapeEquivalenceComparer>();
        _subject = new UniquePolyominoFinder(_shapeEquivalenceComparer);
    }

    private void GivenShapeEquivalenceComparerEvaluatesShapes()
    {
        
    }
}