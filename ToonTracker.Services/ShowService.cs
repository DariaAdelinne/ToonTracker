// Author: Echipa ToonTracker
// Functionalitate: Logica de business pentru seriale animate.

using ToonTracker.Data;
using ToonTracker.Domain;

namespace ToonTracker.Services;

public class ShowService
{
    private readonly IRepository<AnimatedShow> _repository;

    public ShowService(IRepository<AnimatedShow> repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<AnimatedShow> GetAll() => _repository.GetAll();

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

    public IEnumerable<AnimatedShow> Search(string text)
    {
        text = text?.Trim() ?? string.Empty;

        return _repository.GetAll().Where(s =>
            s.Title.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            s.Genre.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            s.Studio.Contains(text, StringComparison.OrdinalIgnoreCase));
    }

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