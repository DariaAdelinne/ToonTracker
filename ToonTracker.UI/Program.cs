/**************************************************************************
 *                                                                        *
 *  File:        Program.cs                                               *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Punctul de pornire pentru interfata grafica WinForms.    *
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
using ToonTracker.Services;

namespace ToonTracker.UI;

/// <summary>
/// Clasa statica ce serveste drept punct de intrare principal pentru aplicatie.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Punctul de intrare principal (Main) pentru aplicatia ToonTracker.
    /// Configureaza infrastructura de date, serviciile si lanseaza interfata grafica.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Initializeaza setarile de configurare ale aplicatiei WinForms
        ApplicationConfiguration.Initialize();
        // Stabileste calea catre fisierul de persistenta JSON in folderul AppData al utilizatorului
        var dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ToonTracker", "shows.json");
        // Initializarea repository-ului generic pentru obiecte de tip AnimatedShow
        var repository = new JsonRepository<AnimatedShow>(dataPath);
        // Initializarea serviciului care gestioneaza logica de business a aplicatiei
        var service = new ShowService(repository);
        // Porneste bucla de mesaje a interfetei grafice prin deschiderea ferestrei principale
        Application.Run(new MainForm(service));
    }
}
