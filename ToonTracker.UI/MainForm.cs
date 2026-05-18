/**************************************************************************
 *                                                                        *
 *  File:        MainForm.cs                                              *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Formularul principal pentru                              *
 *  gestionarea desenelor animate si serialelor.                          *
 *                                                                        *
 **************************************************************************/

using System.Text;
using ToonTracker.Domain;
using ToonTracker.Services;
using System.Drawing.Drawing2D;

namespace ToonTracker.UI;

/// <summary>
/// Enumerare pentru temele vizuale disponibile in aplicatie.
/// </summary>
public enum AppTheme
{
    CozyPink,
    BerryNight,
    OceanBlue
}

/// <summary>
/// Clasa principala a interfetei grafice pentru gestionarea colectiei de seriale animate.
/// </summary>
public partial class MainForm : Form
{
    private readonly ShowService _showService;
    private readonly StatisticsService _statistics = new();
    private readonly SmartRecommendationStrategy _smartStrategy = new SmartRecommendationStrategy();
    private readonly List<AnimatedShow> _lastRecommendations = new();
    private AppTheme _currentTheme = AppTheme.CozyPink;

    /// <summary>
    /// Constructor implicit - initializeaza setarile de baza ale ferestrei.
    /// </summary>
    public MainForm()
    {
        _showService = null!;
        InitializeComponent();
        ApplyTheme();
        ApplyRoundedCornersToStaticControls();
    }

    /// <summary>
    /// Constructor principal care injecteaza serviciul de date.
    /// </summary>
    /// <param name="showService">Serviciul care gestioneaza logica serialelor.</param>
    public MainForm(ShowService showService)
    {
        _showService = showService;
        InitializeComponent();
        ConfigureGridColumns();
        ApplyTheme();
        ApplyRoundedCornersToStaticControls();
        SeedDemoDataIfEmpty();
        LoadData();
    }

