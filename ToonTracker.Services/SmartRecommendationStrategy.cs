// Author: Echipa ToonTracker
// Functionalitate: Strategie avansata de recomandare pe baza istoricului utilizatorului.
// Analizeaza scorurile, genurile, studiourile si statusul desenelor deja vizionate.

using ToonTracker.Domain;

namespace ToonTracker.Services;

public class SmartRecommendationStrategy : IRecommendationStrategy
{
    public string Name => "Recomandare inteligenta pe baza preferintelor";

    public IEnumerable<AnimatedShow> Recommend(IEnumerable<AnimatedShow> shows, UserProfile profile, int count = 5)
    {
        var userShows = shows.ToList();

        if (userShows.Count == 0)
        {
            return Catalog()
                .OrderByDescending(c => c.Popularity)
                .Take(count)
                .Select(c => ToAnimatedShow(c, 8, "Recomandare populara deoarece nu exista inca istoric de vizionare."));
        }

        var watchedTitles = userShows
            .Select(s => Normalize(s.Title))
            .ToHashSet();

        var genreWeights = BuildGenreWeights(userShows);
        var studioWeights = BuildStudioWeights(userShows);
        var preferredMaxRating = DetectPreferredMaxRating(userShows, profile);

        var recommendations = Catalog()
            .Where(c => !watchedTitles.Contains(Normalize(c.Title)))
            .Select(c => ScoreCandidate(c, genreWeights, studioWeights, preferredMaxRating, userShows))
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.Candidate.Popularity)
            .Take(count)
            .Select(x => ToAnimatedShow(
                x.Candidate,
                ConvertScoreToTenPointScale(x.Score),
                x.Explanation))
            .ToList();

