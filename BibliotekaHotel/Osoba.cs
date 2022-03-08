using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaHotel
{
    public abstract class Osoba
    {
        string imie;
        string nazwisko;
        string pesel;
        DateTime dataUrodzenia;
        Plcie plec;
        string numerTelefonu;

        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        public string Pesel { get => pesel; set => pesel = value; }
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }
        public string NumerTelefonu { get => numerTelefonu; set => numerTelefonu = value; }
        public Plcie Plec { get => plec; set => plec = value; }

        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ({Plec}), PESEL: {Pesel}, Data Urodzenia: {DataUrodzenia.ToString("dd-MM-yyyy")}, Nr Telefonu: {NumerTelefonu}";
        }
    }

    public enum Plcie
    {
        K,
        M
    }
}
