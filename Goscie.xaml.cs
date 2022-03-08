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
    /// Logika interakcji dla klasy Goscie.xaml
    /// </summary>
    public partial class Goscie : Page
    {
        public SpisGosci goscie;
        public Goscie()
        {
            InitializeComponent();
            lst_Goscie.SelectionMode = SelectionMode.Extended;
            goscie = (SpisGosci)SpisGosci.OdczytajXML("goscie");
            if(goscie is object)
            {
                lst_Goscie.ItemsSource = new ObservableCollection<Gosc>(goscie.ListaGosci);             
            }
            
        }

        private void btn_DodajGoscia_Click(object sender, RoutedEventArgs e)
        {
            Gosc gosc = new Gosc();
            GoscWindow okno = new GoscWindow(gosc,goscie);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                goscie.DodajGoscia(gosc);
                lst_Goscie.ItemsSource = new
                ObservableCollection<Gosc>(goscie.ListaGosci);
                SpisGosci.ZapiszXML("goscie", goscie);
            }
        }
        private void btn_UsunGoscia_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Goscie.SelectedItems != null)
            {
                foreach (var item in lst_Goscie.SelectedItems)
                {
                    goscie.UsunGosca((Gosc)item);
                }
                lst_Goscie.ItemsSource = new
                ObservableCollection<Gosc>(goscie.ListaGosci);
                SpisGosci.ZapiszXML("goscie", goscie);
            }
        }
    }

       
    }

