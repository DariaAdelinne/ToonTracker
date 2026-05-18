/**************************************************************************
 *                                                                        *
 *  File:        ShowForm.Designer.cs                                     *
 *  Copyright:   (c) 2026, Echipa ToonTracker                             *
 *  Description: Design static pentru formularul de adaugare/editare.      *
 *                                                                        *
 **************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ToonTracker.UI;

partial class ShowForm
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
        _formCard = new Panel();
        _formLayout = new TableLayoutPanel();
        _hintLabel = new Label();
        _mainFieldsLayout = new TableLayoutPanel();
        _titleFieldLabel = new Label();
        _titleTextBox = new TextBox();
        _studioFieldLabel = new Label();
        _studioTextBox = new TextBox();
        _genreFieldLabel = new Label();
        _genreTextBox = new TextBox();
        _ratingFieldLabel = new Label();
        _ratingComboBox = new ComboBox();
        _progressFieldsLayout = new TableLayoutPanel();
        _totalFieldLabel = new Label();
        _totalNumeric = new NumericUpDown();
        _watchedFieldLabel = new Label();
        _watchedNumeric = new NumericUpDown();
        _statusFieldLabel = new Label();
        _statusComboBox = new ComboBox();
        _scoreFieldLabel = new Label();
        _scoreNumeric = new NumericUpDown();
        _favoriteCharacterFieldLabel = new Label();
        _favoriteCharacterTextBox = new TextBox();
        _notesFieldLabel = new Label();
        _notesTextBox = new TextBox();
        _buttonsPanel = new FlowLayoutPanel();
        _saveButton = new Button();
        _cancelButton = new Button();
        _root.SuspendLayout();
        _headerPanel.SuspendLayout();
        _headerLayout.SuspendLayout();
        _formCard.SuspendLayout();
        _formLayout.SuspendLayout();
        _mainFieldsLayout.SuspendLayout();
        _progressFieldsLayout.SuspendLayout();
        ((ISupportInitialize)_totalNumeric).BeginInit();
        ((ISupportInitialize)_watchedNumeric).BeginInit();
        ((ISupportInitialize)_scoreNumeric).BeginInit();
        _buttonsPanel.SuspendLayout();
        SuspendLayout();
        // 
        // _root
        // 
        _root.ColumnCount = 1;
        _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _root.Controls.Add(_headerPanel, 0, 0);
        _root.Controls.Add(_formCard, 0, 1);
        _root.Controls.Add(_buttonsPanel, 0, 2);
        _root.Dock = DockStyle.Fill;
        _root.Location = new Point(0, 0);
        _root.Margin = new Padding(0);
        _root.Name = "_root";
        _root.Padding = new Padding(21, 18, 21, 18);
        _root.RowCount = 3;
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
        _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 59F));
        _root.Size = new Size(640, 720);
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
        _headerPanel.Size = new Size(598, 100);
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
        _headerLayout.Size = new Size(540, 80);
        _headerLayout.TabIndex = 0;
        // 
        // _titleLabel
        // 
        _titleLabel.Dock = DockStyle.Fill;
        _titleLabel.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point);
        _titleLabel.Location = new Point(0, 0);
        _titleLabel.Margin = new Padding(0);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(540, 46);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "Adaugă titlu";
        _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _subtitleLabel
        // 
        _subtitleLabel.Dock = DockStyle.Fill;
        _subtitleLabel.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _subtitleLabel.Location = new Point(0, 46);
        _subtitleLabel.Margin = new Padding(0);
        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.Size = new Size(540, 34);
        _subtitleLabel.TabIndex = 1;
        _subtitleLabel.Text = "Completează detaliile pentru colecția mea";
        _subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _formCard
        // 
        _formCard.Controls.Add(_formLayout);
        _formCard.Dock = DockStyle.Fill;
        _formCard.Location = new Point(21, 130);
        _formCard.Margin = new Padding(0, 0, 0, 12);
        _formCard.Name = "_formCard";
        _formCard.Padding = new Padding(18, 16, 18, 16);
        _formCard.Size = new Size(598, 501);
        _formCard.TabIndex = 1;
        _formCard.Resize += RoundedControl_Resize;
        // 
        // _formLayout
        // 
        _formLayout.ColumnCount = 1;
        _formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _formLayout.Controls.Add(_hintLabel, 0, 0);
        _formLayout.Controls.Add(_mainFieldsLayout, 0, 1);
        _formLayout.Controls.Add(_progressFieldsLayout, 0, 2);
        _formLayout.Controls.Add(_favoriteCharacterFieldLabel, 0, 3);
        _formLayout.Controls.Add(_favoriteCharacterTextBox, 0, 4);
        _formLayout.Controls.Add(_notesFieldLabel, 0, 5);
        _formLayout.Controls.Add(_notesTextBox, 0, 6);
        _formLayout.Dock = DockStyle.Fill;
        _formLayout.Location = new Point(18, 16);
        _formLayout.Margin = new Padding(0);
        _formLayout.Name = "_formLayout";
        _formLayout.RowCount = 7;
        _formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 178F));
        _formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 132F));
        _formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
        _formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _formLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        _formLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _formLayout.Size = new Size(562, 469);
        _formLayout.TabIndex = 0;
        // 
        // _hintLabel
        // 
        _hintLabel.Dock = DockStyle.Fill;
        _hintLabel.Font = new Font("Microsoft Sans Serif", 8.6F, FontStyle.Bold, GraphicsUnit.Point);
        _hintLabel.Location = new Point(0, 0);
        _hintLabel.Margin = new Padding(0, 0, 0, 10);
        _hintLabel.Name = "_hintLabel";
        _hintLabel.Size = new Size(562, 32);
        _hintLabel.TabIndex = 0;
        _hintLabel.Text = "Câmpurile marcate cu * sunt obligatorii. Statusul se ajustează automat după progres.";
        _hintLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _mainFieldsLayout
        // 
        _mainFieldsLayout.ColumnCount = 2;
        _mainFieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _mainFieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _mainFieldsLayout.Controls.Add(_titleFieldLabel, 0, 0);
        _mainFieldsLayout.Controls.Add(_titleTextBox, 0, 1);
        _mainFieldsLayout.Controls.Add(_studioFieldLabel, 1, 0);
        _mainFieldsLayout.Controls.Add(_studioTextBox, 1, 1);
        _mainFieldsLayout.Controls.Add(_genreFieldLabel, 0, 2);
        _mainFieldsLayout.Controls.Add(_genreTextBox, 0, 3);
        _mainFieldsLayout.Controls.Add(_ratingFieldLabel, 1, 2);
        _mainFieldsLayout.Controls.Add(_ratingComboBox, 1, 3);
        _mainFieldsLayout.Dock = DockStyle.Fill;
        _mainFieldsLayout.Location = new Point(0, 42);
        _mainFieldsLayout.Margin = new Padding(0);
        _mainFieldsLayout.Name = "_mainFieldsLayout";
        _mainFieldsLayout.RowCount = 4;
        _mainFieldsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        _mainFieldsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
        _mainFieldsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        _mainFieldsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
        _mainFieldsLayout.Size = new Size(562, 178);
        _mainFieldsLayout.TabIndex = 1;
        // 
        // _titleFieldLabel
        // 
        _titleFieldLabel.Dock = DockStyle.Fill;
        _titleFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _titleFieldLabel.Location = new Point(0, 0);
        _titleFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _titleFieldLabel.Name = "_titleFieldLabel";
        _titleFieldLabel.Size = new Size(281, 24);
        _titleFieldLabel.TabIndex = 0;
        _titleFieldLabel.Text = "Titlu *";
        _titleFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _titleTextBox
        // 
        _titleTextBox.BorderStyle = BorderStyle.FixedSingle;
        _titleTextBox.Dock = DockStyle.Fill;
        _titleTextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _titleTextBox.Location = new Point(0, 28);
        _titleTextBox.Margin = new Padding(0, 0, 10, 12);
        _titleTextBox.Name = "_titleTextBox";
        _titleTextBox.PlaceholderText = "ex. Gravity Falls";
        _titleTextBox.Size = new Size(271, 28);
        _titleTextBox.TabIndex = 0;
        // 
        // _studioFieldLabel
        // 
        _studioFieldLabel.Dock = DockStyle.Fill;
        _studioFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _studioFieldLabel.Location = new Point(281, 0);
        _studioFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _studioFieldLabel.Name = "_studioFieldLabel";
        _studioFieldLabel.Size = new Size(281, 24);
        _studioFieldLabel.TabIndex = 1;
        _studioFieldLabel.Text = "Studio";
        _studioFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _studioTextBox
        // 
        _studioTextBox.BorderStyle = BorderStyle.FixedSingle;
        _studioTextBox.Dock = DockStyle.Fill;
        _studioTextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _studioTextBox.Location = new Point(281, 28);
        _studioTextBox.Margin = new Padding(0, 0, 0, 12);
        _studioTextBox.Name = "_studioTextBox";
        _studioTextBox.PlaceholderText = "ex. Disney";
        _studioTextBox.Size = new Size(281, 28);
        _studioTextBox.TabIndex = 1;
        // 
        // _genreFieldLabel
        // 
        _genreFieldLabel.Dock = DockStyle.Fill;
        _genreFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _genreFieldLabel.Location = new Point(0, 82);
        _genreFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _genreFieldLabel.Name = "_genreFieldLabel";
        _genreFieldLabel.Size = new Size(281, 24);
        _genreFieldLabel.TabIndex = 2;
        _genreFieldLabel.Text = "Gen";
        _genreFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _genreTextBox
        // 
        _genreTextBox.BorderStyle = BorderStyle.FixedSingle;
        _genreTextBox.Dock = DockStyle.Fill;
        _genreTextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _genreTextBox.Location = new Point(0, 110);
        _genreTextBox.Margin = new Padding(0, 0, 10, 12);
        _genreTextBox.Name = "_genreTextBox";
        _genreTextBox.PlaceholderText = "ex. Comedie";
        _genreTextBox.Size = new Size(271, 28);
        _genreTextBox.TabIndex = 2;
        // 
        // _ratingFieldLabel
        // 
        _ratingFieldLabel.Dock = DockStyle.Fill;
        _ratingFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _ratingFieldLabel.Location = new Point(281, 82);
        _ratingFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _ratingFieldLabel.Name = "_ratingFieldLabel";
        _ratingFieldLabel.Size = new Size(281, 24);
        _ratingFieldLabel.TabIndex = 3;
        _ratingFieldLabel.Text = "Rating vârstă";
        _ratingFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _ratingComboBox
        // 
        _ratingComboBox.Dock = DockStyle.Fill;
        _ratingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _ratingComboBox.FlatStyle = FlatStyle.Flat;
        _ratingComboBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _ratingComboBox.FormattingEnabled = true;
        _ratingComboBox.Location = new Point(281, 110);
        _ratingComboBox.Margin = new Padding(0, 0, 0, 12);
        _ratingComboBox.Name = "_ratingComboBox";
        _ratingComboBox.Size = new Size(281, 30);
        _ratingComboBox.TabIndex = 3;
        // 
        // _progressFieldsLayout
        // 
        _progressFieldsLayout.ColumnCount = 4;
        _progressFieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        _progressFieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        _progressFieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        _progressFieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        _progressFieldsLayout.Controls.Add(_totalFieldLabel, 0, 0);
        _progressFieldsLayout.Controls.Add(_totalNumeric, 0, 1);
        _progressFieldsLayout.Controls.Add(_watchedFieldLabel, 1, 0);
        _progressFieldsLayout.Controls.Add(_watchedNumeric, 1, 1);
        _progressFieldsLayout.Controls.Add(_statusFieldLabel, 2, 0);
        _progressFieldsLayout.Controls.Add(_statusComboBox, 2, 1);
        _progressFieldsLayout.Controls.Add(_scoreFieldLabel, 3, 0);
        _progressFieldsLayout.Controls.Add(_scoreNumeric, 3, 1);
        _progressFieldsLayout.Dock = DockStyle.Fill;
        _progressFieldsLayout.Location = new Point(0, 220);
        _progressFieldsLayout.Margin = new Padding(0);
        _progressFieldsLayout.Name = "_progressFieldsLayout";
        _progressFieldsLayout.RowCount = 2;
        _progressFieldsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        _progressFieldsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        _progressFieldsLayout.Size = new Size(562, 132);
        _progressFieldsLayout.TabIndex = 2;
        // 
        // _totalFieldLabel
        // 
        _totalFieldLabel.Dock = DockStyle.Fill;
        _totalFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _totalFieldLabel.Location = new Point(0, 0);
        _totalFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _totalFieldLabel.Name = "_totalFieldLabel";
        _totalFieldLabel.Size = new Size(140, 28);
        _totalFieldLabel.TabIndex = 0;
        _totalFieldLabel.Text = "Episoade";
        _totalFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _totalNumeric
        // 
        _totalNumeric.Dock = DockStyle.Fill;
        _totalNumeric.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _totalNumeric.Location = new Point(0, 32);
        _totalNumeric.Margin = new Padding(0, 0, 10, 12);
        _totalNumeric.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
        _totalNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        _totalNumeric.Name = "_totalNumeric";
        _totalNumeric.Size = new Size(130, 28);
        _totalNumeric.TabIndex = 4;
        _totalNumeric.Value = new decimal(new int[] { 12, 0, 0, 0 });
        _totalNumeric.ValueChanged += TotalNumeric_ValueChanged;
        // 
        // _watchedFieldLabel
        // 
        _watchedFieldLabel.Dock = DockStyle.Fill;
        _watchedFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _watchedFieldLabel.Location = new Point(140, 0);
        _watchedFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _watchedFieldLabel.Name = "_watchedFieldLabel";
        _watchedFieldLabel.Size = new Size(140, 28);
        _watchedFieldLabel.TabIndex = 1;
        _watchedFieldLabel.Text = "Văzute";
        _watchedFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _watchedNumeric
        // 
        _watchedNumeric.Dock = DockStyle.Fill;
        _watchedNumeric.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _watchedNumeric.Location = new Point(140, 32);
        _watchedNumeric.Margin = new Padding(0, 0, 10, 12);
        _watchedNumeric.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
        _watchedNumeric.Name = "_watchedNumeric";
        _watchedNumeric.Size = new Size(130, 28);
        _watchedNumeric.TabIndex = 5;
        _watchedNumeric.ValueChanged += WatchedNumeric_ValueChanged;
        // 
        // _statusFieldLabel
        // 
        _statusFieldLabel.Dock = DockStyle.Fill;
        _statusFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _statusFieldLabel.Location = new Point(280, 0);
        _statusFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _statusFieldLabel.Name = "_statusFieldLabel";
        _statusFieldLabel.Size = new Size(140, 28);
        _statusFieldLabel.TabIndex = 2;
        _statusFieldLabel.Text = "Status";
        _statusFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _statusComboBox
        // 
        _statusComboBox.Dock = DockStyle.Fill;
        _statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _statusComboBox.FlatStyle = FlatStyle.Flat;
        _statusComboBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _statusComboBox.FormattingEnabled = true;
        _statusComboBox.Location = new Point(280, 32);
        _statusComboBox.Margin = new Padding(0, 0, 10, 12);
        _statusComboBox.Name = "_statusComboBox";
        _statusComboBox.Size = new Size(130, 30);
        _statusComboBox.TabIndex = 6;
        _statusComboBox.SelectedIndexChanged += StatusComboBox_SelectedIndexChanged;
        // 
        // _scoreFieldLabel
        // 
        _scoreFieldLabel.Dock = DockStyle.Fill;
        _scoreFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _scoreFieldLabel.Location = new Point(420, 0);
        _scoreFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _scoreFieldLabel.Name = "_scoreFieldLabel";
        _scoreFieldLabel.Size = new Size(142, 28);
        _scoreFieldLabel.TabIndex = 3;
        _scoreFieldLabel.Text = "Scor";
        _scoreFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _scoreNumeric
        // 
        _scoreNumeric.Dock = DockStyle.Fill;
        _scoreNumeric.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _scoreNumeric.Location = new Point(420, 32);
        _scoreNumeric.Margin = new Padding(0, 0, 0, 12);
        _scoreNumeric.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _scoreNumeric.Name = "_scoreNumeric";
        _scoreNumeric.Size = new Size(142, 28);
        _scoreNumeric.TabIndex = 7;
        // 
        // _favoriteCharacterFieldLabel
        // 
        _favoriteCharacterFieldLabel.Dock = DockStyle.Fill;
        _favoriteCharacterFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _favoriteCharacterFieldLabel.Location = new Point(0, 352);
        _favoriteCharacterFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _favoriteCharacterFieldLabel.Name = "_favoriteCharacterFieldLabel";
        _favoriteCharacterFieldLabel.Size = new Size(562, 23);
        _favoriteCharacterFieldLabel.TabIndex = 3;
        _favoriteCharacterFieldLabel.Text = "Personaj favorit";
        _favoriteCharacterFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _favoriteCharacterTextBox
        // 
        _favoriteCharacterTextBox.BorderStyle = BorderStyle.FixedSingle;
        _favoriteCharacterTextBox.Dock = DockStyle.Fill;
        _favoriteCharacterTextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _favoriteCharacterTextBox.Location = new Point(0, 379);
        _favoriteCharacterTextBox.Margin = new Padding(0, 0, 0, 12);
        _favoriteCharacterTextBox.Name = "_favoriteCharacterTextBox";
        _favoriteCharacterTextBox.PlaceholderText = "ex. Mabel";
        _favoriteCharacterTextBox.Size = new Size(562, 28);
        _favoriteCharacterTextBox.TabIndex = 8;
        // 
        // _notesFieldLabel
        // 
        _notesFieldLabel.Dock = DockStyle.Fill;
        _notesFieldLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _notesFieldLabel.Location = new Point(0, 421);
        _notesFieldLabel.Margin = new Padding(0, 0, 0, 4);
        _notesFieldLabel.Name = "_notesFieldLabel";
        _notesFieldLabel.Size = new Size(562, 24);
        _notesFieldLabel.TabIndex = 5;
        _notesFieldLabel.Text = "Note";
        _notesFieldLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _notesTextBox
        // 
        _notesTextBox.BorderStyle = BorderStyle.FixedSingle;
        _notesTextBox.Dock = DockStyle.Fill;
        _notesTextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _notesTextBox.Location = new Point(0, 449);
        _notesTextBox.Margin = new Padding(0);
        _notesTextBox.Multiline = true;
        _notesTextBox.Name = "_notesTextBox";
        _notesTextBox.PlaceholderText = "Observații, motive, impresii...";
        _notesTextBox.ScrollBars = ScrollBars.Vertical;
        _notesTextBox.Size = new Size(562, 20);
        _notesTextBox.TabIndex = 9;
        // 
        // _buttonsPanel
        // 
        _buttonsPanel.AutoSize = true;
        _buttonsPanel.Controls.Add(_saveButton);
        _buttonsPanel.Controls.Add(_cancelButton);
        _buttonsPanel.Dock = DockStyle.Fill;
        _buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
        _buttonsPanel.Location = new Point(21, 643);
        _buttonsPanel.Margin = new Padding(0);
        _buttonsPanel.Name = "_buttonsPanel";
        _buttonsPanel.Padding = new Padding(0, 8, 0, 0);
        _buttonsPanel.Size = new Size(598, 59);
        _buttonsPanel.TabIndex = 2;
        // 
        // _saveButton
        // 
        _saveButton.Cursor = Cursors.Hand;
        _saveButton.Dock = DockStyle.Fill;
        _saveButton.FlatAppearance.BorderSize = 0;
        _saveButton.FlatStyle = FlatStyle.Flat;
        _saveButton.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _saveButton.Location = new Point(455, 11);
        _saveButton.Margin = new Padding(6, 3, 0, 3);
        _saveButton.Name = "_saveButton";
        _saveButton.Size = new Size(143, 38);
        _saveButton.TabIndex = 10;
        _saveButton.Text = "Salvează";
        _saveButton.UseVisualStyleBackColor = false;
        _saveButton.Click += SaveButton_Click;
        _saveButton.Resize += RoundedControl_Resize;
        // 
        // _cancelButton
        // 
        _cancelButton.Cursor = Cursors.Hand;
        _cancelButton.DialogResult = DialogResult.Cancel;
        _cancelButton.FlatAppearance.BorderSize = 0;
        _cancelButton.FlatStyle = FlatStyle.Flat;
        _cancelButton.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _cancelButton.Location = new Point(339, 11);
        _cancelButton.Margin = new Padding(6, 3, 6, 3);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.Size = new Size(104, 38);
        _cancelButton.TabIndex = 11;
        _cancelButton.Text = "Renunță";
        _cancelButton.UseVisualStyleBackColor = false;
        _cancelButton.Click += CancelButton_Click;
        _cancelButton.Resize += RoundedControl_Resize;
        // 
        // ShowForm
        // 
        AcceptButton = _saveButton;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _cancelButton;
        ClientSize = new Size(640, 720);
        Controls.Add(_root);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 3, 4, 3);
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(658, 767);
        Name = "ShowForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Adaugă desen/serial animat";
        _root.ResumeLayout(false);
        _root.PerformLayout();
        _headerPanel.ResumeLayout(false);
        _headerLayout.ResumeLayout(false);
        _formCard.ResumeLayout(false);
        _formLayout.ResumeLayout(false);
        _formLayout.PerformLayout();
        _mainFieldsLayout.ResumeLayout(false);
        _mainFieldsLayout.PerformLayout();
        _progressFieldsLayout.ResumeLayout(false);
        ((ISupportInitialize)_totalNumeric).EndInit();
        ((ISupportInitialize)_watchedNumeric).EndInit();
        ((ISupportInitialize)_scoreNumeric).EndInit();
        _buttonsPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _root;
    private Panel _headerPanel;
    private TableLayoutPanel _headerLayout;
    private Label _titleLabel;
    private Label _subtitleLabel;
    private Panel _formCard;
    private TableLayoutPanel _formLayout;
    private Label _hintLabel;
    private TableLayoutPanel _mainFieldsLayout;
    private Label _titleFieldLabel;
    private TextBox _titleTextBox;
    private Label _studioFieldLabel;
    private TextBox _studioTextBox;
    private Label _genreFieldLabel;
    private TextBox _genreTextBox;
    private Label _ratingFieldLabel;
    private ComboBox _ratingComboBox;
    private TableLayoutPanel _progressFieldsLayout;
    private Label _totalFieldLabel;
    private NumericUpDown _totalNumeric;
    private Label _watchedFieldLabel;
    private NumericUpDown _watchedNumeric;
    private Label _statusFieldLabel;
    private ComboBox _statusComboBox;
    private Label _scoreFieldLabel;
    private NumericUpDown _scoreNumeric;
    private Label _favoriteCharacterFieldLabel;
    private TextBox _favoriteCharacterTextBox;
    private Label _notesFieldLabel;
    private TextBox _notesTextBox;
    private FlowLayoutPanel _buttonsPanel;
    private Button _saveButton;
    private Button _cancelButton;
}
