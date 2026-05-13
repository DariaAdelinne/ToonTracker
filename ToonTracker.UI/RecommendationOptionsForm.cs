/**************************************************************************
 *                                                                        *
 *  File:        RecommendationOptions.cs                                 *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Formular pentru alegerea preferintelor folosite la recomandari custom.*
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using ToonTracker.Domain;

namespace ToonTracker.UI;

/// <summary>
/// Fereastra de dialog care permite utilizatorului sa selecteze manual criterii 
/// pentru generarea recomandarilor personalizate.
/// </summary>
public class RecommendationOptionsForm : Form
{
    private readonly CheckedListBox _genreList = new();
    private readonly CheckedListBox _studioList = new();
    private readonly ComboBox _ratingComboBox = new();
    private readonly CheckBox _shortSeriesCheckBox = new();
    private readonly CheckBox _longSeriesCheckBox = new();

    /// <summary>
    /// Lista de genuri selectate de utilizator.
    /// </summary>
    public List<string> SelectedGenres { get; private set; } = new();

    /// <summary>
    /// Lista de studiouri de productie selectate de utilizator.
    /// </summary>
    public List<string> SelectedStudios { get; private set; } = new();

    /// <summary>
    /// Clasificarea de varsta maxima pe care utilizatorul o accepta in recomandari.
    /// </summary>
    public AgeRating MaximumAcceptedRating { get; private set; } = AgeRating.TV14;

    /// <summary>
    /// Indica daca utilizatorul prefera seriale cu un numar mic de episoade.
    /// </summary>
    public bool PreferShortSeries { get; private set; }

    /// <summary>
    /// Indica daca utilizatorul prefera seriale de lunga durata.
    /// </summary>
    public bool PreferLongSeries { get; private set; }

    /// <summary>
    /// Constructorul clasei. Initializeaza proprietatile vizuale ale ferestrei.
    /// </summary>
    public RecommendationOptionsForm()
    {
        Text = "Preferinte recomandari";
        Width = 620;
        Height = 520;
        MinimumSize = new Size(560, 460);
        StartPosition = FormStartPosition.CenterParent;

        BuildUi();
    }

    /// <summary>
    /// Construieste interfata grafica a formularului si configureaza layout-ul.
    /// </summary>
    private void BuildUi()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 5,
            ColumnCount = 2,
            Padding = new Padding(12)
        };

        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        Controls.Add(root);

        // TITLU
        var title = new Label
        {
            Text = "Alege ce ai chef sa vezi",
            AutoSize = true,
            Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
            Padding = new Padding(0, 0, 0, 10),
            Dock = DockStyle.Fill
        };

        root.Controls.Add(title, 0, 0);
        root.SetColumnSpan(title, 2);

        // SECTIUNE GENURI
        var genreGroup = new GroupBox
        {
            Text = "Genuri preferate",
            Dock = DockStyle.Fill,
            Padding = new Padding(8)
        };

        _genreList.Dock = DockStyle.Fill;
        _genreList.CheckOnClick = true;
        _genreList.Items.AddRange(new object[]
        {
            "Actiune",
            "Aventura",
            "Comedie",
            "Drama",
            "Fantasy",
            "Mister",
            "SF"
        });

        genreGroup.Controls.Add(_genreList);
        root.Controls.Add(genreGroup, 0, 1);

        // SECTIUNE STUDIOURI
        var studioGroup = new GroupBox
        {
            Text = "Studiouri preferate",
            Dock = DockStyle.Fill,
            Padding = new Padding(8)
        };

        _studioList.Dock = DockStyle.Fill;
        _studioList.CheckOnClick = true;
        _studioList.Items.AddRange(new object[]
        {
            "Cartoon Network",
            "Disney",
            "DreamWorks",
            "Netflix",
            "Nickelodeon",
            "Warner Bros",
            "Adult Swim",
            "Fox"
        });

        studioGroup.Controls.Add(_studioList);
        root.Controls.Add(studioGroup, 1, 1);

        // RATING
        var ratingLabel = new Label
        {
            Text = "Rating maxim acceptat:",
            AutoSize = true,
            Padding = new Padding(0, 8, 0, 0)
        };

        root.Controls.Add(ratingLabel, 0, 2);

        _ratingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _ratingComboBox.Dock = DockStyle.Fill;
        _ratingComboBox.Items.AddRange(Enum.GetNames(typeof(AgeRating)));
        _ratingComboBox.SelectedItem = AgeRating.TV14.ToString();

        root.Controls.Add(_ratingComboBox, 1, 2);

        // PREFERINTE DURATA
        _shortSeriesCheckBox.Text = "Prefer seriale scurte, usor de terminat";
        _shortSeriesCheckBox.AutoSize = true;
        _shortSeriesCheckBox.Padding = new Padding(0, 8, 0, 0);

        _longSeriesCheckBox.Text = "Prefer seriale lungi, cu multe episoade";
        _longSeriesCheckBox.AutoSize = true;
        _longSeriesCheckBox.Padding = new Padding(0, 8, 0, 0);

        root.Controls.Add(_shortSeriesCheckBox, 0, 3);
        root.Controls.Add(_longSeriesCheckBox, 1, 3);

        // BUTOANE ACTIUNE
        var buttonsPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            AutoSize = true,
            Padding = new Padding(0, 12, 0, 0)
        };

        var okButton = new Button
        {
            Text = "Genereaza recomandari",
            Width = 170,
            Height = 32
        };

        var cancelButton = new Button
        {
            Text = "Renunta",
            Width = 90,
            Height = 32
        };

        okButton.Click += (_, _) => Confirm();
        cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;

        buttonsPanel.Controls.Add(okButton);
        buttonsPanel.Controls.Add(cancelButton);

        root.Controls.Add(buttonsPanel, 0, 4);
        root.SetColumnSpan(buttonsPanel, 2);
    }

    /// <summary>
    /// Valideaza selectiile utilizatorului si salveaza datele in proprietati inainte de a inchide dialogul.
    /// </summary>
    private void Confirm()
    {
        // Preluarea elementelor bifate din liste
        SelectedGenres = _genreList.CheckedItems
            .Cast<string>()
            .ToList();

        SelectedStudios = _studioList.CheckedItems
            .Cast<string>()
            .ToList();

        // Preluarea valorilor din controalele de tip selectie/bifa
        MaximumAcceptedRating = Enum.Parse<AgeRating>(_ratingComboBox.SelectedItem!.ToString()!);
        PreferShortSeries = _shortSeriesCheckBox.Checked;
        PreferLongSeries = _longSeriesCheckBox.Checked;

        // Validare: utilizatorul trebuie sa aleaga macar un criteriu
        if (SelectedGenres.Count == 0 &&
            SelectedStudios.Count == 0 &&
            !PreferShortSeries &&
            !PreferLongSeries)
        {
            MessageBox.Show(
                "Selecteaza cel putin un gen, studio sau tip de serial.",
                "Preferinte incomplete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        // Inchiderea ferestrei cu succes
        DialogResult = DialogResult.OK;
    }
}