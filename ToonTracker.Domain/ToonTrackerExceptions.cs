/**************************************************************************
 *                                                                        *
 *  File:        ToonTrackerExceptions.cs                                 *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Ierarhia de exceptii custom pentru aplicatia ToonTracker.*
 *               Toate exceptiile specifice domeniului mostenesc           *
 *               ToonTrackerException.                                    *
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

// ─────────────────────────────────────────────────────────────────────────────
// Clasa de baza
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Clasa de baza pentru toate exceptiile specifice aplicatiei ToonTracker.
/// Permite prinderea generica a oricarei erori de domeniu printr-un singur catch.
/// </summary>
public class ToonTrackerException : Exception
{
    /// <summary>
    /// Initializeaza o noua instanta cu un mesaj implicit.
    /// </summary>
    public ToonTrackerException()
        : base("A aparut o eroare in aplicatia ToonTracker.") { }

    /// <summary>
    /// Initializeaza o noua instanta cu un mesaj specific.
    /// </summary>
    /// <param name="message">Descrierea erorii.</param>
    public ToonTrackerException(string message)
        : base(message) { }

    /// <summary>
    /// Initializeaza o noua instanta cu un mesaj specific si exceptia care a cauzat-o.
    /// </summary>
    /// <param name="message">Descrierea erorii.</param>
    /// <param name="innerException">Exceptia originala care a declansat aceasta eroare.</param>
    public ToonTrackerException(string message, Exception innerException)
        : base(message, innerException) { }
}

// ─────────────────────────────────────────────────────────────────────────────
// Exceptii de validare
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Aruncata cand datele unui serial animat nu trec validarea.
/// Contine lista completa a erorilor de validare detectate.
/// </summary>
public class ShowValidationException : ToonTrackerException
{
    /// <summary>
    /// Lista erorilor de validare detectate.
    /// </summary>
    public IReadOnlyList<string> ValidationErrors { get; }

    /// <summary>
    /// Initializeaza exceptia cu o lista de erori de validare.
    /// Mesajul va enumera automat toate erorile.
    /// </summary>
    /// <param name="errors">Lista mesajelor de eroare de validare.</param>
    public ShowValidationException(IEnumerable<string> errors)
        : base(BuildMessage(errors))
    {
        ValidationErrors = errors.ToList().AsReadOnly();
    }

    /// <summary>
    /// Initializeaza exceptia cu un singur mesaj de eroare.
    /// </summary>
    /// <param name="message">Mesajul erorii de validare.</param>
    public ShowValidationException(string message)
        : base(message)
    {
        ValidationErrors = new List<string> { message }.AsReadOnly();
    }

    /// <summary>
    /// Construieste mesajul formatat din lista de erori.
    /// </summary>
    private static string BuildMessage(IEnumerable<string> errors)
    {
        var list = errors.ToList();
        if (list.Count == 0)
            return "Datele introduse nu sunt valide.";

        var lines = string.Join(Environment.NewLine, list.Select(e => "- " + e));
        return $"Datele introduse nu sunt valide:{Environment.NewLine}{lines}";
    }
}

// ─────────────────────────────────────────────────────────────────────────────
// Exceptii legate de existenta serialelor
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Aruncata cand un serial cautat dupa ID sau titlu nu exista in colectie.
/// </summary>
public class ShowNotFoundException : ToonTrackerException
{
    /// <summary>
    /// ID-ul cautat, daca cautarea s-a facut dupa ID.
    /// </summary>
    public Guid? ShowId { get; }

    /// <summary>
    /// Titlul cautat, daca cautarea s-a facut dupa titlu.
    /// </summary>
    public string? ShowTitle { get; }

    /// <summary>
    /// Initializeaza exceptia cu ID-ul serialului negasit.
    /// </summary>
    /// <param name="id">ID-ul serialului care nu a fost gasit.</param>
    public ShowNotFoundException(Guid id)
        : base($"Serialul cu ID-ul '{id}' nu a fost gasit.")
    {
        ShowId = id;
    }

    /// <summary>
    /// Initializeaza exceptia cu titlul serialului negasit.
    /// </summary>
    /// <param name="title">Titlul serialului care nu a fost gasit.</param>
    public ShowNotFoundException(string title)
        : base($"Serialul '{title}' nu a fost gasit.")
    {
        ShowTitle = title;
    }
}

/// <summary>
/// Aruncata cand se incearca adaugarea unui serial cu un titlu care exista deja in colectie.
/// </summary>
public class DuplicateShowException : ToonTrackerException
{
    /// <summary>
    /// Titlul duplicat care a cauzat exceptia.
    /// </summary>
    public string DuplicateTitle { get; }

    /// <summary>
    /// Initializeaza exceptia cu titlul duplicat.
    /// </summary>
    /// <param name="title">Titlul deja existent in colectie.</param>
    public DuplicateShowException(string title)
        : base($"Exista deja un serial cu titlul '{title}' in colectie.")
    {
        DuplicateTitle = title;
    }
}

// ─────────────────────────────────────────────────────────────────────────────
// Exceptii legate de starea de vizionare
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Aruncata cand o operatie de vizionare nu este permisa in starea curenta a serialului.
/// De exemplu, incercarea de a marca episoade pe un serial deja finalizat.
/// </summary>
public class InvalidWatchOperationException : ToonTrackerException
{
    /// <summary>
    /// Statusul curent al serialului la momentul erorii.
    /// </summary>
    public WatchStatus CurrentStatus { get; }

    /// <summary>
    /// Initializeaza exceptia cu un mesaj descriptiv si statusul curent.
    /// </summary>
    /// <param name="message">Descrierea operatiei nepermise.</param>
    /// <param name="currentStatus">Statusul serialului la momentul erorii.</param>
    public InvalidWatchOperationException(string message, WatchStatus currentStatus)
        : base(message)
    {
        CurrentStatus = currentStatus;
    }
}

// ─────────────────────────────────────────────────────────────────────────────
// Exceptii de acces la date
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Aruncata cand apare o eroare la citirea sau scrierea datelor de pe disc.
/// Incapsuleaza erorile de I/O sau de serializare JSON.
/// </summary>
public class DataAccessException : ToonTrackerException
{
    /// <summary>
    /// Initializeaza exceptia cu un mesaj descriptiv si exceptia originala.
    /// </summary>
    /// <param name="message">Descrierea erorii de acces la date.</param>
    /// <param name="innerException">Exceptia I/O sau de serializare care a cauzat eroarea.</param>
    public DataAccessException(string message, Exception innerException)
        : base(message, innerException) { }

    /// <summary>
    /// Initializeaza exceptia cu un mesaj descriptiv (fara exceptie interna).
    /// </summary>
    /// <param name="message">Descrierea erorii de acces la date.</param>
    public DataAccessException(string message)
        : base(message) { }
}
