/**************************************************************************
 *                                                                        *
 *  File:        SatatisticsForm.cs                                       *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Functionalitate: Formular pentru afisarea statisticilor  * 
 *  vizuale despre colectia de desene animate.                            *
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

public class StatisticsForm : Form
{
    private readonly List<AnimatedShow> _shows;
    private readonly AppTheme _currentTheme;

    public StatisticsForm(IEnumerable<AnimatedShow> shows, AppTheme currentTheme)
    {
        _shows = shows.ToList();
        _currentTheme = currentTheme;

        Text = "Statistici ToonTracker";
        Width = 950;
        Height = 650;
        MinimumSize = new Size(850, 550);
        StartPosition = FormStartPosition.CenterParent;

        BuildUi();
        ApplyTheme();
    }

    private void BuildUi()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 1,
            Padding = new Padding(16)
        };

        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 78));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        Controls.Add(root);

        var titlePanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20, 10, 20, 10),
            Margin = new Padding(0, 0, 0, 12)
        };

        var title = new Label
        {
            Text = "Statistici ToonTracker",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold)
        };

        titlePanel.Controls.Add(title);
        root.Controls.Add(titlePanel, 0, 0);

        if (_shows.Count == 0)
        {
            var emptyLabel = new Label
            {
                Text = "Nu exista date pentru statistici. Adauga mai intai cateva desene sau seriale.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold)
            };

            root.Controls.Add(emptyLabel, 0, 1);
            return;
        }

        var tabs = new TabControl
        {
            Dock = DockStyle.Fill,
            Font = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Bold)
        };

        tabs.TabPages.Add(CreateChartTab("Genuri", BuildGenreData(), "Numar de titluri pe gen"));
        tabs.TabPages.Add(CreateChartTab("Studiouri", BuildStudioData(), "Numar de titluri pe studio"));
        tabs.TabPages.Add(CreateChartTab("Rating", BuildRatingData(), "Numar de titluri pe rating"));
        tabs.TabPages.Add(CreateChartTab("Status", BuildStatusData(), "Numar de titluri pe status"));

        root.Controls.Add(tabs, 0, 1);

        var summaryPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(14, 10, 14, 10),
            Margin = new Padding(0, 12, 0, 0),
            Height = 52
        };

        var summary = new Label
        {
            Text = BuildSummaryText(),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Bold)
        };

        summaryPanel.Controls.Add(summary);
        root.Controls.Add(summaryPanel, 0, 2);
    }

    private TabPage CreateChartTab(string tabTitle, Dictionary<string, int> data, string chartTitle)
    {
        var tab = new TabPage(tabTitle);

        var chartPanel = new BarChartPanel(data, chartTitle)
        {
            Dock = DockStyle.Fill
        };

        tab.Controls.Add(chartPanel);

        return tab;
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

        var genreText = mostWatchedGenre == null
            ? "Gen cel mai vizionat: indisponibil"
            : $"Gen cel mai vizionat: {mostWatchedGenre.Genre} ({mostWatchedGenre.WatchedEpisodes} episoade vazute)";

        var studioText = mostUsedStudio == null
            ? "Studio dominant: indisponibil"
            : $"Studio dominant: {mostUsedStudio.Studio} ({mostUsedStudio.Count} titluri)";

        return
            $"Total titluri: {_shows.Count} | " +
            $"Episoade vazute: {totalWatchedEpisodes} | " +
            $"Titluri finalizate: {finishedCount} | " +
            $"{genreText} | " +
            $"{studioText}";
    }

    private void ApplyTheme()
    {
        Color background;
        Color surface;
        Color surfaceSoft;
        Color accent;
        Color textPrimary;
        Color textSecondary;
        Color border;

        switch (_currentTheme)
        {
            case AppTheme.BerryNight:
                background = Color.FromArgb(33, 27, 37);
                surface = Color.FromArgb(49, 42, 55);
                surfaceSoft = Color.FromArgb(61, 53, 68);
                accent = Color.FromArgb(207, 117, 161);
                textPrimary = Color.FromArgb(250, 241, 246);
                textSecondary = Color.FromArgb(222, 198, 212);
                border = Color.FromArgb(96, 83, 103);
                break;

            case AppTheme.OceanBlue:
                background = Color.FromArgb(239, 246, 255);
                surface = Color.White;
                surfaceSoft = Color.FromArgb(224, 238, 255);
                accent = Color.FromArgb(70, 130, 180);
                textPrimary = Color.FromArgb(36, 52, 71);
                textSecondary = Color.FromArgb(80, 100, 125);
                border = Color.FromArgb(190, 215, 240);
                break;

            default:
                background = Color.FromArgb(255, 245, 250);
                surface = Color.White;
                surfaceSoft = Color.FromArgb(255, 237, 245);
                accent = Color.FromArgb(232, 122, 170);
                textPrimary = Color.FromArgb(87, 52, 68);
                textSecondary = Color.FromArgb(129, 93, 108);
                border = Color.FromArgb(242, 204, 222);
                break;
        }

        BackColor = background;
        ForeColor = textPrimary;

        foreach (Control control in GetAllControls(this))
        {
            control.ForeColor = textPrimary;

            if (control is BarChartPanel chartPanel)
            {
                chartPanel.SetTheme(
                    _currentTheme,
                    surface,
                    textPrimary,
                    textSecondary,
                    border);
            }
            else if (control is Panel panel)
            {
                panel.BackColor = surfaceSoft;
            }
            else if (control is TabControl tabControl)
            {
                tabControl.BackColor = surface;
                tabControl.ForeColor = textPrimary;
            }
            else if (control is TabPage tabPage)
            {
                tabPage.BackColor = surface;
                tabPage.ForeColor = textPrimary;
            }
            else if (control is Label label)
            {
                label.BackColor = control.Parent is Panel ? surfaceSoft : background;
                label.ForeColor = textPrimary;
            }
        }

        foreach (Panel panel in GetAllControls(this).OfType<Panel>())
        {
            if (panel.Controls.OfType<Label>().Any(l => l.Text == "Statistici ToonTracker"))
            {
                panel.BackColor = accent;

                foreach (Control child in panel.Controls)
                {
                    child.BackColor = accent;
                    child.ForeColor = Color.White;
                }
            }
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

        private AppTheme _currentTheme = AppTheme.CozyPink;
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
            Dock = DockStyle.Fill;
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
            _currentTheme = currentTheme;
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
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.Clear(_surface);

            using var titleFont = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            using var labelFont = new Font(FontFamily.GenericSansSerif, 9);
            using var valueFont = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);

            using var textBrush = new SolidBrush(_textPrimary);
            using var secondaryBrush = new SolidBrush(_textSecondary);
            using var borderPen = new Pen(_border, 1);
            using var barBrush = new SolidBrush(_barColor);

            graphics.DrawString(_title, titleFont, textBrush, 20, 15);

            if (_data.Count == 0)
            {
                graphics.DrawString(
                    "Nu exista date pentru acest grafic.",
                    labelFont,
                    secondaryBrush,
                    20,
                    60);
                return;
            }

            var chartLeft = 190;
            var chartTop = 60;
            var barHeight = 28;
            var gap = 14;
            var maxBarWidth = Math.Max(260, Width - chartLeft - 120);
            var maxValue = Math.Max(1, _data.Values.Max());

            var y = chartTop;

            foreach (var item in _data)
            {
                var label = TrimLabel(item.Key, 24);
                var barWidth = (int)(item.Value / (double)maxValue * maxBarWidth);

                graphics.DrawString(label, labelFont, textBrush, 20, y + 5);

                graphics.FillRectangle(barBrush, chartLeft, y, barWidth, barHeight);
                graphics.DrawRectangle(borderPen, chartLeft, y, barWidth, barHeight);

                graphics.DrawString(
                    item.Value.ToString(),
                    valueFont,
                    textBrush,
                    chartLeft + Math.Min(barWidth + 8, maxBarWidth + 8),
                    y + 5);

                y += barHeight + gap;
            }
        }

        private static string TrimLabel(string text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            if (text.Length <= maxLength)
            {
                return text;
            }

            return text.Substring(0, maxLength - 3) + "...";
        }
    }
}