// Author: Echipa ToonTracker
// Functionalitate: Calculeaza indicatori pentru dashboard.
using ToonTracker.Domain;

namespace ToonTracker.Services;

public class StatisticsService
{
    public int CountFinished(IEnumerable<AnimatedShow> shows) => shows.Count(s => s.Status == WatchStatus.Finished);
    public double AverageScore(IEnumerable<AnimatedShow> shows) => shows.Any() ? Math.Round(shows.Average(s => s.PersonalScore), 2) : 0;
    public int RemainingEpisodes(IEnumerable<AnimatedShow> shows) => shows.Sum(s => Math.Max(0, s.TotalEpisodes - s.WatchedEpisodes));
    public string FavoriteGenre(IEnumerable<AnimatedShow> shows) => shows.GroupBy(s => s.Genre).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault() ?? "Nedefinit";
}
