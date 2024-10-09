using MovieRatingApp.Models;
using MovieRatingApp.Utilities;
using System;
using System.Windows.Forms;

namespace MovieRatingApp
{
    public partial class MovieForm : Form
    {
        public Movie CurrentMovie { get; set; }

        public MovieForm(Movie movie = null)
        {
            InitializeComponent();

            LoadDirectors();
            LoadGenres();

            if (movie != null)
            {
                CurrentMovie = movie;
                txtTitle.Text = CurrentMovie.Title;
                txtReleaseYear.Text = CurrentMovie.ReleaseYear.ToString();
                cmbDirectors.Text = CurrentMovie.Director;
                cmbGenres.Text = CurrentMovie.Genre;
            }
            else
            {
                CurrentMovie = new Movie();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                MessageBox.Show("Please correct the inputs.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CurrentMovie.Title = txtTitle.Text;
            CurrentMovie.ReleaseYear = int.Parse(txtReleaseYear.Text);
            CurrentMovie.Director = cmbDirectors.Text;
            CurrentMovie.Genre = cmbGenres.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool ValidateInputs()
        {
            return true;
        }

        private void LoadDirectors()
        {
            var directorsList = FileDataManager.LoadData<Director>("directors");
            directorsList.Insert(0, new Director { Id = 0, Name = "Select Director" });
            cmbDirectors.DataSource = directorsList;
            cmbDirectors.DisplayMember = "Name";
            cmbDirectors.ValueMember = "Id";
        }

        private void LoadGenres()
        {
            var genresList = FileDataManager.LoadData<Genre>("genres");
            genresList.Insert(0, new Genre { Id = 0, Name = "Select Genre" });
            cmbGenres.DataSource = genresList;
            cmbGenres.DisplayMember = "Name";
            cmbGenres.ValueMember = "Id";
        }
    }
}
