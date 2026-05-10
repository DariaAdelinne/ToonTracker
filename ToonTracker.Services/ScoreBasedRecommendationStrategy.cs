// Author: Echipa ToonTracker
// Functionalitate: Strategie de recomandare dupa scorul personal.
using ToonTracker.Domain;

namespace ToonTracker.Services;

public class ScoreBasedRecommendationStrategy : IRecommendationStrategy
{
    public string Name => "Recomandare dupa scor";

    public IEnumerable<AnimatedShow> Recommend(IEnumerable<AnimatedShow> shows, UserProfile profile, int count = 5)
    {
        return shows
            .Where(s => s.Status != WatchStatus.Dropped && (int)s.Rating <= (int)profile.MaximumAcceptedRating)
            .OrderByDescending(s => s.PersonalScore)
            .ThenByDescending(s => s.Progress)
            .Take(count);
    }
}
