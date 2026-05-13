/**************************************************************************
 *                                                                        *
 *  File:        StatisticsService.cs                                     *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Calculeaza indicatori pentru dashboard.                  *
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
/// Serviciu pentru calculul statisticilor colectiei de seriale animate
/// </summary>
public class StatisticsService
{
    /// <summary>
    /// Returneaza numarul de seriale finalizate
    /// </summary>
    /// <param name="shows">Lista serialelor din colectia utilizatorului</param>
    /// <returns>Numarul de seriale cu statusul Finished</returns>
    public int CountFinished(IEnumerable<AnimatedShow> shows) => shows.Count(s => s.Status == WatchStatus.Finished);

    /// <summary>
    /// Calculeaza scorul mediu personal al serialelor din colectie
    /// </summary>
    /// <param name="shows">Lista serialelor din colectia utilizatorului</param>
    /// <returns>Media scorurilor rotunjita la 2 zecimale, sau 0 daca lista este goala</returns>
    public double AverageScore(IEnumerable<AnimatedShow> shows) => shows.Any() ? Math.Round(shows.Average(s => s.PersonalScore), 2) : 0;

    /// <summary>
    /// Calculeaza numarul total de episoade ramase nevizionate in colectie
    /// </summary>
    /// <param name="shows">Lista serialelor din colectia utilizatorului</param>
    /// <returns>Suma episoadelor nevizionate din toate serialele</returns>
    public int RemainingEpisodes(IEnumerable<AnimatedShow> shows) => shows.Sum(s => Math.Max(0, s.TotalEpisodes - s.WatchedEpisodes));

    /// <summary>
    /// Determina genul cel mai frecvent intalnit in colectia utilizatorului
    /// </summary>
    /// <param name="shows">Lista serialelor din colectia utilizatorului</param>
    /// <returns>Numele genului cu cele mai multe seriale, sau "Nedefinit" daca lista este goala</returns>
    public string FavoriteGenre(IEnumerable<AnimatedShow> shows) => shows.GroupBy(s => s.Genre).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault() ?? "Nedefinit";
}
