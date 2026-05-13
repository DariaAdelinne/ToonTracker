/**************************************************************************
 *                                                                        *
 *  File:        GenreBasedRecommendationStrategy.cs                      *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Strategie de recomandare dupa genurile favorite.         *
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
/// Strategie de recomandare bazata pe genurile preferate ale utilizatorului
/// </summary>
public class GenreBasedRecommendationStrategy : IRecommendationStrategy
{
    public string Name => "Recomandare dupa gen";

    /// <summary>
    /// Returneaza seriale recomandate pe baza genurilor preferate din profilul utilizatorului
    /// </summary>
    /// <param name="shows">Lista serialelor disponibile</param>
    /// <param name="profile">Profilul utilizatorului cu preferintele sale</param>
    /// <param name="count">Numarul maxim de recomandari returnate</param>
    /// <returns>Lista serialelor recomandate, sortate dupa scor si titlu</returns>
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