        return recommendations;
    }

    private static Dictionary<string, double> BuildGenreWeights(List<AnimatedShow> userShows)
    {
        var weights = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        foreach (var show in userShows)
        {
            if (string.IsNullOrWhiteSpace(show.Genre))
                continue;

            var weight = CalculateUserShowWeight(show);

            if (!weights.ContainsKey(show.Genre))
                weights[show.Genre] = 0;

            weights[show.Genre] += weight;
        }

        return weights;
    }

    private static Dictionary<string, double> BuildStudioWeights(List<AnimatedShow> userShows)
    {
        var weights = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        foreach (var show in userShows)
        {
            if (string.IsNullOrWhiteSpace(show.Studio))
                continue;

            var weight = CalculateUserShowWeight(show) * 0.65;

            if (!weights.ContainsKey(show.Studio))
                weights[show.Studio] = 0;

            weights[show.Studio] += weight;
        }

        return weights;
    }

    private static double CalculateUserShowWeight(AnimatedShow show)
    {
        var scoreWeight = show.PersonalScore / 10.0;
        var progressWeight = show.Progress / 100.0;

        var statusBonus = show.Status switch
        {
            WatchStatus.Finished => 1.20,
            WatchStatus.Watching => 0.80,
            WatchStatus.Planned => 0.25,
            WatchStatus.Dropped => -1.00,
            _ => 0
        };

        return scoreWeight * 2.0 + progressWeight + statusBonus;
    }

    private static AgeRating DetectPreferredMaxRating(List<AnimatedShow> userShows, UserProfile profile)
    {
        var likedShows = userShows
            .Where(s => s.PersonalScore >= 7 && s.Status != WatchStatus.Dropped)
            .ToList();

        if (likedShows.Count == 0)
            return profile.MaximumAcceptedRating;

        return likedShows
            .Select(s => s.Rating)
            .OrderByDescending(r => r)
            .First();
    }

    private static CandidateScore ScoreCandidate(
        CatalogItem candidate,
        Dictionary<string, double> genreWeights,
        Dictionary<string, double> studioWeights,
        AgeRating preferredMaxRating,
        List<AnimatedShow> userShows)
    {
        var score = 1.0;
        var reasons = new List<string>();

        if (genreWeights.TryGetValue(candidate.Genre, out var genreScore))
        {
            score += genreScore * 2.3;
            reasons.Add("gen apropiat de preferintele tale");
        }

        if (studioWeights.TryGetValue(candidate.Studio, out var studioScore))
        {
            score += studioScore * 1.4;
            reasons.Add("studio pe care l-ai apreciat");
        }

        if (candidate.Rating <= preferredMaxRating)
        {
            score += 1.0;
            reasons.Add("rating potrivit cu istoricul tau");
        }
        else
        {
            score -= 0.7;
        }

        score += candidate.Popularity * 0.35;

        var likedLongShows = userShows.Any(s =>
            s.TotalEpisodes >= 80 &&
            s.PersonalScore >= 8 &&
            s.Status != WatchStatus.Dropped);

        var likedShortShows = userShows.Any(s =>
            s.TotalEpisodes <= 50 &&
            s.PersonalScore >= 8 &&
            s.Status != WatchStatus.Dropped);

        if (likedLongShows && candidate.TotalEpisodes >= 80)
        {
            score += 0.8;
            reasons.Add("format lung, asemanator cu serialele apreciate");
        }

        if (likedShortShows && candidate.TotalEpisodes <= 50)
        {
            score += 0.8;
            reasons.Add("format scurt, usor de terminat");
        }

        var droppedGenres = userShows
            .Where(s => s.Status == WatchStatus.Dropped)
            .Select(s => s.Genre)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        if (droppedGenres.Contains(candidate.Genre))
        {
            score -= 1.5;
            reasons.Add("penalizare: ai abandonat titluri din acelasi gen");
        }

        if (reasons.Count == 0)
            reasons.Add("titlu popular din baza de recomandari");

        return new CandidateScore(candidate, score, string.Join(", ", reasons));
    }

    private static int ConvertScoreToTenPointScale(double score)
    {
        var result = (int)Math.Round(Math.Clamp(score, 1.0, 10.0));
        return result;
    }

    private static AnimatedShow ToAnimatedShow(CatalogItem item, int predictedScore, string explanation)
    {
        return new AnimatedShow
        {
            Title = item.Title,
            Studio = item.Studio,
            Genre = item.Genre,
            TotalEpisodes = item.TotalEpisodes,
            WatchedEpisodes = 0,
            Rating = item.Rating,
            Status = WatchStatus.Planned,
            PersonalScore = predictedScore,
            FavoriteCharacter = "-",
            Notes = "Recomandare automata: " + explanation
        };
    }

    private static string Normalize(string text)
    {
        return text.Trim().ToLowerInvariant();
    }

    private static List<CatalogItem> Catalog() => new()
    {
        new("Avatar: The Last Airbender", "Nickelodeon", "Aventura", 61, AgeRating.PG, 10),
        new("The Legend of Korra", "Nickelodeon", "Aventura", 52, AgeRating.PG, 9),
        new("Gravity Falls", "Disney", "Mister", 40, AgeRating.PG, 10),
        new("Adventure Time", "Cartoon Network", "Comedie", 283, AgeRating.PG13, 9),
        new("Regular Show", "Cartoon Network", "Comedie", 261, AgeRating.PG13, 8),
        new("Steven Universe", "Cartoon Network", "Fantasy", 160, AgeRating.PG, 9),
        new("Steven Universe Future", "Cartoon Network", "Fantasy", 20, AgeRating.PG, 8),
        new("The Amazing World of Gumball", "Cartoon Network", "Comedie", 240, AgeRating.PG, 9),
        new("Over the Garden Wall", "Cartoon Network", "Mister", 10, AgeRating.PG, 9),
        new("Infinity Train", "Cartoon Network", "Mister", 40, AgeRating.PG13, 8),

        new("Phineas and Ferb", "Disney", "Comedie", 189, AgeRating.G, 9),
        new("Kim Possible", "Disney", "Aventura", 87, AgeRating.G, 8),
        new("Star vs. the Forces of Evil", "Disney", "Fantasy", 77, AgeRating.PG, 8),
        new("Amphibia", "Disney", "Aventura", 106, AgeRating.PG, 9),
        new("The Owl House", "Disney", "Fantasy", 43, AgeRating.PG, 10),
        new("DuckTales 2017", "Disney", "Aventura", 75, AgeRating.PG, 8),
        new("Big City Greens", "Disney", "Comedie", 100, AgeRating.G, 7),
        new("Recess", "Disney", "Comedie", 127, AgeRating.G, 7),
        new("Lilo & Stitch: The Series", "Disney", "Comedie", 65, AgeRating.G, 7),
        new("American Dragon: Jake Long", "Disney", "Aventura", 52, AgeRating.PG, 7),

        new("SpongeBob SquarePants", "Nickelodeon", "Comedie", 250, AgeRating.G, 9),
        new("The Fairly OddParents", "Nickelodeon", "Comedie", 172, AgeRating.G, 8),
        new("Danny Phantom", "Nickelodeon", "Aventura", 53, AgeRating.PG, 8),
        new("Teenage Mutant Ninja Turtles 2012", "Nickelodeon", "Actiune", 124, AgeRating.PG, 8),
        new("Hey Arnold!", "Nickelodeon", "Comedie", 100, AgeRating.G, 8),
        new("Rugrats", "Nickelodeon", "Comedie", 172, AgeRating.G, 7),
        new("Invader Zim", "Nickelodeon", "SF", 27, AgeRating.PG, 8),
        new("My Life as a Teenage Robot", "Nickelodeon", "SF", 40, AgeRating.G, 7),
        new("CatDog", "Nickelodeon", "Comedie", 68, AgeRating.G, 6),
        new("The Loud House", "Nickelodeon", "Comedie", 150, AgeRating.G, 7),

        new("Ben 10", "Cartoon Network", "Actiune", 52, AgeRating.PG, 9),
        new("Ben 10: Alien Force", "Cartoon Network", "Actiune", 46, AgeRating.PG, 8),
        new("Ben 10: Ultimate Alien", "Cartoon Network", "Actiune", 52, AgeRating.PG, 8),
        new("Teen Titans", "Cartoon Network", "Actiune", 65, AgeRating.PG, 9),
        new("Teen Titans Go!", "Cartoon Network", "Comedie", 350, AgeRating.PG, 7),
        new("Justice League", "Warner Bros", "Actiune", 52, AgeRating.PG, 9),
        new("Justice League Unlimited", "Warner Bros", "Actiune", 39, AgeRating.PG, 9),
        new("Batman: The Animated Series", "Warner Bros", "Actiune", 85, AgeRating.PG, 10),
        new("Superman: The Animated Series", "Warner Bros", "Actiune", 54, AgeRating.PG, 8),
        new("Young Justice", "Warner Bros", "Actiune", 98, AgeRating.PG13, 9),

        new("Scooby-Doo! Mystery Incorporated", "Warner Bros", "Mister", 52, AgeRating.PG, 9),
        new("What's New, Scooby-Doo?", "Warner Bros", "Mister", 42, AgeRating.G, 7),
        new("Tom and Jerry", "MGM", "Comedie", 160, AgeRating.G, 8),
        new("Looney Tunes", "Warner Bros", "Comedie", 200, AgeRating.G, 8),
        new("Animaniacs", "Warner Bros", "Comedie", 99, AgeRating.G, 8),
        new("Pinky and the Brain", "Warner Bros", "Comedie", 65, AgeRating.G, 7),
        new("Tiny Toon Adventures", "Warner Bros", "Comedie", 98, AgeRating.G, 7),
        new("The Powerpuff Girls", "Cartoon Network", "Actiune", 78, AgeRating.G, 8),
        new("Dexter's Laboratory", "Cartoon Network", "Comedie", 78, AgeRating.G, 8),
        new("Courage the Cowardly Dog", "Cartoon Network", "Mister", 52, AgeRating.PG, 8),

        new("Samurai Jack", "Cartoon Network", "Actiune", 62, AgeRating.PG13, 9),
        new("Foster's Home for Imaginary Friends", "Cartoon Network", "Comedie", 79, AgeRating.G, 8),
        new("Ed, Edd n Eddy", "Cartoon Network", "Comedie", 69, AgeRating.G, 8),
        new("Codename: Kids Next Door", "Cartoon Network", "Actiune", 78, AgeRating.G, 8),
        new("Chowder", "Cartoon Network", "Comedie", 49, AgeRating.G, 7),
        new("The Grim Adventures of Billy & Mandy", "Cartoon Network", "Comedie", 77, AgeRating.PG, 8),
        new("Camp Lazlo", "Cartoon Network", "Comedie", 61, AgeRating.G, 6),
        new("Johnny Bravo", "Cartoon Network", "Comedie", 67, AgeRating.PG, 7),
        new("Cow and Chicken", "Cartoon Network", "Comedie", 52, AgeRating.PG, 6),
        new("I Am Weasel", "Cartoon Network", "Comedie", 79, AgeRating.G, 6),

        new("The Dragon Prince", "Netflix", "Fantasy", 63, AgeRating.PG, 9),
        new("She-Ra and the Princesses of Power", "Netflix", "Fantasy", 52, AgeRating.PG, 8),
        new("Kipo and the Age of Wonderbeasts", "Netflix", "Aventura", 30, AgeRating.PG, 8),
        new("Hilda", "Netflix", "Fantasy", 34, AgeRating.G, 9),
        new("Carmen Sandiego", "Netflix", "Aventura", 32, AgeRating.PG, 8),
        new("The Hollow", "Netflix", "Mister", 20, AgeRating.PG, 7),
        new("Trollhunters", "DreamWorks", "Fantasy", 52, AgeRating.PG, 8),
        new("3Below", "DreamWorks", "SF", 26, AgeRating.PG, 7),
        new("Wizards", "DreamWorks", "Fantasy", 10, AgeRating.PG, 7),
        new("Voltron: Legendary Defender", "DreamWorks", "SF", 78, AgeRating.PG, 8),

        new("How to Train Your Dragon: Race to the Edge", "DreamWorks", "Aventura", 78, AgeRating.PG, 8),
        new("Kung Fu Panda: Legends of Awesomeness", "DreamWorks", "Actiune", 80, AgeRating.PG, 7),
        new("The Penguins of Madagascar", "DreamWorks", "Comedie", 149, AgeRating.G, 7),
        new("All Hail King Julien", "DreamWorks", "Comedie", 78, AgeRating.G, 7),
        new("The Boss Baby: Back in Business", "DreamWorks", "Comedie", 49, AgeRating.G, 6),
        new("Dragons: Rescue Riders", "DreamWorks", "Aventura", 46, AgeRating.G, 6),
        new("Spirit Riding Free", "DreamWorks", "Aventura", 52, AgeRating.G, 6),
        new("The Croods: Family Tree", "DreamWorks", "Comedie", 52, AgeRating.G, 6),
        new("Jurassic World: Camp Cretaceous", "DreamWorks", "Aventura", 49, AgeRating.PG, 8),
        new("Fast & Furious Spy Racers", "DreamWorks", "Actiune", 52, AgeRating.PG, 6),

        new("BoJack Horseman", "Netflix", "Drama", 77, AgeRating.TVMA, 9),
        new("Arcane", "Netflix", "Actiune", 9, AgeRating.TV14, 10),
        new("Castlevania", "Netflix", "Actiune", 32, AgeRating.TVMA, 8),
        new("Blue Eye Samurai", "Netflix", "Actiune", 8, AgeRating.TVMA, 9),
        new("Disenchantment", "Netflix", "Comedie", 50, AgeRating.TV14, 7),
        new("F Is for Family", "Netflix", "Comedie", 44, AgeRating.TVMA, 7),
        new("Inside Job", "Netflix", "Comedie", 18, AgeRating.TV14, 8),
        new("The Midnight Gospel", "Netflix", "Fantasy", 8, AgeRating.TVMA, 7),
        new("Love, Death & Robots", "Netflix", "SF", 35, AgeRating.TVMA, 8),
        new("Final Space", "TBS", "SF", 36, AgeRating.TV14, 8),

        new("The Simpsons", "Fox", "Comedie", 750, AgeRating.PG13, 9),
        new("Futurama", "Fox", "SF", 150, AgeRating.PG13, 9),
        new("Bob's Burgers", "Fox", "Comedie", 250, AgeRating.PG13, 8),
        new("Family Guy", "Fox", "Comedie", 400, AgeRating.TV14, 7),
        new("American Dad!", "Fox", "Comedie", 350, AgeRating.TV14, 7),
        new("Rick and Morty", "Adult Swim", "SF", 70, AgeRating.TVMA, 9),
        new("Solar Opposites", "Hulu", "SF", 50, AgeRating.TVMA, 7),
        new("Close Enough", "HBO Max", "Comedie", 24, AgeRating.TV14, 7),
        new("Primal", "Adult Swim", "Actiune", 20, AgeRating.TVMA, 8),
        new("Smiling Friends", "Adult Swim", "Comedie", 18, AgeRating.TVMA, 7)
    };

    private record CatalogItem(
        string Title,
        string Studio,
        string Genre,
        int TotalEpisodes,
        AgeRating Rating,
        int Popularity);

    private record CandidateScore(
        CatalogItem Candidate,
        double Score,
        string Explanation);

    public IEnumerable<AnimatedShow> RecommendByPreferences(
    IEnumerable<AnimatedShow> existingShows,
    List<string> preferredGenres,
    List<string> preferredStudios,
    AgeRating maximumAcceptedRating,
    bool preferShortSeries,
    bool preferLongSeries,
    int count = 8)
    {
        var existingTitles = existingShows
            .Select(s => Normalize(s.Title))
            .ToHashSet();

        var genreSet = preferredGenres
            .Where(g => !string.IsNullOrWhiteSpace(g))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var studioSet = preferredStudios
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var recommendations = Catalog()
            .Where(c => !existingTitles.Contains(Normalize(c.Title)))
            .Select(c =>
            {
                var score = 1.0;
                var reasons = new List<string>();

                if (genreSet.Contains(c.Genre))
                {
                    score += 4.0;
                    reasons.Add("se potriveste cu genurile selectate");
                }

                if (studioSet.Contains(c.Studio))
                {
                    score += 3.0;
                    reasons.Add("este de la un studio selectat");
                }

                if (c.Rating <= maximumAcceptedRating)
                {
                    score += 1.5;
                    reasons.Add("are rating potrivit");
                }
                else
                {
                    score -= 2.0;
                    reasons.Add("rating mai ridicat decat preferinta aleasa");
                }

                if (preferShortSeries && c.TotalEpisodes <= 50)
                {
                    score += 2.0;
                    reasons.Add("este un serial scurt");
                }

                if (preferLongSeries && c.TotalEpisodes >= 80)
                {
                    score += 2.0;
                    reasons.Add("este un serial lung");
                }

                score += c.Popularity * 0.25;

                if (reasons.Count == 0)
                {
                    reasons.Add("titlu popular din catalogul de recomandari");
                }

                return new CandidateScore(
                    c,
                    score,
                    string.Join(", ", reasons));
            })
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.Candidate.Popularity)
            .Take(count)
            .Select(x => ToAnimatedShow(
                x.Candidate,
                ConvertScoreToTenPointScale(x.Score),
                x.Explanation))
            .ToList();

        return recommendations;
    }
}