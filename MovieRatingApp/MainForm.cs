using System;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using MovieRatingApp.Models;
using System.Collections.Generic;
using System.Linq;
using MovieRatingApp.Data;
using MovieRatingApp.Utilities;
using Microsoft.Data.Sqlite;
using System.ComponentModel;

namespace MovieRatingApp
{
    public partial class MainForm : Form
    {
        private List<Movie> MovieList = new List<Movie>();
        private DatabaseManager databaseManager;

        public MainForm()
        {
            InitializeComponent();
            SetupMovieDataGridViewColumns();
            InitializeListView();
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MovieDatabase.db");
            databaseManager = new DatabaseManager(dbPath);
            LoadMoviesData();
            LoadGenresAndDirectors();
            PopulateMovieComboBox();
            LoadAndDisplayAllReviews();
            this.Load += MainForm_Load;
            this.btnSearch.Click += btnSearch_Click;
        }

        private void InitializeSearchTabPageDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;

            if (dataGridView1.Columns["Title"] == null)
            {
                dataGridView1.Columns.Add("Title", "Název");
                dataGridView1.Columns["Title"].DataPropertyName = "Title";
                dataGridView1.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dataGridView1.Columns["ReleaseYear"] == null)
            {
                dataGridView1.Columns.Add("ReleaseYear", "Rok vydání");
                dataGridView1.Columns["ReleaseYear"].DataPropertyName = "ReleaseYear";
                dataGridView1.Columns["ReleaseYear"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dataGridView1.Columns["Director"] == null)
            {
                dataGridView1.Columns.Add("Director", "Režisér(ka)");
                dataGridView1.Columns["Director"].DataPropertyName = "Director";
                dataGridView1.Columns["Director"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dataGridView1.Columns["Genre"] == null)
            {
                dataGridView1.Columns.Add("Genre", "Žánr(y)");
                dataGridView1.Columns["Genre"].DataPropertyName = "Genre";
                dataGridView1.Columns["Genre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (var column in dataGridView1.Columns.Cast<DataGridViewColumn>().ToList())
            {
                if (column.Name != "Title" && column.Name != "ReleaseYear" && column.Name != "Director" && column.Name != "Genre")
                {
                    dataGridView1.Columns.Remove(column);
                }
            }
        }

        private void LoadReviewsForSelectedMovie(int selectedMovieId)
        {
            var reviews = databaseManager.GetReviewsForMovie(selectedMovieId);
            var selectedMovie = MovieList.FirstOrDefault(movie => movie.Id == selectedMovieId);
            listView1.Items.Clear();

            if (selectedMovie != null)
            {
                foreach (var review in reviews)
                {
                    var listViewItem = new ListViewItem(selectedMovie.Title);
                    listViewItem.SubItems.Add(review.Rating.ToString());
                    listViewItem.SubItems.Add(review.ReviewText);
                    listView1.Items.Add(listViewItem);
                }
            }
        }

        private void SetupDataGridView(DataGridView dataGridView, string name)
        {
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new System.Drawing.Point(3, 3);
            dataGridView.Name = name;
            dataGridView.Size = new System.Drawing.Size(786, 392);
            dataGridView.TabIndex = 0;

            if (dataGridView.Name == "moviesDataGridView")
            {
                dataGridView.Columns.Add("Title", "Název");
                dataGridView.Columns.Add("ReleaseYear", "Rok vydání");
                dataGridView.Columns.Add("Director", "Režisér(ka)");
                dataGridView.Columns.Add("Genre", "Žánr(y)");
            }
        }

        private void LoadAndDisplayAllReviews()
        {
            var allMovies = databaseManager.GetMovies();
            var allReviews = databaseManager.GetAllUserReviews();

            listView1.Items.Clear();
            foreach (var movie in allMovies)
            {
                var reviews = databaseManager.GetReviewsForMovie(movie.Id);
                foreach (var review in reviews)
                {
                    var item = new ListViewItem(new[] { movie.Title, review.Rating.ToString(), review.ReviewText });
                    listView1.Items.Add(item);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string title = txtSearchTitle.Text.Trim();
            Director selectedDirector = cmbSearchDirector.SelectedItem as Director;
            string director = selectedDirector?.Name;
            Genre selectedGenre = cmbSearchGenre.SelectedItem as Genre;
            string genre = selectedGenre?.Name;

            var searchResults = databaseManager.SearchMovies(title, director, genre);
            dataGridView1.DataSource = searchResults;

            if (!searchResults.Any())
            {
                MessageBox.Show("No results found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadGenres()
        {
            var genresList = FileDataManager.LoadData<Genre>("genres");
            genresList.Insert(0, new Genre { Id = 0, Name = "Select Genre" });
            cmbSearchGenre.DataSource = genresList;
            cmbSearchGenre.DisplayMember = "Name";
            cmbSearchGenre.ValueMember = "Id";
        }

        private void LoadDirectors()
        {
            var directorsList = FileDataManager.LoadData<Director>("directors");
            directorsList.Insert(0, new Director { Id = 0, Name = "Select Director" });
            cmbSearchDirector.DataSource = directorsList;
            cmbSearchDirector.DisplayMember = "Name";
            cmbSearchDirector.ValueMember = "Id";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadGenresAndDirectors();
            LoadMoviesData();
            InitializeSearchTabPageDataGridView();
            LoadAndDisplayAllReviews();
        }

        private void LoadGenresAndDirectors()
        {
            LoadGenres();
            LoadDirectors();
        }


        private void BtnAllReviews_Click(object sender, EventArgs e)
        {
            LoadAndDisplayAllReviews();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON Files (*.json)|*.json";
                sfd.DefaultExt = "json";
                sfd.AddExtension = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string movieFilePath = sfd.FileName;
                    var moviesData = JsonSerializer.Serialize(MovieList, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(movieFilePath, moviesData);
                    MessageBox.Show("Data úspěšně uložena do " + movieFilePath);
                }
            }
        }

        private void SetupMovieDataGridViewColumns()
        {
            moviesDataGridView.Columns.Clear();
            moviesDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TitleColumn",
                HeaderText = "Název",
                DataPropertyName = "Title"
            });

            moviesDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ReleaseYearColumn",
                HeaderText = "Rok vydání",
                DataPropertyName = "ReleaseYear"
            });

            moviesDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DirectorsColumn",
                HeaderText = "Režisér(ka)",
                DataPropertyName = "Directors"
            });

            moviesDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "GenresColumn",
                HeaderText = "Žánr(y)",
                DataPropertyName = "Genres"
            });

            moviesDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "AverageRatingColumn",
                HeaderText = "Průměrné hodnocení",
                DataPropertyName = "AverageRating"
            });

            foreach (DataGridViewColumn column in moviesDataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void LoadMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON Files (*.json)|*.json";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string movieFilePath = ofd.FileName;
                    try
                    {
                        var moviesData = File.ReadAllText(movieFilePath);
                        var tempMovieList = JsonSerializer.Deserialize<List<Movie>>(moviesData);
                        if (tempMovieList == null)
                        {
                            MessageBox.Show("Nepodařilo se analyzovat JSON data.", "Chyba při načítání", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        databaseManager.DeleteAllMovies();

                        foreach (var movie in tempMovieList)
                        {
                            if (!databaseManager.DirectorExists(movie.Director))
                            {
                                databaseManager.AddDirector(new Director { Name = movie.Director });
                            }

                            if (!databaseManager.GenreExists(movie.Genre))
                            {
                                databaseManager.AddGenre(new Genre { Name = movie.Genre });
                            }

                            databaseManager.AddMovie(movie);
                        }

                        LoadMoviesData();
                        MessageBox.Show("Data úspěšně načtena z " + movieFilePath);
                    }
                    catch (JsonException jsonEx)
                    {
                        MessageBox.Show($"Nepodařilo se analyzovat JSON data. Chyba: {jsonEx.Message}", "Chyba při načítání", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Při načítání dat došlo k chybě. Chyba: {ex.Message}", "Chyba načítání", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadMoviesData()
        {
            MovieList = databaseManager.GetMovies();
            moviesDataGridView.Rows.Clear();
            comboBoxMovies.Items.Clear();
            foreach (Movie movie in MovieList)
            {
                movie.Ratings = databaseManager.GetRatingsForMovie(movie.Id);
                moviesDataGridView.Rows.Add(movie.Title, movie.ReleaseYear, movie.Director, movie.Genre, movie.AverageRating.ToString("0.##"));
                comboBoxMovies.Items.Add(movie.Title);
            }
        }

        private void AddMovie(object sender, EventArgs e)
        {
            var movieForm = new MovieForm();
            if (movieForm.ShowDialog() == DialogResult.OK)
            {
                Movie newMovie = movieForm.CurrentMovie;
                databaseManager.AddMovie(newMovie);
                LoadMoviesData();
            }
        }

        private void EditMovie(object sender, EventArgs e)
        {
            if (moviesDataGridView.SelectedRows.Count > 0)
            {
                int rowIndex = moviesDataGridView.CurrentCell.RowIndex;
                Movie selectedMovie = MovieList[rowIndex];

                var movieForm = new MovieForm(selectedMovie);
                if (movieForm.ShowDialog() == DialogResult.OK)
                {
                    bool success = databaseManager.UpdateMovie(movieForm.CurrentMovie);
                    if (success)
                    {
                        LoadMoviesData();
                    }
                    else
                    {
                        MessageBox.Show("Aktualizace filmu se nezdařila.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Prosím, vyberte film k úpravě.");
            }
        }

        private void PopulateListView()
        {
            listView1.Items.Clear();

            foreach (var movie in MovieList)
            {
                var item = new ListViewItem(movie.Title);
                listView1.Items.Add(item);
            }
        }

        private void DeleteMovie(object sender, EventArgs e)
        {
            if (moviesDataGridView.SelectedRows.Count > 0)
            {
                int rowIndex = moviesDataGridView.CurrentCell.RowIndex;
                Movie movieToDelete = MovieList[rowIndex];

                bool success = databaseManager.DeleteMovie(movieToDelete.Id);
                if (success)
                {
                    LoadMoviesData();
                    comboBoxMovies.Items.Remove(movieToDelete.Title);
                }
                else
                {
                    MessageBox.Show("Nepodařilo se smazat film.");
                }
            }
        }

        private void PopulateMovieComboBox()
        {
            foreach (var movie in MovieList)
            {
                if (!comboBoxMovies.Items.Contains(movie.Title))
                    comboBoxMovies.Items.Add(movie.Title);
            }
        }

        private void AddReview(object sender, EventArgs e)
        {
            if (comboBoxMovies.SelectedIndex == -1)
            {
                MessageBox.Show("Prosím, vyberte film.");
                return;
            }

            var selectedMovieTitle = comboBoxMovies.SelectedItem.ToString();
            var selectedMovie = MovieList.FirstOrDefault(movie => movie.Title == selectedMovieTitle);

            if (selectedMovie != null)
            {
                var review = new UserReview
                {
                    Rating = (int)numericUpDownRating.Value,
                    ReviewText = textReview.Text
                };

                databaseManager.AddReview(review, selectedMovie.Id);
                selectedMovie.Ratings.Add(review.Rating);
                UpdateAverageRating(selectedMovie);

                PopulateListViewWithReviews(selectedMovie.Title, review.Rating, review.ReviewText);

                textReview.Clear();
                numericUpDownRating.Value = numericUpDownRating.Minimum;
            }
        }

        private void UpdateAverageRating(Movie movie)
        {
            List<int> ratings = new List<int> { 5, 4, 3, 5 };

            double averageRating = ratings.Any() ? ratings.Average() : 0;

            movie.ReviewCount = (int)Math.Round(averageRating);
        }

        private void InitializeListView()
        {
            listView1.View = View.Details;

            listView1.Columns.Add("Název filmu", 150);
            listView1.Columns.Add("Hodnocení", 250);
            listView1.Columns.Add("Recenze", 300);

        }

        private void btnAddReview_Click(object sender, EventArgs e)
        {
            AddReviewToMovie();
        }

        private void AddReviewToMovie()
        {
            if (comboBoxMovies.SelectedIndex != -1 && numericUpDownRating.Value > 0)
            {
                var selectedMovieTitle = comboBoxMovies.SelectedItem.ToString();
                var selectedMovie = MovieList.FirstOrDefault(movie => movie.Title == selectedMovieTitle);
                if (selectedMovie != null)
                {
                    var review = new UserReview
                    {
                        Rating = (int)numericUpDownRating.Value,
                        ReviewText = textReview.Text
                    };

                    databaseManager.AddReview(review, selectedMovie.Id);

                    PopulateListViewWithReviews(selectedMovie.Title, review.Rating, review.ReviewText);

                    textReview.Clear();
                    numericUpDownRating.Value = numericUpDownRating.Minimum;
                    comboBoxMovies.SelectedIndex = -1;
                    UpdateAverageRating(selectedMovie, review.Rating);
                }
            }
            else
            {
                MessageBox.Show("Prosím, vyberte film a zadejte platné hodnocení.");
            }
        }

        private void PopulateListViewWithReviews(string movieTitle, int rating, string reviewText)
        {
            var item = new ListViewItem(new[] { movieTitle, rating.ToString(), reviewText });
            listView1.Items.Add(item);

            if (listView1.Columns.Count < 3)
            {
                listView1.Columns.Clear();
                listView1.Columns.Add("Název filmu", -2, HorizontalAlignment.Left);
                listView1.Columns.Add("Hodnocení", -2, HorizontalAlignment.Left);
                listView1.Columns.Add("Recenze", -2, HorizontalAlignment.Left);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void UpdateAverageRating(Movie movie, int newRating)
        {
            movie.Ratings.Add(newRating);

            foreach (DataGridViewRow row in moviesDataGridView.Rows)
            {
                if (row.Cells["TitleColumn"].Value.ToString() == movie.Title)
                {
                    row.Cells["AverageRatingColumn"].Value = movie.AverageRating.ToString("0.##");
                    break;
                }
            }
        }

        private void LoadReviewsForMovie(int selectedMovieId)
        {
            var reviews = databaseManager.GetReviewsForMovie(selectedMovieId);
            var selectedMovie = MovieList.FirstOrDefault(movie => movie.Id == selectedMovieId);
            listView1.Items.Clear();

            if (selectedMovie != null)
            {
                foreach (var review in reviews)
                {
                    var listViewItem = new ListViewItem(selectedMovie.Title);
                    listViewItem.SubItems.Add(review.Rating.ToString());
                    listViewItem.SubItems.Add(review.ReviewText);
                    listView1.Items.Add(listViewItem);
                }
            }
        }

        private void LoadReviewsForMovie()
        {
            var reviews = databaseManager.GetAllUserReviews();
            listView1.Items.Clear();
            foreach (var review in reviews)
            {
                var item = new ListViewItem(new[] { review.Rating.ToString(), review.ReviewText });
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            databaseManager.DeleteAllMovies();
            moviesDataGridView.Rows.Clear();
        }

        private void btnDeleteAllReviews_Click(object sender, EventArgs e)
        {
            databaseManager.DeleteAllReviews();
            listView1.Items.Clear();

            foreach (var movie in MovieList)
            {
                movie.Ratings.Clear();
                foreach (DataGridViewRow row in moviesDataGridView.Rows)
                {
                    if (row.Cells["TitleColumn"].Value.ToString() == movie.Title)
                    {
                        row.Cells["AverageRatingColumn"].Value = "0";
                        break;
                    }
                }
            }

            MessageBox.Show("Všechny recenze byly úspěšně smazány.", "Operace dokončena",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textReview_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
