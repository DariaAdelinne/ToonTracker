/**************************************************************************
 *                                                                        *
 *  File:        MainForm.cs                                              *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Formularul principal pentru                              *
 *  gestionarea desenelor animate si serialelor.                          *
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
using ToonTracker.Services;
using System.Drawing.Drawing2D;

namespace ToonTracker.UI;
public enum AppTheme
{
    CozyPink,
    BerryNight,
    OceanBlue
}
public class MainForm : Form
{
    private readonly ShowService _showService;
    private readonly StatisticsService _statistics = new();
    private readonly SmartRecommendationStrategy _smartStrategy = new SmartRecommendationStrategy();

    private readonly DataGridView _grid = new();
    private readonly TextBox _searchBox = new();
    private readonly ComboBox _genreFilter = new();
    private readonly ComboBox _statusFilter = new();
    private readonly ComboBox _sortComboBox = new();
    private readonly Label _statsLabel = new();
    private readonly BindingSource _bindingSource = new();
    private readonly TextBox _recommendationsBox = new();
    private readonly ComboBox _recommendationPicker = new();
    private readonly List<AnimatedShow> _lastRecommendations = new();
    private Panel? _headerPanel;
    private Panel? _controlsCard;
    private GroupBox? _collectionGroup;
    private GroupBox? _recommendationsGroup;
    private AppTheme _currentTheme = AppTheme.CozyPink;
    private readonly List<Button> _mainButtons = new();
    private readonly List<Label> _mainLabels = new();
    private readonly List<GroupBox> _mainGroupBoxes = new();

    public MainForm()
    {
        _showService = null!;
        Text = "ToonTracker - jurnal pentru desene animate si seriale";
        Width = 1100;
        Height = 680;
        StartPosition = FormStartPosition.CenterScreen;
        BuildUi();
    }

    public MainForm(ShowService showService)
    {
        _showService = showService;
        Text = "ToonTracker - jurnal pentru desene animate si seriale";
        MinimumSize = new Size(980, 720);
        Width = 1200;
        Height = 750;
        StartPosition = FormStartPosition.CenterScreen;

        BuildUi();
        SeedDemoDataIfEmpty();
        LoadData();
    }

    private void BuildUi()
    {
        Controls.Clear();
        _mainButtons.Clear();
        _mainLabels.Clear();
        _mainGroupBoxes.Clear();

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1,
            Padding = new Padding(18, 14, 18, 14)
        };

        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 74));      // header
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 185));     // butoane si filtre    
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));      // tabel + recomandari
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));      // statistici jos

        Controls.Add(root);

        // HEADER
        _headerPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Height = 64,
            Padding = new Padding(22, 10, 22, 10),
            Margin = new Padding(0, 0, 0, 10)
        };
        ApplyRoundedCorners(_headerPanel, 22);
        _headerPanel.Resize += (_, _) => ApplyRoundedCorners(_headerPanel, 22);

        var headerLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 1,
            ColumnCount = 1
        };

        var title = new Label
        {
            Text = "ToonTracker",
            AutoSize = true,
            Font = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold),
            Margin = new Padding(0),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        _mainLabels.Add(title);

        headerLayout.Controls.Add(title, 0, 0);
        _headerPanel.Controls.Add(headerLayout);
        root.Controls.Add(_headerPanel, 0, 0);

        // BUTTONS + FILTERS CARD
        _controlsCard = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8, 6, 8, 6),
            Margin = new Padding(0, 0, 0, 10)
        };
        ApplyRoundedCorners(_controlsCard, 18);
        _controlsCard.Resize += (_, _) => ApplyRoundedCorners(_controlsCard, 18);

        var controlsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 1
        };

        // Layout responsive: cele 3 zone se redimensioneaza proportional cu fereastra.
        controlsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24)); // Administrare
        controlsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43)); // Descopera
        controlsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33)); // Filtre

        _controlsCard.Controls.Add(controlsLayout);
        root.Controls.Add(_controlsCard, 0, 1);

        // Buttons
        var addButton = CreateStyledButton("Adauga", 78);
        var editButton = CreateStyledButton("Editeaza", 78);
        var deleteButton = CreateStyledButton("Sterge", 78);
        var watchedButton = CreateStyledButton("+1 episod", 82);

        var recommendButton = CreateStyledButton("Recomandari", 110);
        var customRecommendButton = CreateStyledButton("Preferinte", 100);
        var statisticsButton = CreateStyledButton("Statistici", 90);
        var exportButton = CreateStyledButton("Export", 80);
        var themeButton = CreateStyledButton("Tema", 70);
        var helpButton = CreateStyledButton("Help", 70);

        var resetButton = CreateStyledButton("Reset", 70);
        resetButton.Dock = DockStyle.Fill;
        var addToWishlistButton = CreateStyledButton("Adauga in wishlist", 150);

        _mainButtons.AddRange(new[]
        {
    addButton,
    editButton,
    deleteButton,
    watchedButton,
    recommendButton,
    customRecommendButton,
    statisticsButton,
    exportButton,
    themeButton,
    helpButton,
    resetButton,
    addToWishlistButton
});

        addButton.Click += (_, _) => AddShow();
        editButton.Click += (_, _) => EditSelected();
        deleteButton.Click += (_, _) => DeleteSelected();
        watchedButton.Click += (_, _) => MarkWatched();
        recommendButton.Click += (_, _) => ShowRecommendations();
        customRecommendButton.Click += (_, _) => ShowCustomRecommendations();
        statisticsButton.Click += (_, _) => ShowStatistics();
        exportButton.Click += (_, _) => ExportReport();
        themeButton.Click += (_, _) => ChooseTheme();
        helpButton.Click += (_, _) => MessageBox.Show(
            HelpText.Content,
            "Ajutor ToonTracker",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        resetButton.Click += (_, _) => ResetFilters();
        addToWishlistButton.Click += (_, _) => AddSelectedRecommendationToWishlist();

        var actionsGroup = CreateSectionGroup("Administrare");
        var discoverGroup = CreateSectionGroup("Descopera");
        var filtersGroup = CreateSectionGroup("Cautare & filtre");

        _mainGroupBoxes.Add(actionsGroup);
        _mainGroupBoxes.Add(discoverGroup);
        _mainGroupBoxes.Add(filtersGroup);

        controlsLayout.Controls.Add(actionsGroup, 0, 0);
        controlsLayout.Controls.Add(discoverGroup, 1, 0);
        controlsLayout.Controls.Add(filtersGroup, 2, 0);

        var actionsCenterPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 1,
            ColumnCount = 1,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };

        var actionsFlow = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = true,
            AutoScroll = false,
            FlowDirection = FlowDirection.LeftToRight,
            Margin = new Padding(0),
            Padding = new Padding(0),
            Anchor = AnchorStyles.None
        };

        actionsFlow.Controls.Add(addButton);
        actionsFlow.Controls.Add(editButton);
        actionsFlow.Controls.Add(deleteButton);
        actionsFlow.Controls.Add(watchedButton);

        actionsCenterPanel.Controls.Add(actionsFlow, 0, 0);
        actionsGroup.Controls.Add(actionsCenterPanel);

        var discoverCenterPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 1,
            ColumnCount = 1,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };

        var discoverFlow = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = true,
            AutoScroll = false,
            FlowDirection = FlowDirection.LeftToRight,
            Margin = new Padding(0),
            Padding = new Padding(0),
            Anchor = AnchorStyles.None
        };

        discoverFlow.Controls.Add(recommendButton);
        discoverFlow.Controls.Add(customRecommendButton);
        discoverFlow.Controls.Add(statisticsButton);
        discoverFlow.Controls.Add(exportButton);
        discoverFlow.Controls.Add(themeButton);
        discoverFlow.Controls.Add(helpButton);

        discoverCenterPanel.Controls.Add(discoverFlow, 0, 0);
        discoverGroup.Controls.Add(discoverCenterPanel);

        // FILTERS
        _searchBox.Dock = DockStyle.Fill;
        _searchBox.PlaceholderText = "Cauta titlu, gen, studio...";
        _searchBox.TextChanged += (_, _) => ApplyFilters();
        StyleTextBox(_searchBox);

        _genreFilter.Dock = DockStyle.Fill;
        _genreFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        _genreFilter.SelectedIndexChanged += (_, _) => ApplyFilters();
        StyleComboBox(_genreFilter);

        _statusFilter.Dock = DockStyle.Fill;
        _statusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        _statusFilter.SelectedIndexChanged += (_, _) => ApplyFilters();
        StyleComboBox(_statusFilter);

        _sortComboBox.Dock = DockStyle.Fill;
        _sortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _sortComboBox.Items.Clear();
        _sortComboBox.Items.AddRange(new object[]
        {
    "Alfabetic",
    "Numar episoade",
    "Progres",
    "Scor"
        });
        _sortComboBox.SelectedIndex = 0;
        _sortComboBox.SelectedIndexChanged += (_, _) => ApplyFilters();
        StyleComboBox(_sortComboBox);

        var searchLabel = CreateSmallLabel("Cautare");
        var genreLabel = CreateSmallLabel("Gen");
        var statusLabel = CreateSmallLabel("Status");
        var sortLabel = CreateSmallLabel("Ordonare");

        _mainLabels.Add(searchLabel);
        _mainLabels.Add(genreLabel);
        _mainLabels.Add(statusLabel);
        _mainLabels.Add(sortLabel);

        var filtersLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 6,
            Margin = new Padding(0)
        };

        filtersLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        filtersLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22)); // label cautare
        filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34)); // textbox cautare
        filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22)); // label gen/status
        filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34)); // combo gen/status
        filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22)); // label ordonare
        filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36)); // combo ordonare + reset

        filtersLayout.Controls.Add(searchLabel, 0, 0);
        filtersLayout.SetColumnSpan(searchLabel, 2);

        filtersLayout.Controls.Add(_searchBox, 0, 1);
        filtersLayout.SetColumnSpan(_searchBox, 2);

        filtersLayout.Controls.Add(genreLabel, 0, 2);
        filtersLayout.Controls.Add(statusLabel, 1, 2);

        filtersLayout.Controls.Add(_genreFilter, 0, 3);
        filtersLayout.Controls.Add(_statusFilter, 1, 3);

        filtersLayout.Controls.Add(sortLabel, 0, 4);
        filtersLayout.Controls.Add(new Panel(), 1, 4);

        filtersLayout.Controls.Add(_sortComboBox, 0, 5);
        filtersLayout.Controls.Add(resetButton, 1, 5);

        filtersGroup.Controls.Add(filtersLayout);

        // MAIN CONTENT
        var contentLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1
        };
        contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 63));
        contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 37));

        root.Controls.Add(contentLayout, 0, 2);

        _collectionGroup = CreateSectionGroup("Colectia mea");
        _recommendationsGroup = CreateSectionGroup("Recomandari inteligente & Wishlist");

        _mainGroupBoxes.Add(_collectionGroup);
        _mainGroupBoxes.Add(_recommendationsGroup);

        contentLayout.Controls.Add(_collectionGroup, 0, 0);
        contentLayout.Controls.Add(_recommendationsGroup, 0, 1);

        _grid.Dock = DockStyle.Fill;
        _grid.AutoGenerateColumns = false;
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.ReadOnly = true;
        _grid.MultiSelect = false;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        _grid.ScrollBars = ScrollBars.Both;
        _grid.RowHeadersVisible = false;
        _grid.BorderStyle = BorderStyle.None;
        _grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        _grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single; ;
        _grid.RowTemplate.Height = 32;
        _grid.ColumnHeadersHeight = 34;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.DefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.8f);
        _grid.ColumnHeadersDefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.8f, FontStyle.Bold);
        _grid.DataSource = _bindingSource;
        _grid.CellDoubleClick += (_, _) => EditSelected();

        if (_grid.Columns.Count == 0)
        {
            AddGridColumn("Title", "Titlu", 180);
            AddGridColumn("Studio", "Studio", 130);
            AddGridColumn("Genre", "Gen", 120);
            AddGridColumn("TotalEpisodes", "Episoade", 80);
            AddGridColumn("WatchedEpisodes", "Vazute", 80);
            AddGridColumn("Progress", "Progres %", 90);
            AddGridColumn("Rating", "Rating", 80);
            AddGridColumn("Status", "Status", 100);
            AddGridColumn("PersonalScore", "Scor", 70);
            AddGridColumn("FavoriteCharacter", "Personaj favorit", 140);
            AddGridColumn("Notes", "Note", 200);
        }

        _collectionGroup.Controls.Add(_grid);

        var recommendationsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1
        };
        recommendationsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        recommendationsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _recommendationsBox.Dock = DockStyle.Fill;
        _recommendationsBox.Multiline = true;
        _recommendationsBox.ReadOnly = true;
        _recommendationsBox.ScrollBars = ScrollBars.Vertical;
        _recommendationsBox.BorderStyle = BorderStyle.FixedSingle;
        _recommendationsBox.Font = new Font(FontFamily.GenericSansSerif, 9f);
        _recommendationsBox.Text =
            "Apasa Recomandari pentru sugestii pe baza titlurilor finalizate sau Recomandari custom pentru preferinte manuale.";

        recommendationsLayout.Controls.Add(_recommendationsBox, 0, 0);

        var wishlistPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Padding = new Padding(0, 8, 0, 0),
            WrapContents = true
        };

        var pickerLabel = CreateSmallLabel("Recomandare selectata:");
        _mainLabels.Add(pickerLabel);

        _recommendationPicker.Width = 260;
        _recommendationPicker.DropDownStyle = ComboBoxStyle.DropDownList;
        StyleComboBox(_recommendationPicker);

        wishlistPanel.Controls.Add(pickerLabel);
        wishlistPanel.Controls.Add(_recommendationPicker);
        wishlistPanel.Controls.Add(addToWishlistButton);

        recommendationsLayout.Controls.Add(wishlistPanel, 0, 1);
        _recommendationsGroup.Controls.Add(recommendationsLayout);

        // FOOTER STATS
        var footerCard = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(16, 8, 16, 8),
            Margin = new Padding(0, 10, 0, 0)
        };
        ApplyRoundedCorners(footerCard, 18);
        footerCard.Resize += (_, _) => ApplyRoundedCorners(footerCard, 18);

        _statsLabel.Dock = DockStyle.Fill;
        _statsLabel.AutoSize = false;
        _statsLabel.TextAlign = ContentAlignment.MiddleLeft;
        _statsLabel.Font = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Bold);

        _mainLabels.Add(_statsLabel);

        footerCard.Controls.Add(_statsLabel);
        root.Controls.Add(footerCard, 0, 3);

        ApplyTheme();
    }

    private GroupBox CreateSectionGroup(string text)
    {
        return new GroupBox
        {
            Text = text,
            Dock = DockStyle.Fill,
            Padding = new Padding(10, 16, 10, 8),
            Font = new Font(FontFamily.GenericSansSerif, 8.8f, FontStyle.Bold),
            Margin = new Padding(4)
        };
    }

    private Button CreateStyledButton(string text, int width = 120)
    {
        var button = new Button
        {
            Text = text,
            Width = width,
            Height = 30,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Font = new Font(FontFamily.GenericSansSerif, 8.2f, FontStyle.Bold),
            Margin = new Padding(4, 3, 4, 3),
            UseVisualStyleBackColor = false
        };

        button.FlatAppearance.BorderSize = 0;
        ApplyRoundedCorners(button, 13);
        button.Resize += (_, _) => ApplyRoundedCorners(button, 13);

        return button;
    }

    private Label CreateSmallLabel(string text)
    {
        return new Label
        {
            Text = text,
            AutoSize = true,
            Font = new Font(FontFamily.GenericSansSerif, 8.4f, FontStyle.Bold),
            Margin = new Padding(0, 2, 0, 4)
        };
    }

    private void StyleTextBox(TextBox textBox)
    {
        textBox.BorderStyle = BorderStyle.FixedSingle;
        textBox.Font = new Font(FontFamily.GenericSansSerif, 9f);
        textBox.Margin = new Padding(0, 0, 8, 6);
        textBox.Height = 30;
    }

    private void StyleComboBox(ComboBox comboBox)
    {
        comboBox.FlatStyle = FlatStyle.Flat;
        comboBox.Font = new Font(FontFamily.GenericSansSerif, 9f);
        comboBox.Margin = new Padding(0, 0, 8, 6);
        comboBox.Height = 30;
    }

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

    private void AddGridColumn(string propertyName, string header, int width)
    {
        var column = new DataGridViewTextBoxColumn
        {
            DataPropertyName = propertyName,
            HeaderText = header,
            MinimumWidth = Math.Min(width, 70),
            Width = width,
            SortMode = DataGridViewColumnSortMode.NotSortable
        };

        if (propertyName == "Notes")
        {
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.MinimumWidth = 220;
        }
        else
        {
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        _grid.Columns.Add(column);
    }

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
                "Eroare la incarcarea datelor demo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

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

    private void GenreFilterChanged(object? sender, EventArgs e)
    {
        ApplyFilters();
    }

    private void StatusFilterChanged(object? sender, EventArgs e)
    {
        ApplyFilters();
    }

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

    private List<AnimatedShow> SortShows(List<AnimatedShow> shows)
    {
        var selectedSort = _sortComboBox.SelectedItem?.ToString() ?? "Alfabetic";

        return selectedSort switch
        {
            "Numar episoade" => shows
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

    private void UpdateStats(List<AnimatedShow> shows)
    {
        _statsLabel.Text =
            $"Titluri afisate: {shows.Count} | " +
            $"Finalizate: {_statistics.CountFinished(shows)} | " +
            $"Scor mediu: {_statistics.AverageScore(shows)} | " +
            $"Episoade ramase: {_statistics.RemainingEpisodes(shows)} | " +
            $"Gen preferat: {_statistics.FavoriteGenre(shows)}";
    }

    private AnimatedShow? SelectedShow()
    {
        if (_grid.CurrentRow == null)
        {
            return null;
        }

        return _grid.CurrentRow.DataBoundItem as AnimatedShow;
    }

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
                "Selecteaza mai intai un titlu din tabel.",
                "Atentie",
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
                "Selecteaza mai intai un titlu din tabel.",
                "Atentie",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        var confirmation = MessageBox.Show(
            $"Sigur stergi '{selected.Title}'?",
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
                "Selecteaza mai intai un titlu din tabel.",
                "Atentie",
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
                    "Nu exista titluri finalizate. Recomandarile automate se bazeaza strict pe ce ai terminat.\r\n\r\n" +
                    "Poti folosi butonul Recomandari custom pentru a alege manual ce ai chef sa vezi.";
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
                "Recomandari generate automat doar pe baza titlurilor finalizate:");
        }
        catch (Exception ex)
        {
            _recommendationsBox.Text = "Eroare la generarea recomandarilor: " + ex.Message;
            ClearRecommendationPicker();
        }
    }

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
                "Recomandari generate pe baza preferintelor alese manual:");
        }
        catch (Exception ex)
        {
            _recommendationsBox.Text = "Eroare la generarea recomandarilor custom: " + ex.Message;
            ClearRecommendationPicker();
        }
    }

    private void DisplayRecommendations(List<AnimatedShow> recommendations, string title)
    {
        _lastRecommendations.Clear();
        _lastRecommendations.AddRange(recommendations);

        if (recommendations.Count == 0)
        {
            _recommendationsBox.Text =
                "Nu exista recomandari noi. Toate titlurile potrivite sunt deja in lista ta.";
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
            text += $"   Motiv: {show.Notes.Replace("Recomandare automata: ", "")}" + Environment.NewLine;
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

    private void ClearRecommendationPicker()
    {
        _lastRecommendations.Clear();
        _recommendationPicker.DataSource = null;
        _recommendationPicker.Items.Clear();
    }

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
                "Recomandarea selectata nu mai este disponibila.",
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
                "Titlul exista deja in lista ta.",
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
                Notes = "Adaugat in wishlist din recomandarile inteligente."
            };

            _showService.Add(wishlistShow);
            LoadData();

            _lastRecommendations.Remove(recommendation);

            DisplayRecommendations(
                _lastRecommendations.ToList(),
                "Recomandari ramase:");

            MessageBox.Show(
                $"'{wishlistShow.Title}' a fost adaugat in wishlist cu status Planned.",
                "Wishlist actualizat",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Nu s-a putut adauga recomandarea in wishlist: " + ex.Message,
                "Eroare wishlist",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

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
                "Nu s-au putut afisa statisticile: " + ex.Message,
                "Eroare statistici",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

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
                    "Nu exista titluri de exportat. Adauga mai intai cateva desene sau seriale.",
                    "Export raport",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            shows = SortShows(shows);

            using var saveDialog = new SaveFileDialog
            {
                Title = "Export raport ToonTracker",
                Filter = "Raport text (*.txt)|*.txt|Fisier CSV pentru Excel (*.csv)|*.csv",
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
        builder.AppendLine($"Episoade ramase: {_statistics.RemainingEpisodes(shows)}");
        builder.AppendLine($"Gen preferat: {_statistics.FavoriteGenre(shows)}");
        builder.AppendLine($"Episoade vazute total: {shows.Sum(s => s.WatchedEpisodes)}");
        builder.AppendLine();

        builder.AppendLine("LISTA TITLURI");
        builder.AppendLine("----------------------------------------");

        var index = 1;

        foreach (var show in shows)
        {
            builder.AppendLine($"{index}. {show.Title}");
            builder.AppendLine($"   Studio: {show.Studio}");
            builder.AppendLine($"   Gen: {show.Genre}");
            builder.AppendLine($"   Episoade vazute/total: {show.WatchedEpisodes}/{show.TotalEpisodes}");
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
        builder.AppendLine("Raport generat automat de aplicatia ToonTracker.");

        return builder.ToString();
    }

    private string BuildCsvReport(List<AnimatedShow> shows)
    {
        var builder = new StringBuilder();

        builder.AppendLine("Titlu,Studio,Gen,Episoade totale,Episoade vazute,Progres,Rating,Status,Scor,Personaj favorit,Note");

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
                "Au fost incarcate exemplele pentru prezentare.",
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

    private void ChooseTheme()
    {
        using var themeForm = new Form
        {
            Text = "Alege vibe-ul aplicatiei",
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
            Text = "Aplica",
            Width = 90,
            Height = 30
        };

        var cancelButton = new Button
        {
            Text = "Renunta",
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

        foreach (var group in _mainGroupBoxes)
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

            if (panel.Parent == _controlsCard || panel.Parent is GroupBox)
            {
                panel.BackColor = surface;
            }
            else if (panel.Controls.Contains(_statsLabel))
            {
                panel.BackColor = surfaceSoft;
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
            else if (table.Parent == _controlsCard || table.Parent is GroupBox)
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
            if (flow.Parent is GroupBox || flow.Parent == _controlsCard)
            {
                flow.BackColor = surface;
            }
            else
            {
                flow.BackColor = background;
            }

            flow.ForeColor = textPrimary;
        }

        foreach (var button in _mainButtons)
        {
            var useSecondaryStyle =
                button.Text == "Tema" ||
                button.Text == "Help" ||
                button.Text == "Reset";

            var isDelete = button.Text == "Sterge";

            if (useSecondaryStyle)
            {
                button.BackColor = surfaceSoft;
                button.ForeColor = textPrimary;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = border;
                button.FlatAppearance.MouseOverBackColor = accentHover;
                button.FlatAppearance.MouseDownBackColor = accent;
            }
            else if (isDelete)
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

        foreach (var label in _mainLabels)
        {
            if (_headerPanel != null && (label.Parent == _headerPanel || IsDescendantOf(label, _headerPanel)))
            {
                label.BackColor = accent;
                label.ForeColor = Color.White;
            }
            else if (label == _statsLabel || (label.Parent != null && label.Parent.Controls.Contains(_statsLabel)))
            {
                label.BackColor = surfaceSoft;
                label.ForeColor = textSecondary;
            }
            else if (label.Parent is GroupBox || label.Parent == _controlsCard)
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

        if (_statsLabel.Parent != null)
        {
            _statsLabel.Parent.BackColor = surfaceSoft;
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
}