// Author: Echipa ToonTracker
// Functionalitate: Teste unitare pentru domeniu si servicii.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToonTracker.Data;
using ToonTracker.Domain;
using ToonTracker.Services;

namespace ToonTracker.Tests;

[TestClass]
public class ShowServiceTests
{
    private class MemoryRepository<T> : IRepository<T>
    {
        public List<T> Items { get; set; } = new();
        public IReadOnlyList<T> GetAll() => Items.ToList();
        public void SaveAll(IEnumerable<T> items) => Items = items.ToList();
    }

    private static AnimatedShow ValidShow(string title = "Gravity Falls") => new()
    {
        Title = title, Studio = "Disney", Genre = "Adventure", TotalEpisodes = 40,
        WatchedEpisodes = 10, Rating = AgeRating.PG, Status = WatchStatus.Watching,
        PersonalScore = 9, FavoriteCharacter = "Mabel", Notes = "Mister si umor"
    };

    [TestMethod] public void Progress_IsCalculatedCorrectly() => Assert.AreEqual(25, ValidShow().Progress);
    [TestMethod] public void Validate_RejectsEmptyTitle() { var s = ValidShow(); s.Title = ""; Assert.ThrowsException<ShowValidationException>(() => s.Validate()); }
    [TestMethod] public void Validate_RejectsZeroEpisodes() { var s = ValidShow(); s.TotalEpisodes = 0; Assert.ThrowsException<ShowValidationException>(() => s.Validate()); }
    [TestMethod] public void Validate_RejectsNegativeWatched() { var s = ValidShow(); s.WatchedEpisodes = -1; Assert.ThrowsException<ShowValidationException>(() => s.Validate()); }
    [TestMethod] public void Validate_RejectsWatchedGreaterThanTotal() { var s = ValidShow(); s.WatchedEpisodes = 50; Assert.ThrowsException<ShowValidationException>(() => s.Validate()); }
    [TestMethod] public void Validate_RejectsScoreAboveTen() { var s = ValidShow(); s.PersonalScore = 11; Assert.ThrowsException<ShowValidationException>(() => s.Validate()); }
    [TestMethod] public void Add_SavesValidShow() { var repo = new MemoryRepository<AnimatedShow>(); var svc = new ShowService(repo); svc.Add(ValidShow()); Assert.AreEqual(1, repo.Items.Count); }
    [TestMethod] public void Add_RejectsDuplicateTitle() { var repo = new MemoryRepository<AnimatedShow>(); var svc = new ShowService(repo); svc.Add(ValidShow()); Assert.ThrowsException<DuplicateShowException>(() => svc.Add(ValidShow())); }
    [TestMethod] public void Search_FindsByTitle() { var repo = new MemoryRepository<AnimatedShow> { Items = new() { ValidShow() } }; var svc = new ShowService(repo); Assert.AreEqual(1, svc.Search("Gravity").Count()); }
    [TestMethod] public void Search_FindsByGenre() { var repo = new MemoryRepository<AnimatedShow> { Items = new() { ValidShow() } }; var svc = new ShowService(repo); Assert.AreEqual(1, svc.Search("Adventure").Count()); }
    [TestMethod] public void Search_FindsByStudio() { var repo = new MemoryRepository<AnimatedShow> { Items = new() { ValidShow() } }; var svc = new ShowService(repo); Assert.AreEqual(1, svc.Search("Disney").Count()); }
    [TestMethod] public void Update_ChangesExistingShow() { var s = ValidShow(); var repo = new MemoryRepository<AnimatedShow> { Items = new() { s } }; var svc = new ShowService(repo); s.PersonalScore = 10; svc.Update(s); Assert.AreEqual(10, repo.Items[0].PersonalScore); }
    [TestMethod] public void Update_ThrowsForMissingShow() { var svc = new ShowService(new MemoryRepository<AnimatedShow>()); Assert.ThrowsException<ShowNotFoundException>(() => svc.Update(ValidShow())); }
    [TestMethod] public void Delete_RemovesShow() { var s = ValidShow(); var repo = new MemoryRepository<AnimatedShow> { Items = new() { s } }; var svc = new ShowService(repo); svc.Delete(s.Id); Assert.AreEqual(0, repo.Items.Count); }
    [TestMethod] public void Delete_ThrowsForMissingShow() { var svc = new ShowService(new MemoryRepository<AnimatedShow>()); Assert.ThrowsException<ShowNotFoundException>(() => svc.Delete(Guid.NewGuid())); }
    [TestMethod] public void MarkEpisodeWatched_IncrementsProgress() { var s = ValidShow(); var repo = new MemoryRepository<AnimatedShow> { Items = new() { s } }; var svc = new ShowService(repo); svc.MarkEpisodeWatched(s.Id); Assert.AreEqual(11, repo.Items[0].WatchedEpisodes); }
    [TestMethod] public void MarkEpisodeWatched_FinishesAtTotal() { var s = ValidShow(); s.WatchedEpisodes = 39; var repo = new MemoryRepository<AnimatedShow> { Items = new() { s } }; var svc = new ShowService(repo); svc.MarkEpisodeWatched(s.Id); Assert.AreEqual(WatchStatus.Finished, repo.Items[0].Status); }
    [TestMethod] public void Statistics_CountFinishedWorks() { var stats = new StatisticsService(); Assert.AreEqual(1, stats.CountFinished(new[] { ValidShow(), new AnimatedShow { Title = "X", TotalEpisodes = 1, Status = WatchStatus.Finished } })); }
    [TestMethod] public void Statistics_AverageScoreWorks() { var stats = new StatisticsService(); Assert.AreEqual(8.5, stats.AverageScore(new[] { ValidShow(), new AnimatedShow { Title = "X", TotalEpisodes = 1, PersonalScore = 8 } })); }
    [TestMethod] public void GenreStrategy_ReturnsPreferredGenre() { var strat = new GenreBasedRecommendationStrategy(); var recs = strat.Recommend(new[] { ValidShow(), ValidShow("Other") }, new UserProfile { FavoriteGenres = new() { "Adventure" } }).ToList(); Assert.IsTrue(recs.All(s => s.Genre == "Adventure")); }
    [TestMethod] public void ScoreStrategy_OrdersByScore() { var a = ValidShow("A"); a.PersonalScore = 7; var b = ValidShow("B"); b.PersonalScore = 10; var first = new ScoreBasedRecommendationStrategy().Recommend(new[] { a, b }, new UserProfile()).First(); Assert.AreEqual("B", first.Title); }
}
