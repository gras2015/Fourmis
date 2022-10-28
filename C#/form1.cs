     using AntsWinForm.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntsWinForm
{
    public partial class Form1 : Form
    {
        public static Case[,] Plateau;
        public static Random Rnd = new Random();
        private int ligneDepartNid = 10;
        private int colonneDepartNid = 9;
        public int ToursMax = 300;
        public int TourActuel = 0;
        public int TourActuelManuel = 0;
        public float PourcentageDiminutionPheromoneSucre = 6;
        public int tailleplateau = 20;

        public List<string[,]> HistoriquePlateau;

        public static string cheminCsv = @"..\..\..\Fourmilliere.csv";

        public Form1()
        {
            InitializeComponent();

            Plateau = new Case[tailleplateau, tailleplateau];
            HistoriquePlateau = new List<string[,]>();
            List<Fourmi> listFourmis = new List<Fourmi>();

            InitPlateau();

            InitFichierCSV();

            while (TourActuel < ToursMax)
            {
                foreach (Case c in Plateau)
                {
                    if (c.Contenu.ToString() == "F" && !c.Contenu.Deplace)
                    {
                        Fourmi f = c.Contenu;

                        if (TourActuel == 0)
                        {
                            listFourmis.Add(f);
                        }

                        if (f.ContientSucre)
                        {
                            f.RentrerNid();
                        }
                        else
                        {
                            f.ChercheSucre();
                        }
                    }
                }
                foreach (Fourmi f in listFourmis)
                {
                    f.Deplace = false;
                }
                EcrireDansCSV();
                TourActuel++;
                EvaporationPheromoneSucre();

                AjouterHistorique();
            }

            Affichage();
        }

        /// <summary>
        /// Methode qui permet de creer le tableau de Case en 2D et qui place les différents items
        /// </summary>
        private void InitPlateau()
        {
            for (int i = 0; i < Plateau.GetLength(0); i++)
            {
                for (int j = 0; j < Plateau.GetLength(1); j++)
                {
                    Plateau[i, j] = new Case();
                }
            }
            PlaceNid();
            PlaceFourmi();
            PlaceCailloux();
            PlaceSucre();
        }

        /// <summary>
        /// Méthode qui place le nid
        /// </summary>
        private void PlaceNid()
        {
            Plateau[ligneDepartNid, colonneDepartNid].Contenu = new Nid();
            Plateau[ligneDepartNid - 1, colonneDepartNid].Contenu = new Nid();
            Plateau[ligneDepartNid, colonneDepartNid + 1].Contenu = new Nid();
            Plateau[ligneDepartNid - 1, colonneDepartNid + 1].Contenu = new Nid();

            Plateau[ligneDepartNid, colonneDepartNid].Contenu.PropagationPheromone();
        }

        /// <summary>
        /// Méthode qui place les fourmis
        /// </summary>
        private void PlaceFourmi()
        {
            Plateau[ligneDepartNid - 2, colonneDepartNid].Contenu = new Fourmi("rouge", ligneDepartNid - 2, colonneDepartNid);
            Plateau[ligneDepartNid - 2, colonneDepartNid + 1].Contenu = new Fourmi("bleu", ligneDepartNid - 2, colonneDepartNid + 1);
            Plateau[ligneDepartNid - 2, colonneDepartNid + 2].Contenu = new Fourmi("vert", ligneDepartNid - 2, colonneDepartNid + 2);
            Plateau[ligneDepartNid - 1, colonneDepartNid + 2].Contenu = new Fourmi("jaune", ligneDepartNid - 1, colonneDepartNid + 2);
            Plateau[ligneDepartNid, colonneDepartNid + 2].Contenu = new Fourmi("violet", ligneDepartNid, colonneDepartNid + 2);
        }

        /// <summary>
        /// Méthode qui place les cailloux
        /// </summary>
        private void PlaceCailloux()
        {
            for (int i = 0; i < 10; i++)
            {
                ligneDepartNid = Rnd.Next(0, 20);
                colonneDepartNid = Rnd.Next(0, 20);
                if (Plateau[ligneDepartNid, colonneDepartNid].EstVide() )
                {
                    Plateau[ligneDepartNid, colonneDepartNid].Contenu = "X";
                }
            }
        }

        /// <summary>
        /// Méthode qui place les sucres
        /// </summary>
        private void PlaceSucre()
        {
            for (int i = 0; i < 10; i++)
            {
                ligneDepartNid = Rnd.Next(0, 20);
                colonneDepartNid = Rnd.Next(0, 20);
                if (Plateau[ligneDepartNid, colonneDepartNid].EstVide())
                {
                    Plateau[ligneDepartNid, colonneDepartNid].Contenu = new Sucre();
                }
            }
        }

        /// <summary>
        /// Méthode d'initialisation du CSV, la premiere ligne
        /// </summary>
        private void InitFichierCSV()
        {
            if (File.Exists(cheminCsv))
            {
                File.Delete(cheminCsv);
            }
            StreamWriter sw = new StreamWriter(cheminCsv, true);
            sw.WriteLine(tailleplateau + " " + tailleplateau + " " + ToursMax);
            sw.Close();
        }
        
        /// <summary>
        /// Méthode qui affiche le Plateau dans le datagridview
        /// </summary>
        private void Affichage()
        {            
            labelNbTour.Text = "Tour numero " + TourActuelManuel;
            RefreshGrille();
        }

        /// <summary>
        /// Methode qui met a jour le label du tour actuel
        /// </summary>
        private void RefreshLabelTour()
        {
            labelNbTour.Text = "Tour numero " + TourActuelManuel;
        }

        /// <summary>
        /// Méthode qui met a jour la textbox
        /// </summary>
        private void RefreshTextBox()
        {
            TextBoxTourActuelManuel.Text = "";
        }

        /// <summary>
        /// Méthode qui permet d'ecrire dans le fichier CSV
        /// </summary>
        private void EcrireDansCSV()
        {
            StreamWriter sw = new StreamWriter(cheminCsv, true);
            for (int i = 0; i < this.tailleplateau; i++)
            {
                for (int j = 0; j < this.tailleplateau; j++)
                {
                    int stack = 0;
                    if (Plateau[i, j].ToString() == "S")
                    {
                        stack = Plateau[i, j].Contenu.Stack;
                    }
                    string carac = Plateau[i, j].Contenu.ToString();
                    if (Plateau[i, j].Contenu.ToString() == " ")
                    {
                        carac = "F";
                    }
                    if (Plateau[i, j].Contenu.ToString() == "F")
                    {
                        if (Plateau[i, j].Contenu.ContientSucre)
                        {
                            carac = "A";
                        }
                        else carac = "a";
                    }

                    sw.Write(carac + " " + stack + " " + Plateau[i, j].PheromoneNid + " " + Plateau[i, j].PheromoneSucre.ToString().Replace(",",".") + "\n");
                }
            }
            
            sw.Close();
        }

        /// <summary>
        /// Méthode qui gère l'evaporation des phéromones de sucre chaque fin de tour
        /// </summary>
        private void EvaporationPheromoneSucre()
        {
            for (int i = 0; i < tailleplateau; i++)
            {
                for (int j = 0; j < tailleplateau; j++)
                {
                    if (Plateau[i, j].PheromoneSucre > 0)
                    {
                        Plateau[i, j].PheromoneSucre = Plateau[i, j].PheromoneSucre - 10 * PourcentageDiminutionPheromoneSucre / 100;
                        if (Plateau[i, j].PheromoneSucre <= 0)
                        {
                            Plateau[i, j].PheromoneSucre = 0;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Méthode qui met a jour le datagridview
        /// </summary>
        private void RefreshGrille()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.ColumnCount = this.tailleplateau;
            for (int i = 0; i < this.tailleplateau; i++)
            {
                string[] row = new string[this.tailleplateau];
                for (int j = 0; j < this.tailleplateau; j++)
                {
                    row[j] = HistoriquePlateau[TourActuelManuel][i, j];
                }
                this.dataGridView1.Rows.Add(row);
            }
        }

        /// <summary>
        /// Méthode qui permet d'ajouter chaque itération de tour dans une list qui sert d'historique
        /// </summary>
        private void AjouterHistorique()
        {
            string[,] tab = new string[this.tailleplateau, this.tailleplateau];
            for (int i = 0; i < this.tailleplateau; i++)
            {
                for (int j = 0; j < this.tailleplateau; j++)
                {
                    tab[i,j] = Plateau[i, j].Contenu.ToString();
                }
            }

            HistoriquePlateau.Add(tab);
        }

        /// <summary>
        /// Méthode de gestion du bouton Précedent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonPrecedent_Click(object sender, EventArgs e)
        {
            if(TourActuelManuel > 0)
            {
                TourActuelManuel--;
                RefreshGrille();
                RefreshLabelTour();
                RefreshTextBox();
            }
        }

        /// <summary>
        /// Méthode de gestion du bouton Suivant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonSuivant_Click(object sender, EventArgs e)
        {
            if (TourActuelManuel < ToursMax-1)
            {
                TourActuelManuel++;
                RefreshGrille();
                RefreshLabelTour();
                RefreshTextBox();
            }
        }

        /// <summary>
        /// Méthode de gestion du bouton reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonReset_Click(object sender, EventArgs e)
        {
            TourActuelManuel = 0;
            RefreshGrille();
            RefreshLabelTour();
            RefreshTextBox();
        }

        /// <summary>
        /// Méthode de gestion de la textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxTourActuelManuel_TextChanged(object sender, EventArgs e)
        {
            int output = 0;
            if( int.TryParse( TextBoxTourActuelManuel.Text, out output) ) {
                if( output >=0 && output < ToursMax )
                {
                    TourActuelManuel = output;
                    RefreshGrille();
                    RefreshLabelTour();
                }
            }
        }
    }
}

