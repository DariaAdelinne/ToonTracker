// Author: Echipa ToonTracker
// Functionalitate: Strategie de recomandare dupa genurile favorite.
using ToonTracker.Domain;

namespace ToonTracker.Services;

public class GenreBasedRecommendationStrategy : IRecommendationStrategy
{
    public string Name => "Recomandare dupa gen";

    public IEnumerable<AnimatedShow> Recommend(IEnumerable<AnimatedShow> shows, UserProfile profile, int count = 5)
    {
        return shows
            .Where(s => s.Status == WatchStatus.Planned || s.Status == WatchStatus.Watching)
            .Where(s => profile.FavoriteGenres.Contains(s.Genre, StringComparer.OrdinalIgnoreCase))
            .OrderByDescending(s => s.PersonalScore)
            .ThenBy(s => s.Title)
            .Take(count);
    }
}
