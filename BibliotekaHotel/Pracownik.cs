using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaHotel
{
    [Serializable]
    public class Pracownik : Osoba
    {
        string numerKonta;

        Funkcje funkcja;
        double zarobki;

        public string NumerKonta { get => numerKonta; set => numerKonta = value; }

        public Funkcje Funkcja { get => funkcja; set => funkcja = value; }
        public double Zarobki { get => zarobki; set => zarobki = value; }

        public Pracownik() { }

        public Pracownik(string imie, string nazwisko, string pesel, string dataUrodzenia, Plcie plec, string numerTelefonu, string numerKonta, Funkcje funkcja) : this()
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Pesel = pesel;
            DateTime date = new DateTime();
            if (DateTime.TryParseExact(dataUrodzenia, new[] { "dd.MM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out date))
                this.DataUrodzenia = date;
            this.Plec = plec;
            this.NumerTelefonu = numerTelefonu;
            this.NumerKonta = numerKonta;

            this.Funkcja = funkcja;
            this.Zarobki = Placa(Funkcja);
        }

        /// <summary>
        /// Funkcja obliczająca zarobki na podstawie funkcji i zmiany
        /// </summary>
        /// <param name="funkcja">Pełniona funkcja</param>

        /// <returns></returns>
        private double Placa(Funkcje funkcja)
        {
            double placa;
            switch (funkcja)
            {
                case Funkcje.Bagażowy:
                    placa = 3000;
                    break;
                case Funkcje.Kelner:
                    placa = 3200;
                    break;
                case Funkcje.KonserwatorPowierzchniPłaskich:
                    placa = 3500;
                    break;
                case Funkcje.Kucharz:
                    placa = 5000;
                    break;
                case Funkcje.Ochroniarz:
                    placa = 2900;
                    break;
                case Funkcje.PomocKuchenna:
                    placa = 3300;
                    break;
                case Funkcje.Portier:
                    placa = 3100;
                    break;
                case Funkcje.ZłotaRączka:
                    placa = 4500;
                    break;
                default:
                    placa = 0;
                    break;
            }
            return placa;
        }
        public override string ToString()
        {
            return base.ToString() + " " + Funkcja;
        }

    }

    public enum Funkcje
    {
        Portier,
        KonserwatorPowierzchniPłaskich,
        Bagażowy,
        Kucharz,
        Kelner,
        PomocKuchenna,
        Ochroniarz,
        ZłotaRączka
    }
}
