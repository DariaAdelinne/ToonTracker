/**************************************************************************
 *                                                                        *
 *  File:        WatchSessions.cs                                         *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Retine o sesiune de vizionare pentru statistici.         *
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
/// Reprezinta o sesiune individuala de vizionare, utilizata pentru monitorizarea activitatii si statistici.
/// </summary>
public class WatchSession
{
    /// <summary> Identificatorul unic al sesiunii de vizionare. </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    // <summary> Identificatorul serialului la care s-a uitat utilizatorul. </summary>
    public Guid ShowId { get; set; }

    /// <summary> Data la care a avut loc sesiunea de vizionare. </summary>
    // Se initializeaza implicit cu data curenta
    public DateTime Date { get; set; } = DateTime.Today;

    /// <summary> Numarul de episoade vizionate in cadrul acestei sesiuni. </summary>
    public int EpisodesWatched { get; set; }

    /// <summary> Durata totala a sesiunii exprimata in minute. </summary>
    public int MinutesSpent { get; set; }


    /// <summary>
    /// Verifica daca datele sesiunii sunt valide din punct de vedere logic.
    /// </summary>
    /// <exception cref="ArgumentException"> Aruncata daca ShowId este invalid sau valorile numerice sunt zero/negative. </exception>
    public void Validate()
    {
        // O sesiune trebuie sa fie legata obligatoriu de un serial existent
        if (ShowId == Guid.Empty) throw new ArgumentException("Serialul asociat sesiunii este obligatoriu.");
        // Nu se poate inregistra o sesiune fara episoade vizionate
        if (EpisodesWatched <= 0) throw new ArgumentException("Numarul de episoade vizionate trebuie sa fie pozitiv.");
        // Timpul petrecut trebuie sa fie o valoare mai mare decat zero
        if (MinutesSpent <= 0) throw new ArgumentException("Durata sesiunii trebuie sa fie pozitiva.");
    }
}
