/**************************************************************************
 *                                                                        *
 *  File:        ShowService.cs                                           *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Logica de business pentru seriale animate.               *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using ToonTracker.Data;
using ToonTracker.Domain;

namespace ToonTracker.Services;

/// <summary>
/// Serviciu pentru gestionarea serialelor animate
/// </summary>
public class ShowService
{
    private readonly IRepository<AnimatedShow> _repository;

    /// <summary>
    /// Constructorul serviciului pentru seriale
    /// </summary>
    /// <param name="repository">Repository-ul folosit pentru persistenta datelor</param>
    public ShowService(IRepository<AnimatedShow> repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<AnimatedShow> GetAll() => _repository.GetAll();

    /// <summary>
    /// Adauga un serial nou, dupa validare si verificarea duplicatelor dupa titlu
    /// </summary>
    /// <param name="show">Serialul de adaugat</param>
    /// <returns>Serialul adaugat</returns>
    /// <exception cref="InvalidOperationException">Daca exista deja un serial cu acelasi titlu</exception>
    public AnimatedShow Add(AnimatedShow show)
    {
        ShowValidator.Validate(show);

        var shows = _repository.GetAll().ToList();

        var alreadyExists = shows.Any(s =>
            s.Title.Equals(show.Title, StringComparison.OrdinalIgnoreCase));

        if (alreadyExists)
        {
            throw new InvalidOperationException("Exista deja un desen/serial cu acest titlu.");
        }

        shows.Add(show);
        _repository.SaveAll(shows);

        return show;
    }

    /// <summary>
    /// Actualizeaza datele unui serial existent, dupa validare si verificarea duplicatelor dupa titlu
    /// </summary>
    /// <param name="show">Serialul cu datele actualizate</param>
    /// <exception cref="KeyNotFoundException">Daca serialul nu a fost gasit</exception>
    /// <exception cref="InvalidOperationException">Daca exista deja un alt serial cu acelasi titlu</exception>
    public void Update(AnimatedShow show)
    {
        ShowValidator.Validate(show);

        var shows = _repository.GetAll().ToList();

        var index = shows.FindIndex(s => s.Id == show.Id);

        if (index < 0)
        {
            throw new KeyNotFoundException("Serialul nu a fost gasit.");
        }

        var duplicateTitle = shows.Any(s =>
            s.Id != show.Id &&
            s.Title.Equals(show.Title, StringComparison.OrdinalIgnoreCase));

        if (duplicateTitle)
        {
            throw new InvalidOperationException("Exista deja un alt desen/serial cu acest titlu.");
        }

        shows[index] = show;
        _repository.SaveAll(shows);
    }

    /// <summary>
    /// Sterge un serial dupa ID
    /// </summary>
    /// <param name="id">ID-ul serialului de sters</param>
    /// <exception cref="KeyNotFoundException">Daca serialul nu a fost gasit</exception>
    public void Delete(Guid id)
    {
        var shows = _repository.GetAll().ToList();

        var removed = shows.RemoveAll(s => s.Id == id);

        if (removed == 0)
        {
            throw new KeyNotFoundException("Serialul nu a fost gasit.");
        }

        _repository.SaveAll(shows);
    }

    /// <summary>
    /// Cauta seriale dupa titlu, gen sau studio
    /// </summary>
    /// <param name="text">Textul cautat</param>
    /// <returns>Lista serialelor care corespund cautarii</returns>
    public IEnumerable<AnimatedShow> Search(string text)
    {
        text = text?.Trim() ?? string.Empty;

        return _repository.GetAll().Where(s =>
            s.Title.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            s.Genre.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            s.Studio.Contains(text, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Marcheaza un numar de episoade ca vizionate si actualizeaza statusul serialului
    /// </summary>
    /// <param name="id">ID-ul serialului</param>
    /// <param name="episodes">Numarul de episoade de marcat ca vizionate</param>
    /// <exception cref="ArgumentException">Daca numarul de episoade este invalid</exception>
    /// <exception cref="KeyNotFoundException">Daca serialul nu a fost gasit</exception>
    /// <exception cref="InvalidOperationException">Daca serialul este deja complet vizionat</exception>
    public void MarkEpisodeWatched(Guid id, int episodes = 1)
    {
        if (episodes <= 0)
        {
            throw new ArgumentException("Numarul de episoade adaugate trebuie sa fie mai mare decat 0.");
        }

        var shows = _repository.GetAll().ToList();

        var show = shows.FirstOrDefault(s => s.Id == id)
            ?? throw new KeyNotFoundException("Serialul nu a fost gasit.");

        if (show.Status == WatchStatus.Planned)
        {
            show.Status = WatchStatus.Watching;
        }

        if (show.WatchedEpisodes >= show.TotalEpisodes)
        {
            throw new InvalidOperationException("Titlul este deja complet vizionat.");
        }

        show.WatchedEpisodes += episodes;

        if (show.WatchedEpisodes > show.TotalEpisodes)
        {
            show.WatchedEpisodes = show.TotalEpisodes;
        }

        if (show.WatchedEpisodes == show.TotalEpisodes)
        {
            show.Status = WatchStatus.Finished;
        }
        else
        {
            show.Status = WatchStatus.Watching;
        }

        ShowValidator.Validate(show);
        _repository.SaveAll(shows);
    }
}