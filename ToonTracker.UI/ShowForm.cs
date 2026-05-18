/**************************************************************************
 *                                                                        *
 *  File:        ShowForm.cs                                              *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Logica formularului pentru adaugarea si editarea          *
 *               unui desen/serial animat.                                *
 *                                                                        *
 **************************************************************************/

using System.Drawing.Drawing2D;
using ToonTracker.Domain;
using ToonTracker.Services;

namespace ToonTracker.UI;

/// <summary>
/// Formular de dialog pentru crearea sau modificarea unui obiect AnimatedShow.
/// </summary>
public partial class ShowForm : Form
{
    private readonly List<AgeRating> _ratingValues = Enum.GetValues(typeof(AgeRating)).Cast<AgeRating>().ToList();
    private readonly List<WatchStatus> _statusValues = Enum.GetValues(typeof(WatchStatus)).Cast<WatchStatus>().ToList();
    private bool _isLoadingFields;

    /// <summary>
    /// Obiectul AnimatedShow creat sau editat in formular.
    /// </summary>
    public AnimatedShow Show { get; private set; }

    /// <summary>
    /// Constructor pentru adaugarea unui serial nou sau editarea unuia existent.
    /// </summary>
    /// <param name="show">Serialul editat. Daca este null, se creeaza un serial nou.</param>
    public ShowForm(AnimatedShow? show = null)
    {
        Show = show == null
            ? new AnimatedShow()
            : new AnimatedShow
            {
                Id = show.Id,
                Title = show.Title,
                Studio = show.Studio,
                Genre = show.Genre,
                TotalEpisodes = show.TotalEpisodes,
                WatchedEpisodes = show.WatchedEpisodes,
                Rating = show.Rating,
                Status = show.Status,
                PersonalScore = show.PersonalScore,
                FavoriteCharacter = show.FavoriteCharacter,
                Notes = show.Notes
            };

        InitializeComponent();

        Text = show == null ? "Adaugă desen/serial animat" : "Editează desen/serial animat";
        _titleLabel.Text = show == null ? "Adaugă titlu" : "Editează titlu";
        _subtitleLabel.Text = show == null
            ? "Completează detaliile pentru un desen sau serial animat nou."
            : "Actualizează detaliile titlului selectat din colecție.";

        LoadStaticOptions();
        FillFields();
        ApplyTheme();
        ApplyRoundedCornersToStaticControls();
    }

    private void LoadStaticOptions()
    {
        _ratingComboBox.Items.Clear();
        _ratingComboBox.Items.AddRange(_ratingValues.Cast<object>().ToArray());

        _statusComboBox.Items.Clear();
        _statusComboBox.Items.AddRange(_statusValues.Cast<object>().ToArray());

        if (_ratingComboBox.Items.Count > 0)
        {
            _ratingComboBox.SelectedIndex = 0;
        }

        var plannedIndex = _statusValues.IndexOf(WatchStatus.Planned);
        _statusComboBox.SelectedIndex = plannedIndex >= 0 ? plannedIndex : 0;
    }

    private void FillFields()
    {
        _isLoadingFields = true;

        _titleTextBox.Text = Show.Title;
        _studioTextBox.Text = Show.Studio;
        _genreTextBox.Text = Show.Genre;

        _totalNumeric.Value = Show.TotalEpisodes <= 0
            ? 12
            : Math.Min(Math.Max(Show.TotalEpisodes, _totalNumeric.Minimum), _totalNumeric.Maximum);

        _watchedNumeric.Value = Math.Min(Math.Max(Show.WatchedEpisodes, _watchedNumeric.Minimum), _watchedNumeric.Maximum);

        var ratingIndex = _ratingValues.IndexOf(Show.Rating);
        _ratingComboBox.SelectedIndex = ratingIndex >= 0 ? ratingIndex : 0;

        var statusIndex = _statusValues.IndexOf(Show.Status);
        if (statusIndex < 0)
        {
            statusIndex = _statusValues.IndexOf(WatchStatus.Planned);
        }
        _statusComboBox.SelectedIndex = statusIndex >= 0 ? statusIndex : 0;

        _scoreNumeric.Value = Math.Min(Math.Max(Show.PersonalScore, _scoreNumeric.Minimum), _scoreNumeric.Maximum);
        _favoriteCharacterTextBox.Text = Show.FavoriteCharacter;
        _notesTextBox.Text = Show.Notes;

        _isLoadingFields = false;
        SyncStatusAndScore();
    }

    private void WatchedNumeric_ValueChanged(object? sender, EventArgs e)
    {
        SyncStatusAndScore();
    }

    private void TotalNumeric_ValueChanged(object? sender, EventArgs e)
    {
        if (_watchedNumeric.Value > _totalNumeric.Value)
        {
            _watchedNumeric.Value = _totalNumeric.Value;
        }

        SyncStatusAndScore();
    }

