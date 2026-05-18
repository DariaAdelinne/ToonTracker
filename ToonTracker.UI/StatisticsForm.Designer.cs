/**************************************************************************
 *                                                                        *
 *  File:        StatisticsForm.Designer.cs                               *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Design static pentru formularul de statistici.            *
 *                                                                        *
 **************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ToonTracker.UI;

partial class StatisticsForm
{
    private IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        _root = new TableLayoutPanel();
        _headerPanel = new Panel();
        _headerLayout = new TableLayoutPanel();
        _titleLabel = new Label();
        _subtitleLabel = new Label();
        _contentPanel = new Panel();
        _tabs = new TabControl();
        _genreTab = new TabPage();
        _studioTab = new TabPage();
        _ratingTab = new TabPage();
        _statusTab = new TabPage();
        _emptyLabel = new Label();
        _summaryPanel = new Panel();
        _summaryLayout = new TableLayoutPanel();
        _summaryLabel = new Label();
        _closeButton = new Button();
        _root.SuspendLayout();
        _headerPanel.SuspendLayout();
        _headerLayout.SuspendLayout();
        _contentPanel.SuspendLayout();
        _tabs.SuspendLayout();
        _summaryPanel.SuspendLayout();
        _summaryLayout.SuspendLayout();
        SuspendLayout();
        // 
        // _root
        // 
        _root.ColumnCount = 1;
        _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _root.Controls.Add(_headerPanel, 0, 0);
        _root.Controls.Add(_contentPanel, 0, 1);
        _root.Controls.Add(_summaryPanel, 0, 2);
        _root.Dock = DockStyle.Fill;
        _root.Location = new Point(0, 0);
        _root.Margin = new Padding(0);
        _root.Name = "_root";
        _root.Padding = new Padding(21, 18, 21, 18);
        _root.RowCount = 3;
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 134F));
        _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
        _root.Size = new Size(980, 650);
        _root.TabIndex = 0;
        // 
        // _headerPanel
        // 
        _headerPanel.Controls.Add(_headerLayout);
        _headerPanel.Dock = DockStyle.Fill;
        _headerPanel.Location = new Point(21, 18);
        _headerPanel.Margin = new Padding(0, 0, 0, 12);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.Padding = new Padding(29, 10, 29, 10);
        _headerPanel.Size = new Size(938, 122);
        _headerPanel.TabIndex = 0;
        _headerPanel.Resize += RoundedControl_Resize;
        // 
        // _headerLayout
        // 
        _headerLayout.ColumnCount = 1;
        _headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _headerLayout.Controls.Add(_titleLabel, 0, 0);
        _headerLayout.Controls.Add(_subtitleLabel, 0, 1);
        _headerLayout.Dock = DockStyle.Fill;
        _headerLayout.Location = new Point(29, 10);
        _headerLayout.Margin = new Padding(0);
        _headerLayout.Name = "_headerLayout";
        _headerLayout.RowCount = 2;
        _headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
        _headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
        _headerLayout.Size = new Size(880, 102);
        _headerLayout.TabIndex = 0;
        // 
        // _titleLabel
        // 
        _titleLabel.Dock = DockStyle.Fill;
        _titleLabel.Font = new Font("Microsoft Sans Serif", 17F, FontStyle.Bold, GraphicsUnit.Point);
        _titleLabel.Location = new Point(0, 0);
        _titleLabel.Margin = new Padding(0);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(880, 59);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "Statistici ToonTracker";
        _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _subtitleLabel
        // 
        _subtitleLabel.Dock = DockStyle.Fill;
        _subtitleLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _subtitleLabel.Location = new Point(0, 59);
        _subtitleLabel.Margin = new Padding(0);
        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.Size = new Size(880, 43);
        _subtitleLabel.TabIndex = 1;
        _subtitleLabel.Text = "Analiză vizuală pentru colecția mea";
        _subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _contentPanel
        // 
        _contentPanel.Controls.Add(_tabs);
        _contentPanel.Controls.Add(_emptyLabel);
        _contentPanel.Dock = DockStyle.Fill;
        _contentPanel.Location = new Point(21, 152);
        _contentPanel.Margin = new Padding(0, 0, 0, 12);
        _contentPanel.Name = "_contentPanel";
        _contentPanel.Size = new Size(938, 390);
        _contentPanel.TabIndex = 1;
        // 
        // _tabs
        // 
        _tabs.Controls.Add(_genreTab);
        _tabs.Controls.Add(_studioTab);
        _tabs.Controls.Add(_ratingTab);
        _tabs.Controls.Add(_statusTab);
        _tabs.Dock = DockStyle.Fill;
        _tabs.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _tabs.Location = new Point(0, 0);
        _tabs.Margin = new Padding(0);
        _tabs.Name = "_tabs";
        _tabs.SelectedIndex = 0;
        _tabs.Size = new Size(938, 390);
        _tabs.TabIndex = 0;
        // 
        // _genreTab
        // 
        _genreTab.Location = new Point(4, 31);
        _genreTab.Name = "_genreTab";
        _genreTab.Padding = new Padding(10);
        _genreTab.Size = new Size(930, 355);
        _genreTab.TabIndex = 0;
        _genreTab.Text = "Genuri";
        // 
        // _studioTab
        // 
        _studioTab.Location = new Point(4, 31);
        _studioTab.Name = "_studioTab";
        _studioTab.Padding = new Padding(10);
        _studioTab.Size = new Size(930, 403);
        _studioTab.TabIndex = 1;
        _studioTab.Text = "Studiouri";
        // 
        // _ratingTab
        // 
        _ratingTab.Location = new Point(4, 31);
        _ratingTab.Name = "_ratingTab";
        _ratingTab.Padding = new Padding(10);
        _ratingTab.Size = new Size(930, 403);
        _ratingTab.TabIndex = 2;
        _ratingTab.Text = "Rating";
        // 
        // _statusTab
        // 
        _statusTab.Location = new Point(4, 31);
        _statusTab.Name = "_statusTab";
        _statusTab.Padding = new Padding(10);
        _statusTab.Size = new Size(930, 403);
        _statusTab.TabIndex = 3;
        _statusTab.Text = "Status";
        // 
        // _emptyLabel
        // 
        _emptyLabel.Dock = DockStyle.Fill;
        _emptyLabel.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _emptyLabel.Location = new Point(0, 0);
        _emptyLabel.Margin = new Padding(0);
        _emptyLabel.Name = "_emptyLabel";
        _emptyLabel.Padding = new Padding(25);
        _emptyLabel.Size = new Size(938, 390);
        _emptyLabel.TabIndex = 1;
        _emptyLabel.Text = "Nu există date pentru statistici. Adaugă mai întâi câteva desene sau seriale.";
        _emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
        _emptyLabel.Visible = false;
        // 
        // _summaryPanel
        // 
        _summaryPanel.Controls.Add(_summaryLayout);
        _summaryPanel.Dock = DockStyle.Fill;
        _summaryPanel.Location = new Point(21, 554);
        _summaryPanel.Margin = new Padding(0);
        _summaryPanel.Name = "_summaryPanel";
        _summaryPanel.Padding = new Padding(18, 10, 18, 10);
        _summaryPanel.Size = new Size(938, 78);
        _summaryPanel.TabIndex = 2;
        _summaryPanel.Resize += RoundedControl_Resize;
        // 
        // _summaryLayout
        // 
        _summaryLayout.ColumnCount = 2;
        _summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 118F));
        _summaryLayout.Controls.Add(_summaryLabel, 0, 0);
        _summaryLayout.Controls.Add(_closeButton, 1, 0);
        _summaryLayout.Dock = DockStyle.Fill;
        _summaryLayout.Location = new Point(18, 10);
        _summaryLayout.Margin = new Padding(0);
        _summaryLayout.Name = "_summaryLayout";
        _summaryLayout.RowCount = 1;
        _summaryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _summaryLayout.Size = new Size(902, 58);
        _summaryLayout.TabIndex = 0;
        // 
        // _summaryLabel
        // 
        _summaryLabel.Dock = DockStyle.Fill;
        _summaryLabel.Font = new Font("Microsoft Sans Serif", 8.6F, FontStyle.Bold, GraphicsUnit.Point);
        _summaryLabel.Location = new Point(0, 0);
        _summaryLabel.Margin = new Padding(0, 0, 12, 0);
        _summaryLabel.Name = "_summaryLabel";
        _summaryLabel.Size = new Size(772, 58);
        _summaryLabel.TabIndex = 0;
        _summaryLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _closeButton
        // 
        _closeButton.Cursor = Cursors.Hand;
        _closeButton.Dock = DockStyle.Fill;
        _closeButton.FlatAppearance.BorderSize = 0;
        _closeButton.FlatStyle = FlatStyle.Flat;
        _closeButton.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _closeButton.Location = new Point(790, 10);
        _closeButton.Margin = new Padding(6, 10, 0, 10);
        _closeButton.Name = "_closeButton";
        _closeButton.Size = new Size(112, 38);
        _closeButton.TabIndex = 1;
        _closeButton.Text = "Închide";
        _closeButton.UseVisualStyleBackColor = false;
        _closeButton.Click += CloseButton_Click;
        _closeButton.Resize += RoundedControl_Resize;
        // 
        // StatisticsForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(980, 650);
        Controls.Add(_root);
        Margin = new Padding(4, 3, 4, 3);
        MinimumSize = new Size(850, 560);
        Name = "StatisticsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Statistici ToonTracker";
        _root.ResumeLayout(false);
        _headerPanel.ResumeLayout(false);
        _headerLayout.ResumeLayout(false);
        _contentPanel.ResumeLayout(false);
        _tabs.ResumeLayout(false);
        _summaryPanel.ResumeLayout(false);
        _summaryLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _root;
    private Panel _headerPanel;
    private TableLayoutPanel _headerLayout;
    private Label _titleLabel;
    private Label _subtitleLabel;
    private Panel _contentPanel;
    private TabControl _tabs;
    private TabPage _genreTab;
    private TabPage _studioTab;
    private TabPage _ratingTab;
    private TabPage _statusTab;
    private Label _emptyLabel;
    private Panel _summaryPanel;
    private TableLayoutPanel _summaryLayout;
    private Label _summaryLabel;
    private Button _closeButton;
}
