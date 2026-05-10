// Author: Echipa ToonTracker
// Functionalitate: Dialog pentru adaugarea si editarea unui desen/serial animat.
using ToonTracker.Domain;
using ToonTracker.Services;

namespace ToonTracker.UI;

public class ShowForm : Form
{
    private readonly TextBox _title = new();
    private readonly TextBox _studio = new();
    private readonly TextBox _genre = new();
    private readonly NumericUpDown _total = new() { Minimum = 1, Maximum = 2000, Value = 12 };
    private readonly NumericUpDown _watched = new() { Minimum = 0, Maximum = 2000 };
    private readonly ComboBox _rating = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox _status = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly NumericUpDown _score = new() { Minimum = 0, Maximum = 10, Value = 0 };
    private readonly TextBox _character = new();
    private readonly TextBox _notes = new() { Multiline = true, Height = 80, ScrollBars = ScrollBars.Vertical };
    private bool _isLoadingFields;
    private readonly List<AgeRating> _ratingValues = Enum.GetValues(typeof(AgeRating)).Cast<AgeRating>().ToList();
    private readonly List<WatchStatus> _statusValues = Enum.GetValues(typeof(WatchStatus)).Cast<WatchStatus>().ToList();

    public AnimatedShow Show { get; private set; }

    public ShowForm(AnimatedShow? show = null)
    {
        Show = show == null ? new AnimatedShow() : new AnimatedShow
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

        Text = show == null ? "Adauga desen/serial animat" : "Editeaza desen/serial animat";
        Width = 520;
        Height = 610;
        MinimumSize = new Size(520, 610);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        BuildUi();
        FillFields();
    }

    private void BuildUi()
    {
        _rating.Items.Clear();
        _rating.Items.AddRange(_ratingValues.Cast<object>().ToArray());

        _status.Items.Clear();
        _status.Items.AddRange(_statusValues.Cast<object>().ToArray());

        _rating.SelectedIndex = 0;
        _status.SelectedIndex = 0;
        _watched.ValueChanged += (_, _) => SyncStatusAndScore();
        _total.ValueChanged += (_, _) => SyncStatusAndScore();
        _status.SelectedIndexChanged += (_, _) => SyncStatusAndScore();

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(12)
        };
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        Controls.Add(root);

        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            AutoSize = false
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        AddRow(table, "Titlu *", _title);
        AddRow(table, "Studio", _studio);
        AddRow(table, "Gen *", _genre);
        AddRow(table, "Episoade totale", _total);
        AddRow(table, "Episoade vazute", _watched);
        AddRow(table, "Rating varsta", _rating);
        AddRow(table, "Status", _status);
        AddRow(table, "Scor personal", _score);
        AddRow(table, "Personaj favorit", _character);
        AddRow(table, "Note", _notes);
        root.Controls.Add(table, 0, 0);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            AutoSize = true
        };
        var save = new Button { Text = "Salveaza", Width = 110, Height = 32 };
        var cancel = new Button { Text = "Renunta", Width = 110, Height = 32, DialogResult = DialogResult.Cancel };
        save.Click += Save_Click;
        buttons.Controls.Add(save);
        buttons.Controls.Add(cancel);
        root.Controls.Add(buttons, 0, 1);

        AcceptButton = save;
        CancelButton = cancel;
    }

    private static void AddRow(TableLayoutPanel table, string label, Control input)
    {
        var row = table.RowCount++;
        table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        table.Controls.Add(new Label
        {
            Text = label,
            AutoSize = true,
            Padding = new Padding(3, 7, 3, 7),
            Dock = DockStyle.Fill
        }, 0, row);
        input.Dock = DockStyle.Fill;
        input.Margin = new Padding(3, 4, 3, 4);
        table.Controls.Add(input, 1, row);
    }

    private void FillFields()
    {
        _isLoadingFields = true;

        _title.Text = Show.Title;
        _studio.Text = Show.Studio;
        _genre.Text = Show.Genre;

        _total.Value = Show.TotalEpisodes <= 0 ? 12 : Show.TotalEpisodes;
        _watched.Value = Math.Min(Math.Max(Show.WatchedEpisodes, _watched.Minimum), _watched.Maximum);

        var ratingIndex = _ratingValues.IndexOf(Show.Rating);
        if (ratingIndex < 0)
        {
            ratingIndex = 0;
        }

        if (_rating.Items.Count > ratingIndex)
        {
            _rating.SelectedIndex = ratingIndex;
        }

        var plannedIndex = _statusValues.IndexOf(WatchStatus.Planned);
        var statusIndex = _statusValues.IndexOf(Show.Status);

        if (statusIndex < 0)
        {
            statusIndex = plannedIndex >= 0 ? plannedIndex : 0;
        }

        if (_status.Items.Count > statusIndex)
        {
            _status.SelectedIndex = statusIndex;
        }

        _score.Value = Math.Min(Math.Max(Show.PersonalScore, _score.Minimum), _score.Maximum);

        _character.Text = Show.FavoriteCharacter;
        _notes.Text = Show.Notes;

        _isLoadingFields = false;
        SyncStatusAndScore();
    }

    private void SyncStatusAndScore()
    {
        if (_isLoadingFields)
        {
            return;
        }

        if (_status.SelectedIndex < 0 || _status.SelectedIndex >= _statusValues.Count)
        {
            return;
        }

        var watched = (int)_watched.Value;
        var total = (int)_total.Value;
        var selectedStatus = _statusValues[_status.SelectedIndex];

        if (watched == 0)
        {
            _score.Value = 0;
            _score.Enabled = false;

            var plannedIndex = _statusValues.IndexOf(WatchStatus.Planned);
            if (plannedIndex >= 0 && _status.SelectedIndex != plannedIndex)
            {
                _status.SelectedIndex = plannedIndex;
            }

            return;
        }

        _score.Enabled = true;

        if (watched == total)
        {
            var finishedIndex = _statusValues.IndexOf(WatchStatus.Finished);
            if (finishedIndex >= 0 && _status.SelectedIndex != finishedIndex)
            {
                _status.SelectedIndex = finishedIndex;
            }

            return;
        }

        if (selectedStatus == WatchStatus.Planned)
        {
            var watchingIndex = _statusValues.IndexOf(WatchStatus.Watching);
            if (watchingIndex >= 0)
            {
                _status.SelectedIndex = watchingIndex;
            }
        }
    }

    private void Save_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_watched.Value > _total.Value)
            {
                MessageBox.Show(
                    "Episoadele vazute nu pot fi mai multe decat episoadele totale.",
                    "Eroare validare",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var totalEpisodes = (int)_total.Value;
            var watchedEpisodes = (int)_watched.Value;
            var selectedStatus = _status.SelectedIndex >= 0 && _status.SelectedIndex < _statusValues.Count
            ? _statusValues[_status.SelectedIndex]
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

            Show.Title = _title.Text.Trim();
            Show.Studio = _studio.Text.Trim();
            Show.Genre = _genre.Text.Trim();
            Show.TotalEpisodes = totalEpisodes;
            Show.WatchedEpisodes = watchedEpisodes;
            Show.Rating = _rating.SelectedIndex >= 0 && _rating.SelectedIndex < _ratingValues.Count
            ? _ratingValues[_rating.SelectedIndex]
            : _ratingValues[0];
            Show.Status = selectedStatus;
            Show.PersonalScore = watchedEpisodes == 0 ? 0 : (int)_score.Value;
            Show.FavoriteCharacter = _character.Text.Trim();
            Show.Notes = _notes.Text.Trim();

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
}
