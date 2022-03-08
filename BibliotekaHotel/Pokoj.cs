using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaHotel
{
    [Serializable]
    public class Pokoj : IComparable<Pokoj>
    {
        int numerPokoju;
        int pietro;
        int iloscMiejsc;
        double oplata;
        Standardy standard;
        public int NumerPokoju { get => numerPokoju; set => numerPokoju = value; }
        public int Pietro { get => pietro; set => pietro = value; }
        public int IloscMiejsc { get => iloscMiejsc; set => iloscMiejsc = value; }
        public double Oplata { get => oplata; set => oplata = value; }
        public Standardy Standard { get => standard; set => standard = value; }
        public Pokoj() 
        {
            
        }
        public Pokoj(int iloscMiejsc, Standardy standard, int numerPokoju, int pietro) : this()
        {
            this.Oplata = Koszt(standard, iloscMiejsc);
            this.Pietro = pietro;
            this.NumerPokoju = numerPokoju;
            this.IloscMiejsc = iloscMiejsc;
            this.Standard = standard;
        }
        public double Koszt(Standardy standard, int iloscMiejsc)
        {
            double wynik;
            switch (standard)
            {
                case Standardy.podstawowy:
                    wynik = 100 * iloscMiejsc;
                    break;
                case Standardy.komfort:
                    wynik = 150 * iloscMiejsc;
                    break;
                case Standardy.komfortPlus:
                    wynik = 180 * iloscMiejsc;
                    break;
                default:
                    wynik = 0;
                    break;
            }
            return wynik;
        }

        public override string ToString()
        {
            return $"Nr pokoju {Pietro}{NumerPokoju}, Ilość miejsc {IloscMiejsc}, Standard {Standard}, oplata: {Oplata}";
        }

        public int CompareTo(Pokoj other)
        {
            if  (other == null)
                return 1;
            int wynik = Pietro.CompareTo(other.Pietro);
            if (wynik == 0)
            {
                wynik = NumerPokoju.CompareTo(other.NumerPokoju);
            }
            return wynik;
        }
    }
    public enum Standardy
    {
        podstawowy,
        komfort,
        komfortPlus
    }
}
