using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaHotel
{
    [Serializable]
    public class Gosc : Osoba
    {
        public Gosc() { }
        public Gosc(string imie, string nazwisko, string pesel, string dataUrodzenia, Plcie plec, string numerTelefonu) : this()
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Pesel = pesel;
            DateTime date = new DateTime();
            if (DateTime.TryParseExact(dataUrodzenia, new[] { "dd.MM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out date))
                this.DataUrodzenia = date;
            this.Plec = plec;
            this.NumerTelefonu = numerTelefonu;

        }
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
