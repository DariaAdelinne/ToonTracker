/**************************************************************************
 *                                                                        *
 *  File:        RecommendationOptionsForm.cs                             *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Logica formularului pentru preferinte de recomandare.     *
 *                                                                        *
 **************************************************************************/

using System.Drawing.Drawing2D;
using ToonTracker.Domain;

namespace ToonTracker.UI;

/// <summary>
/// Fereastra de dialog care permite utilizatorului sa selecteze manual criterii
/// pentru generarea recomandarilor personalizate.
/// </summary>
public partial class RecommendationOptionsForm : Form
{
    private readonly AppTheme _currentTheme;

    public List<string> SelectedGenres { get; private set; } = new();
    public List<string> SelectedStudios { get; private set; } = new();
    public AgeRating MaximumAcceptedRating { get; private set; } = AgeRating.TV14;
    public bool PreferShortSeries { get; private set; }
    public bool PreferLongSeries { get; private set; }

    public RecommendationOptionsForm() : this(AppTheme.CozyPink)
    {
    }

    public RecommendationOptionsForm(AppTheme currentTheme)
    {
        _currentTheme = currentTheme;

        InitializeComponent();
        LoadStaticOptions();
        ApplyTheme();
        ApplyRoundedCornersToStaticControls();
    }

    private void LoadStaticOptions()
    {
        _genreList.Items.Clear();
        _genreList.Items.AddRange(new object[]
        {
            "Acțiune",
            "Aventură",
            "Comedie",
            "Drama",
            "Fantasy",
            "Mister",
            "SF"
        });

        _studioList.Items.Clear();
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

        _ratingComboBox.Items.Clear();
        _ratingComboBox.Items.AddRange(Enum.GetNames(typeof(AgeRating)));
        _ratingComboBox.SelectedItem = AgeRating.TV14.ToString();
    }

    private void ConfirmButton_Click(object? sender, EventArgs e)
    {
        Confirm();
    }

    private void CancelButton_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void Confirm()
    {
        SelectedGenres = _genreList.CheckedItems.Cast<string>().ToList();
        SelectedStudios = _studioList.CheckedItems.Cast<string>().ToList();
        MaximumAcceptedRating = Enum.Parse<AgeRating>(_ratingComboBox.SelectedItem!.ToString()!);
        PreferShortSeries = _shortSeriesCheckBox.Checked;
        PreferLongSeries = _longSeriesCheckBox.Checked;

        if (SelectedGenres.Count == 0 &&
            SelectedStudios.Count == 0 &&
            !PreferShortSeries &&
            !PreferLongSeries)
        {
            MessageBox.Show(
                "Selectează cel puțin un gen, studio sau tip de serial.",
                "Preferinte incomplete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        DialogResult = DialogResult.OK;
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
        else if (ReferenceEquals(sender, _optionsCard))
        {
            ApplyRoundedCorners(_optionsCard, 18);
        }
    }

    private void ApplyRoundedCornersToStaticControls()
    {
        ApplyRoundedCorners(_headerPanel, 22);
        ApplyRoundedCorners(_optionsCard, 18);
        ApplyRoundedCorners(_confirmButton, 13);
        ApplyRoundedCorners(_cancelButton, 13);
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
                break;
        }

        BackColor = background;
        ForeColor = textPrimary;

        _headerPanel.BackColor = accent;
        _titleLabel.BackColor = accent;
        _titleLabel.ForeColor = Color.White;
        _subtitleLabel.BackColor = accent;
        _subtitleLabel.ForeColor = Color.White;

        _optionsCard.BackColor = surface;
        _optionsCard.ForeColor = textPrimary;
        _optionsLayout.BackColor = surface;
        _buttonsPanel.BackColor = surface;

        foreach (var group in new[] { _genreGroup, _studioGroup, _ratingGroup, _durationGroup })
        {
            group.BackColor = surface;
            group.ForeColor = textPrimary;
        }

        foreach (var label in new[] { _ratingLabel, _durationHintLabel })
        {
            label.BackColor = surface;
            label.ForeColor = textSecondary;
        }

        foreach (var checkBox in new[] { _shortSeriesCheckBox, _longSeriesCheckBox })
        {
            checkBox.BackColor = surface;
            checkBox.ForeColor = textPrimary;
        }

        foreach (var list in new[] { _genreList, _studioList })
        {
            list.BackColor = inputBackground;
            list.ForeColor = textPrimary;
            list.BorderStyle = BorderStyle.FixedSingle;
        }

        _ratingComboBox.BackColor = inputBackground;
        _ratingComboBox.ForeColor = textPrimary;

        foreach (var button in new[] { _confirmButton, _cancelButton })
        {
            button.FlatStyle = FlatStyle.Flat;
            button.Cursor = Cursors.Hand;
            button.BackColor = accent;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = accentHover;
            button.FlatAppearance.MouseDownBackColor = accentHover;
        }

        _cancelButton.BackColor = surfaceSoft;
        _cancelButton.ForeColor = textPrimary;
        _cancelButton.FlatAppearance.BorderColor = border;
    }
}
