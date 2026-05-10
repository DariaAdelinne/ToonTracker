// Author: Echipa ToonTracker
// Functionalitate: Profilul utilizatorului si preferintele sale.
namespace ToonTracker.Domain;

public class UserProfile
{
    public string Name { get; set; } = "Invitat";
    public List<string> FavoriteGenres { get; set; } = new();
    public AgeRating MaximumAcceptedRating { get; set; } = AgeRating.TV14;
}
