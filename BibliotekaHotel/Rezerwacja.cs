using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaHotel
{
    [Serializable]
    public class Rezerwacja
    {
        
        int dlugoscPobytu;
        DateTime dataWprowadzenia;
        string pesel;
        int numerPokoju;

        
        public int DlugoscPobytu { get => dlugoscPobytu; set => dlugoscPobytu = value; }
        public DateTime DataWprowadzenia { get => dataWprowadzenia; set => dataWprowadzenia = value; }
        public int NumerPokoju { get => numerPokoju; set => numerPokoju = value; }
        public string Pesel { get => pesel; set => pesel = value; }

        public Rezerwacja() { }
        public Rezerwacja(int dlugoscPobytu, DateTime dataWprowadzenia, string pesel, int numerPokoju) : this()
        {
            DlugoscPobytu = dlugoscPobytu;
            DataWprowadzenia = dataWprowadzenia;
            Pesel = pesel;
            NumerPokoju = numerPokoju;
        }


        public DateTime Wyprowadzka()
        {
            DateTime wyprowadzka = DataWprowadzenia.AddDays(DlugoscPobytu).Date;
            return wyprowadzka;
        }

        public override string ToString()
        {
            return $"Nr pokoju: {NumerPokoju}, data wprowadzenia: {DataWprowadzenia.ToString("dd-MM-yyyy")}, data wyprowadzki: {Wyprowadzka().ToString("dd-MM-yyyy")}";
        }
    }
}
