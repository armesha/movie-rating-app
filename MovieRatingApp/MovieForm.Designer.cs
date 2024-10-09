using System.Windows.Forms;

namespace MovieRatingApp
{
    partial class MovieForm
    {
        private Label lblTitle;
        private TextBox txtTitle;
        private Label lblReleaseYear;
        private TextBox txtReleaseYear;
        private Label lblDirectors;
        private ComboBox cmbDirectors;
        private Label lblGenres;
        private ComboBox cmbGenres;
        private Button btnSave;
        private Button btnCancel;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.txtTitle = new TextBox();
            this.lblReleaseYear = new Label();
            this.txtReleaseYear = new TextBox();
            this.lblDirectors = new Label();
            this.cmbDirectors = new ComboBox();
            this.lblGenres = new Label();
            this.cmbGenres = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // Title Label
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 15);
            this.lblTitle.Text = "Název:";

            // Title TextBox
            this.txtTitle.Location = new System.Drawing.Point(12, 27);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(490, 23);

            // Release Year Label
            this.lblReleaseYear.AutoSize = true;
            this.lblReleaseYear.Location = new System.Drawing.Point(12, 53);
            this.lblReleaseYear.Name = "lblReleaseYear";
            this.lblReleaseYear.Size = new System.Drawing.Size(94, 15);
            this.lblReleaseYear.Text = "Rok vydání:";

            // Release Year TextBox
            this.txtReleaseYear.Location = new System.Drawing.Point(12, 71);
            this.txtReleaseYear.Name = "txtReleaseYear";
            this.txtReleaseYear.Size = new System.Drawing.Size(100, 23);

            // Directors Label
            this.lblDirectors.AutoSize = true;
            this.lblDirectors.Location = new System.Drawing.Point(12, 97);
            this.lblDirectors.Name = "lblDirectors";
            this.lblDirectors.Size = new System.Drawing.Size(73, 15);
            this.lblDirectors.Text = "Režisérovi:";

            // Directors ComboBox
            this.cmbDirectors.Location = new System.Drawing.Point(12, 115);
            this.cmbDirectors.Name = "cmbDirectors";
            this.cmbDirectors.Size = new System.Drawing.Size(490, 23);

            // Genres Label
            this.lblGenres.AutoSize = true;
            this.lblGenres.Location = new System.Drawing.Point(12, 141);
            this.lblGenres.Name = "lblGenres";
            this.lblGenres.Size = new System.Drawing.Size(48, 15);
            this.lblGenres.Text = "Žánry:";

            // Genres ComboBox
            this.cmbGenres.Location = new System.Drawing.Point(12, 159);
            this.cmbGenres.Name = "cmbGenres";
            this.cmbGenres.Size = new System.Drawing.Size(490, 23);

            // Save Button
            this.btnSave.Location = new System.Drawing.Point(346, 242);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Text = "Uložit";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Cancel Button
            this.btnCancel.Location = new System.Drawing.Point(427, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Storno";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // MovieForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(514, 277);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblReleaseYear);
            this.Controls.Add(this.txtReleaseYear);
            this.Controls.Add(this.lblDirectors);
            this.Controls.Add(this.cmbDirectors);
            this.Controls.Add(this.lblGenres);
            this.Controls.Add(this.cmbGenres);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "MovieForm";
            this.Text = "Detaily filmu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}