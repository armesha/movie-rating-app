using System;
using Microsoft.Data.Sqlite;
using MovieRatingApp.Models;
using System.Collections.Generic;
using System.Text;

namespace MovieRatingApp.Data
{
    public class DatabaseManager
    {
        private SqliteConnection dbConnection;

        public DatabaseManager(string dbPath)
        {
            dbConnection = new SqliteConnection($"Data Source={dbPath};");
            InitializeDatabase();
        }

        public List<Movie> SearchMovies(string title = "", string director = "", string genre = "")
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT Id, Title, ReleaseYear, Director, Genre FROM Movies WHERE 1=1");

            if (!string.IsNullOrEmpty(title))
                queryBuilder.Append(" AND Title LIKE @Title");
            if (!string.IsNullOrEmpty(director) && director != "Select Director")
                queryBuilder.Append(" AND Director = @Director");
            if (!string.IsNullOrEmpty(genre) && genre != "Select Genre")
                queryBuilder.Append(" AND Genre = @Genre");

            string commandText = queryBuilder.ToString();
            List<Movie> filteredMovies = new List<Movie>();

            dbConnection.Open();
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                if (!string.IsNullOrEmpty(title))
                    cmd.Parameters.AddWithValue("@Title", "%" + title + "%");
                if (!string.IsNullOrEmpty(director) && director != "Select Director")
                    cmd.Parameters.AddWithValue("@Director", director);
                if (!string.IsNullOrEmpty(genre) && genre != "Select Genre")
                    cmd.Parameters.AddWithValue("@Genre", genre);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        filteredMovies.Add(new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            ReleaseYear = Convert.ToInt32(reader["ReleaseYear"]),
                            Director = reader["Director"].ToString(),
                            Genre = reader["Genre"].ToString()
                        });
                    }
                }
            }
            dbConnection.Close();
            return filteredMovies;
        }

        public List<Director> GetDirectors()
        {
            var directors = new List<Director>();
            var commandText = "SELECT * FROM Directors";
            OpenConnection();
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        directors.Add(new Director
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }
            CloseConnection();
            return directors;
        }

        public List<Genre> GetGenres()
        {
            var genres = new List<Genre>();
            var commandText = "SELECT * FROM Genres";
            OpenConnection();
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genres.Add(new Genre
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }
            CloseConnection();
            return genres;
        }

        public List<int> GetRatingsForMovie(int movieId)
        {
            List<int> ratings = new List<int>();
            var commandText = "SELECT Rating FROM Reviews WHERE MovieId = @MovieId";
            OpenConnection();
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                cmd.Parameters.AddWithValue("@MovieId", movieId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ratings.Add(Convert.ToInt32(reader["Rating"]));
                    }
                }
            }
            CloseConnection();
            return ratings;
        }


        public void AddDirector(Director director)
        {
            string commandText = "INSERT INTO Directors (Name) VALUES (@Name)";
            OpenConnection();
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                cmd.Parameters.AddWithValue("@Name", director.Name);
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }

        public void AddGenre(Genre genre)
        {
            string commandText = "INSERT INTO Genres (Name) VALUES (@Name)";
            OpenConnection();
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                cmd.Parameters.AddWithValue("@Name", genre.Name);
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }

        public bool DirectorExists(string directorName)
        {
            var commandText = "SELECT COUNT(1) FROM Directors WHERE Name = @Name";
            OpenConnection();
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                cmd.Parameters.AddWithValue("@Name", directorName);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                CloseConnection();
                return count > 0;
            }
        }

        public bool GenreExists(string genreName)
        {
            var commandText = "SELECT COUNT(1) FROM Genres WHERE Name = @Name";
            OpenConnection();
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                cmd.Parameters.AddWithValue("@Name", genreName);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                CloseConnection();
                return count > 0;
            }
        }
        public bool UpdateMovie(Movie movie)
        {
            var commandText = "UPDATE Movies SET Title = @Title, ReleaseYear = @ReleaseYear, Director = @Director, Genre = @Genre WHERE Id = @Id";
            try
            {
                OpenConnection();
                using (var cmd = new SqliteCommand(commandText, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@Id", movie.Id);
                    cmd.Parameters.AddWithValue("@Title", movie.Title);
                    cmd.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
                    cmd.Parameters.AddWithValue("@Director", movie.Director);
                    cmd.Parameters.AddWithValue("@Genre", movie.Genre);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool DeleteMovie(int movieId)
        {
            try
            {
                OpenConnection();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    var deleteReviewsCommandText = "DELETE FROM Reviews WHERE MovieId = @MovieId";
                    using (var cmd = new SqliteCommand(deleteReviewsCommandText, dbConnection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MovieId", movieId);
                        cmd.ExecuteNonQuery();
                    }

                    var deleteMovieCommandText = "DELETE FROM Movies WHERE Id = @Id";
                    using (var cmd = new SqliteCommand(deleteMovieCommandText, dbConnection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", movieId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        transaction.Commit();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    using (var transaction = dbConnection.BeginTransaction())
                    {
                        transaction.Rollback();
                    }
                }
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        private void InitializeDatabase()
        {
            OpenConnection();
            var commands = new[]
            {
        @"CREATE TABLE IF NOT EXISTS Movies (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT NOT NULL,
            ReleaseYear INTEGER NOT NULL,
            Director TEXT,
            Genre TEXT
        );",
        @"CREATE TABLE IF NOT EXISTS Reviews (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            MovieId INTEGER NOT NULL,
            Rating INTEGER NOT NULL,
            ReviewText TEXT,
            FOREIGN KEY(MovieId) REFERENCES Movies(Id)
        );" };

            foreach (var commandText in commands)
            {
                using (var command = new SqliteCommand(commandText, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }

            CloseConnection();
        }


        public void OpenConnection()
        {
            if (dbConnection.State != System.Data.ConnectionState.Open)
                dbConnection.Open();
        }

        public void CloseConnection()
        {
            if (dbConnection.State != System.Data.ConnectionState.Closed)
                dbConnection.Close();
        }

        public void AddMovie(Movie movie, bool setId = true)
        {
            var commandText = "INSERT INTO Movies (Title, ReleaseYear, Director, Genre) VALUES (@Title, @ReleaseYear, @Director, @Genre)";
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                cmd.Parameters.AddWithValue("@Title", movie.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
                cmd.Parameters.AddWithValue("@Director", movie.Director);
                cmd.Parameters.AddWithValue("@Genre", movie.Genre);

                OpenConnection();
                cmd.ExecuteNonQuery();

                if (setId)
                {
                    cmd.CommandText = "SELECT last_insert_rowid()";
                    long lastId = (long)cmd.ExecuteScalar();
                    movie.Id = (int)lastId;
                }

                CloseConnection();
            }
        }

        public List<Movie> GetMovies()
        {
            var movies = new List<Movie>();
            var commandText = "SELECT * FROM Movies";
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                OpenConnection();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            ReleaseYear = Convert.ToInt32(reader["ReleaseYear"]),
                            Director = reader["Director"].ToString(),
                            Genre = reader["Genre"].ToString()
                        });
                    }
                }
                CloseConnection();
            }
            return movies;
        }

        public void DeleteAllMovies()
        {
            try
            {
                OpenConnection();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    var deleteReviewsCommandText = "DELETE FROM Reviews";
                    using (var cmd = new SqliteCommand(deleteReviewsCommandText, dbConnection, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    var deleteMoviesCommandText = "DELETE FROM Movies";
                    using (var cmd = new SqliteCommand(deleteMoviesCommandText, dbConnection, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    using (var transaction = dbConnection.BeginTransaction())
                    {
                        transaction.Rollback();
                    }
                }
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeleteAllReviews()
        {
            var commandText = "DELETE FROM Reviews";
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                OpenConnection();
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public void AddReview(UserReview review, int movieId)
        {
            var commandText = "INSERT INTO Reviews (MovieId, Rating, ReviewText) VALUES (@MovieId, @Rating, @ReviewText)";
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                cmd.Parameters.AddWithValue("@MovieId", movieId);
                cmd.Parameters.AddWithValue("@Rating", review.Rating);
                cmd.Parameters.AddWithValue("@ReviewText", review.ReviewText ?? string.Empty); 
                OpenConnection();
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }


        public List<UserReview> GetAllUserReviews()
        {
            var reviews = new List<UserReview>();
            var commandText = "SELECT * FROM Reviews";
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                OpenConnection();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new UserReview
                        {
                            Rating = Convert.ToInt32(reader["Rating"]),
                            ReviewText = reader["ReviewText"].ToString()
                        });
                    }
                }
                CloseConnection();
            }
            return reviews;
        }

        public List<UserReview> GetReviewsForMovie(int movieId)
        {
            var reviews = new List<UserReview>();
            var commandText = "SELECT Rating, ReviewText FROM Reviews WHERE MovieId = @MovieId";
            using (var cmd = new SqliteCommand(commandText, dbConnection))
            {
                cmd.Parameters.AddWithValue("@MovieId", movieId);
                OpenConnection();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new UserReview
                        {
                            Rating = Convert.ToInt32(reader["Rating"]),
                            ReviewText = reader["ReviewText"].ToString()
                        });
                    }
                }
                CloseConnection();
            }
            return reviews;
        }
    }
}
