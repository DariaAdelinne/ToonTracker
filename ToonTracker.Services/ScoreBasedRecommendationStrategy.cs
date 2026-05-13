/**************************************************************************
 *                                                                        *
 *  File:        ScoreBasedRecommendationStrategy.cs                      *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Strategie de recomandare dupa scorul personal.           *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using ToonTracker.Domain;

namespace ToonTracker.Services;

/// <summary>
/// Strategie de recomandare bazata pe scorul personal al serialelor
/// </summary>
public class ScoreBasedRecommendationStrategy : IRecommendationStrategy
{
    public string Name => "Recomandare dupa scor";

    /// <summary>
    /// Returneaza serialele cu cel mai mare scor personal, filtrate dupa ratingul acceptat
    /// </summary>
    /// <param name="shows">Lista serialelor disponibile</param>
    /// <param name="profile">Profilul utilizatorului cu preferintele sale</param>
    /// <param name="count">Numarul maxim de recomandari returnate</param>
    /// <returns>Lista serialelor recomandate, sortate dupa scor si progres</returns>
    public IEnumerable<AnimatedShow> Recommend(IEnumerable<AnimatedShow> shows, UserProfile profile, int count = 5)
    {
        return shows
            .Where(s => s.Status != WatchStatus.Dropped && (int)s.Rating <= (int)profile.MaximumAcceptedRating)
            .OrderByDescending(s => s.PersonalScore)
            .ThenByDescending(s => s.Progress)
            .Take(count);
    }
}
