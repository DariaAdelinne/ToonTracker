/**************************************************************************
 *                                                                        *
 *  File:        UserProfile.cs                                           *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Profilul utilizatorului si preferintele sale.            *
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
/// Gestioneaza informatiile despre utilizator si preferintele de filtrare ale acestuia.
/// </summary>
public class UserProfile
{
    /// <summary> Numele de utilizator afisat in aplicatie. </summary>
    // Valoarea implicita este setata pe "Invitat"
    public string Name { get; set; } = "Invitat";

    /// <summary> Lista de genuri preferate. </summary>
    // Initializata ca o lista goala pentru a evita erorile de tip null
    public List<string> FavoriteGenres { get; set; } = new();

    /// <summary> Clasificarea maxima de varsta permisa pentru afisarea continutului. </summary>
    // In mod implicit, profilul restrictioneaza continutul peste TV14
    public AgeRating MaximumAcceptedRating { get; set; } = AgeRating.TV14;
}
