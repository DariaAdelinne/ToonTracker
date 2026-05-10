// Author: Echipa ToonTracker
// Functionalitate: Modelul principal pentru seriale/desene animate.
namespace ToonTracker.Domain;

public class AnimatedShow
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Studio { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int TotalEpisodes { get; set; }
    public int WatchedEpisodes { get; set; }
    public AgeRating Rating { get; set; }
    public WatchStatus Status { get; set; }
    public int PersonalScore { get; set; }
    public string FavoriteCharacter { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public double Progress => TotalEpisodes == 0 ? 0 : Math.Round((double)WatchedEpisodes / TotalEpisodes * 100, 2);

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Titlul este obligatoriu.");
        if (TotalEpisodes <= 0) throw new ArgumentException("Numarul total de episoade trebuie sa fie pozitiv.");
        if (WatchedEpisodes < 0 || WatchedEpisodes > TotalEpisodes) throw new ArgumentException("Episoadele vizionate sunt invalide.");
        if (PersonalScore < 0 || PersonalScore > 10) throw new ArgumentException("Scorul personal trebuie sa fie intre 0 si 10.");
    }
}
