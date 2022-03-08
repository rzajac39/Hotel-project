using BibliotekaHotel;
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

namespace GUIProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy Kadra.xaml
    /// </summary>
    public partial class Kadra : Page
    {
        KadraHotelu kadraHotelu;
        List<Pracownik> listaPracownikow = new List<Pracownik>();

        public Kadra()
        {
            InitializeComponent();
            lst_Pracownicy.SelectionMode = SelectionMode.Extended;
            kadraHotelu = (KadraHotelu)KadraHotelu.OdczytajXML("kadra");
            if (kadraHotelu is object)
            {
                txt_Kierownik.Text = kadraHotelu.KierownikGlowny.ToString();
                lst_Kierownicy.ItemsSource = new ObservableCollection<Kierownik>(kadraHotelu.KadraKierownikow);

                foreach (var pracownicy in kadraHotelu.KadraPracownikow)
                {
                    foreach (var item in pracownicy)
                    {
                        listaPracownikow.Add(item);
                    }
                }
                lst_Pracownicy.ItemsSource = new ObservableCollection<Pracownik>(listaPracownikow);
            }
        }

        private void btn_Wyszukaj_Click(object sender, RoutedEventArgs e)
        {
            lst_Pracownicy.UnselectAll();
            foreach(var item in lst_Pracownicy.Items)
            {
                if(((Pracownik)item).Funkcja.ToString() == cmb_Funkcje.Text)
                {
                    lst_Pracownicy.SelectedItems.Add(item);
                }
            }
        }

        private void btn_Wyswietl_Click(object sender, RoutedEventArgs e)
        {
            ZarobkiWindow okno = new ZarobkiWindow(kadraHotelu);
            bool? result = okno.ShowDialog();
        }
    }
}
