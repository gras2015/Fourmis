
namespace AntsWinForm
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelNbTour = new System.Windows.Forms.Label();
            this.BoutonPrecedent = new System.Windows.Forms.Button();
            this.BoutonSuivant = new System.Windows.Forms.Button();
            this.BoutonReset = new System.Windows.Forms.Button();
            this.TextBoxTourActuelManuel = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(775, 403);
            this.dataGridView1.TabIndex = 0;
            // 
            // labelNbTour
            // 
            this.labelNbTour.AutoSize = true;
            this.labelNbTour.Location = new System.Drawing.Point(10, 419);
            this.labelNbTour.Name = "labelNbTour";
            this.labelNbTour.Size = new System.Drawing.Size(76, 13);
            this.labelNbTour.TabIndex = 1;
            this.labelNbTour.Text = "Tour numero 0";
            // 
            // BoutonPrecedent
            // 
            this.BoutonPrecedent.Location = new System.Drawing.Point(13, 440);
            this.BoutonPrecedent.Name = "BoutonPrecedent";
            this.BoutonPrecedent.Size = new System.Drawing.Size(75, 23);
            this.BoutonPrecedent.TabIndex = 2;
            this.BoutonPrecedent.Text = "Précédent";
            this.BoutonPrecedent.UseVisualStyleBackColor = true;
            this.BoutonPrecedent.Click += new System.EventHandler(this.BoutonPrecedent_Click);
            // 
            // BoutonSuivant
            // 
            this.BoutonSuivant.Location = new System.Drawing.Point(95, 439);
            this.BoutonSuivant.Name = "BoutonSuivant";
            this.BoutonSuivant.Size = new System.Drawing.Size(75, 23);
            this.BoutonSuivant.TabIndex = 3;
            this.BoutonSuivant.Text = "Suivant";
            this.BoutonSuivant.UseVisualStyleBackColor = true;
            this.BoutonSuivant.Click += new System.EventHandler(this.BoutonSuivant_Click);
            // 
            // BoutonReset
            // 
            this.BoutonReset.Location = new System.Drawing.Point(262, 440);
            this.BoutonReset.Name = "BoutonReset";
            this.BoutonReset.Size = new System.Drawing.Size(75, 23);
            this.BoutonReset.TabIndex = 4;
            this.BoutonReset.Text = "Reset";
            this.BoutonReset.UseVisualStyleBackColor = true;
            this.BoutonReset.Click += new System.EventHandler(this.BoutonReset_Click);
            // 
            // TextBoxTourActuelManuel
            // 
            this.TextBoxTourActuelManuel.Location = new System.Drawing.Point(344, 442);
            this.TextBoxTourActuelManuel.Name = "TextBoxTourActuelManuel";
            this.TextBoxTourActuelManuel.Size = new System.Drawing.Size(50, 20);
            this.TextBoxTourActuelManuel.TabIndex = 5;
            this.TextBoxTourActuelManuel.TextChanged += new System.EventHandler(this.TextBoxTourActuelManuel_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 475);
            this.Controls.Add(this.TextBoxTourActuelManuel);
            this.Controls.Add(this.BoutonReset);
            this.Controls.Add(this.BoutonSuivant);
            this.Controls.Add(this.BoutonPrecedent);
            this.Controls.Add(this.labelNbTour);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelNbTour;
        private System.Windows.Forms.Button BoutonPrecedent;
        private System.Windows.Forms.Button BoutonSuivant;
        private System.Windows.Forms.Button BoutonReset;
        private System.Windows.Forms.TextBox TextBoxTourActuelManuel;
    }
}

