// Author: Echipa ToonTracker
// Functionalitate: Retine o sesiune de vizionare pentru statistici.
namespace ToonTracker.Domain;

public class WatchSession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ShowId { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public int EpisodesWatched { get; set; }
    public int MinutesSpent { get; set; }

    public void Validate()
    {
        if (ShowId == Guid.Empty) throw new ArgumentException("Serialul asociat sesiunii este obligatoriu.");
        if (EpisodesWatched <= 0) throw new ArgumentException("Numarul de episoade vizionate trebuie sa fie pozitiv.");
        if (MinutesSpent <= 0) throw new ArgumentException("Durata sesiunii trebuie sa fie pozitiva.");
    }
}
