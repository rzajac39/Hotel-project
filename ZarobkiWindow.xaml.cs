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

namespace GUIProjekt
{
    /// <summary>
    /// Interaction logic for ZarobkiWindow.xaml
    /// </summary>
    public partial class ZarobkiWindow : Window
    {
        KadraHotelu kadraHotelu;
        public ZarobkiWindow()
        {
            InitializeComponent();
        }

        public ZarobkiWindow(KadraHotelu kadra) : this()
        {
            double suma = 0;
            kadraHotelu = kadra;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Zarobki:");
            sb.AppendLine();
            suma += kadraHotelu.KierownikGlowny.Zarobki;
            sb.AppendLine($"Kierownik główny: {kadraHotelu.KierownikGlowny.Imie} {kadraHotelu.KierownikGlowny.Nazwisko}, zarobki: {kadraHotelu.KierownikGlowny.Zarobki}");
            sb.AppendLine();
            sb.AppendLine("Kierownicy zmian:");
            sb.AppendLine();
            foreach (var item in kadraHotelu.KadraKierownikow)
            {
                sb.AppendLine($"{item.Imie} {item.Nazwisko}, zarobki: {item.Zarobki}");
                suma += item.Zarobki;
            }
            sb.AppendLine();
            sb.AppendLine("Pracownicy:");
            sb.AppendLine();
            foreach (var pracownicy in kadraHotelu.KadraPracownikow)
            {
                foreach (var item in pracownicy)
                {
                    sb.AppendLine($"{item.Imie} {item.Nazwisko}, {item.Funkcja}, zarobki: {item.Zarobki}");
                    suma += item.Zarobki;
                }
            }
            sb.AppendLine();
            sb.AppendLine($"Suma zarobków wynosi: {suma}");

            txt_Zarobki.Text = sb.ToString();

        }

        private void btn_Zamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
