/**************************************************************************
 *                                                                        *
 *  File:        IRecommendation.cs                                       *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Contract pentru sablonul de proiectare Strategy.         *
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
/// Interfata pentru strategiile de recomandare de seriale
/// </summary>
public interface IRecommendationStrategy
{
    string Name { get; }

    /// <summary>
    /// Returneaza seriale recomandate pe baza profilului utilizatorului
    /// </summary>
    /// <param name="shows">Lista serialelor disponibile</param>
    /// <param name="profile">Profilul utilizatorului cu preferintele sale</param>
    /// <param name="count">Numarul maxim de recomandari returnate</param>
    /// <returns>Lista serialelor recomandate</returns>
    IEnumerable<AnimatedShow> Recommend(IEnumerable<AnimatedShow> shows, UserProfile profile, int count = 5);
}
