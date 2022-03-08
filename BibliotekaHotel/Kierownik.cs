using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaHotel
{
    [Serializable]
    public class Kierownik : Osoba
    {
        Stopnie stopien;
        string numerKonta;
        double zarobki;

        public Stopnie Stopien { get => stopien; set => stopien = value; }
        public string NumerKonta { get => numerKonta; set => numerKonta = value; }
        public double Zarobki { get => zarobki; set => zarobki = value; }

        public Kierownik() { }

        public Kierownik(string imie, string nazwisko, string pesel, string dataUrodzenia, Plcie plec, string numerTelefonu, string numerKonta, Stopnie stopien) : this()
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
            this.Stopien = stopien;
            this.Zarobki = Placa(stopien);

        }

        private double Placa(Stopnie stopien)
        {
            if (stopien == Stopnie.KierownikGłówny) { return 8000; }
            else return 6000;
        }

        public override string ToString()
        {
            return base.ToString() + $" {Stopien}";
        }
    }


    public enum Stopnie
    {
        KierownikZespołu,
        KierownikGłówny
    }
}
