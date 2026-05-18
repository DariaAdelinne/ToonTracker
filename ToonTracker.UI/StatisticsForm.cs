/**************************************************************************
 *                                                                        *
 *  File:        StatisticsForm.cs                                        *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Logica formularului pentru afisarea statisticilor.        *
 *                                                                        *
 **************************************************************************/

using System.Drawing.Drawing2D;
using ToonTracker.Domain;

namespace ToonTracker.UI;

/// <summary>
/// Formular responsabil pentru afisarea statisticilor vizuale si textuale
/// referitoare la colectia de desene animate a utilizatorului.
/// </summary>
public partial class StatisticsForm : Form
{
    private readonly List<AnimatedShow> _shows;
    private readonly AppTheme _currentTheme;

    public StatisticsForm(IEnumerable<AnimatedShow> shows, AppTheme currentTheme)
    {
        _shows = shows.ToList();
        _currentTheme = currentTheme;

        InitializeComponent();
        LoadStatistics();
        ApplyTheme();
        ApplyRoundedCornersToStaticControls();
    }

    private void LoadStatistics()
    {
        _summaryLabel.Text = BuildSummaryText();

        if (_shows.Count == 0)
        {
            _emptyLabel.Visible = true;
            _tabs.Visible = false;
            return;
        }

        _emptyLabel.Visible = false;
        _tabs.Visible = true;

        AddChart(_genreTab, BuildGenreData(), "Număr de titluri pe gen");
        AddChart(_studioTab, BuildStudioData(), "Număr de titluri pe studio");
        AddChart(_ratingTab, BuildRatingData(), "Număr de titluri pe rating");
        AddChart(_statusTab, BuildStatusData(), "Număr de titluri pe status");
    }

    private void AddChart(TabPage tab, Dictionary<string, int> data, string title)
    {
        tab.Controls.Clear();

        var chartPanel = new BarChartPanel(data, title)
        {
            Dock = DockStyle.Fill
        };

        tab.Controls.Add(chartPanel);
    }

