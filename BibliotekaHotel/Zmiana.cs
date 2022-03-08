using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BibliotekaHotel
{
    [Serializable]
    public class Zmiana
    {
        Kierownik kierownikZmiany;
        List<Pracownik> listaPracownikow;
        int liczbaPracownikowZmiany;
        Zmiany nazwaZmiany;
        

        public Kierownik KierownikZmiany { get => kierownikZmiany; set => kierownikZmiany = value; }
        public List<Pracownik> ListaPracownikow { get => listaPracownikow; set => listaPracownikow = value; }
        public int LiczbaPracownikowZmiany { get => liczbaPracownikowZmiany; set => liczbaPracownikowZmiany = value; }
        public Zmiany NazwaZmiany { get => nazwaZmiany; set => nazwaZmiany = value; }

        public Zmiana()
        {
            liczbaPracownikowZmiany = 0;
            KierownikZmiany = null;
            ListaPracownikow = new List<Pracownik>();
        }
        public Zmiana(Zmiany nazwaZmiany, Kierownik kierownikZmiany) : this()
        {
            NazwaZmiany = nazwaZmiany;
            KierownikZmiany = kierownikZmiany;
        }
        public void DodajPracownika(Pracownik pracownik)
        {
            if (ListaPracownikow.Contains(pracownik) == false)
            {
                LiczbaPracownikowZmiany++;
                ListaPracownikow.Add(pracownik);
            }
        }
        public void UsunPracownika(Pracownik pracownik)
        {
            if (ListaPracownikow.Contains(pracownik) == true)
            {
                LiczbaPracownikowZmiany--;
                ListaPracownikow.Remove(pracownik);
            }
        }
        public void Sortuj()
        {
            ListaPracownikow.Sort();
        }

        public override string ToString()
        {
            string wyjsce = "";
            wyjsce += NazwaZmiany.ToString() + Environment.NewLine;
            wyjsce += KierownikZmiany.ToString() + Environment.NewLine;
            foreach (var pracownik in ListaPracownikow)
            {
                wyjsce += pracownik.ToString() + Environment.NewLine;
            }
            return wyjsce;
        }
        public static void ZapiszXML(string nazwa, Zmiana z)
        {
            XmlSerializer sr = new XmlSerializer(typeof(Zmiana));
            using (StreamWriter sw = new StreamWriter($"{nazwa}.xml"))
            {
                sr.Serialize(sw, z);
            }
        }
        public static Zmiana OdczytajXML(string nazwa)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Zmiana));
            Zmiana z;
            using (StreamReader reader = new StreamReader($"{nazwa}.xml"))
            {
                z = (Zmiana)serializer.Deserialize(reader);
            }
            return z;
        }
    }
    public enum Zmiany
    {
        Poranna,
        Popołudniowa,
        Nocna
    }
}
