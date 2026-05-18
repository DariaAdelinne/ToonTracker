/**************************************************************************
 *                                                                        *
 *  File:        RecommendationOptionsForm.Designer.cs                    *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Design static pentru formularul de preferinte.            *
 *                                                                        *
 **************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ToonTracker.UI;

partial class RecommendationOptionsForm
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
        _optionsCard = new Panel();
        _optionsLayout = new TableLayoutPanel();
        _genreGroup = new GroupBox();
        _genreList = new CheckedListBox();
        _studioGroup = new GroupBox();
        _studioList = new CheckedListBox();
        _ratingGroup = new GroupBox();
        _ratingLayout = new TableLayoutPanel();
        _ratingLabel = new Label();
        _ratingComboBox = new ComboBox();
        _durationGroup = new GroupBox();
        _durationLayout = new TableLayoutPanel();
        _durationHintLabel = new Label();
        _shortSeriesCheckBox = new CheckBox();
        _longSeriesCheckBox = new CheckBox();
        _buttonsPanel = new FlowLayoutPanel();
        _confirmButton = new Button();
        _cancelButton = new Button();
        _root.SuspendLayout();
        _headerPanel.SuspendLayout();
        _headerLayout.SuspendLayout();
        _optionsCard.SuspendLayout();
        _optionsLayout.SuspendLayout();
        _genreGroup.SuspendLayout();
        _studioGroup.SuspendLayout();
        _ratingGroup.SuspendLayout();
        _ratingLayout.SuspendLayout();
        _durationGroup.SuspendLayout();
        _durationLayout.SuspendLayout();
        _buttonsPanel.SuspendLayout();
        SuspendLayout();
        // 
        // _root
        // 
        _root.ColumnCount = 1;
        _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _root.Controls.Add(_headerPanel, 0, 0);
        _root.Controls.Add(_optionsCard, 0, 1);
        _root.Dock = DockStyle.Fill;
        _root.Location = new Point(0, 0);
        _root.Margin = new Padding(0);
        _root.Name = "_root";
        _root.Padding = new Padding(21, 18, 21, 18);
        _root.RowCount = 2;
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
        _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _root.Size = new Size(720, 560);
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
        _headerPanel.Size = new Size(678, 74);
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
        _headerLayout.Size = new Size(620, 54);
        _headerLayout.TabIndex = 0;
        // 
        // _titleLabel
        // 
        _titleLabel.Dock = DockStyle.Fill;
        _titleLabel.Font = new Font("Microsoft Sans Serif", 17F, FontStyle.Bold, GraphicsUnit.Point);
        _titleLabel.Location = new Point(0, 0);
        _titleLabel.Margin = new Padding(0);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(620, 31);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "Preferințe recomandări";
        _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _subtitleLabel
        // 
        _subtitleLabel.Dock = DockStyle.Fill;
        _subtitleLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _subtitleLabel.Location = new Point(0, 31);
        _subtitleLabel.Margin = new Padding(0);
        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.Size = new Size(620, 23);
        _subtitleLabel.TabIndex = 1;
        _subtitleLabel.Text = "Alege criteriile pentru recomandările custom";
        _subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _optionsCard
        // 
        _optionsCard.Controls.Add(_optionsLayout);
        _optionsCard.Dock = DockStyle.Fill;
        _optionsCard.Location = new Point(21, 104);
        _optionsCard.Margin = new Padding(0);
        _optionsCard.Name = "_optionsCard";
        _optionsCard.Padding = new Padding(14, 12, 14, 12);
        _optionsCard.Size = new Size(678, 438);
        _optionsCard.TabIndex = 1;
        _optionsCard.Resize += RoundedControl_Resize;
        // 
        // _optionsLayout
        // 
        _optionsLayout.ColumnCount = 2;
        _optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _optionsLayout.Controls.Add(_genreGroup, 0, 0);
        _optionsLayout.Controls.Add(_studioGroup, 1, 0);
        _optionsLayout.Controls.Add(_ratingGroup, 0, 1);
        _optionsLayout.Controls.Add(_durationGroup, 1, 1);
        _optionsLayout.Controls.Add(_buttonsPanel, 0, 2);
        _optionsLayout.Dock = DockStyle.Fill;
        _optionsLayout.Location = new Point(14, 12);
        _optionsLayout.Margin = new Padding(0);
        _optionsLayout.Name = "_optionsLayout";
        _optionsLayout.RowCount = 3;
        _optionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _optionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 137F));
        _optionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        _optionsLayout.Size = new Size(650, 414);
        _optionsLayout.TabIndex = 0;
        // 
        // _genreGroup
        // 
        _genreGroup.Controls.Add(_genreList);
        _genreGroup.Dock = DockStyle.Fill;
        _genreGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _genreGroup.Location = new Point(6, 5);
        _genreGroup.Margin = new Padding(6, 5, 6, 5);
        _genreGroup.Name = "_genreGroup";
        _genreGroup.Padding = new Padding(11, 20, 11, 10);
        _genreGroup.Size = new Size(313, 209);
        _genreGroup.TabIndex = 0;
        _genreGroup.TabStop = false;
        _genreGroup.Text = "Genuri preferate";
        // 
        // _genreList
        // 
        _genreList.BorderStyle = BorderStyle.FixedSingle;
        _genreList.CheckOnClick = true;
        _genreList.Dock = DockStyle.Fill;
        _genreList.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _genreList.FormattingEnabled = true;
        _genreList.Location = new Point(11, 40);
        _genreList.Margin = new Padding(0);
        _genreList.Name = "_genreList";
        _genreList.Size = new Size(291, 159);
        _genreList.TabIndex = 0;
        // 
        // _studioGroup
        // 
        _studioGroup.Controls.Add(_studioList);
        _studioGroup.Dock = DockStyle.Fill;
        _studioGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _studioGroup.Location = new Point(331, 5);
        _studioGroup.Margin = new Padding(6, 5, 6, 5);
        _studioGroup.Name = "_studioGroup";
        _studioGroup.Padding = new Padding(11, 20, 11, 10);
        _studioGroup.Size = new Size(313, 209);
        _studioGroup.TabIndex = 1;
        _studioGroup.TabStop = false;
        _studioGroup.Text = "Studiouri preferate";
        // 
        // _studioList
        // 
        _studioList.BorderStyle = BorderStyle.FixedSingle;
        _studioList.CheckOnClick = true;
        _studioList.Dock = DockStyle.Fill;
        _studioList.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _studioList.FormattingEnabled = true;
        _studioList.Location = new Point(11, 40);
        _studioList.Margin = new Padding(0);
        _studioList.Name = "_studioList";
        _studioList.Size = new Size(291, 159);
        _studioList.TabIndex = 0;
        // 
        // _ratingGroup
        // 
        _ratingGroup.Controls.Add(_ratingLayout);
        _ratingGroup.Dock = DockStyle.Fill;
        _ratingGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _ratingGroup.Location = new Point(6, 224);
        _ratingGroup.Margin = new Padding(6, 5, 6, 5);
        _ratingGroup.Name = "_ratingGroup";
        _ratingGroup.Padding = new Padding(11, 20, 11, 10);
        _ratingGroup.Size = new Size(313, 127);
        _ratingGroup.TabIndex = 2;
        _ratingGroup.TabStop = false;
        _ratingGroup.Text = "Rating";
        // 
        // _ratingLayout
        // 
        _ratingLayout.ColumnCount = 1;
        _ratingLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _ratingLayout.Controls.Add(_ratingLabel, 0, 0);
        _ratingLayout.Controls.Add(_ratingComboBox, 0, 1);
        _ratingLayout.Dock = DockStyle.Fill;
        _ratingLayout.Location = new Point(11, 40);
        _ratingLayout.Margin = new Padding(0);
        _ratingLayout.Name = "_ratingLayout";
        _ratingLayout.RowCount = 2;
        _ratingLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        _ratingLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _ratingLayout.Size = new Size(291, 77);
        _ratingLayout.TabIndex = 0;
        // 
        // _ratingLabel
        // 
        _ratingLabel.Dock = DockStyle.Fill;
        _ratingLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _ratingLabel.Location = new Point(0, 0);
        _ratingLabel.Margin = new Padding(0);
        _ratingLabel.Name = "_ratingLabel";
        _ratingLabel.Size = new Size(291, 24);
        _ratingLabel.TabIndex = 0;
        _ratingLabel.Text = "Rating maxim acceptat";
        _ratingLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _ratingComboBox
        // 
        _ratingComboBox.Dock = DockStyle.Fill;
        _ratingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _ratingComboBox.FlatStyle = FlatStyle.Flat;
        _ratingComboBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _ratingComboBox.FormattingEnabled = true;
        _ratingComboBox.Location = new Point(0, 26);
        _ratingComboBox.Margin = new Padding(0, 2, 0, 0);
        _ratingComboBox.Name = "_ratingComboBox";
        _ratingComboBox.Size = new Size(291, 30);
        _ratingComboBox.TabIndex = 1;
        // 
        // _durationGroup
        // 
        _durationGroup.Controls.Add(_durationLayout);
        _durationGroup.Dock = DockStyle.Fill;
        _durationGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _durationGroup.Location = new Point(331, 224);
        _durationGroup.Margin = new Padding(6, 5, 6, 5);
        _durationGroup.Name = "_durationGroup";
        _durationGroup.Padding = new Padding(11, 20, 11, 10);
        _durationGroup.Size = new Size(313, 127);
        _durationGroup.TabIndex = 3;
        _durationGroup.TabStop = false;
        _durationGroup.Text = "Durată";
        // 
        // _durationLayout
        // 
        _durationLayout.ColumnCount = 1;
        _durationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _durationLayout.Controls.Add(_durationHintLabel, 0, 0);
        _durationLayout.Controls.Add(_shortSeriesCheckBox, 0, 1);
        _durationLayout.Controls.Add(_longSeriesCheckBox, 0, 2);
        _durationLayout.Dock = DockStyle.Fill;
        _durationLayout.Location = new Point(11, 40);
        _durationLayout.Margin = new Padding(0);
        _durationLayout.Name = "_durationLayout";
        _durationLayout.RowCount = 3;
        _durationLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        _durationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _durationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _durationLayout.Size = new Size(291, 77);
        _durationLayout.TabIndex = 0;
        // 
        // _durationHintLabel
        // 
        _durationHintLabel.Dock = DockStyle.Fill;
        _durationHintLabel.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
        _durationHintLabel.Location = new Point(0, 0);
        _durationHintLabel.Margin = new Padding(0);
        _durationHintLabel.Name = "_durationHintLabel";
        _durationHintLabel.Size = new Size(291, 20);
        _durationHintLabel.TabIndex = 0;
        _durationHintLabel.Text = "Alege ritmul preferat";
        _durationHintLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _shortSeriesCheckBox
        // 
        _shortSeriesCheckBox.AutoSize = true;
        _shortSeriesCheckBox.Dock = DockStyle.Fill;
        _shortSeriesCheckBox.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _shortSeriesCheckBox.Location = new Point(0, 20);
        _shortSeriesCheckBox.Margin = new Padding(0);
        _shortSeriesCheckBox.Name = "_shortSeriesCheckBox";
        _shortSeriesCheckBox.Size = new Size(291, 28);
        _shortSeriesCheckBox.TabIndex = 1;
        _shortSeriesCheckBox.Text = "Seriale scurte";
        _shortSeriesCheckBox.UseVisualStyleBackColor = true;
        // 
        // _longSeriesCheckBox
        // 
        _longSeriesCheckBox.AutoSize = true;
        _longSeriesCheckBox.Dock = DockStyle.Fill;
        _longSeriesCheckBox.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _longSeriesCheckBox.Location = new Point(0, 48);
        _longSeriesCheckBox.Margin = new Padding(0);
        _longSeriesCheckBox.Name = "_longSeriesCheckBox";
        _longSeriesCheckBox.Size = new Size(291, 29);
        _longSeriesCheckBox.TabIndex = 2;
        _longSeriesCheckBox.Text = "Seriale lungi";
        _longSeriesCheckBox.UseVisualStyleBackColor = true;
        // 
        // _buttonsPanel
        // 
        _buttonsPanel.AutoSize = true;
        _optionsLayout.SetColumnSpan(_buttonsPanel, 2);
        _buttonsPanel.Controls.Add(_confirmButton);
        _buttonsPanel.Controls.Add(_cancelButton);
        _buttonsPanel.Dock = DockStyle.Fill;
        _buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
        _buttonsPanel.Location = new Point(6, 361);
        _buttonsPanel.Margin = new Padding(6, 5, 6, 5);
        _buttonsPanel.Name = "_buttonsPanel";
        _buttonsPanel.Padding = new Padding(0, 6, 0, 0);
        _buttonsPanel.Size = new Size(638, 48);
        _buttonsPanel.TabIndex = 4;
        // 
        // _confirmButton
        // 
        _confirmButton.Cursor = Cursors.Hand;
        _confirmButton.FlatAppearance.BorderSize = 0;
        _confirmButton.FlatStyle = FlatStyle.Flat;
        _confirmButton.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _confirmButton.Location = new Point(431, 9);
        _confirmButton.Margin = new Padding(6, 3, 0, 3);
        _confirmButton.Name = "_confirmButton";
        _confirmButton.Size = new Size(207, 38);
        _confirmButton.TabIndex = 0;
        _confirmButton.Text = "Generează recomandări";
        _confirmButton.UseVisualStyleBackColor = false;
        _confirmButton.Click += ConfirmButton_Click;
        _confirmButton.Resize += RoundedControl_Resize;
        // 
        // _cancelButton
        // 
        _cancelButton.Cursor = Cursors.Hand;
        _cancelButton.FlatAppearance.BorderSize = 0;
        _cancelButton.FlatStyle = FlatStyle.Flat;
        _cancelButton.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _cancelButton.Location = new Point(319, 9);
        _cancelButton.Margin = new Padding(6, 3, 6, 3);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.Size = new Size(100, 38);
        _cancelButton.TabIndex = 1;
        _cancelButton.Text = "Renunță";
        _cancelButton.UseVisualStyleBackColor = false;
        _cancelButton.Click += CancelButton_Click;
        _cancelButton.Resize += RoundedControl_Resize;
        // 
        // RecommendationOptionsForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(720, 560);
        Controls.Add(_root);
        Margin = new Padding(4, 3, 4, 3);
        MinimumSize = new Size(640, 520);
        Name = "RecommendationOptionsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Preferinte recomandari";
        _root.ResumeLayout(false);
        _headerPanel.ResumeLayout(false);
        _headerLayout.ResumeLayout(false);
        _optionsCard.ResumeLayout(false);
        _optionsLayout.ResumeLayout(false);
        _optionsLayout.PerformLayout();
        _genreGroup.ResumeLayout(false);
        _studioGroup.ResumeLayout(false);
        _ratingGroup.ResumeLayout(false);
        _ratingLayout.ResumeLayout(false);
        _durationGroup.ResumeLayout(false);
        _durationLayout.ResumeLayout(false);
        _durationLayout.PerformLayout();
        _buttonsPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _root;
    private Panel _headerPanel;
    private TableLayoutPanel _headerLayout;
    private Label _titleLabel;
    private Label _subtitleLabel;
    private Panel _optionsCard;
    private TableLayoutPanel _optionsLayout;
    private GroupBox _genreGroup;
    private CheckedListBox _genreList;
    private GroupBox _studioGroup;
    private CheckedListBox _studioList;
    private GroupBox _ratingGroup;
    private TableLayoutPanel _ratingLayout;
    private Label _ratingLabel;
    private ComboBox _ratingComboBox;
    private GroupBox _durationGroup;
    private TableLayoutPanel _durationLayout;
    private Label _durationHintLabel;
    private CheckBox _shortSeriesCheckBox;
    private CheckBox _longSeriesCheckBox;
    private FlowLayoutPanel _buttonsPanel;
    private Button _confirmButton;
    private Button _cancelButton;
}