    private Dictionary<string, int> BuildGenreData()
    {
        return _shows
            .Where(s => !string.IsNullOrWhiteSpace(s.Genre))
            .GroupBy(s => s.Genre)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private Dictionary<string, int> BuildStudioData()
    {
        return _shows
            .Where(s => !string.IsNullOrWhiteSpace(s.Studio))
            .GroupBy(s => s.Studio)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private Dictionary<string, int> BuildRatingData()
    {
        return _shows
            .GroupBy(s => s.Rating.ToString())
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private Dictionary<string, int> BuildStatusData()
    {
        return _shows
            .GroupBy(s => s.Status.ToString())
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private string BuildSummaryText()
    {
        if (_shows.Count == 0)
        {
            return "Total titluri: 0 | Nu există date pentru statistici.";
        }

        var mostWatchedGenre = _shows
            .Where(s => s.WatchedEpisodes > 0 && !string.IsNullOrWhiteSpace(s.Genre))
            .GroupBy(s => s.Genre)
            .Select(g => new
            {
                Genre = g.Key,
                WatchedEpisodes = g.Sum(s => s.WatchedEpisodes)
            })
            .OrderByDescending(g => g.WatchedEpisodes)
            .FirstOrDefault();

        var mostUsedStudio = _shows
            .Where(s => !string.IsNullOrWhiteSpace(s.Studio))
            .GroupBy(s => s.Studio)
            .Select(g => new
            {
                Studio = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(g => g.Count)
            .FirstOrDefault();

        var finishedCount = _shows.Count(s => s.Status == WatchStatus.Finished);
        var totalWatchedEpisodes = _shows.Sum(s => s.WatchedEpisodes);
        var averageScore = Math.Round(_shows.Average(s => s.PersonalScore), 2);

        var genreText = mostWatchedGenre == null
            ? "Gen cel mai vizionat: indisponibil"
            : $"Gen cel mai vizionat: {mostWatchedGenre.Genre} ({mostWatchedGenre.WatchedEpisodes} episoade)";

        var studioText = mostUsedStudio == null
            ? "Studio dominant: indisponibil"
            : $"Studio dominant: {mostUsedStudio.Studio} ({mostUsedStudio.Count} titluri)";

        return
            $"Total titluri: {_shows.Count} | " +
            $"Episoade văzute: {totalWatchedEpisodes} | " +
            $"Finalizate: {finishedCount} | " +
            $"Scor mediu: {averageScore} | " +
            $"{genreText} | " +
            $"{studioText}";
    }

    private void CloseButton_Click(object? sender, EventArgs e)
    {
        Close();
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
        else if (ReferenceEquals(sender, _summaryPanel))
        {
            ApplyRoundedCorners(_summaryPanel, 18);
        }
    }

    private void ApplyRoundedCornersToStaticControls()
    {
        ApplyRoundedCorners(_headerPanel, 22);
        ApplyRoundedCorners(_summaryPanel, 18);
        ApplyRoundedCorners(_closeButton, 13);
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
        Color accent;
        Color accentHover;
        Color inputBackground;
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

        _tabs.BackColor = surface;
        _tabs.ForeColor = textPrimary;

        foreach (TabPage tab in _tabs.TabPages)
        {
            tab.BackColor = surface;
            tab.ForeColor = textPrimary;
        }

        _emptyLabel.BackColor = surface;
        _emptyLabel.ForeColor = textSecondary;

        _summaryPanel.BackColor = surfaceSoft;
        _summaryLabel.BackColor = surfaceSoft;
        _summaryLabel.ForeColor = textSecondary;

        _closeButton.FlatStyle = FlatStyle.Flat;
        _closeButton.Cursor = Cursors.Hand;
        _closeButton.BackColor = accent;
        _closeButton.ForeColor = Color.White;
        _closeButton.FlatAppearance.BorderSize = 0;
        _closeButton.FlatAppearance.MouseOverBackColor = accentHover;
        _closeButton.FlatAppearance.MouseDownBackColor = accentHover;

        foreach (var chartPanel in GetAllControls(this).OfType<BarChartPanel>())
        {
            chartPanel.SetTheme(_currentTheme, inputBackground, textPrimary, textSecondary, border);
        }
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

    private class BarChartPanel : Panel
    {
        private readonly Dictionary<string, int> _data;
        private readonly string _title;

        private Color _surface = Color.White;
        private Color _textPrimary = Color.Black;
        private Color _textSecondary = Color.DimGray;
        private Color _border = Color.LightGray;
        private Color _barColor = Color.FromArgb(232, 122, 170);

        public BarChartPanel(Dictionary<string, int> data, string title)
        {
            _data = data;
            _title = title;

            DoubleBuffered = true;
            BackColor = Color.White;
            Padding = new Padding(10);
        }

        public void SetTheme(
            AppTheme currentTheme,
            Color surface,
            Color textPrimary,
            Color textSecondary,
            Color border)
        {
            _surface = surface;
            _textPrimary = textPrimary;
            _textSecondary = textSecondary;
            _border = border;

            _barColor = currentTheme switch
            {
                AppTheme.BerryNight => Color.FromArgb(207, 117, 161),
                AppTheme.OceanBlue => Color.FromArgb(70, 130, 180),
                _ => Color.FromArgb(232, 122, 170)
            };

            BackColor = surface;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(_surface);

            using var titleFont = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            using var labelFont = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Regular);
            using var valueFont = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Bold);
            using var textBrush = new SolidBrush(_textPrimary);
            using var secondaryBrush = new SolidBrush(_textSecondary);
            using var borderPen = new Pen(_border, 1);
            using var barBrush = new SolidBrush(_barColor);

            graphics.DrawString(_title, titleFont, textBrush, 20, 15);

            if (_data.Count == 0)
            {
                graphics.DrawString("Nu există date pentru acest grafic.", labelFont, secondaryBrush, 20, 60);
                return;
            }

            var chartLeft = Math.Min(210, Math.Max(140, Width / 4));
            var chartTop = 62;
            var barHeight = 28;
            var gap = 14;
            var maxBarWidth = Math.Max(120, Width - chartLeft - 90);
            var maxValue = Math.Max(1, _data.Values.Max());
            var y = chartTop;

            foreach (var item in _data.Take(9))
            {
                var label = TrimLabel(item.Key, 24);
                var barWidth = Math.Max(8, (int)(item.Value / (double)maxValue * maxBarWidth));

                graphics.DrawString(label, labelFont, textBrush, 20, y + 5);
                graphics.FillRectangle(barBrush, chartLeft, y, barWidth, barHeight);
                graphics.DrawRectangle(borderPen, chartLeft, y, barWidth, barHeight);
                graphics.DrawString(item.Value.ToString(), valueFont, textBrush, chartLeft + Math.Min(barWidth + 8, maxBarWidth + 8), y + 5);

                y += barHeight + gap;
            }
        }

        private static string TrimLabel(string text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            return text.Length <= maxLength ? text : text[..(maxLength - 3)] + "...";
        }
    }

 
}
