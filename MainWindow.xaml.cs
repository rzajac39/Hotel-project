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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BibliotekaHotel;

namespace GUIProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Glowna();
        }

        private void btn_Pokoje_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Pokoje();
        }

        private void btn_Rezerwacje_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Rezerwacje();
        }


        private void btn_Goscie_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Goscie();
        }

        private void btn_Kadra_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Kadra();
        }

        private void btn_Zmiany_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Zmiany();
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_Zamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
