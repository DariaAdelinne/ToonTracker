using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ToonTracker.UI;

partial class MainForm
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
        components = new Container();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        _bindingSource = new BindingSource(components);
        _root = new TableLayoutPanel();
        _headerPanel = new Panel();
        _headerLayout = new TableLayoutPanel();
        _titleLabel = new Label();
        _controlsCard = new Panel();
        _controlsLayout = new TableLayoutPanel();
        _actionsGroup = new GroupBox();
        _actionsCenterPanel = new TableLayoutPanel();
        _actionsFlow = new FlowLayoutPanel();
        _addButton = new Button();
        _editButton = new Button();
        _deleteButton = new Button();
        _watchedButton = new Button();
        _discoverGroup = new GroupBox();
        _discoverCenterPanel = new TableLayoutPanel();
        _discoverFlow = new FlowLayoutPanel();
        _recommendButton = new Button();
        _customRecommendButton = new Button();
        _statisticsButton = new Button();
        _exportButton = new Button();
        _themeButton = new Button();
        _helpButton = new Button();
        _resetButton = new Button();
        _filtersGroup = new GroupBox();
        _filtersLayout = new TableLayoutPanel();
        _statusLabel = new Label();
        _genreFilter = new ComboBox();
        _genreLabel = new Label();
        _searchLabel = new Label();
        _sortLabel = new Label();
        _sortComboBox = new ComboBox();
        _searchBox = new TextBox();
        _statusFilter = new ComboBox();
        _contentLayout = new TableLayoutPanel();
        _collectionGroup = new GroupBox();
        _grid = new DataGridView();
        _recommendationsGroup = new GroupBox();
        _recommendationsLayout = new TableLayoutPanel();
        _recommendationsBox = new TextBox();
        _wishlistPanel = new FlowLayoutPanel();
        _pickerLabel = new Label();
        _recommendationPicker = new ComboBox();
        _addToWishlistButton = new Button();
        _footerCard = new Panel();
        _statsLabel = new Label();
        ((ISupportInitialize)_bindingSource).BeginInit();
        _root.SuspendLayout();
        _headerPanel.SuspendLayout();
        _headerLayout.SuspendLayout();
        _controlsCard.SuspendLayout();
        _controlsLayout.SuspendLayout();
        _actionsGroup.SuspendLayout();
        _actionsCenterPanel.SuspendLayout();
        _actionsFlow.SuspendLayout();
        _discoverGroup.SuspendLayout();
        _discoverCenterPanel.SuspendLayout();
        _discoverFlow.SuspendLayout();
        _filtersGroup.SuspendLayout();
        _filtersLayout.SuspendLayout();
        _contentLayout.SuspendLayout();
        _collectionGroup.SuspendLayout();
        ((ISupportInitialize)_grid).BeginInit();
        _recommendationsGroup.SuspendLayout();
        _recommendationsLayout.SuspendLayout();
        _wishlistPanel.SuspendLayout();
        _footerCard.SuspendLayout();
        SuspendLayout();
        // 
        // _root
        // 
        _root.ColumnCount = 1;
        _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _root.Controls.Add(_headerPanel, 0, 0);
        _root.Controls.Add(_controlsCard, 0, 1);
        _root.Controls.Add(_contentLayout, 0, 2);
        _root.Controls.Add(_footerCard, 0, 3);
        _root.Dock = DockStyle.Fill;
        _root.Location = new Point(0, 0);
        _root.Margin = new Padding(0);
        _root.Name = "_root";
        _root.Padding = new Padding(15, 11, 15, 11);
        _root.RowCount = 4;
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 149F));
        _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        _root.Size = new Size(1050, 563);
        _root.TabIndex = 0;
        // 
        // _headerPanel
        // 
        _headerPanel.Controls.Add(_headerLayout);
        _headerPanel.Location = new Point(15, 11);
        _headerPanel.Margin = new Padding(0, 0, 0, 7);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.Padding = new Padding(20, 7, 20, 7);
        _headerPanel.Size = new Size(1019, 37);
        _headerPanel.TabIndex = 0;
        _headerPanel.Resize += RoundedControl_Resize;
        // 
        // _headerLayout
        // 
        _headerLayout.ColumnCount = 1;
        _headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _headerLayout.Controls.Add(_titleLabel, 0, 0);
        _headerLayout.Dock = DockStyle.Fill;
        _headerLayout.Location = new Point(20, 7);
        _headerLayout.Margin = new Padding(0);
        _headerLayout.Name = "_headerLayout";
        _headerLayout.RowCount = 1;
        _headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _headerLayout.Size = new Size(979, 23);
        _headerLayout.TabIndex = 0;
        // 
        // _titleLabel
        // 
        _titleLabel.AutoSize = true;
        _titleLabel.Dock = DockStyle.Fill;
        _titleLabel.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
        _titleLabel.Location = new Point(0, 0);
        _titleLabel.Margin = new Padding(0);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(979, 23);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "ToonTracker";
        _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _controlsCard
        // 
        _controlsCard.Controls.Add(_controlsLayout);
        _controlsCard.Dock = DockStyle.Fill;
        _controlsCard.Location = new Point(15, 55);
        _controlsCard.Margin = new Padding(0, 0, 0, 7);
        _controlsCard.Name = "_controlsCard";
        _controlsCard.Padding = new Padding(7, 5, 7, 5);
        _controlsCard.Size = new Size(1020, 142);
        _controlsCard.TabIndex = 1;
        _controlsCard.Resize += RoundedControl_Resize;
        // 
        // _controlsLayout
        // 
        _controlsLayout.ColumnCount = 3;
        _controlsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
        _controlsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.56546F));
        _controlsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.4791069F));
        _controlsLayout.Controls.Add(_actionsGroup, 0, 0);
        _controlsLayout.Controls.Add(_discoverGroup, 1, 0);
        _controlsLayout.Controls.Add(_filtersGroup, 2, 0);
        _controlsLayout.Dock = DockStyle.Fill;
        _controlsLayout.Location = new Point(7, 5);
        _controlsLayout.Margin = new Padding(0);
        _controlsLayout.Name = "_controlsLayout";
        _controlsLayout.RowCount = 1;
        _controlsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _controlsLayout.Size = new Size(1006, 132);
        _controlsLayout.TabIndex = 0;
        // 
        // _actionsGroup
        // 
        _actionsGroup.Controls.Add(_actionsCenterPanel);
        _actionsGroup.Dock = DockStyle.Fill;
        _actionsGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _actionsGroup.Location = new Point(4, 3);
        _actionsGroup.Margin = new Padding(4, 3, 4, 3);
        _actionsGroup.Name = "_actionsGroup";
        _actionsGroup.Padding = new Padding(8, 12, 8, 6);
        _actionsGroup.Size = new Size(233, 126);
        _actionsGroup.TabIndex = 0;
        _actionsGroup.TabStop = false;
        _actionsGroup.Text = "Administrare";
        // 
        // _actionsCenterPanel
        // 
        _actionsCenterPanel.ColumnCount = 1;
        _actionsCenterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _actionsCenterPanel.Controls.Add(_actionsFlow, 0, 0);
        _actionsCenterPanel.Dock = DockStyle.Fill;
        _actionsCenterPanel.Location = new Point(8, 26);
        _actionsCenterPanel.Margin = new Padding(0);
        _actionsCenterPanel.Name = "_actionsCenterPanel";
        _actionsCenterPanel.RowCount = 1;
        _actionsCenterPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _actionsCenterPanel.Size = new Size(217, 94);
        _actionsCenterPanel.TabIndex = 0;
        // 
        // _actionsFlow
        // 
        _actionsFlow.Anchor = AnchorStyles.None;
        _actionsFlow.AutoSize = true;
        _actionsFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _actionsFlow.Controls.Add(_addButton);
        _actionsFlow.Controls.Add(_editButton);
        _actionsFlow.Controls.Add(_deleteButton);
        _actionsFlow.Controls.Add(_watchedButton);
        _actionsFlow.Location = new Point(30, 20);
        _actionsFlow.Margin = new Padding(0);
        _actionsFlow.Name = "_actionsFlow";
        _actionsFlow.Size = new Size(156, 54);
        _actionsFlow.TabIndex = 0;
        // 
        // _addButton
        // 
        _addButton.Cursor = Cursors.Hand;
        _addButton.FlatAppearance.BorderSize = 0;
        _addButton.FlatStyle = FlatStyle.Flat;
        _addButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _addButton.Location = new Point(4, 2);
        _addButton.Margin = new Padding(4, 2, 4, 2);
        _addButton.Name = "_addButton";
        _addButton.Size = new Size(69, 23);
        _addButton.TabIndex = 0;
        _addButton.Text = "Adauga";
        _addButton.UseVisualStyleBackColor = false;
        _addButton.Click += AddButton_Click;
        _addButton.Resize += RoundedControl_Resize;
        // 
        // _editButton
        // 
        _editButton.Cursor = Cursors.Hand;
        _editButton.FlatAppearance.BorderSize = 0;
        _editButton.FlatStyle = FlatStyle.Flat;
        _editButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _editButton.Location = new Point(81, 2);
        _editButton.Margin = new Padding(4, 2, 4, 2);
        _editButton.Name = "_editButton";
        _editButton.Size = new Size(69, 23);
        _editButton.TabIndex = 1;
        _editButton.Text = "Editeaza";
        _editButton.UseVisualStyleBackColor = false;
        _editButton.Click += EditButton_Click;
        _editButton.Resize += RoundedControl_Resize;
        // 
        // _deleteButton
        // 
        _deleteButton.Cursor = Cursors.Hand;
        _deleteButton.FlatAppearance.BorderSize = 0;
        _deleteButton.FlatStyle = FlatStyle.Flat;
        _deleteButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _deleteButton.Location = new Point(4, 29);
        _deleteButton.Margin = new Padding(4, 2, 4, 2);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.Size = new Size(69, 23);
        _deleteButton.TabIndex = 2;
        _deleteButton.Text = "Sterge";
        _deleteButton.UseVisualStyleBackColor = false;
        _deleteButton.Click += DeleteButton_Click;
        _deleteButton.Resize += RoundedControl_Resize;
        // 
        // _watchedButton
        // 
        _watchedButton.Cursor = Cursors.Hand;
        _watchedButton.FlatAppearance.BorderSize = 0;
        _watchedButton.FlatStyle = FlatStyle.Flat;
        _watchedButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _watchedButton.Location = new Point(81, 29);
        _watchedButton.Margin = new Padding(4, 2, 4, 2);
        _watchedButton.Name = "_watchedButton";
        _watchedButton.Size = new Size(71, 23);
        _watchedButton.TabIndex = 3;
        _watchedButton.Text = "+1 episod";
        _watchedButton.UseVisualStyleBackColor = false;
        _watchedButton.Click += WatchedButton_Click;
        _watchedButton.Resize += RoundedControl_Resize;
        // 
        // _discoverGroup
        // 
        _discoverGroup.Controls.Add(_discoverCenterPanel);
        _discoverGroup.Dock = DockStyle.Fill;
        _discoverGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _discoverGroup.Location = new Point(245, 3);
        _discoverGroup.Margin = new Padding(4, 3, 4, 3);
        _discoverGroup.Name = "_discoverGroup";
        _discoverGroup.Padding = new Padding(8, 12, 8, 6);
        _discoverGroup.Size = new Size(329, 126);
        _discoverGroup.TabIndex = 1;
        _discoverGroup.TabStop = false;
        _discoverGroup.Text = "Descopera";
        // 
        // _discoverCenterPanel
        // 
        _discoverCenterPanel.ColumnCount = 1;
        _discoverCenterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _discoverCenterPanel.Controls.Add(_discoverFlow, 0, 0);
        _discoverCenterPanel.Dock = DockStyle.Fill;
        _discoverCenterPanel.Location = new Point(8, 26);
        _discoverCenterPanel.Margin = new Padding(0);
        _discoverCenterPanel.Name = "_discoverCenterPanel";
        _discoverCenterPanel.RowCount = 1;
        _discoverCenterPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _discoverCenterPanel.Size = new Size(313, 94);
        _discoverCenterPanel.TabIndex = 0;
        // 
        // _discoverFlow
        // 
        _discoverFlow.Anchor = AnchorStyles.None;
        _discoverFlow.AutoSize = true;
        _discoverFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _discoverFlow.Controls.Add(_recommendButton);
        _discoverFlow.Controls.Add(_customRecommendButton);
        _discoverFlow.Controls.Add(_statisticsButton);
        _discoverFlow.Controls.Add(_exportButton);
        _discoverFlow.Controls.Add(_themeButton);
        _discoverFlow.Controls.Add(_helpButton);
        _discoverFlow.Controls.Add(_resetButton);
        _discoverFlow.Location = new Point(12, 20);
        _discoverFlow.Margin = new Padding(0);
        _discoverFlow.Name = "_discoverFlow";
        _discoverFlow.Size = new Size(288, 54);
        _discoverFlow.TabIndex = 0;
        // 
        // _recommendButton
        // 
        _recommendButton.Cursor = Cursors.Hand;
        _recommendButton.FlatAppearance.BorderSize = 0;
        _recommendButton.FlatStyle = FlatStyle.Flat;
        _recommendButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _recommendButton.Location = new Point(4, 2);
        _recommendButton.Margin = new Padding(4, 2, 4, 2);
        _recommendButton.Name = "_recommendButton";
        _recommendButton.Size = new Size(97, 23);
        _recommendButton.TabIndex = 0;
        _recommendButton.Text = "Recomandari";
        _recommendButton.UseVisualStyleBackColor = false;
        _recommendButton.Click += RecommendButton_Click;
        _recommendButton.Resize += RoundedControl_Resize;
        // 
        // _customRecommendButton
        // 
        _customRecommendButton.Cursor = Cursors.Hand;
        _customRecommendButton.FlatAppearance.BorderSize = 0;
        _customRecommendButton.FlatStyle = FlatStyle.Flat;
        _customRecommendButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _customRecommendButton.Location = new Point(109, 2);
        _customRecommendButton.Margin = new Padding(4, 2, 4, 2);
        _customRecommendButton.Name = "_customRecommendButton";
        _customRecommendButton.Size = new Size(88, 23);
        _customRecommendButton.TabIndex = 1;
        _customRecommendButton.Text = "Preferinte";
        _customRecommendButton.UseVisualStyleBackColor = false;
        _customRecommendButton.Click += CustomRecommendButton_Click;
        _customRecommendButton.Resize += RoundedControl_Resize;
        // 
        // _statisticsButton
        // 
        _statisticsButton.Cursor = Cursors.Hand;
        _statisticsButton.FlatAppearance.BorderSize = 0;
        _statisticsButton.FlatStyle = FlatStyle.Flat;
        _statisticsButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _statisticsButton.Location = new Point(205, 2);
        _statisticsButton.Margin = new Padding(4, 2, 4, 2);
        _statisticsButton.Name = "_statisticsButton";
        _statisticsButton.Size = new Size(78, 23);
        _statisticsButton.TabIndex = 2;
        _statisticsButton.Text = "Statistici";
        _statisticsButton.UseVisualStyleBackColor = false;
        _statisticsButton.Click += StatisticsButton_Click;
        _statisticsButton.Resize += RoundedControl_Resize;
        // 
        // _exportButton
        // 
        _exportButton.Cursor = Cursors.Hand;
        _exportButton.FlatAppearance.BorderSize = 0;
        _exportButton.FlatStyle = FlatStyle.Flat;
        _exportButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _exportButton.Location = new Point(4, 29);
        _exportButton.Margin = new Padding(4, 2, 4, 2);
        _exportButton.Name = "_exportButton";
        _exportButton.Size = new Size(70, 23);
        _exportButton.TabIndex = 3;
        _exportButton.Text = "Export";
        _exportButton.UseVisualStyleBackColor = false;
        _exportButton.Click += ExportButton_Click;
        _exportButton.Resize += RoundedControl_Resize;
        // 
        // _themeButton
        // 
        _themeButton.Cursor = Cursors.Hand;
        _themeButton.FlatAppearance.BorderSize = 0;
        _themeButton.FlatStyle = FlatStyle.Flat;
        _themeButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _themeButton.Location = new Point(82, 29);
        _themeButton.Margin = new Padding(4, 2, 4, 2);
        _themeButton.Name = "_themeButton";
        _themeButton.Size = new Size(62, 23);
        _themeButton.TabIndex = 4;
        _themeButton.Text = "Tema";
        _themeButton.UseVisualStyleBackColor = false;
        _themeButton.Click += ThemeButton_Click;
        _themeButton.Resize += RoundedControl_Resize;
        // 
        // _helpButton
        // 
        _helpButton.Cursor = Cursors.Hand;
        _helpButton.FlatAppearance.BorderSize = 0;
        _helpButton.FlatStyle = FlatStyle.Flat;
        _helpButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _helpButton.Location = new Point(152, 29);
        _helpButton.Margin = new Padding(4, 2, 4, 2);
        _helpButton.Name = "_helpButton";
        _helpButton.Size = new Size(62, 23);
        _helpButton.TabIndex = 5;
        _helpButton.Text = "Help";
        _helpButton.UseVisualStyleBackColor = false;
        _helpButton.Click += HelpButton_Click;
        _helpButton.Resize += RoundedControl_Resize;
        // 
        // _resetButton
        // 
        _resetButton.Cursor = Cursors.Hand;
        _resetButton.FlatAppearance.BorderSize = 0;
        _resetButton.FlatStyle = FlatStyle.Flat;
        _resetButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _resetButton.Location = new Point(222, 29);
        _resetButton.Margin = new Padding(4, 2, 4, 2);
        _resetButton.Name = "_resetButton";
        _resetButton.Size = new Size(62, 23);
        _resetButton.TabIndex = 6;
        _resetButton.Text = "Reset";
        _resetButton.UseVisualStyleBackColor = false;
        _resetButton.Click += ResetButton_Click;
        _resetButton.Resize += RoundedControl_Resize;
        // 
        // _filtersGroup
        // 
        _filtersGroup.Controls.Add(_filtersLayout);
        _filtersGroup.Dock = DockStyle.Fill;
        _filtersGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _filtersGroup.Location = new Point(582, 3);
        _filtersGroup.Margin = new Padding(4, 3, 4, 3);
        _filtersGroup.Name = "_filtersGroup";
        _filtersGroup.Padding = new Padding(8, 12, 8, 6);
        _filtersGroup.Size = new Size(420, 126);
        _filtersGroup.TabIndex = 2;
        _filtersGroup.TabStop = false;
        _filtersGroup.Text = "Cautare & filtre";
        // 
        // _filtersLayout
        // 
        _filtersLayout.ColumnCount = 2;
        _filtersLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.2201042F));
        _filtersLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.7798958F));
        _filtersLayout.Controls.Add(_statusLabel, 1, 2);
        _filtersLayout.Controls.Add(_genreFilter, 0, 3);
        _filtersLayout.Controls.Add(_genreLabel, 0, 2);
        _filtersLayout.Controls.Add(_searchLabel, 0, 0);
        _filtersLayout.Controls.Add(_sortLabel, 0, 0);
        _filtersLayout.Controls.Add(_sortComboBox, 1, 1);
        _filtersLayout.Controls.Add(_searchBox, 0, 1);
        _filtersLayout.Controls.Add(_statusFilter, 1, 3);
        _filtersLayout.Location = new Point(8, 24);
        _filtersLayout.Margin = new Padding(0);
        _filtersLayout.Name = "_filtersLayout";
        _filtersLayout.RowCount = 5;
        _filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 17F));
        _filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
        _filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        _filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        _filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        _filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
        _filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
        _filtersLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
        _filtersLayout.Size = new Size(404, 97);
        _filtersLayout.TabIndex = 0;
        _filtersLayout.Paint += _filtersLayout_Paint;
        // 
        // _statusLabel
        // 
        _statusLabel.AutoSize = true;
        _statusLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _statusLabel.Location = new Point(198, 45);
        _statusLabel.Margin = new Padding(0, 1, 0, 3);
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Size = new Size(47, 15);
        _statusLabel.TabIndex = 2;
        _statusLabel.Text = "Status";
        // 
        // _genreFilter
        // 
        _genreFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        _genreFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        _genreFilter.FlatStyle = FlatStyle.Flat;
        _genreFilter.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _genreFilter.FormattingEnabled = true;
        _genreFilter.Location = new Point(4, 63);
        _genreFilter.Margin = new Padding(4, 0, 7, 5);
        _genreFilter.Name = "_genreFilter";
        _genreFilter.Size = new Size(187, 23);
        _genreFilter.TabIndex = 1;
        _genreFilter.SelectedIndexChanged += GenreFilterChanged;
        // 
        // _genreLabel
        // 
        _genreLabel.AutoSize = true;
        _genreLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _genreLabel.Location = new Point(0, 45);
        _genreLabel.Margin = new Padding(0, 1, 0, 3);
        _genreLabel.Name = "_genreLabel";
        _genreLabel.Size = new Size(33, 15);
        _genreLabel.TabIndex = 1;
        _genreLabel.Text = "Gen";
        // 
        // _searchLabel
        // 
        _searchLabel.AutoSize = true;
        _searchLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _searchLabel.Location = new Point(198, 1);
        _searchLabel.Margin = new Padding(0, 1, 0, 3);
        _searchLabel.Name = "_searchLabel";
        _searchLabel.Size = new Size(57, 13);
        _searchLabel.TabIndex = 0;
        _searchLabel.Text = "Cautare";
        // 
        // _sortLabel
        // 
        _sortLabel.AutoSize = true;
        _sortLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _sortLabel.Location = new Point(0, 1);
        _sortLabel.Margin = new Padding(0, 1, 0, 3);
        _sortLabel.Name = "_sortLabel";
        _sortLabel.Size = new Size(67, 13);
        _sortLabel.TabIndex = 3;
        _sortLabel.Text = "Ordonare";
        // 
        // _sortComboBox
        // 
        _sortComboBox.Dock = DockStyle.Bottom;
        _sortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _sortComboBox.FlatStyle = FlatStyle.Flat;
        _sortComboBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _sortComboBox.FormattingEnabled = true;
        _sortComboBox.Items.AddRange(new object[] { "Alfabetic", "Numar episoade", "Progres", "Scor" });
        _sortComboBox.Location = new Point(198, 17);
        _sortComboBox.Margin = new Padding(0, 0, 7, 5);
        _sortComboBox.Name = "_sortComboBox";
        _sortComboBox.Size = new Size(199, 23);
        _sortComboBox.TabIndex = 3;
        _sortComboBox.SelectedIndexChanged += SortComboBox_SelectedIndexChanged;
        // 
        // _searchBox
        // 
        _searchBox.BorderStyle = BorderStyle.FixedSingle;
        _searchBox.Dock = DockStyle.Bottom;
        _searchBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _searchBox.Location = new Point(0, 18);
        _searchBox.Margin = new Padding(0, 0, 7, 5);
        _searchBox.Name = "_searchBox";
        _searchBox.PlaceholderText = "Cauta titlu, gen, studio...";
        _searchBox.Size = new Size(191, 21);
        _searchBox.TabIndex = 0;
        _searchBox.TextChanged += SearchBox_TextChanged;
        // 
        // _statusFilter
        // 
        _statusFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        _statusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        _statusFilter.FlatStyle = FlatStyle.Flat;
        _statusFilter.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _statusFilter.FormattingEnabled = true;
        _statusFilter.Location = new Point(198, 63);
        _statusFilter.Margin = new Padding(0, 0, 7, 5);
        _statusFilter.Name = "_statusFilter";
        _statusFilter.Size = new Size(199, 23);
        _statusFilter.TabIndex = 2;
        _statusFilter.SelectedIndexChanged += StatusFilterChanged;
        // 
        // _contentLayout
        // 
        _contentLayout.ColumnCount = 1;
        _contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _contentLayout.Controls.Add(_collectionGroup, 0, 0);
        _contentLayout.Controls.Add(_recommendationsGroup, 0, 1);
        _contentLayout.Dock = DockStyle.Fill;
        _contentLayout.Location = new Point(15, 204);
        _contentLayout.Margin = new Padding(0);
        _contentLayout.Name = "_contentLayout";
        _contentLayout.RowCount = 2;
        _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 63F));
        _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 37F));
        _contentLayout.Size = new Size(1020, 312);
        _contentLayout.TabIndex = 2;
        // 
        // _collectionGroup
        // 
        _collectionGroup.Controls.Add(_grid);
        _collectionGroup.Dock = DockStyle.Fill;
        _collectionGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _collectionGroup.Location = new Point(4, 3);
        _collectionGroup.Margin = new Padding(4, 3, 4, 3);
        _collectionGroup.Name = "_collectionGroup";
        _collectionGroup.Padding = new Padding(8, 12, 8, 6);
        _collectionGroup.Size = new Size(1012, 190);
        _collectionGroup.TabIndex = 0;
        _collectionGroup.TabStop = false;
        _collectionGroup.Text = "Colectia mea";
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AutoGenerateColumns = false;
        _grid.BorderStyle = BorderStyle.None;
        _grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _grid.ColumnHeadersHeight = 38;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.DataSource = _bindingSource;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Regular, GraphicsUnit.Point);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.Padding = new Padding(6, 4, 6, 4);
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle2;
        _grid.Dock = DockStyle.Fill;
        _grid.Location = new Point(8, 26);
        _grid.Margin = new Padding(3, 2, 3, 2);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 51;
        _grid.RowTemplate.Height = 34;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(996, 158);
        _grid.TabIndex = 0;
        _grid.CellDoubleClick += Grid_CellDoubleClick;
        // 
        // _recommendationsGroup
        // 
        _recommendationsGroup.Controls.Add(_recommendationsLayout);
        _recommendationsGroup.Dock = DockStyle.Fill;
        _recommendationsGroup.Font = new Font("Microsoft Sans Serif", 8.8F, FontStyle.Bold, GraphicsUnit.Point);
        _recommendationsGroup.Location = new Point(4, 199);
        _recommendationsGroup.Margin = new Padding(4, 3, 4, 3);
        _recommendationsGroup.Name = "_recommendationsGroup";
        _recommendationsGroup.Padding = new Padding(8, 12, 8, 6);
        _recommendationsGroup.Size = new Size(1012, 110);
        _recommendationsGroup.TabIndex = 1;
        _recommendationsGroup.TabStop = false;
        _recommendationsGroup.Text = "Recomandari inteligente & Wishlist";
        // 
        // _recommendationsLayout
        // 
        _recommendationsLayout.ColumnCount = 1;
        _recommendationsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _recommendationsLayout.Controls.Add(_recommendationsBox, 0, 0);
        _recommendationsLayout.Controls.Add(_wishlistPanel, 0, 1);
        _recommendationsLayout.Dock = DockStyle.Fill;
        _recommendationsLayout.Location = new Point(8, 26);
        _recommendationsLayout.Margin = new Padding(0);
        _recommendationsLayout.Name = "_recommendationsLayout";
        _recommendationsLayout.RowCount = 2;
        _recommendationsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _recommendationsLayout.RowStyles.Add(new RowStyle());
        _recommendationsLayout.Size = new Size(996, 78);
        _recommendationsLayout.TabIndex = 0;
        // 
        // _recommendationsBox
        // 
        _recommendationsBox.BorderStyle = BorderStyle.FixedSingle;
        _recommendationsBox.Dock = DockStyle.Fill;
        _recommendationsBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _recommendationsBox.Location = new Point(3, 2);
        _recommendationsBox.Margin = new Padding(3, 2, 3, 2);
        _recommendationsBox.Multiline = true;
        _recommendationsBox.Name = "_recommendationsBox";
        _recommendationsBox.ReadOnly = true;
        _recommendationsBox.ScrollBars = ScrollBars.Vertical;
        _recommendationsBox.Size = new Size(990, 36);
        _recommendationsBox.TabIndex = 0;
        _recommendationsBox.Text = "Apasa Recomandari pentru sugestii pe baza titlurilor finalizate sau Recomandari custom pentru preferinte manuale.";
        // 
        // _wishlistPanel
        // 
        _wishlistPanel.AutoSize = true;
        _wishlistPanel.Controls.Add(_pickerLabel);
        _wishlistPanel.Controls.Add(_recommendationPicker);
        _wishlistPanel.Controls.Add(_addToWishlistButton);
        _wishlistPanel.Dock = DockStyle.Fill;
        _wishlistPanel.Location = new Point(3, 42);
        _wishlistPanel.Margin = new Padding(3, 2, 3, 2);
        _wishlistPanel.Name = "_wishlistPanel";
        _wishlistPanel.Padding = new Padding(0, 6, 0, 0);
        _wishlistPanel.Size = new Size(990, 34);
        _wishlistPanel.TabIndex = 1;
        // 
        // _pickerLabel
        // 
        _pickerLabel.AutoSize = true;
        _pickerLabel.Font = new Font("Microsoft Sans Serif", 8.4F, FontStyle.Bold, GraphicsUnit.Point);
        _pickerLabel.Location = new Point(0, 13);
        _pickerLabel.Margin = new Padding(0, 7, 7, 3);
        _pickerLabel.Name = "_pickerLabel";
        _pickerLabel.Size = new Size(163, 15);
        _pickerLabel.TabIndex = 0;
        _pickerLabel.Text = "Recomandare selectata:";
        // 
        // _recommendationPicker
        // 
        _recommendationPicker.DropDownStyle = ComboBoxStyle.DropDownList;
        _recommendationPicker.FlatStyle = FlatStyle.Flat;
        _recommendationPicker.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _recommendationPicker.FormattingEnabled = true;
        _recommendationPicker.Location = new Point(170, 6);
        _recommendationPicker.Margin = new Padding(0, 0, 7, 5);
        _recommendationPicker.Name = "_recommendationPicker";
        _recommendationPicker.Size = new Size(228, 23);
        _recommendationPicker.TabIndex = 1;
        // 
        // _addToWishlistButton
        // 
        _addToWishlistButton.Cursor = Cursors.Hand;
        _addToWishlistButton.FlatAppearance.BorderSize = 0;
        _addToWishlistButton.FlatStyle = FlatStyle.Flat;
        _addToWishlistButton.Font = new Font("Microsoft Sans Serif", 8.2F, FontStyle.Bold, GraphicsUnit.Point);
        _addToWishlistButton.Location = new Point(409, 8);
        _addToWishlistButton.Margin = new Padding(4, 2, 4, 2);
        _addToWishlistButton.Name = "_addToWishlistButton";
        _addToWishlistButton.Size = new Size(132, 23);
        _addToWishlistButton.TabIndex = 2;
        _addToWishlistButton.Text = "Adauga in wishlist";
        _addToWishlistButton.UseVisualStyleBackColor = false;
        _addToWishlistButton.Click += AddToWishlistButton_Click;
        _addToWishlistButton.Resize += RoundedControl_Resize;
        // 
        // _footerCard
        // 
        _footerCard.Controls.Add(_statsLabel);
        _footerCard.Dock = DockStyle.Fill;
        _footerCard.Location = new Point(15, 523);
        _footerCard.Margin = new Padding(0, 7, 0, 0);
        _footerCard.Name = "_footerCard";
        _footerCard.Padding = new Padding(14, 6, 14, 6);
        _footerCard.Size = new Size(1020, 29);
        _footerCard.TabIndex = 3;
        _footerCard.Resize += RoundedControl_Resize;
        // 
        // _statsLabel
        // 
        _statsLabel.Dock = DockStyle.Fill;
        _statsLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _statsLabel.Location = new Point(14, 6);
        _statsLabel.Name = "_statsLabel";
        _statsLabel.Size = new Size(992, 17);
        _statsLabel.TabIndex = 0;
        _statsLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1050, 563);
        Controls.Add(_root);
        Margin = new Padding(3, 2, 3, 2);
        MinimumSize = new Size(859, 547);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ToonTracker - jurnal pentru desene animate si seriale";
        ((ISupportInitialize)_bindingSource).EndInit();
        _root.ResumeLayout(false);
        _headerPanel.ResumeLayout(false);
        _headerLayout.ResumeLayout(false);
        _headerLayout.PerformLayout();
        _controlsCard.ResumeLayout(false);
        _controlsLayout.ResumeLayout(false);
        _actionsGroup.ResumeLayout(false);
        _actionsCenterPanel.ResumeLayout(false);
        _actionsCenterPanel.PerformLayout();
        _actionsFlow.ResumeLayout(false);
        _discoverGroup.ResumeLayout(false);
        _discoverCenterPanel.ResumeLayout(false);
        _discoverCenterPanel.PerformLayout();
        _discoverFlow.ResumeLayout(false);
        _filtersGroup.ResumeLayout(false);
        _filtersLayout.ResumeLayout(false);
        _filtersLayout.PerformLayout();
        _contentLayout.ResumeLayout(false);
        _collectionGroup.ResumeLayout(false);
        ((ISupportInitialize)_grid).EndInit();
        _recommendationsGroup.ResumeLayout(false);
        _recommendationsLayout.ResumeLayout(false);
        _recommendationsLayout.PerformLayout();
        _wishlistPanel.ResumeLayout(false);
        _wishlistPanel.PerformLayout();
        _footerCard.ResumeLayout(false);
        ResumeLayout(false);
    }
    #endregion

    private BindingSource _bindingSource;
    private TableLayoutPanel _root;
    private Panel _headerPanel;
    private TableLayoutPanel _headerLayout;
    private Label _titleLabel;
    private Panel _controlsCard;
    private TableLayoutPanel _controlsLayout;
    private GroupBox _actionsGroup;
    private TableLayoutPanel _actionsCenterPanel;
    private FlowLayoutPanel _actionsFlow;
    private Button _addButton;
    private Button _editButton;
    private Button _deleteButton;
    private Button _watchedButton;
    private GroupBox _discoverGroup;
    private TableLayoutPanel _discoverCenterPanel;
    private FlowLayoutPanel _discoverFlow;
    private Button _recommendButton;
    private Button _customRecommendButton;
    private Button _statisticsButton;
    private Button _exportButton;
    private Button _themeButton;
    private Button _helpButton;
    private GroupBox _filtersGroup;
    private TableLayoutPanel _filtersLayout;
    private Label _searchLabel;
    private Label _genreLabel;
    private Label _statusLabel;
    private Label _sortLabel;
    private TextBox _searchBox;
    private ComboBox _genreFilter;
    private ComboBox _statusFilter;
    private ComboBox _sortComboBox;
    private Button _resetButton;
    private TableLayoutPanel _contentLayout;
    private GroupBox _collectionGroup;
    private DataGridView _grid;
    private DataGridViewTextBoxColumn _titleColumn;
    private DataGridViewTextBoxColumn _studioColumn;
    private DataGridViewTextBoxColumn _genreColumn;
    private DataGridViewTextBoxColumn _totalEpisodesColumn;
    private DataGridViewTextBoxColumn _watchedEpisodesColumn;
    private DataGridViewTextBoxColumn _progressColumn;
    private DataGridViewTextBoxColumn _ratingColumn;
    private DataGridViewTextBoxColumn _statusColumn;
    private DataGridViewTextBoxColumn _personalScoreColumn;
    private DataGridViewTextBoxColumn _favoriteCharacterColumn;
    private DataGridViewTextBoxColumn _notesColumn;
    private GroupBox _recommendationsGroup;
    private TableLayoutPanel _recommendationsLayout;
    private TextBox _recommendationsBox;
    private FlowLayoutPanel _wishlistPanel;
    private Label _pickerLabel;
    private ComboBox _recommendationPicker;
    private Button _addToWishlistButton;
    private Panel _footerCard;
    private Label _statsLabel;
    private PaintEventHandler _actionsFlow_Paint;
}
