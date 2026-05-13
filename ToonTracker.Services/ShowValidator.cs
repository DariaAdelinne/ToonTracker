/**************************************************************************
 *                                                                        *
 *  File:        ShowValidator.cs                                         *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Validarea datelor introduse                              *
 *  pentru un desen sau serial animat.                                    *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


using System.Text;
using ToonTracker.Domain;

namespace ToonTracker.Services;

/// <summary>
/// Clasa statica pentru validarea si normalizarea obiectelor de tip AnimatedShow
/// </summary>
public static class ShowValidator
{
    /// <summary>
    /// Valideaza datele unui desen animat sau serial.
    /// Verifica campurile obligatorii, limitele de lungime, consistenta dintre
    /// episoade si status, si scorul personal.
    /// La final, normalizeaza datele obiectului daca validarea trece.
    /// </summary>
    /// <param name="show">Obiectul AnimatedShow care urmeaza sa fie validat</param>
    /// <exception cref="ArgumentException">Daca show este null sau daca exista una sau mai multe erori de validare</exception>
    public static void Validate(AnimatedShow show)
    {
        var errors = new List<string>();

        if (show == null)
        {
            throw new ArgumentException("Obiectul desenului/serialului nu poate fi null.");
        }

        if (string.IsNullOrWhiteSpace(show.Title))
        {
            errors.Add("Titlul este obligatoriu.");
        }
        else
        {
            if (show.Title.Trim().Length < 2)
            {
                errors.Add("Titlul trebuie sa aiba cel putin 2 caractere.");
            }

            if (show.Title.Trim().Length > 100)
            {
                errors.Add("Titlul nu poate avea mai mult de 100 de caractere.");
            }
        }

        if (string.IsNullOrWhiteSpace(show.Studio))
        {
            errors.Add("Studio-ul este obligatoriu.");
        }
        else if (show.Studio.Trim().Length > 80)
        {
            errors.Add("Studio-ul nu poate avea mai mult de 80 de caractere.");
        }

        if (string.IsNullOrWhiteSpace(show.Genre))
        {
            errors.Add("Genul este obligatoriu.");
        }
        else if (show.Genre.Trim().Length > 50)
        {
            errors.Add("Genul nu poate avea mai mult de 50 de caractere.");
        }

        if (show.TotalEpisodes <= 0)
        {
            errors.Add("Numarul total de episoade trebuie sa fie mai mare decat 0.");
        }

        if (show.TotalEpisodes > 5000)
        {
            errors.Add("Numarul total de episoade este prea mare. Valoarea maxima acceptata este 5000.");
        }

        if (show.WatchedEpisodes < 0)
        {
            errors.Add("Numarul de episoade vazute nu poate fi negativ.");
        }

        if (show.WatchedEpisodes > show.TotalEpisodes)
        {
            errors.Add("Episoadele vazute nu pot fi mai multe decat episoadele totale.");
        }

        if (show.PersonalScore < 0 || show.PersonalScore > 10)
        {
            errors.Add("Scorul personal trebuie sa fie intre 0 si 10.");
        }

        if (show.WatchedEpisodes == 0 && show.PersonalScore > 0)
        {
            errors.Add("Nu poti acorda scor daca nu ai vazut niciun episod.");
        }

        if (!Enum.IsDefined(typeof(AgeRating), show.Rating))
        {
            errors.Add("Rating-ul selectat nu este valid.");
        }

        if (!Enum.IsDefined(typeof(WatchStatus), show.Status))
        {
            errors.Add("Statusul selectat nu este valid.");
        }

        if (!string.IsNullOrWhiteSpace(show.FavoriteCharacter) &&
            show.FavoriteCharacter.Trim().Length > 80)
        {
            errors.Add("Numele personajului favorit nu poate avea mai mult de 80 de caractere.");
        }

        if (!string.IsNullOrWhiteSpace(show.Notes) &&
            show.Notes.Trim().Length > 500)
        {
            errors.Add("Notele nu pot avea mai mult de 500 de caractere.");
        }

        if (show.Status == WatchStatus.Finished &&
            show.WatchedEpisodes != show.TotalEpisodes)
        {
            errors.Add("Pentru statusul Finished, episoadele vazute trebuie sa fie egale cu episoadele totale.");
        }

        if (show.WatchedEpisodes == show.TotalEpisodes)
        {
            show.Status = WatchStatus.Finished;
        }

        if (show.Status == WatchStatus.Planned &&
            show.WatchedEpisodes != 0)
        {
            errors.Add("Pentru statusul Planned, episoadele vazute trebuie sa fie 0.");
        }

        if (show.Status == WatchStatus.Dropped &&
            show.WatchedEpisodes >= show.TotalEpisodes)
        {
            errors.Add("Un titlu abandonat nu ar trebui sa aiba toate episoadele vazute.");
        }

        if (errors.Count > 0)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Datele introduse nu sunt valide:");
            builder.AppendLine();

            foreach (var error in errors)
            {
                builder.AppendLine("- " + error);
            }

            throw new ArgumentException(builder.ToString());
        }

        Normalize(show);
    }

    /// <summary>
    /// Normalizeaza campurile unui obiect AnimatedShow dupa ce validarea a trecut cu succes.
    /// Trimite spatiile albe din campurile text, reseteaza scorul daca nu s-au vazut episoade
    /// si sincronizeaza statusul cu numarul de episoade vizionate.
    /// </summary>
    /// <param name="show">Obiectul AnimatedShow care urmeaza sa fie normalizat. Se modifica direct (in-place).</param>
    private static void Normalize(AnimatedShow show)
    {
        show.Title = show.Title.Trim();
        show.Studio = show.Studio.Trim();
        show.Genre = show.Genre.Trim();

        if (!string.IsNullOrWhiteSpace(show.FavoriteCharacter))
        {
            show.FavoriteCharacter = show.FavoriteCharacter.Trim();
        }

        if (!string.IsNullOrWhiteSpace(show.Notes))
        {
            show.Notes = show.Notes.Trim();
        }

        if (show.WatchedEpisodes == 0)
        {
            show.PersonalScore = 0;
        }

        if (show.WatchedEpisodes == show.TotalEpisodes)
        {
            show.Status = WatchStatus.Finished;
        }

        if (show.Status == WatchStatus.Finished)
        {
            show.WatchedEpisodes = show.TotalEpisodes;
        }

        if (show.Status == WatchStatus.Planned)
        {
            show.WatchedEpisodes = 0;
        }
    }
}