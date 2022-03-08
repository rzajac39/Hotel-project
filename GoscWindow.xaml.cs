using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GUIProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy GoscWindow.xaml
    /// </summary>
    public partial class GoscWindow : Window
    {
        Gosc gosc;
        SpisGosci lista;
        public GoscWindow()
        {
            InitializeComponent();
        }
        public GoscWindow(Gosc g, SpisGosci sg) : this()
        {
            gosc = g;
            lista = sg;
        }

        private void btn_Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            bool blad = false;
            bool czyIstnieje = false;
            foreach (var item in lista.ListaGosci)
            {
                if (txt_Pesel.Text == item.Pesel )
                {
                    czyIstnieje = true;
                }
            }

            if (czyIstnieje == true)
            {
                MessageBox.Show("Taki gość już został dodany", "Błąd", MessageBoxButton.OK);
                DialogResult = false;
            }
            
            if (txt_Imie.Text != "" && txt_Nazwisko.Text != "" && txt_Pesel.Text != "" && txt_DataUrodzenia.Text != "" && 
                txt_nrTelefonu.Text != "")
            {
                //Data
                if (DateTime.TryParseExact(txt_DataUrodzenia.Text,
                    new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy" },
                    null, DateTimeStyles.None, out DateTime date))
                    gosc.DataUrodzenia = date;
                else
                {
                    MessageBox.Show("Podano nieporawną datę", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                //Płeć
                if (cmbox_Plec.Text == "Kobieta")
                {
                    gosc.Plec = Plcie.K;
                }
                else
                {
                    gosc.Plec = Plcie.M;
                }
                //Pesel
                Regex rgx = new Regex(@"^\d{11}$");
                if (rgx.IsMatch(txt_Pesel.Text))
                {
                    gosc.Pesel = txt_Pesel.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawny Pesel", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                //Numer telefonu
                Regex rgx1 = new Regex(@"^\d{3}[-]\d{3}[-]\d{3}");
                if (rgx1.IsMatch(txt_nrTelefonu.Text))
                {
                    gosc.NumerTelefonu = txt_nrTelefonu.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawny nr telefonu", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                //Poprawnosc imienia i nazwiska
                Regex rgx2 = new Regex(@"^[A-Z][a-z-żźćńółęąś]+");
                if (rgx2.IsMatch(txt_Imie.Text))
                {
                    gosc.Imie = txt_Imie.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawne imię", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                Regex rgx3 = new Regex(@"^[A-Z][a-z-żźćńółęąś]+");
                if (rgx3.IsMatch(txt_Nazwisko.Text))
                {
                    gosc.Nazwisko = txt_Nazwisko.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawne Nazwisko", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }



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

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
