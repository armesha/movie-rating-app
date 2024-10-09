using System;
using System.Windows.Forms;

namespace MovieRatingApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private DataGridView directorsDataGridView;
        private DataGridView genresDataGridView;
        private ToolStripMenuItem saveMenuItem;
        private ToolStripMenuItem loadMenuItem;
        private TabPage moviesTabPage;
        private TabPage reviewsTabPage;
        private Button btnAddMovie;
        private Button btnEditMovie;
        private Button btnDeleteMovie;
        private DataGridView moviesDataGridView;
        private TabControl mainTabControl;
        private Button btnAddReview;
        private ComboBox comboBoxMovies;
        private NumericUpDown numericUpDownRating;
        private Button btnSaveReview;
        private TextBox txtSearchTitle;
        private ComboBox cmbSearchDirector;
        private ComboBox cmbSearchGenre;
        private Button btnSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            saveMenuItem = new ToolStripMenuItem();
            loadMenuItem = new ToolStripMenuItem();
            directorsDataGridView = new DataGridView();
            genresDataGridView = new DataGridView();
            moviesTabPage = new TabPage();
            btnDeleteAllMovies = new Button();
            btnAddMovie = new Button();
            btnEditMovie = new Button();
            btnDeleteMovie = new Button();
            moviesDataGridView = new DataGridView();
            reviewsTabPage = new TabPage();
            btnDeleteAllReviews = new Button();
            textReview = new RichTextBox();
            btnAddReview = new Button();
            listView1 = new ListView();
            numericUpDownRating = new NumericUpDown();
            comboBoxMovies = new ComboBox();
            btnSaveReview = new Button();
            mainTabControl = new TabControl();
            searchTabPage = new TabPage();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtSearchTitle = new TextBox();
            cmbSearchDirector = new ComboBox();
            cmbSearchGenre = new ComboBox();
            btnSearch = new Button();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)directorsDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)genresDataGridView).BeginInit();
            moviesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)moviesDataGridView).BeginInit();
            reviewsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRating).BeginInit();
            mainTabControl.SuspendLayout();
            searchTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { saveMenuItem, loadMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(1107, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";

            // tento řádek kódu zajišťuje, že se okno aplikace nedá zvětšit nebo zmenšit
            this.FormBorderStyle = FormBorderStyle.FixedSingle; 

            // 
            // saveMenuItem
            // 
            saveMenuItem.Name = "saveMenuItem";
            saveMenuItem.Size = new Size(49, 20);
            saveMenuItem.Text = "Uložit";
            saveMenuItem.Click += SaveMenuItem_Click;
            // 
            // loadMenuItem
            // 
            loadMenuItem.Name = "loadMenuItem";
            loadMenuItem.Size = new Size(52, 20);
            loadMenuItem.Text = "Načíst";
            loadMenuItem.Click += LoadMenuItem_Click;
            // 
            // directorsDataGridView
            // 
            directorsDataGridView.ColumnHeadersHeight = 29;
            directorsDataGridView.Location = new Point(0, 0);
            directorsDataGridView.Margin = new Padding(4, 3, 4, 3);
            directorsDataGridView.Name = "directorsDataGridView";
            directorsDataGridView.RowHeadersWidth = 51;
            directorsDataGridView.Size = new Size(280, 173);
            directorsDataGridView.TabIndex = 0;
            // 
            // genresDataGridView
            // 
            genresDataGridView.ColumnHeadersHeight = 29;
            genresDataGridView.Location = new Point(0, 0);
            genresDataGridView.Margin = new Padding(4, 3, 4, 3);
            genresDataGridView.Name = "genresDataGridView";
            genresDataGridView.RowHeadersWidth = 51;
            genresDataGridView.Size = new Size(280, 173);
            genresDataGridView.TabIndex = 0;
            // 
            // moviesTabPage
            // 
            moviesTabPage.Controls.Add(btnDeleteAllMovies);
            moviesTabPage.Controls.Add(btnAddMovie);
            moviesTabPage.Controls.Add(btnEditMovie);
            moviesTabPage.Controls.Add(btnDeleteMovie);
            moviesTabPage.Controls.Add(moviesDataGridView);
            moviesTabPage.Location = new Point(4, 24);
            moviesTabPage.Margin = new Padding(4, 3, 4, 3);
            moviesTabPage.Name = "moviesTabPage";
            moviesTabPage.Padding = new Padding(4, 3, 4, 3);
            moviesTabPage.Size = new Size(1099, 519);
            moviesTabPage.TabIndex = 0;
            moviesTabPage.Text = "Filmy";
            moviesTabPage.UseVisualStyleBackColor = true;
            // 
            // btnDeleteAllMovies
            // 
            btnDeleteAllMovies.Location = new Point(947, 425);
            btnDeleteAllMovies.Name = "btnDeleteAllMovies";
            btnDeleteAllMovies.Size = new Size(145, 57);
            btnDeleteAllMovies.TabIndex = 3;
            btnDeleteAllMovies.Text = "Smazat všechny filmy";
            btnDeleteAllMovies.UseVisualStyleBackColor = true;
            btnDeleteAllMovies.Click += button1_Click;
            // 
            // btnAddMovie
            // 
            btnAddMovie.Location = new Point(15, 425);
            btnAddMovie.Name = "btnAddMovie";
            btnAddMovie.Size = new Size(80, 47);
            btnAddMovie.TabIndex = 0;
            btnAddMovie.Text = "Přidat film";
            btnAddMovie.UseVisualStyleBackColor = true;
            btnAddMovie.Click += AddMovie;
            // 
            // btnEditMovie
            // 
            btnEditMovie.Location = new Point(101, 425);
            btnEditMovie.Name = "btnEditMovie";
            btnEditMovie.Size = new Size(129, 47);
            btnEditMovie.TabIndex = 1;
            btnEditMovie.Text = "Upravit film";
            btnEditMovie.UseVisualStyleBackColor = true;
            btnEditMovie.Click += EditMovie;
            // 
            // btnDeleteMovie
            // 
            btnDeleteMovie.Location = new Point(340, 425);
            btnDeleteMovie.Name = "btnDeleteMovie";
            btnDeleteMovie.Size = new Size(118, 47);
            btnDeleteMovie.TabIndex = 2;
            btnDeleteMovie.Text = "Odstranit film";
            btnDeleteMovie.UseVisualStyleBackColor = true;
            btnDeleteMovie.Click += DeleteMovie;
            // 
            // moviesDataGridView
            // 
            moviesDataGridView.ColumnHeadersHeight = 29;
            moviesDataGridView.Location = new Point(0, 0);
            moviesDataGridView.Margin = new Padding(4, 3, 4, 3);
            moviesDataGridView.Name = "moviesDataGridView";
            moviesDataGridView.RowHeadersWidth = 51;
            moviesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            moviesDataGridView.Size = new Size(1095, 419);
            moviesDataGridView.TabIndex = 0;
            // 
            // reviewsTabPage
            // 
            reviewsTabPage.Controls.Add(btnDeleteAllReviews);
            reviewsTabPage.Controls.Add(textReview);
            reviewsTabPage.Controls.Add(btnAddReview);
            reviewsTabPage.Controls.Add(listView1);
            reviewsTabPage.Controls.Add(numericUpDownRating);
            reviewsTabPage.Controls.Add(comboBoxMovies);
            reviewsTabPage.Controls.Add(btnSaveReview);
            reviewsTabPage.Location = new Point(4, 24);
            reviewsTabPage.Name = "reviewsTabPage";
            reviewsTabPage.Padding = new Padding(3);
            reviewsTabPage.Size = new Size(1099, 519);
            reviewsTabPage.TabIndex = 2;
            reviewsTabPage.Text = "Recenze";
            reviewsTabPage.UseVisualStyleBackColor = true;
            // 
            // btnDeleteAllReviews
            // 
            btnDeleteAllReviews.Location = new Point(958, 436);
            btnDeleteAllReviews.Name = "btnDeleteAllReviews";
            btnDeleteAllReviews.Size = new Size(135, 52);
            btnDeleteAllReviews.TabIndex = 14;
            btnDeleteAllReviews.Text = "Smazat všechny recenze";
            btnDeleteAllReviews.UseVisualStyleBackColor = true;
            btnDeleteAllReviews.Click += btnDeleteAllReviews_Click;
            // 
            // textReview
            // 
            textReview.Location = new Point(328, 419);
            textReview.Name = "textReview";
            textReview.Size = new Size(454, 69);
            textReview.TabIndex = 12;
            textReview.Text = "";
            textReview.TextChanged += textReview_TextChanged;
            // 
            // btnAddReview
            // 
            btnAddReview.Location = new Point(788, 420);
            btnAddReview.Name = "btnAddReview";
            btnAddReview.Size = new Size(85, 39);
            btnAddReview.TabIndex = 7;
            btnAddReview.Text = "Přidat";
            btnAddReview.Click += btnAddReview_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(8, 6);
            listView1.Name = "listView1";
            listView1.Size = new Size(1088, 408);
            listView1.TabIndex = 6;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // numericUpDownRating
            // 
            numericUpDownRating.Location = new Point(202, 420);
            numericUpDownRating.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownRating.Name = "numericUpDownRating";
            numericUpDownRating.Size = new Size(120, 23);
            numericUpDownRating.TabIndex = 3;
            // 
            // comboBoxMovies
            // 
            comboBoxMovies.Location = new Point(8, 420);
            comboBoxMovies.Name = "comboBoxMovies";
            comboBoxMovies.Size = new Size(166, 23);
            comboBoxMovies.TabIndex = 2;
            // 
            // btnSaveReview
            // 
            btnSaveReview.Location = new Point(30, 29);
            btnSaveReview.Name = "btnSaveReview";
            btnSaveReview.Size = new Size(75, 23);
            btnSaveReview.TabIndex = 8;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(moviesTabPage);
            mainTabControl.Controls.Add(reviewsTabPage);
            mainTabControl.Controls.Add(searchTabPage);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 24);
            mainTabControl.Margin = new Padding(4, 3, 4, 3);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(1107, 547);
            mainTabControl.TabIndex = 1;
            // 
            // searchTabPage
            // 
            searchTabPage.Controls.Add(dataGridView1);
            searchTabPage.Controls.Add(label3);
            searchTabPage.Controls.Add(label2);
            searchTabPage.Controls.Add(label1);
            searchTabPage.Controls.Add(txtSearchTitle);
            searchTabPage.Controls.Add(cmbSearchDirector);
            searchTabPage.Controls.Add(cmbSearchGenre);
            searchTabPage.Controls.Add(btnSearch);
            searchTabPage.Location = new Point(4, 24);
            searchTabPage.Margin = new Padding(3, 2, 3, 2);
            searchTabPage.Name = "searchTabPage";
            searchTabPage.Padding = new Padding(3, 2, 3, 2);
            searchTabPage.Size = new Size(1099, 519);
            searchTabPage.TabIndex = 3;
            searchTabPage.Text = "Vyhledat film";
            searchTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(19, 86);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1080, 425);
            dataGridView1.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(388, 32);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 8;
            label3.Text = "Žánr:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(198, 32);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 7;
            label2.Text = "Režisér(ka):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 32);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 6;
            label1.Text = "Název:";
            label1.Click += label1_Click;
            // 
            // txtSearchTitle
            // 
            txtSearchTitle.Location = new Point(18, 58);
            txtSearchTitle.Margin = new Padding(3, 2, 3, 2);
            txtSearchTitle.Name = "txtSearchTitle";
            txtSearchTitle.Size = new Size(176, 23);
            txtSearchTitle.TabIndex = 0;
            // 
            // cmbSearchDirector
            // 
            cmbSearchDirector.Location = new Point(198, 57);
            cmbSearchDirector.Margin = new Padding(3, 2, 3, 2);
            cmbSearchDirector.Name = "cmbSearchDirector";
            cmbSearchDirector.Size = new Size(176, 23);
            cmbSearchDirector.TabIndex = 1;
            // 
            // cmbSearchGenre
            // 
            cmbSearchGenre.Location = new Point(380, 58);
            cmbSearchGenre.Margin = new Padding(3, 2, 3, 2);
            cmbSearchGenre.Name = "cmbSearchGenre";
            cmbSearchGenre.Size = new Size(176, 23);
            cmbSearchGenre.TabIndex = 2;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(562, 57);
            btnSearch.Margin = new Padding(3, 2, 3, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(194, 24);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Zahájit hledání";
            btnSearch.Click += btnSearch_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1107, 571);
            Controls.Add(mainTabControl);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "Databáze hodnocení filmů";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)directorsDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)genresDataGridView).EndInit();
            moviesTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)moviesDataGridView).EndInit();
            reviewsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownRating).EndInit();
            mainTabControl.ResumeLayout(false);
            searchTabPage.ResumeLayout(false);
            searchTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void MoviesMenuItem_Click(object sender, EventArgs e)
        {
            mainTabControl.SelectedTab = moviesTabPage;
        }

        private void ReviwesMenuItem_Click(object sender, EventArgs e)
        {
            mainTabControl.SelectedTab = reviewsTabPage;
            comboBoxMovies.Items.Clear();
            foreach (var movie in MovieList)
            {
                comboBoxMovies.Items.Add(movie.Title);
            }
        }

        private ListView listView1;
        private RichTextBox textReview;
        private TabPage searchTabPage;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView dataGridView1;
        private Button btnDeleteAllMovies;
        private Button btnDeleteAllReviews;
    }
}