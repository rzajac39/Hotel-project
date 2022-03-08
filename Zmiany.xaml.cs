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
    /// Logika interakcji dla klasy Zmiany.xaml
    /// </summary>
    public partial class Zmiany : Page
    {
        public Zmiana zmiana1;
        public Zmiana zmiana2;
        public Zmiana zmiana3;
        KadraHotelu kadraHotelu;
        public Zmiany()
        {
            InitializeComponent();
            lst_Zmiana1.SelectionMode = SelectionMode.Extended;
            lst_Zmiana2.SelectionMode = SelectionMode.Extended;
            lst_Zmiana3.SelectionMode = SelectionMode.Extended;
            zmiana1 = (Zmiana)Zmiana.OdczytajXML("zmiana1");
            zmiana2 = (Zmiana)Zmiana.OdczytajXML("zmiana2");
            zmiana3 = (Zmiana)Zmiana.OdczytajXML("zmiana3");
            kadraHotelu = (KadraHotelu)KadraHotelu.OdczytajXML("kadra");
            if (zmiana1 is object && zmiana2 is object && zmiana3 is object)
            {
                lst_Zmiana1.ItemsSource = new ObservableCollection<Pracownik>(zmiana1.ListaPracownikow);
                lst_Zmiana2.ItemsSource = new ObservableCollection<Pracownik>(zmiana2.ListaPracownikow);
                lst_Zmiana3.ItemsSource = new ObservableCollection<Pracownik>(zmiana3.ListaPracownikow);
                txt_Kierownik1.Text = zmiana1.KierownikZmiany.ToString();
                txt_Kierownik2.Text = zmiana2.KierownikZmiany.ToString();
                txt_Kierownik3.Text = zmiana3.KierownikZmiany.ToString();
            }
        }

        private void btn_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Pracownik pracownik = new Pracownik();
            DodajPracownikaWindow okno = new DodajPracownikaWindow(zmiana1, zmiana2, zmiana3, pracownik);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                if (okno.JakaZmiana() == 1)
                {
                    zmiana1.DodajPracownika(pracownik);
                    lst_Zmiana1.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana1.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana1", zmiana1);
                    kadraHotelu.KadraPracownikow.RemoveAt(0);
                    kadraHotelu.KadraPracownikow.Insert(0, zmiana1.ListaPracownikow);

                }
                else if (okno.JakaZmiana() == 2)
                {
                    zmiana2.DodajPracownika(pracownik);
                    lst_Zmiana2.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana2.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana2", zmiana2);
                    kadraHotelu.KadraPracownikow.RemoveAt(1);
                    kadraHotelu.KadraPracownikow.Insert(1, zmiana2.ListaPracownikow);
                }
                else 
                {
                    zmiana3.DodajPracownika(pracownik);
                    lst_Zmiana3.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana3.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana3", zmiana3);
                    kadraHotelu.KadraPracownikow.RemoveAt(2);
                    kadraHotelu.KadraPracownikow.Insert(2, zmiana3.ListaPracownikow);
                }
                KadraHotelu.ZapiszXML("kadra", kadraHotelu);
            }
        }

        private void btn_Usun_Click(object sender, RoutedEventArgs e)
        { 
            if (lst_Zmiana1.SelectedItems != null)
            {
                
                foreach (var item in lst_Zmiana1.SelectedItems)
                {
                    zmiana1.UsunPracownika((Pracownik)item);
                }
                lst_Zmiana1.ItemsSource = new
                ObservableCollection<Pracownik>(zmiana1.ListaPracownikow);
                Zmiana.ZapiszXML("Zmiana1", zmiana1);

                kadraHotelu.KadraPracownikow.RemoveAt(0);
                kadraHotelu.KadraPracownikow.Insert(0, zmiana1.ListaPracownikow);
                KadraHotelu.ZapiszXML("kadra", kadraHotelu);
            }
            if (lst_Zmiana2.SelectedItems != null)
            {
                
                foreach (var item in lst_Zmiana2.SelectedItems)
                {
                    zmiana2.UsunPracownika((Pracownik)item);
                }
                lst_Zmiana2.ItemsSource = new
                ObservableCollection<Pracownik>(zmiana2.ListaPracownikow);
                Zmiana.ZapiszXML("Zmiana2", zmiana2);

                kadraHotelu.KadraPracownikow.RemoveAt(1);
                kadraHotelu.KadraPracownikow.Insert(1, zmiana2.ListaPracownikow);
                KadraHotelu.ZapiszXML("kadra", kadraHotelu);
            }
            if (lst_Zmiana3.SelectedItems != null)
            {
                foreach (var item in lst_Zmiana3.SelectedItems)
                {
                    zmiana3.UsunPracownika((Pracownik)item);
                }
                lst_Zmiana3.ItemsSource = new
                ObservableCollection<Pracownik>(zmiana3.ListaPracownikow);
                Zmiana.ZapiszXML("Zmiana3", zmiana3);

                kadraHotelu.KadraPracownikow.RemoveAt(2);
                kadraHotelu.KadraPracownikow.Insert(2, zmiana3.ListaPracownikow);
                KadraHotelu.ZapiszXML("kadra", kadraHotelu);
            }
        }

        private void btn_Zmien_Click(object sender, RoutedEventArgs e)
        {
            
            KierownikWindow okno = new KierownikWindow(zmiana1.KierownikZmiany, zmiana2.KierownikZmiany, zmiana3.KierownikZmiany);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                if (okno.Wybor() == 0)
                {
                    kadraHotelu.KadraKierownikow.RemoveAt(0);
                    kadraHotelu.KadraKierownikow.Insert(0,zmiana1.KierownikZmiany);
                   
                    
                    txt_Kierownik1.Text = zmiana1.KierownikZmiany.ToString();
                    
                    Zmiana.ZapiszXML("Zmiana1", zmiana1);
                    
                }
                else if (okno.Wybor() == 1)
                {
                    txt_Kierownik2.Text = zmiana2.KierownikZmiany.ToString();
                    kadraHotelu.KadraKierownikow.RemoveAt(1);
                    kadraHotelu.KadraKierownikow.Insert(1, zmiana2.KierownikZmiany);
                    Zmiana.ZapiszXML("Zmiana2", zmiana2);
                   
                }
                else
                {
                    txt_Kierownik3.Text = zmiana3.KierownikZmiany.ToString();
                    kadraHotelu.KadraKierownikow.RemoveAt(2);
                    kadraHotelu.KadraKierownikow.Insert(2, zmiana3.KierownikZmiany);
                    Zmiana.ZapiszXML("Zmiana3", zmiana3);
                    
                }
                KadraHotelu.ZapiszXML("kadra", kadraHotelu);
            }
        }

        private void btn_Wymien_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Zmiana1.SelectedItems.Count + lst_Zmiana2.SelectedItems.Count + lst_Zmiana3.SelectedItems.Count == 2)
            {
                if (lst_Zmiana1.SelectedItems.Count == 1 && lst_Zmiana2.SelectedItems.Count ==1 && ((Pracownik)lst_Zmiana1.SelectedItem).Funkcja == ((Pracownik)lst_Zmiana2.SelectedItem).Funkcja)
                {
                    
                    zmiana2.DodajPracownika((Pracownik)lst_Zmiana1.SelectedItem);
                    zmiana1.DodajPracownika((Pracownik)lst_Zmiana2.SelectedItem);
                    zmiana1.UsunPracownika((Pracownik)lst_Zmiana1.SelectedItem);
                    zmiana2.UsunPracownika((Pracownik)lst_Zmiana2.SelectedItem);

                    kadraHotelu.KadraPracownikow.RemoveAt(0);
                    kadraHotelu.KadraPracownikow.Insert(0, zmiana1.ListaPracownikow);
                    kadraHotelu.KadraPracownikow.RemoveAt(1);
                    kadraHotelu.KadraPracownikow.Insert(1, zmiana2.ListaPracownikow);
                    lst_Zmiana1.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana1.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana1", zmiana1);
                    lst_Zmiana2.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana2.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana2", zmiana2);
                    KadraHotelu.ZapiszXML("kadra", kadraHotelu);
                }
                if (lst_Zmiana1.SelectedItems.Count == 1 && lst_Zmiana3.SelectedItems.Count ==1 && ((Pracownik)lst_Zmiana1.SelectedItem).Funkcja == ((Pracownik)lst_Zmiana3.SelectedItem).Funkcja)
                {
                    ;
                    zmiana1.DodajPracownika((Pracownik)lst_Zmiana3.SelectedItem);
                    zmiana3.DodajPracownika((Pracownik)lst_Zmiana1.SelectedItem);
                    zmiana1.UsunPracownika((Pracownik)lst_Zmiana1.SelectedItem);
                    zmiana3.UsunPracownika((Pracownik)lst_Zmiana3.SelectedItem);

                    kadraHotelu.KadraPracownikow.RemoveAt(0);
                    kadraHotelu.KadraPracownikow.Insert(0, zmiana1.ListaPracownikow);
                    kadraHotelu.KadraPracownikow.RemoveAt(2);
                    kadraHotelu.KadraPracownikow.Insert(2, zmiana3.ListaPracownikow);
                    lst_Zmiana1.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana1.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana1", zmiana1);
                    lst_Zmiana3.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana3.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana3", zmiana3);
                    KadraHotelu.ZapiszXML("kadra", kadraHotelu);
                }
                if (lst_Zmiana2.SelectedItems.Count == 1 && lst_Zmiana3.SelectedItems.Count ==1 && ((Pracownik)lst_Zmiana2.SelectedItem).Funkcja == ((Pracownik)lst_Zmiana3.SelectedItem).Funkcja)
                {
                    
                    zmiana2.DodajPracownika((Pracownik)lst_Zmiana3.SelectedItem);
                    zmiana3.DodajPracownika((Pracownik)lst_Zmiana2.SelectedItem);
                    zmiana2.UsunPracownika((Pracownik)lst_Zmiana2.SelectedItem);
                    zmiana3.UsunPracownika((Pracownik)lst_Zmiana3.SelectedItem);

                    kadraHotelu.KadraPracownikow.RemoveAt(1);
                    kadraHotelu.KadraPracownikow.Insert(1, zmiana2.ListaPracownikow);
                    kadraHotelu.KadraPracownikow.RemoveAt(2);
                    kadraHotelu.KadraPracownikow.Insert(2, zmiana3.ListaPracownikow);
                    lst_Zmiana2.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana2.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana2", zmiana2);
                    lst_Zmiana3.ItemsSource = new
                    ObservableCollection<Pracownik>(zmiana3.ListaPracownikow);
                    Zmiana.ZapiszXML("Zmiana3", zmiana3);
                    KadraHotelu.ZapiszXML("kadra", kadraHotelu);
                }
                
            } else
            {
                MessageBox.Show("Błędnie zaznaczono obiekty", "Błąd", MessageBoxButton.OK);
            }
        }
    }
}