    private void AddButton_Click(object? sender, EventArgs e) => AddShow();
    private void EditButton_Click(object? sender, EventArgs e) => EditSelected();
    private void DeleteButton_Click(object? sender, EventArgs e) => DeleteSelected();
    private void WatchedButton_Click(object? sender, EventArgs e) => MarkWatched();
    private void RecommendButton_Click(object? sender, EventArgs e) => ShowRecommendations();
    private void CustomRecommendButton_Click(object? sender, EventArgs e) => ShowCustomRecommendations();
    private void StatisticsButton_Click(object? sender, EventArgs e) => ShowStatistics();
    private void ExportButton_Click(object? sender, EventArgs e) => ExportReport();
    private void ThemeButton_Click(object? sender, EventArgs e) => ChooseTheme();
    private void ResetButton_Click(object? sender, EventArgs e) => ResetFilters();
    private void AddToWishlistButton_Click(object? sender, EventArgs e) => AddSelectedRecommendationToWishlist();
    private void Grid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e) => EditSelected();
    private void SearchBox_TextChanged(object? sender, EventArgs e) => ApplyFilters();
    private void SortComboBox_SelectedIndexChanged(object? sender, EventArgs e) => ApplyFilters();

    private void HelpButton_Click(object? sender, EventArgs e)
    {
        MessageBox.Show(
            HelpText.Content,
            "Ajutor ToonTracker",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void ApplyRoundedCornersToStaticControls()
    {
        ApplyRoundedCorners(_headerPanel, 22);
        ApplyRoundedCorners(_controlsCard, 18);
        ApplyRoundedCorners(_footerCard, 18);

        foreach (var button in GetAllControls(this).OfType<Button>())
        {
            ApplyRoundedCorners(button, 13);
        }
    }

    private void RoundedControl_Resize(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            ApplyRoundedCorners(button, 13);
            return;
        }

        if (ReferenceEquals(sender, _headerPanel))
        {
            ApplyRoundedCorners(_headerPanel, 22);
        }
        else if (ReferenceEquals(sender, _controlsCard) || ReferenceEquals(sender, _footerCard))
        {
            ApplyRoundedCorners((Control)sender, 18);
        }
    }

    /// <summary>
    /// Modifica regiunea unui control pentru a-i oferi margini rotunjite.
    /// </summary>
    /// <param name="control">Controlul tinta.</param>
    /// <param name="radius">Raza de rotunjire a colturilor.</param>
    private void ApplyRoundedCorners(Control control, int radius)
    {
        if (control.Width <= 0 || control.Height <= 0)
        {
            return;
        }

        using var path = new GraphicsPath();
        var diameter = radius * 2;

        path.StartFigure();
        path.AddArc(0, 0, diameter, diameter, 180, 90);
        path.AddArc(control.Width - diameter, 0, diameter, diameter, 270, 90);
        path.AddArc(control.Width - diameter, control.Height - diameter, diameter, diameter, 0, 90);
        path.AddArc(0, control.Height - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        control.Region = new Region(path);
    }


    /// <summary>
    /// Verifica daca un control este descendentul altui control.
    /// </summary>
    /// <param name="control">Controlul de verificat.</param>
    /// <param name="potentialAncestor">Controlul stramos potential.</param>
    /// <returns>True daca este descendent, false in caz contrar.</returns>
    private static bool IsDescendantOf(Control control, Control? potentialAncestor)
    {
        var current = control.Parent;

        while (current != null)
        {
            if (current == potentialAncestor)
            {
                return true;
            }

            current = current.Parent;
        }

        return false;
    }

    /// <summary>
    /// Returneaza recursiv toate controalele continute de un parinte.
    /// </summary>
    /// <param name="parent">Controlul radacina.</param>
    /// <returns>O colectie de controale descendente.</returns>
    private IEnumerable<Control> GetAllControls(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            yield return control;

            foreach (var child in GetAllControls(control))
            {
                yield return child;
            }
        }
    }

    /// <summary>
    /// Populeaza aplicatia cu date demo daca nu exista nicio intrare.
    /// </summary>
    private void SeedDemoDataIfEmpty()
    {
        if (_showService == null)
        {
            return;
        }

        try
        {
            if (_showService.GetAll().Count == 0)
            {
                foreach (var show in DemoShows())
                {
                    _showService.Add(show);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Eroare la încărcarea datelor demo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
    /// <summary>
    /// Configureaza coloanele tabelului principal in care sunt afisate titlurile din colectie.
    /// </summary>
    private void ConfigureGridColumns()
    {
        _grid.Columns.Clear();
        _grid.AutoGenerateColumns = false;

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Title",
            HeaderText = "Titlu",
            Width = 180
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Studio",
            HeaderText = "Studio",
            Width = 130
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Genre",
            HeaderText = "Gen",
            Width = 120
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "TotalEpisodes",
            HeaderText = "Episoade",
            Width = 90
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "WatchedEpisodes",
            HeaderText = "Vizionate",
            Width = 80
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Progress",
            HeaderText = "Progres %",
            Width = 90
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Rating",
            HeaderText = "Rating",
            Width = 90
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Status",
            HeaderText = "Status",
            Width = 100
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "PersonalScore",
            HeaderText = "Scor",
            Width = 70
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "FavoriteCharacter",
            HeaderText = "Personaj favorit",
            Width = 150
        });

        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Notes",
            HeaderText = "Note",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
    }

    /// <summary>
    /// Incarca datele in BindingSource si actualizeaza interfata.
    /// </summary>
    /// <param name="source">Sursa de date optionala. Daca e null, preia tot din serviciu.</param>
    private void LoadData(IEnumerable<AnimatedShow>? source = null)
    {
        if (_showService == null)
        {
            return;
        }

        try
        {
            var shows = (source ?? _showService.GetAll()).ToList();
            shows = SortShows(shows);

            _bindingSource.DataSource = shows;
            RefreshFilters();
            UpdateStats(shows);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Eroare",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Actualizeaza listele derulante pentru filtre pe baza datelor curente.
    /// </summary>
    private void RefreshFilters()
    {
        if (_showService == null)
        {
            return;
        }

        var selectedGenre = _genreFilter.SelectedItem?.ToString() ?? "Toate genurile";
        var selectedStatus = _statusFilter.SelectedItem?.ToString() ?? "Toate statusurile";

        var genres = new List<string> { "Toate genurile" };
        genres.AddRange(
            _showService.GetAll()
                .Select(s => s.Genre)
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(g => g));

        _genreFilter.SelectedIndexChanged -= GenreFilterChanged;
        _genreFilter.DataSource = genres;
        _genreFilter.SelectedItem = genres.Contains(selectedGenre) ? selectedGenre : "Toate genurile";
        _genreFilter.SelectedIndexChanged += GenreFilterChanged;

        var statuses = new List<string> { "Toate statusurile" };
        statuses.AddRange(Enum.GetNames(typeof(WatchStatus)));

        _statusFilter.SelectedIndexChanged -= StatusFilterChanged;
        _statusFilter.DataSource = statuses;
        _statusFilter.SelectedItem = statuses.Contains(selectedStatus) ? selectedStatus : "Toate statusurile";
        _statusFilter.SelectedIndexChanged += StatusFilterChanged;
    }

    /// <summary>
    /// Event handler pentru filtru de gen.
    /// </summary>
    private void GenreFilterChanged(object? sender, EventArgs e)
    {
        ApplyFilters();
    }

    /// <summary>
    /// event handler pentru filtru de status.
    /// </summary>
    private void StatusFilterChanged(object? sender, EventArgs e)
    {
        ApplyFilters();
    }

    /// <summary>
    /// Filtreaza si sorteaza lista de seriale afisata in tabel.
    /// </summary>
    private void ApplyFilters()
    {
        if (_showService == null)
        {
            return;
        }

        try
        {
            var shows = _showService.Search(_searchBox.Text).ToList();
            var genre = _genreFilter.SelectedItem?.ToString();
            var status = _statusFilter.SelectedItem?.ToString();

            if (!string.IsNullOrWhiteSpace(genre) && genre != "Toate genurile")
            {
                shows = shows
                    .Where(s => s.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(status) && status != "Toate statusurile")
            {
                shows = shows
                    .Where(s => s.Status.ToString() == status)
                    .ToList();
            }

            shows = SortShows(shows);

            _bindingSource.DataSource = shows;
            UpdateStats(shows);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Eroare filtrare",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    /// <summary>
    /// Sorteaza lista de seriale primita conform optiunii din ComboBox.
    /// </summary>
    /// <param name="shows">Lista care trebuie sortata.</param>
    /// <returns>Lista sortata.</returns>
    private List<AnimatedShow> SortShows(List<AnimatedShow> shows)
    {
        var selectedSort = _sortComboBox.SelectedItem?.ToString() ?? "Alfabetic";

        return selectedSort switch
        {
            "Număr episoade" => shows
                .OrderByDescending(s => s.TotalEpisodes)
                .ThenBy(s => s.Title)
                .ToList(),

            "Progres" => shows
                .OrderByDescending(s => s.Progress)
                .ThenBy(s => s.Title)
                .ToList(),

            "Scor" => shows
                .OrderByDescending(s => s.PersonalScore)
                .ThenBy(s => s.Title)
                .ToList(),

            _ => shows
                .OrderBy(s => s.Title)
                .ToList()
        };
    }

    /// <summary>
    /// Actualizeaza textul de statistici din subsolul ferestrei.
    /// </summary>
    /// <param name="shows">Lista de seriale pentru care se calculeaza statisticile.</param>
    private void UpdateStats(List<AnimatedShow> shows)
    {
        _statsLabel.Text =
            $"Titluri afișate: {shows.Count} | " +
            $"Finalizate: {_statistics.CountFinished(shows)} | " +
            $"Scor mediu: {_statistics.AverageScore(shows)} | " +
            $"Episoade rămase: {_statistics.RemainingEpisodes(shows)} | " +
            $"Gen preferat: {_statistics.FavoriteGenre(shows)}";
    }

    /// <summary>
    /// Obtine obiectul serial selectat curent in tabel.
    /// </summary>
    /// <returns>Obiectul AnimatedShow sau null daca nu exista selectie.</returns>
    private AnimatedShow? SelectedShow()
    {
        if (_grid.CurrentRow == null)
        {
            return null;
        }

        return _grid.CurrentRow.DataBoundItem as AnimatedShow;
    }

    /// <summary>
    /// Lanseaza dialogul de adaugare a unui nou serial.
    /// </summary>
    private void AddShow()
    {
        if (_showService == null)
        {
            return;
        }

        using var dialog = new ShowForm();

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                _showService.Add(dialog.Show);
                LoadData();
                SelectById(dialog.Show.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Eroare validare",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }

    /// <summary>
    /// Lanseaza dialogul de editare pentru serialul selectat.
    /// </summary>
    private void EditSelected()
    {
        if (_showService == null)
        {
            return;
        }

        var selected = SelectedShow();

        if (selected == null)
        {
            MessageBox.Show(
                "Selectează mai întâi un titlu din tabel.",
                "Atenție",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        using var dialog = new ShowForm(selected);

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                _showService.Update(dialog.Show);
                LoadData();
                SelectById(dialog.Show.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Eroare validare",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }

    /// <summary>
    /// Sterge serialul selectat dupa confirmarea utilizatorului.
    /// </summary>
    private void DeleteSelected()
    {
        if (_showService == null)
        {
            return;
        }

        var selected = SelectedShow();

        if (selected == null)
        {
            MessageBox.Show(
                "Selectează mai întâi un titlu din tabel.",
                "Atenție",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        var confirmation = MessageBox.Show(
            $"Sigur ștergi '{selected.Title}'?",
            "Confirmare",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmation == DialogResult.Yes)
        {
            try
            {
                _showService.Delete(selected.Id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Eroare",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Incrementeaza numarul de episoade vazute pentru titlul selectat.
    /// </summary>
    private void MarkWatched()
    {
        if (_showService == null)
        {
            return;
        }

        var selected = SelectedShow();

        if (selected == null)
        {
            MessageBox.Show(
                "Selectează mai întâi un titlu din tabel.",
                "Atenție",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        try
        {
            _showService.MarkEpisodeWatched(selected.Id);
            LoadData();
            SelectById(selected.Id);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Eroare",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Genereaza recomandari automate pe baza serialelor marcate ca "Finished".
    /// </summary>
    private void ShowRecommendations()
    {
        if (_showService == null)
        {
            return;
        }

        try
        {
            var finishedShows = _showService.GetAll()
                .Where(s => s.Status == WatchStatus.Finished)
                .ToList();

            if (finishedShows.Count == 0)
            {
                _recommendationsBox.Text =
                    "Nu există titluri finalizate. Recomandările automate se bazează strict pe ce ai terminat.\r\n\r\n" +
                    "Poți folosi butonul Recomandări custom pentru a alege manual ce ai chef să vezi.";
                ClearRecommendationPicker();
                return;
            }

            var profile = new UserProfile
            {
                Name = "Utilizator",
                FavoriteGenres = finishedShows
                    .Where(s => s.PersonalScore >= 8)
                    .Select(s => s.Genre)
                    .Where(g => !string.IsNullOrWhiteSpace(g))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList(),
                MaximumAcceptedRating = AgeRating.TV14
            };

            var existingShows = _showService.GetAll().ToList();

            var recommendations = _smartStrategy
                .Recommend(finishedShows, profile, 8)
                .Where(r => !existingShows.Any(s => s.Title.Equals(r.Title, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            DisplayRecommendations(
                recommendations,
                "Recomandări generate automat doar pe baza titlurilor finalizate:");
        }
        catch (Exception ex)
        {
            _recommendationsBox.Text = "Eroare la generarea recomandărilor: " + ex.Message;
            ClearRecommendationPicker();
        }
    }

    /// <summary>
    /// Deschide formularul de preferinte pentru a genera recomandari personalizate.
    /// </summary>
    private void ShowCustomRecommendations()
    {
        if (_showService == null)
        {
            return;
        }

        using var optionsForm = new RecommendationOptionsForm();

        if (optionsForm.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            var existingShows = _showService.GetAll().ToList();

            var recommendations = _smartStrategy
                .RecommendByPreferences(
                    existingShows,
                    optionsForm.SelectedGenres,
                    optionsForm.SelectedStudios,
                    optionsForm.MaximumAcceptedRating,
                    optionsForm.PreferShortSeries,
                    optionsForm.PreferLongSeries,
                    8)
                .ToList();

            DisplayRecommendations(
                recommendations,
                "Recomandări generate pe baza preferințelor alese manual:");
        }
        catch (Exception ex)
        {
            _recommendationsBox.Text = "Eroare la generarea recomandărilor custom: " + ex.Message;
            ClearRecommendationPicker();
        }
    }

    /// <summary>
    /// Afiseaza lista de recomandari primita in zona dedicata din interfata.
    /// </summary>
    /// <param name="recommendations">Lista de obiecte recomandate.</param>
    /// <param name="title">Titlul sectiunii de recomandari.</param>
    private void DisplayRecommendations(List<AnimatedShow> recommendations, string title)
    {
        _lastRecommendations.Clear();
        _lastRecommendations.AddRange(recommendations);

        if (recommendations.Count == 0)
        {
            _recommendationsBox.Text =
                "Nu există recomandări noi. Toate titlurile potrivite sunt deja în lista ta.";
            ClearRecommendationPicker();
            return;
        }

        var text = title + Environment.NewLine + Environment.NewLine;

        for (var index = 0; index < recommendations.Count; index++)
        {
            var show = recommendations[index];

            text += $"{index + 1}. {show.Title}" + Environment.NewLine;
            text += $"   Studio: {show.Studio}" + Environment.NewLine;
            text += $"   Gen: {show.Genre}" + Environment.NewLine;
            text += $"   Episoade: {show.TotalEpisodes}" + Environment.NewLine;
            text += $"   Rating: {show.Rating}" + Environment.NewLine;
            text += $"   Scor estimat de potrivire: {show.PersonalScore}/10" + Environment.NewLine;
            text += $"   Motiv: {show.Notes.Replace("Recomandare automată: ", "")}" + Environment.NewLine;
            text += Environment.NewLine;
        }

        _recommendationsBox.Text = text;

        _recommendationPicker.DataSource = null;
        _recommendationPicker.DataSource = recommendations
            .Select(r => r.Title)
            .ToList();

        if (_recommendationPicker.Items.Count > 0)
        {
            _recommendationPicker.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Curata picker-ul de recomandari si lista interna.
    /// </summary>
    private void ClearRecommendationPicker()
    {
        _lastRecommendations.Clear();
        _recommendationPicker.DataSource = null;
        _recommendationPicker.Items.Clear();
    }

    /// <summary>
    /// Adauga titlul recomandat selectat in colectia personala cu statusul "Planned".
    /// </summary>
    private void AddSelectedRecommendationToWishlist()
    {
        if (_showService == null)
        {
            return;
        }

        if (_recommendationPicker.SelectedItem == null)
        {
            MessageBox.Show(
                "Nu ai selectat nicio recomandare.",
                "Wishlist",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        var selectedTitle = _recommendationPicker.SelectedItem.ToString();

        var recommendation = _lastRecommendations
            .FirstOrDefault(r => r.Title.Equals(selectedTitle, StringComparison.OrdinalIgnoreCase));

        if (recommendation == null)
        {
            MessageBox.Show(
                "Recomandarea selectată nu mai este disponibilă.",
                "Wishlist",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var alreadyExists = _showService.GetAll()
            .Any(s => s.Title.Equals(recommendation.Title, StringComparison.OrdinalIgnoreCase));

        if (alreadyExists)
        {
            MessageBox.Show(
                "Titlul există deja în lista ta.",
                "Wishlist",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        try
        {
            var wishlistShow = new AnimatedShow
            {
                Id = Guid.NewGuid(),
                Title = recommendation.Title,
                Studio = recommendation.Studio,
                Genre = recommendation.Genre,
                TotalEpisodes = recommendation.TotalEpisodes,
                WatchedEpisodes = 0,
                Rating = recommendation.Rating,
                Status = WatchStatus.Planned,
                PersonalScore = 0,
                FavoriteCharacter = string.Empty,
                Notes = "Adăugat în wishlist din recomandările inteligente."
            };

            _showService.Add(wishlistShow);
            LoadData();

            _lastRecommendations.Remove(recommendation);

            DisplayRecommendations(
                _lastRecommendations.ToList(),
                "Recomandări rămase:");

            MessageBox.Show(
                $"'{wishlistShow.Title}' a fost adăugat în wishlist cu status Planned.",
                "Wishlist actualizat",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Nu s-a putut adăuga recomandarea în wishlist: " + ex.Message,
                "Eroare wishlist",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Deschide fereastra complexa de statistici.
    /// </summary>
    private void ShowStatistics()
    {
        if (_showService == null)
        {
            return;
        }

        try
        {
            var shows = _showService.GetAll().ToList();

            using var statisticsForm = new StatisticsForm(shows, _currentTheme);
            statisticsForm.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Nu s-au putut afişa statisticile: " + ex.Message,
                "Eroare statistici",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Exporta datele colectiei intr-un fisier extern ales de utilizator.
    /// </summary>
    private void ExportReport()
    {
        if (_showService == null)
        {
            return;
        }

        try
        {
            var shows = _showService.GetAll().ToList();

            if (shows.Count == 0)
            {
                MessageBox.Show(
                    "Nu există titluri de exportat. Adaugă mai întâi câteva desene sau seriale.",
                    "Export raport",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            shows = SortShows(shows);

            using var saveDialog = new SaveFileDialog
            {
                Title = "Export raport ToonTracker",
                Filter = "Raport text (*.txt)|*.txt|Fișier CSV pentru Excel (*.csv)|*.csv",
                FileName = $"ToonTracker_Raport_{DateTime.Now:yyyyMMdd_HHmm}",
                DefaultExt = "txt",
                AddExtension = true
            };

            if (saveDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var extension = Path.GetExtension(saveDialog.FileName).ToLowerInvariant();

            var content = extension == ".csv"
                ? BuildCsvReport(shows)
                : BuildTextReport(shows);

            File.WriteAllText(saveDialog.FileName, content, Encoding.UTF8);

            MessageBox.Show(
                "Raportul a fost exportat cu succes.",
                "Export finalizat",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Nu s-a putut exporta raportul: " + ex.Message,
                "Eroare export",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Construieste continutul unui raport tip text (uman lizibil).
    /// </summary>
    /// <param name="shows">Lista de seriale.</param>
    /// <returns>Sirul de caractere reprezentand raportul.</returns>
    private string BuildTextReport(List<AnimatedShow> shows)
    {
        var builder = new StringBuilder();

        builder.AppendLine("TOONTRACKER - RAPORT COLECTIE");
        builder.AppendLine("========================================");
        builder.AppendLine($"Data exportului: {DateTime.Now:dd.MM.yyyy HH:mm}");
        builder.AppendLine();

        builder.AppendLine("REZUMAT");
        builder.AppendLine("----------------------------------------");
        builder.AppendLine($"Total titluri: {shows.Count}");
        builder.AppendLine($"Titluri finalizate: {_statistics.CountFinished(shows)}");
        builder.AppendLine($"Scor mediu: {_statistics.AverageScore(shows)}");
        builder.AppendLine($"Episoade rămase: {_statistics.RemainingEpisodes(shows)}");
        builder.AppendLine($"Gen preferat: {_statistics.FavoriteGenre(shows)}");
        builder.AppendLine($"Episoade văzute total: {shows.Sum(s => s.WatchedEpisodes)}");
        builder.AppendLine();

        builder.AppendLine("LISTA TITLURI");
        builder.AppendLine("----------------------------------------");

        var index = 1;

        foreach (var show in shows)
        {
            builder.AppendLine($"{index}. {show.Title}");
            builder.AppendLine($"   Studio: {show.Studio}");
            builder.AppendLine($"   Gen: {show.Genre}");
            builder.AppendLine($"   Episoade văzute/total: {show.WatchedEpisodes}/{show.TotalEpisodes}");
            builder.AppendLine($"   Progres: {show.Progress}%");
            builder.AppendLine($"   Rating: {show.Rating}");
            builder.AppendLine($"   Status: {show.Status}");
            builder.AppendLine($"   Scor personal: {show.PersonalScore}/10");
            builder.AppendLine($"   Personaj favorit: {show.FavoriteCharacter}");
            builder.AppendLine($"   Note: {show.Notes}");
            builder.AppendLine();

            index++;
        }

        builder.AppendLine("========================================");
        builder.AppendLine("Raport generat automat de aplicația ToonTracker.");

        return builder.ToString();
    }

    /// <summary>
    /// Construieste continutul unui raport in format CSV.
    /// </summary>
    /// <param name="shows">Lista de seriale.</param>
    /// <returns>Sirul de caractere formatat CSV.</returns>
    private string BuildCsvReport(List<AnimatedShow> shows)
    {
        var builder = new StringBuilder();

        builder.AppendLine("Titlu,Studio,Gen,Episoade totale,Episoade văzute,Progres,Rating,Status,Scor,Personaj favorit,Note");

        foreach (var show in shows)
        {
            builder.AppendLine(string.Join(",",
                EscapeCsv(show.Title),
                EscapeCsv(show.Studio),
                EscapeCsv(show.Genre),
                show.TotalEpisodes,
                show.WatchedEpisodes,
                show.Progress,
                EscapeCsv(show.Rating.ToString()),
                EscapeCsv(show.Status.ToString()),
                show.PersonalScore,
                EscapeCsv(show.FavoriteCharacter),
                EscapeCsv(show.Notes)));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Formateaza un camp text pentru a fi valid intr-un fisier CSV.
    /// </summary>
    /// <param name="value">Valoarea de intrare.</param>
    /// <returns>Valoarea procesata pentru CSV.</returns>
    private static string EscapeCsv(string? value)
    {
        value ??= string.Empty;

        if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }

        return value;
    }

    /// <summary>
    /// Incarca date demo in serviciu, evitand duplicatele dupa titlu, 
    /// apoi reimprospateaza lista si afiseaza un mesaj de confirmare. 
    /// La eroare, afiseaza mesajul exceptiei.
    /// </summary>
    private void LoadDemoData()
    {
        if (_showService == null)
        {
            return;
        }

        try
        {
            foreach (var show in DemoShows())
            {
                if (!_showService.GetAll().Any(s => s.Title.Equals(show.Title, StringComparison.OrdinalIgnoreCase)))
                {
                    _showService.Add(show);
                }
            }

            LoadData();

            MessageBox.Show(
                "Au fost încărcate exemplele pentru prezentare.",
                "Date demo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Eroare",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    /// <summary>
    /// Reseteaza toate filtrele de cautare la valorile implicite.
    /// </summary>
    private void ResetFilters()
    {
        _searchBox.Clear();

        if (_genreFilter.Items.Count > 0)
        {
            _genreFilter.SelectedItem = "Toate genurile";
        }

        if (_statusFilter.Items.Count > 0)
        {
            _statusFilter.SelectedItem = "Toate statusurile";
        }

        if (_sortComboBox.Items.Count > 0)
        {
            _sortComboBox.SelectedItem = "Alfabetic";
        }

        LoadData();
    }

    /// <summary>
    /// Deschide dialogul pentru schimbarea temei vizuale.
    /// </summary>
    private void ChooseTheme()
    {
        using var themeForm = new Form
        {
            Text = "Alege vibe-ul aplicației",
            Width = 340,
            Height = 220,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 1,
            Padding = new Padding(12)
        };

        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var title = new Label
        {
            Text = "Selecteaza stilul vizual:",
            AutoSize = true,
            Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
            Padding = new Padding(0, 0, 0, 10)
        };

        var pinkRadio = new RadioButton
        {
            Text = "Cozy Pink",
            AutoSize = true,
            Checked = _currentTheme == AppTheme.CozyPink,
            Padding = new Padding(0, 4, 0, 4)
        };

        var darkRadio = new RadioButton
        {
            Text = "Berry Night",
            AutoSize = true,
            Checked = _currentTheme == AppTheme.BerryNight,
            Padding = new Padding(0, 4, 0, 4)
        };

        var blueRadio = new RadioButton
        {
            Text = "Ocean Blue",
            AutoSize = true,
            Checked = _currentTheme == AppTheme.OceanBlue,
            Padding = new Padding(0, 4, 0, 4)
        };

        var options = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            AutoSize = true
        };

        options.Controls.Add(pinkRadio);
        options.Controls.Add(darkRadio);
        options.Controls.Add(blueRadio);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            AutoSize = true
        };

        var okButton = new Button
        {
            Text = "Aplică",
            Width = 90,
            Height = 30
        };

        var cancelButton = new Button
        {
            Text = "Renunță",
            Width = 90,
            Height = 30
        };

        okButton.Click += (_, _) =>
        {
            if (pinkRadio.Checked)
            {
                _currentTheme = AppTheme.CozyPink;
            }
            else if (darkRadio.Checked)
            {
                _currentTheme = AppTheme.BerryNight;
            }
            else
            {
                _currentTheme = AppTheme.OceanBlue;
            }

            ApplyTheme();
            themeForm.DialogResult = DialogResult.OK;
        };

        cancelButton.Click += (_, _) =>
        {
            themeForm.DialogResult = DialogResult.Cancel;
        };

        buttons.Controls.Add(okButton);
        buttons.Controls.Add(cancelButton);

        root.Controls.Add(title, 0, 0);
        root.Controls.Add(options, 0, 1);
        root.Controls.Add(buttons, 0, 2);

        themeForm.Controls.Add(root);
        themeForm.ShowDialog(this);
    }

    /// <summary>
    /// Aplica setarile de culori si fonturi pentru tema selectata asupra tuturor controalelor.
    /// </summary>
    private void ApplyTheme()
    {
        Color background;
        Color surface;
        Color surfaceSoft;
        Color inputBackground;
        Color accent;
        Color accentHover;
        Color textPrimary;
        Color textSecondary;
        Color border;
        Color gridHeader;

        switch (_currentTheme)
        {
            case AppTheme.BerryNight:
                background = Color.FromArgb(33, 27, 37);
                surface = Color.FromArgb(49, 42, 55);
                surfaceSoft = Color.FromArgb(61, 53, 68);
                inputBackground = Color.FromArgb(70, 61, 78);
                accent = Color.FromArgb(207, 117, 161);
                accentHover = Color.FromArgb(224, 137, 181);
                textPrimary = Color.FromArgb(250, 241, 246);
                textSecondary = Color.FromArgb(222, 198, 212);
                border = Color.FromArgb(96, 83, 103);
                gridHeader = Color.FromArgb(82, 70, 90);
                break;

            case AppTheme.OceanBlue:
                background = Color.FromArgb(239, 246, 255);
                surface = Color.White;
                surfaceSoft = Color.FromArgb(224, 238, 255);
                inputBackground = Color.FromArgb(245, 250, 255);
                accent = Color.FromArgb(70, 130, 180);
                accentHover = Color.FromArgb(100, 149, 237);
                textPrimary = Color.FromArgb(36, 52, 71);
                textSecondary = Color.FromArgb(80, 100, 125);
                border = Color.FromArgb(190, 215, 240);
                gridHeader = Color.FromArgb(214, 232, 255);
                break;

            default:
                background = Color.FromArgb(255, 245, 250);
                surface = Color.White;
                surfaceSoft = Color.FromArgb(255, 237, 245);
                inputBackground = Color.FromArgb(255, 250, 252);
                accent = Color.FromArgb(232, 122, 170);
                accentHover = Color.FromArgb(244, 150, 193);
                textPrimary = Color.FromArgb(87, 52, 68);
                textSecondary = Color.FromArgb(129, 93, 108);
                border = Color.FromArgb(242, 204, 222);
                gridHeader = Color.FromArgb(252, 222, 236);
                break;
        }

        BackColor = background;
        ForeColor = textPrimary;

        if (_headerPanel != null)
        {
            _headerPanel.BackColor = accent;
            foreach (Control child in GetAllControls(_headerPanel))
            {
                child.BackColor = accent;
                child.ForeColor = Color.White;
            }
        }

        if (_controlsCard != null)
        {
            _controlsCard.BackColor = surface;
            _controlsCard.ForeColor = textPrimary;
        }

        foreach (var group in GetAllControls(this).OfType<GroupBox>())
        {
            group.BackColor = surface;
            group.ForeColor = textPrimary;
        }

        foreach (var panel in GetAllControls(this).OfType<Panel>())
        {
            if (panel == _headerPanel)
            {
                continue;
            }

            if (panel == _footerCard)
            {
                panel.BackColor = surfaceSoft;
            }
            else if (panel.Parent == _controlsCard || panel.Parent is GroupBox || IsDescendantOf(panel, _controlsCard))
            {
                panel.BackColor = surface;
            }
            else
            {
                panel.BackColor = background;
            }
        }

        foreach (var table in GetAllControls(this).OfType<TableLayoutPanel>())
        {
            if (IsDescendantOf(table, _headerPanel))
            {
                table.BackColor = accent;
            }
            else if (table.Parent == _controlsCard || table.Parent is GroupBox || IsDescendantOf(table, _controlsCard))
            {
                table.BackColor = surface;
            }
            else
            {
                table.BackColor = background;
            }

            table.ForeColor = textPrimary;
        }

        foreach (var flow in GetAllControls(this).OfType<FlowLayoutPanel>())
        {
            if (flow.Parent is GroupBox || IsDescendantOf(flow, _controlsCard))
            {
                flow.BackColor = surface;
            }
            else
            {
                flow.BackColor = background;
            }

            flow.ForeColor = textPrimary;
        }

        foreach (var button in GetAllControls(this).OfType<Button>())
        {
            var isDelete = button.Text == "Șterge";

            button.FlatStyle = FlatStyle.Flat;
            button.Cursor = Cursors.Hand;

            if (isDelete)
            {
                button.BackColor = _currentTheme switch
                {
                    AppTheme.BerryNight => Color.FromArgb(182, 95, 126),
                    AppTheme.OceanBlue => Color.FromArgb(52, 110, 160),
                    _ => Color.FromArgb(227, 114, 156)
                };

                button.ForeColor = Color.White;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = accentHover;
                button.FlatAppearance.MouseDownBackColor = accent;
            }
            else
            {
                button.BackColor = accent;
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = accentHover;
                button.FlatAppearance.MouseDownBackColor = accentHover;
            }
        }

        foreach (var label in GetAllControls(this).OfType<Label>())
        {
            if (_headerPanel != null && (label.Parent == _headerPanel || IsDescendantOf(label, _headerPanel)))
            {
                label.BackColor = accent;
                label.ForeColor = Color.White;
            }
            else if (label == _statsLabel || label.Parent == _footerCard || IsDescendantOf(label, _footerCard))
            {
                label.BackColor = surfaceSoft;
                label.ForeColor = textSecondary;
            }
            else if (label.Parent is GroupBox || IsDescendantOf(label, _controlsCard))
            {
                label.BackColor = surface;
                label.ForeColor = textPrimary;
            }
            else
            {
                label.BackColor = background;
                label.ForeColor = textPrimary;
            }
        }

        _searchBox.BackColor = inputBackground;
        _searchBox.ForeColor = textPrimary;

        _genreFilter.BackColor = inputBackground;
        _genreFilter.ForeColor = textPrimary;

        _statusFilter.BackColor = inputBackground;
        _statusFilter.ForeColor = textPrimary;

        _sortComboBox.BackColor = inputBackground;
        _sortComboBox.ForeColor = textPrimary;

        _recommendationPicker.BackColor = inputBackground;
        _recommendationPicker.ForeColor = textPrimary;

        _recommendationsBox.BackColor = inputBackground;
        _recommendationsBox.ForeColor = textPrimary;

        _grid.BackgroundColor = surface;
        _grid.GridColor = border;

        var selectedRowColor = _currentTheme switch
        {
            AppTheme.BerryNight => Color.FromArgb(78, 68, 86),
            AppTheme.OceanBlue => Color.FromArgb(200, 225, 245),
            _ => Color.FromArgb(250, 226, 238)
        };

        var alternateRowColor = _currentTheme switch
        {
            AppTheme.BerryNight => Color.FromArgb(61, 53, 68),
            AppTheme.OceanBlue => Color.FromArgb(245, 249, 255),
            _ => Color.FromArgb(255, 243, 248)
        };

        _grid.DefaultCellStyle.BackColor = inputBackground;
        _grid.DefaultCellStyle.ForeColor = textPrimary;
        _grid.DefaultCellStyle.SelectionBackColor = selectedRowColor;
        _grid.DefaultCellStyle.SelectionForeColor = _currentTheme == AppTheme.BerryNight ? Color.White : textPrimary;
        _grid.DefaultCellStyle.Padding = new Padding(6, 4, 6, 4);

        _grid.AlternatingRowsDefaultCellStyle.BackColor = alternateRowColor;
        _grid.AlternatingRowsDefaultCellStyle.ForeColor = textPrimary;
        _grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = selectedRowColor;
        _grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = _currentTheme == AppTheme.BerryNight ? Color.White : textPrimary;

        _grid.ColumnHeadersDefaultCellStyle.BackColor = gridHeader;
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = textPrimary;
        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = gridHeader;
        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = textPrimary;
        _grid.ColumnHeadersDefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Bold);
        _grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6);

        _grid.EnableHeadersVisualStyles = false;

        if (_footerCard != null)
        {
            _footerCard.BackColor = surfaceSoft;
        }
    }

    private void ApplyThemeRecursive(
        Control control,
        Color background,
        Color foreground,
        Color panelBackground,
        Color inputBackground,
        Color buttonBackground)
    {
        foreach (Control child in control.Controls)
        {
            child.ForeColor = foreground;

            if (child is Button)
            {
                child.BackColor = buttonBackground;
                child.ForeColor = foreground;
                ((Button)child).FlatStyle = FlatStyle.Standard;
            }
            else if (child is TextBox || child is ComboBox)
            {
                child.BackColor = inputBackground;
                child.ForeColor = foreground;
            }
            else if (child is DataGridView)
            {
                child.BackColor = panelBackground;
                child.ForeColor = foreground;
            }
            else if (child is GroupBox)
            {
                child.BackColor = background;
                child.ForeColor = foreground;
            }
            else if (child is Panel || child is TableLayoutPanel || child is FlowLayoutPanel)
            {
                child.BackColor = background;
                child.ForeColor = foreground;
            }
            else
            {
                child.BackColor = background;
                child.ForeColor = foreground;
            }

            if (child.HasChildren)
            {
                ApplyThemeRecursive(child, background, foreground, panelBackground, inputBackground, buttonBackground);
            }
        }
    }

    /// <summary>
    /// Selecteaza si focuseaza un serial in tabel pe baza ID-ului.
    /// </summary>
    /// <param name="id">ID-ul serialului.</param>
    private void SelectById(Guid id)
    {
        foreach (DataGridViewRow row in _grid.Rows)
        {
            if (row.DataBoundItem is AnimatedShow show && show.Id == id)
            {
                row.Selected = true;
                _grid.CurrentCell = row.Cells[0];
                break;
            }
        }
    }


    /// <summary>
    /// Creeaza o lista initiala de seriale pentru exemplificare.
    /// </summary>
    /// <returns>Lista de obiecte AnimatedShow.</returns>
    private static List<AnimatedShow> DemoShows() => new()
    {
        new AnimatedShow
        {
            Title = "Avatar: The Last Airbender",
            Studio = "Nickelodeon",
            Genre = "Aventura",
            TotalEpisodes = 61,
            WatchedEpisodes = 61,
            Rating = AgeRating.PG,
            Status = WatchStatus.Finished,
            PersonalScore = 10,
            FavoriteCharacter = "Aang",
            Notes = "Serial finalizat, foarte bun pentru recomandari."
        },
        new AnimatedShow
        {
            Title = "Gravity Falls",
            Studio = "Disney",
            Genre = "Mister",
            TotalEpisodes = 40,
            WatchedEpisodes = 18,
            Rating = AgeRating.PG,
            Status = WatchStatus.Watching,
            PersonalScore = 9,
            FavoriteCharacter = "Mabel",
            Notes = "Potrivit pentru fanii serialelor animate cu mister."
        },
        new AnimatedShow
        {
            Title = "Adventure Time",
            Studio = "Cartoon Network",
            Genre = "Comedie",
            TotalEpisodes = 283,
            WatchedEpisodes = 34,
            Rating = AgeRating.PG13,
            Status = WatchStatus.Watching,
            PersonalScore = 8,
            FavoriteCharacter = "Finn",
            Notes = "Multe episoade, bun pentru progres."
        },
        new AnimatedShow
        {
            Title = "The Amazing World of Gumball",
            Studio = "Cartoon Network",
            Genre = "Comedie",
            TotalEpisodes = 240,
            WatchedEpisodes = 58,
            Rating = AgeRating.PG,
            Status = WatchStatus.Watching,
            PersonalScore = 8,
            FavoriteCharacter = "Gumball",
            Notes = "Exemplu pentru cautare dupa studio."
        },
        new AnimatedShow
        {
            Title = "Steven Universe",
            Studio = "Cartoon Network",
            Genre = "Fantasy",
            TotalEpisodes = 160,
            WatchedEpisodes = 160,
            Rating = AgeRating.PG,
            Status = WatchStatus.Finished,
            PersonalScore = 9,
            FavoriteCharacter = "Garnet",
            Notes = "Exemplu de serial terminat."
        },
        new AnimatedShow
        {
            Title = "Phineas and Ferb",
            Studio = "Disney",
            Genre = "Comedie",
            TotalEpisodes = 189,
            WatchedEpisodes = 80,
            Rating = AgeRating.G,
            Status = WatchStatus.Watching,
            PersonalScore = 9,
            FavoriteCharacter = "Perry",
            Notes = "Recomandabil pentru prezentare."
        }
    };

    private void _filtersLayout_Paint(object sender, PaintEventArgs e)
    {

    }
}