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
    /// Logika interakcji dla klasy Pokoje.xaml
    /// </summary>
    public partial class Pokoje : Page
    {
        public ListaPokojow pokoje;
        public Pokoje()
        {
            InitializeComponent();
            lst_Pokoje.SelectionMode = SelectionMode.Extended;
            pokoje = (ListaPokojow)ListaPokojow.OdczytajXML("pokoje");
            if(pokoje is object)
            {
                lst_Pokoje.ItemsSource = new ObservableCollection<Pokoj>(pokoje.Pokoje);                
            }
            txt_Liczba.Text = pokoje.LiczbaPokojow.ToString();
        }

        private void btn_DodajPokoj_Click(object sender, RoutedEventArgs e)
        {
            Pokoj pokoj = new Pokoj();
            PokojWindow okno = new PokojWindow(pokoj, pokoje);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                pokoj.Oplata = pokoj.Koszt(pokoj.Standard, pokoj.IloscMiejsc);
                pokoje.DodajPokoj(pokoj); 
                lst_Pokoje.ItemsSource = new
                ObservableCollection<Pokoj>(pokoje.Pokoje);
                ListaPokojow.ZapiszXML("pokoje", pokoje);
            }
            txt_Liczba.Text = pokoje.LiczbaPokojow.ToString();
        }

        private void btn_UsunPokoj_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Pokoje.SelectedItems != null)
            {
                foreach (var item in lst_Pokoje.SelectedItems)
                {
                    pokoje.UsunPokoj((Pokoj)item);
                }
                lst_Pokoje.ItemsSource = new
                ObservableCollection<Pokoj>(pokoje.Pokoje);
                ListaPokojow.ZapiszXML("pokoje", pokoje);
            }
            txt_Liczba.Text = pokoje.LiczbaPokojow.ToString();
        }

        private void btn_Sortuj_Click(object sender, RoutedEventArgs e)
        {
            pokoje.Sortuj();
            lst_Pokoje.ItemsSource = new
            ObservableCollection<Pokoj>(pokoje.Pokoje);
            ListaPokojow.ZapiszXML("pokoje", pokoje);
        }

        
    }
}
