// Author: Echipa ToonTracker
// Functionalitate: Contract pentru sablonul de proiectare Strategy.
using ToonTracker.Domain;

namespace ToonTracker.Services;

public interface IRecommendationStrategy
{
    string Name { get; }
    IEnumerable<AnimatedShow> Recommend(IEnumerable<AnimatedShow> shows, UserProfile profile, int count = 5);
}
