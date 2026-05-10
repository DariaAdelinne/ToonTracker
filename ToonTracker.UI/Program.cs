// Author: Echipa ToonTracker
// Functionalitate: Punctul de pornire pentru interfata grafica WinForms.
using ToonTracker.Data;
using ToonTracker.Domain;
using ToonTracker.Services;

namespace ToonTracker.UI;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        var dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ToonTracker", "shows.json");
        var repository = new JsonRepository<AnimatedShow>(dataPath);
        var service = new ShowService(repository);
        Application.Run(new MainForm(service));
    }
}
