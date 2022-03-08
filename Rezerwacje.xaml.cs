using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BibliotekaHotel;

namespace GUIProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy Rezerwacje.xaml
    /// </summary>
    public partial class Rezerwacje : Page
    {
        // Deklarujemy zmienne, aby potem je wczytać
        ListaRezerwacji listaRezerwacji;
        SpisGosci spisGosci;
        ListaPokojow listaPokojow;
        /// <summary>
        /// Konstruktor nieparametryczny
        /// </summary>
        public Rezerwacje()
        {
            // Wczytujemy stronę
            InitializeComponent();
            // Ustawiamy możliwy wybór selekcji w listboxie na pojedynczy
            lst_Rezerwacje.SelectionMode = SelectionMode.Single;
            // Wczytujemy dane do zmiennych, z plików XML
            spisGosci = (SpisGosci)SpisGosci.OdczytajXML("goscie");
            listaPokojow = (ListaPokojow)ListaPokojow.OdczytajXML("pokoje");
            listaRezerwacji = (ListaRezerwacji)ListaRezerwacji.OdczytajXML("rezerwacje");
            // Tworzymy listy numerów pokojów i peseli
            List<int> listaNumerowPokojow = new List<int>();
            List<string> listaPeseli = new List<string>();
            // Obliczamy nr pokoju dla każdego obiektu i dodajemy go do listy
            foreach (var item in listaPokojow.Pokoje)
            {
                int nrPokoju = (item.Pietro * 10) + item.NumerPokoju;
                listaNumerowPokojow.Add(nrPokoju);
            }
            // Sortujemy listę Pokojów i ustawiamy jej elementy do comboBoxa
            listaNumerowPokojow.Sort()
; this.xmb_Pokoj.ItemsSource = listaNumerowPokojow;
            // Wczytujemy pesele elementów do listy i ustawiamy ją jako elementy comboBoxa
            foreach (var item in spisGosci.ListaGosci)
            {
                listaPeseli.Add(item.Pesel);
            }
            this.xmb_Pesel.ItemsSource = listaPeseli;
            // Wkładamy do listBoxa wczytane rezerwacje
            if (listaRezerwacji is object)
            {
                lst_Rezerwacje.ItemsSource = new ObservableCollection<Rezerwacja>(listaRezerwacji.Rezerwacje);
            }
            // Ustawiamy możliwy wybór daty w kalendarzu na pojedynczy
            calendar.SelectionMode = CalendarSelectionMode.SingleDate;
            // Ustawiamy początek kalendarza na dzisiaj
            calendar.DisplayDateStart = DateTime.Today;
            // Ustawiamy koniec kalendarza na wybraną datę
            calendar.DisplayDateEnd = new DateTime(2023, 12, 31);

        }


        /// <summary>
        /// Przycisk usuwa rezerwację z listy oraz z kalendarza danego pokoju
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Usun_Click(object sender, RoutedEventArgs e)
        {
            // Jeśli zaznaczono obiekt rezerwacji w listBox
            if (lst_Rezerwacje.SelectedItem != null)
            {
                // Czyścimy kalendarz z zablokowanych terminów
                calendar.BlackoutDates.Clear();
                // Zmieniamy opcję selekcji kalendarza, aby dalsze używane funkcje działały
                calendar.SelectionMode = CalendarSelectionMode.SingleRange;
                // Deklarujemy do zmiennej nrPokoju z którego usuwamy rezerwację
                string nrPokoju = ((Rezerwacja)lst_Rezerwacje.SelectedItem).NumerPokoju.ToString();
                // Usuwamy rezerwację z listy
                listaRezerwacji.UsunRezerwacje((Rezerwacja)lst_Rezerwacje.SelectedItem);
                // Aktualizujemy listBox o usuniętą rezerwację
                lst_Rezerwacje.ItemsSource = new
                ObservableCollection<Rezerwacja>(listaRezerwacji.Rezerwacje);
                // Zapisujemy zaktualizowaną listę do pliku
                ListaRezerwacji.ZapiszXML("rezerwacje", listaRezerwacji);
                // Jeśli użytkownik miałby otwarty kalendarz danego pokoju to wczytujemy dla niego pozostałę rezerwacje do kalendarza
                foreach (var item in listaRezerwacji.Rezerwacje)
                {
                    if (item.NumerPokoju.ToString() == nrPokoju)
                    {

                        calendar.BlackoutDates.Add(new CalendarDateRange(item.DataWprowadzenia, item.Wyprowadzka()));

                    }
                }
                // Zmieniamy tryb selekcji kalendarza na pojedynczy
                calendar.SelectionMode = CalendarSelectionMode.SingleDate;

            }
        }


        /// <summary>
        /// Wybranie nr Pokoju w listBox i dodatkowe wczytanie zarezerwowanych dni tego pokoju do kalendarza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xmb_Pokoj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Oczyszczamy kalendarz z rezerwacji (zablokowanych dni)
            calendar.BlackoutDates.Clear();
            // Zmieniamy opcję selekcji kalendarza, aby dalsze używane funkcje działały
            calendar.SelectionMode = CalendarSelectionMode.SingleRange;
            // Obliczamy piętro i nrPokoju na podstawie "nrPokoju", aby móc wczytać ze spisu pokojów ilość osób, które mogą w nim nocować
            int pietro = int.Parse(this.xmb_Pokoj.SelectedItem.ToString()) / 10;
            int nrPokoju = int.Parse(this.xmb_Pokoj.SelectedItem.ToString()) % 10;
            foreach (var item in listaPokojow.Pokoje)
            {
                if (item.NumerPokoju == nrPokoju && item.Pietro == pietro)
                {
                    txt_IloscOsob.Text = item.IloscMiejsc.ToString();
                }
            }
            // Spawdzamy, które rezerwację są na wybrany pokój i dodajemy zarezerwowane dni do kalendarza
            int pietro1, nrPokoju1;

            foreach (var item in listaRezerwacji.Rezerwacje)
            {
                pietro1 = item.NumerPokoju / 10;
                nrPokoju1 = item.NumerPokoju % 10;
                if (nrPokoju1 == nrPokoju && pietro1 == pietro)
                {
                    calendar.BlackoutDates.Add(new CalendarDateRange(item.DataWprowadzenia, item.Wyprowadzka()));
                }
            }
            // Zmieniamy tryb selekcji kalendarza na pojedynczy
            calendar.SelectionMode = CalendarSelectionMode.SingleDate;
        }
        /// <summary>
        /// Przycisk dodawania rezerwacji do wybranego pokoju, który sprawdza czy chciana rezerwacja jest możliwa do wykonania, jeśli jest zaznacza ją w kalendarzu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            int result;
            // Sprawdzamy czy podana ilość dni jest liczbą, jeśli nie wysyłąmy użytkownikowi informację o tym
            if (int.TryParse(this.txt_IloscDni.Text, out result) == false)
            {
                MessageBox.Show("Podano niepoprawną ilość dni", "Błąd", MessageBoxButton.OK);
            }
            // W sumie to nigdy się nie zdarzy
            else if (calendar.SelectedDates.Count != 1)
            {
                MessageBox.Show("Zaznaczono więcej niż jeden dzień wprowadzenia", "Błąd", MessageBoxButton.OK);
            }

            else
            {
                // Jeśli wybrano pokój, pesel oraz wybrano datę wprowadzenia
                if (this.xmb_Pesel.SelectedItem.ToString() != "" && this.xmb_Pokoj.SelectedItem.ToString() != "" && calendar.SelectedDate != null)
                {
                   // Tworzymy nową instancję obiektu rezerwacji
                    Rezerwacja rezerwacja = new Rezerwacja(int.Parse(txt_IloscDni.Text), (DateTime)this.calendar.SelectedDate, this.xmb_Pesel.Text, int.Parse(this.xmb_Pokoj.Text));
                    // Zmieniamy opcję selekcji kalendarza, aby dalsze używane funkcje działały
                    calendar.SelectionMode = CalendarSelectionMode.SingleRange;
                    // Tworzymy dwa obiekty daty, które przechowują odpowiednio datę wprowadzenia i wyprowadzenia z pokoju
                    DateTime date1 = new DateTime(rezerwacja.DataWprowadzenia.Year, rezerwacja.DataWprowadzenia.Month, rezerwacja.DataWprowadzenia.Day);
                    DateTime date2 = new DateTime(rezerwacja.Wyprowadzka().Year, rezerwacja.Wyprowadzka().Month, rezerwacja.Wyprowadzka().Day);
                    //Jeśli zarezerwowane dni pokrywają się z inną rezerwacją informujemy o tym użytkownika
                    if (calendar.BlackoutDates.ContainsAny(new CalendarDateRange(date1, date2)) == true)
                    {
                        MessageBox.Show("Zarezerwowane dni się pokrywają, zrób inną rezerwację", "Błąd", MessageBoxButton.OK);
                    }
                    // Jeśli wszystko się zgadza
                    else { 
                        // Odznaczamy zaznaczoną datę wprowadzenia
                    calendar.SelectedDates.Clear();
                        // Dodajemy do kalendarza zarezerwowane dni
                    calendar.BlackoutDates.Add(new CalendarDateRange(date1, date2));

                        // Dodajemy do listy rezerwacji stworzoną rezerwację
                    listaRezerwacji.DodajRezerwacje(rezerwacja);
                        // Aktualizujemy listBox o nową rezerwację
                    lst_Rezerwacje.ItemsSource = new
                                 ObservableCollection<Rezerwacja>(listaRezerwacji.Rezerwacje);
                        // Zapisujemy zaktualizowaną listę do pliku XML
                    ListaRezerwacji.ZapiszXML("rezerwacje", listaRezerwacji);
                        }
                    // Zmieniamy tryb selekcji kalendarza na pojedynczy
                    calendar.SelectionMode = CalendarSelectionMode.SingleDate;

                }
            }
        }
    }
}
