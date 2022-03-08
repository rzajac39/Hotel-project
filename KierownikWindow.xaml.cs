using BibliotekaHotel;
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

namespace GUIProjekt
{
    /// <summary>
    /// Interaction logic for KierownikWindow.xaml
    /// </summary>
    public partial class KierownikWindow : Window
    {
        int wybor;
        Kierownik kierownik1;
        Kierownik kierownik2;
        Kierownik kierownik3;
        List<Kierownik> lista;
        public KierownikWindow()
        {
            InitializeComponent();
        }

        public KierownikWindow(Kierownik k1, Kierownik k2, Kierownik k3) : this()
        {
            kierownik1 = k1;
            kierownik2 = k2;
            kierownik3 = k3;
            lista = new List<Kierownik>();
            lista.Add(k1);
            lista.Add(k2);
            lista.Add(k3);
        }

        public int Wybor()
        {
            return wybor;
        }


        private void btn_Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            bool blad = false;

            if (txt_Imie.Text != "" && txt_Nazwisko.Text != "" && txt_Pesel.Text != "" && txt_DataUrodzenia.Text != "" &&
                txt_nrTelefonu.Text != "" && txt_NumerKonta.Text != "")
            {
                //Data
                if (DateTime.TryParseExact(txt_DataUrodzenia.Text,
                    new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy" },
                    null, DateTimeStyles.None, out DateTime date))
                    lista[wybor].DataUrodzenia = date;
                else
                {
                    MessageBox.Show("Podano nieporawną datę", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                //Płeć
                if (cmbox_Plec.Text == "Kobieta")
                {
                    lista[wybor].Plec = Plcie.K;
                }
                else
                {
                    lista[wybor].Plec = Plcie.M;
                }
                //Pesel
                Regex rgx = new Regex(@"^\d{11}$");
                if (rgx.IsMatch(txt_Pesel.Text))
                {
                    lista[wybor].Pesel = txt_Pesel.Text;
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
                    lista[wybor].NumerTelefonu = txt_nrTelefonu.Text;
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
                    lista[wybor].Imie = txt_Imie.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawne imię", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                Regex rgx3 = new Regex(@"^[A-Z][a-z-żźćńółęąś]+");
                if (rgx3.IsMatch(txt_Nazwisko.Text))
                {
                    lista[wybor].Nazwisko = txt_Nazwisko.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawne Nazwisko", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                //Numer konta
                Regex rgx4 = new Regex(@"^\d{2}[\s]\d{4}[\s]\d{4}[\s]\d{4}[\s]\d{4}[\s]\d{4}[\s]\d{4}$");
                if (rgx4.IsMatch(txt_NumerKonta.Text))
                {
                    lista[wybor].NumerKonta = txt_NumerKonta.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawnu numer konta", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }

                if (blad == false)
                {
                    DialogResult = true;
                }

            }
            else DialogResult = false;
        }

        private void btn_Poranna_Click(object sender, RoutedEventArgs e)
        {
            wybor = 0;

            txt_Pesel.Text = kierownik1.Pesel;
            txt_Imie.Text = kierownik1.Imie;
            txt_Nazwisko.Text = kierownik1.Nazwisko;
            txt_DataUrodzenia.Text = $"{kierownik1.DataUrodzenia:dd-MM-yyyy}";
            txt_nrTelefonu.Text = kierownik1.NumerTelefonu.ToString();
            cmbox_Plec.Text = (kierownik1.Plec == Plcie.K) ? "Kobieta" : "Mężczyzna";
            txt_NumerKonta.Text = kierownik1.NumerKonta;
        }

        private void btn_Popoludniowa_Click(object sender, RoutedEventArgs e)
        {
            wybor = 1;

            txt_Pesel.Text = kierownik2.Pesel;
            txt_Imie.Text = kierownik2.Imie;
            txt_Nazwisko.Text = kierownik2.Nazwisko;
            txt_DataUrodzenia.Text = $"{kierownik2.DataUrodzenia:dd-MM-yyyy}";
            txt_nrTelefonu.Text = kierownik2.NumerTelefonu.ToString();
            cmbox_Plec.Text = (kierownik2.Plec == Plcie.K) ? "Kobieta" : "Mężczyzna";
            txt_NumerKonta.Text = kierownik2.NumerKonta;
        }

        private void btn_Nocna_Click(object sender, RoutedEventArgs e)
        {
            wybor = 2;

            txt_Pesel.Text = kierownik3.Pesel;
            txt_Imie.Text = kierownik3.Imie;
            txt_Nazwisko.Text = kierownik3.Nazwisko;
            txt_DataUrodzenia.Text = $"{kierownik3.DataUrodzenia:dd-MM-yyyy}";
            txt_nrTelefonu.Text = kierownik3.NumerTelefonu.ToString();
            cmbox_Plec.Text = (kierownik3.Plec == Plcie.K) ? "Kobieta" : "Mężczyzna";
            txt_NumerKonta.Text = kierownik3.NumerKonta;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
