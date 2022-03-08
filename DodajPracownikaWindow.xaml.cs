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
    /// Interaction logic for DodajPracownikaWindow.xaml
    /// </summary>
    public partial class DodajPracownikaWindow : Window
    {
        Zmiana zmiana1;
        Zmiana zmiana2;
        Zmiana zmiana3;
        Pracownik pracownik;

        public DodajPracownikaWindow()
        {
            InitializeComponent();
        }
        public DodajPracownikaWindow(Zmiana z1, Zmiana z2, Zmiana z3, Pracownik p) : this()
        {
            zmiana1 = z1;
            zmiana2 = z2;
            zmiana3 = z3;
            pracownik = p;
        }

        public int JakaZmiana()
        {
            if (cmbox_Zmiana.Text == "Poranna")
            {
                return 1;                                
            } else if (cmbox_Zmiana.Text == "Popołudniowa")
            {
                return 2;
            } else
            {
                return 3;
            }
        }

        private void btn_Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            bool blad = false;
            bool czyIstnieje1 = false;
            bool czyIstnieje2 = false;
            bool czyIstnieje3 = false;
            foreach (var item in zmiana1.ListaPracownikow)
            {
                if (txt_Pesel.Text == item.Pesel)
                {
                    czyIstnieje1 = true;
                }
            }
            foreach (var item in zmiana2.ListaPracownikow)
            {
                if (txt_Pesel.Text == item.Pesel)
                {
                    czyIstnieje2 = true;
                }
            }
            foreach (var item in zmiana3.ListaPracownikow)
            {
                if (txt_Pesel.Text == item.Pesel)
                {
                    czyIstnieje3 = true;
                }
            }

            if (czyIstnieje1 == true || czyIstnieje2 == true || czyIstnieje3 == true)
            {
                MessageBox.Show("Taki pracownik już został dodany", "Błąd", MessageBoxButton.OK);
                DialogResult = false;
            } 

            if (txt_Imie.Text != "" && txt_Nazwisko.Text != "" && txt_Pesel.Text != "" && txt_DataUrodzenia.Text != "" &&
                txt_nrTelefonu.Text != "" && txt_NumerKonta.Text != "" && cmbox_Funkcje.Text != "")
            {
                //Data
                if (DateTime.TryParseExact(txt_DataUrodzenia.Text,
                    new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy" },
                    null, DateTimeStyles.None, out DateTime date))
                    pracownik.DataUrodzenia = date;
                else
                {
                    MessageBox.Show("Podano nieporawną datę", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                //Płeć
                if (cmbox_Plec.Text == "Kobieta")
                {
                    pracownik.Plec = Plcie.K;
                }
                else
                {
                    pracownik.Plec = Plcie.M;
                }
                //Pesel
                Regex rgx = new Regex(@"^\d{11}$");
                if (rgx.IsMatch(txt_Pesel.Text))
                {
                    pracownik.Pesel = txt_Pesel.Text;
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
                    pracownik.NumerTelefonu = txt_nrTelefonu.Text;
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
                    pracownik.Imie = txt_Imie.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawne imię", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }
                Regex rgx3 = new Regex(@"^[A-Z][a-z-żźćńółęąś]+");
                if (rgx3.IsMatch(txt_Nazwisko.Text))
                {
                    pracownik.Nazwisko = txt_Nazwisko.Text;
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
                    pracownik.NumerKonta = txt_NumerKonta.Text;
                }
                else
                {
                    MessageBox.Show("Podano nieporawnu numer konta", "Błąd", MessageBoxButton.OK);
                    blad = true;
                }

                if (blad == false && czyIstnieje1 == false && czyIstnieje2 == false && czyIstnieje3 == false)
                {
                    pracownik.Zarobki = Placa(pracownik.Funkcja);
                    DialogResult = true;
                }

            }
            else DialogResult = false;
        }

        private double Placa(Funkcje funkcja)
        {
            double placa;
            switch (funkcja)
            {
                case Funkcje.Bagażowy:
                    placa = 3000;
                    break;
                case Funkcje.Kelner:
                    placa = 3200;
                    break;
                case Funkcje.KonserwatorPowierzchniPłaskich:
                    placa = 3500;
                    break;
                case Funkcje.Kucharz:
                    placa = 5000;
                    break;
                case Funkcje.Ochroniarz:
                    placa = 2900;
                    break;
                case Funkcje.PomocKuchenna:
                    placa = 3300;
                    break;
                case Funkcje.Portier:
                    placa = 3100;
                    break;
                case Funkcje.ZłotaRączka:
                    placa = 4500;
                    break;
                default:
                    placa = 0;
                    break;
            }
            return placa;
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
