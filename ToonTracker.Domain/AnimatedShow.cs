/**************************************************************************
 *                                                                        *
 *  File:        AnimatedShow.cs                                          *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Modelul principal pentru seriale/desene animate.         *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/
namespace ToonTracker.Domain;

/// <summary>
/// Reprezinta modelul de date pentru un serial de animatie in sistemul ToonTracker.
/// </summary>
public class AnimatedShow
{
    /// <summary> Identificatorul unic al serialului. </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary> Titlul serialului de animatie. </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary> Studioul de animatie care a produs serialul. </summary>
    public string Studio { get; set; } = string.Empty;

    /// <summary> Genul cinematic. </summary>
    public string Genre { get; set; } = string.Empty;

    /// <summary> Numarul total de episoade disponibile. </summary>
    public int TotalEpisodes { get; set; }

    /// <summary> Numarul de episoade vizionate de utilizator pana in prezent. </summary>
    public int WatchedEpisodes { get; set; }

    /// <summary> Clasificarea pe categorii de varsta. </summary>
    public AgeRating Rating { get; set; }

    /// <summary> Statusul actual al vizionarii. </summary>
    public WatchStatus Status { get; set; }

    /// <summary> Nota acordata de utilizator. </summary>
    public int PersonalScore { get; set; }

    /// <summary> Numele personajului preferat din serial. </summary>
    public string FavoriteCharacter { get; set; } = string.Empty;

    /// <summary> Observatii suplimentare sau recenzii scurte. </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Calculeaza progresul vizionarii sub forma de procentaj.
    /// </summary>
    /// <value> Valoare intre 0 si 100, rotunjita la doua zecimale. </value>
    public double Progress => TotalEpisodes == 0 ? 0 : Math.Round((double)WatchedEpisodes / TotalEpisodes * 100, 2);


    /// <summary>
    /// Valideaza integritatea datelor pentru obiectul curent.
    /// </summary>
    /// <exception cref="ArgumentException"> Aruncata cand datele sunt inconsistente sau invalide. </exception>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Titlul este obligatoriu.");
        if (TotalEpisodes <= 0) throw new ArgumentException("Numarul total de episoade trebuie sa fie pozitiv.");
        if (WatchedEpisodes < 0 || WatchedEpisodes > TotalEpisodes) throw new ArgumentException("Episoadele vizionate sunt invalide.");
        if (PersonalScore < 0 || PersonalScore > 10) throw new ArgumentException("Scorul personal trebuie sa fie intre 0 si 10.");
    }
}