    private void StatusComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        SyncStatusAndScore();
    }

    private void SyncStatusAndScore()
    {
        if (_isLoadingFields || _statusComboBox.SelectedIndex < 0)
        {
            return;
        }

        var watched = (int)_watchedNumeric.Value;
        var total = (int)_totalNumeric.Value;
        var selectedStatus = _statusValues[_statusComboBox.SelectedIndex];

        if (watched == 0)
        {
            _scoreNumeric.Value = 0;
            _scoreNumeric.Enabled = false;

            var plannedIndex = _statusValues.IndexOf(WatchStatus.Planned);
            if (plannedIndex >= 0 && _statusComboBox.SelectedIndex != plannedIndex)
            {
                _statusComboBox.SelectedIndex = plannedIndex;
            }

            return;
        }

        _scoreNumeric.Enabled = true;

        if (watched == total)
        {
            var finishedIndex = _statusValues.IndexOf(WatchStatus.Finished);
            if (finishedIndex >= 0 && _statusComboBox.SelectedIndex != finishedIndex)
            {
                _statusComboBox.SelectedIndex = finishedIndex;
            }

            return;
        }

        if (selectedStatus == WatchStatus.Planned)
        {
            var watchingIndex = _statusValues.IndexOf(WatchStatus.Watching);
            if (watchingIndex >= 0)
            {
                _statusComboBox.SelectedIndex = watchingIndex;
            }
        }
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        SaveShow();
    }

    private void CancelButton_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void SaveShow()
    {
        try
        {
            if (_watchedNumeric.Value > _totalNumeric.Value)
            {
                MessageBox.Show(
                    "Episoadele văzute nu pot fi mai multe decât episoadele totale.",
                    "Eroare validare",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var totalEpisodes = (int)_totalNumeric.Value;
            var watchedEpisodes = (int)_watchedNumeric.Value;
            var selectedStatus = _statusComboBox.SelectedIndex >= 0 && _statusComboBox.SelectedIndex < _statusValues.Count
                ? _statusValues[_statusComboBox.SelectedIndex]
                : WatchStatus.Planned;

            if (watchedEpisodes == 0)
            {
                selectedStatus = WatchStatus.Planned;
            }
            else if (watchedEpisodes == totalEpisodes)
            {
                selectedStatus = WatchStatus.Finished;
            }
            else if (selectedStatus == WatchStatus.Planned)
            {
                selectedStatus = WatchStatus.Watching;
            }

            Show.Title = _titleTextBox.Text.Trim();
            Show.Studio = _studioTextBox.Text.Trim();
            Show.Genre = _genreTextBox.Text.Trim();
            Show.TotalEpisodes = totalEpisodes;
            Show.WatchedEpisodes = watchedEpisodes;
            Show.Rating = _ratingComboBox.SelectedIndex >= 0 && _ratingComboBox.SelectedIndex < _ratingValues.Count
                ? _ratingValues[_ratingComboBox.SelectedIndex]
                : _ratingValues[0];
            Show.Status = selectedStatus;
            Show.PersonalScore = watchedEpisodes == 0 ? 0 : (int)_scoreNumeric.Value;
            Show.FavoriteCharacter = _favoriteCharacterTextBox.Text.Trim();
            Show.Notes = _notesTextBox.Text.Trim();

            ShowValidator.Validate(Show);

            DialogResult = DialogResult.OK;
            Close();
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
        else if (ReferenceEquals(sender, _formCard))
        {
            ApplyRoundedCorners(_formCard, 18);
        }
    }

    private void ApplyRoundedCornersToStaticControls()
    {
        ApplyRoundedCorners(_headerPanel, 22);
        ApplyRoundedCorners(_formCard, 18);
        ApplyRoundedCorners(_saveButton, 13);
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
        var background = Color.FromArgb(255, 245, 250);
        var surface = Color.White;
        var surfaceSoft = Color.FromArgb(255, 237, 245);
        var inputBackground = Color.FromArgb(255, 250, 252);
        var accent = Color.FromArgb(232, 122, 170);
        var accentHover = Color.FromArgb(244, 150, 193);
        var textPrimary = Color.FromArgb(87, 52, 68);
        var textSecondary = Color.FromArgb(129, 93, 108);
        var border = Color.FromArgb(242, 204, 222);

        BackColor = background;
        ForeColor = textPrimary;

        _headerPanel.BackColor = accent;
        _titleLabel.BackColor = accent;
        _titleLabel.ForeColor = Color.White;
        _subtitleLabel.BackColor = accent;
        _subtitleLabel.ForeColor = Color.White;

        _formCard.BackColor = surface;
        _formLayout.BackColor = surface;
        _buttonsPanel.BackColor = surface;
        _mainFieldsLayout.BackColor = surface;
        _progressFieldsLayout.BackColor = surface;

        foreach (var label in GetAllControls(this).OfType<Label>())
        {
            if (label.Parent == _headerLayout || label == _titleLabel || label == _subtitleLabel)
            {
                label.BackColor = accent;
                label.ForeColor = Color.White;
            }
            else
            {
                label.BackColor = surface;
                label.ForeColor = label == _hintLabel ? textSecondary : textPrimary;
            }
        }

        foreach (var input in GetAllControls(this).Where(c => c is TextBox || c is ComboBox || c is NumericUpDown))
        {
            input.BackColor = inputBackground;
            input.ForeColor = textPrimary;
        }

        _notesTextBox.BackColor = inputBackground;
        _notesTextBox.ForeColor = textPrimary;

        foreach (var button in GetAllControls(this).OfType<Button>())
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
        _cancelButton.FlatAppearance.BorderSize = 1;
        _cancelButton.FlatAppearance.BorderColor = border;
        _cancelButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 229, 241);
        _cancelButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 221, 236);
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
}
