using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BibliotekaHotel;
using GUIProjekt;
namespace GUIProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy PokojWindow.xaml
    /// </summary>
    public partial class PokojWindow : Window
    {
        Pokoj pokoj;
        ListaPokojow lista;
        public PokojWindow()
        {
            InitializeComponent();
        }

        public PokojWindow(Pokoj p, ListaPokojow lp) : this()
        {
            pokoj = p;
            lista = lp;
        }

        private void btn_Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            bool blad = false;
            int result;
            bool czyIstnieje = false;
            foreach (var item in lista.Pokoje)
            {
                if(txt_NrPietra.Text == item.Pietro.ToString() && txt_NrPokoju.Text == item.NumerPokoju.ToString())
                {
                    czyIstnieje = true;
                }
            }
            if (czyIstnieje == true)
            {
                MessageBox.Show("Taki pokój już istnieje", "Błąd", MessageBoxButton.OK);
                DialogResult = false;
            } 
            if (txt_Miejsca.Text != "" && txt_NrPietra.Text != "" && txt_NrPokoju.Text != "")
            {
                if (int.TryParse(txt_Miejsca.Text.ToString(), out result) == true)
                {
                     pokoj.IloscMiejsc = int.Parse(txt_Miejsca.Text);
                }
                else {
                    MessageBox.Show("Podano niepoprawną liczbę miejsc", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }

                if (int.TryParse(txt_NrPietra.Text.ToString(), out result) == true)
                {
                    pokoj.Pietro = int.Parse(txt_NrPietra.Text);
                }
                else {
                    MessageBox.Show("Podano niepoprawny nr piętra", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }

                if (int.TryParse(txt_NrPokoju.Text.ToString(), out result) == true)
                {
                    pokoj.NumerPokoju = int.Parse(txt_NrPokoju.Text);
                }
                else { MessageBox.Show("Podano niepoprawny numer pokoju", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }

                if (cmbox_Standard.Text == "Podstawowy")
                {
                    pokoj.Standard = Standardy.podstawowy;
                }
                else if (cmbox_Standard.Text == "Komfort")
                {
                    pokoj.Standard = Standardy.komfort;
                }
                else pokoj.Standard = Standardy.komfortPlus;
                if (blad == false && czyIstnieje == false)
                {
                    DialogResult = true;
                }

            }
            else DialogResult = false;
        }

        private void btn_Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
