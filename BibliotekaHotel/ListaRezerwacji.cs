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
    public class ListaRezerwacji
    {
        List<Rezerwacja> rezerwacje;

        public List<Rezerwacja> Rezerwacje { get => rezerwacje; set => rezerwacje = value; }

        public ListaRezerwacji()
        {
            Rezerwacje = new List<Rezerwacja>();
        }
        public void DodajRezerwacje(Rezerwacja r)
        {
            if (Rezerwacje.Contains(r) == false)
            {
                Rezerwacje.Add(r);
            }
        }
            
        public void UsunRezerwacje(Rezerwacja r)
        {
            if (Rezerwacje.Contains(r) == true)
            {
                Rezerwacje.Remove(r);
            }
        }

        public override string ToString()
        {
            string wyjsce = "";
            foreach (var rezerwacja in Rezerwacje)
            {
                wyjsce += rezerwacja.ToString() + Environment.NewLine;
            }
            return wyjsce;
        }
        public static void ZapiszXML(string nazwa, ListaRezerwacji z)
        {
            XmlSerializer sr = new XmlSerializer(typeof(ListaRezerwacji));
            using (StreamWriter sw = new StreamWriter($"{nazwa}.xml"))
            {
                sr.Serialize(sw, z);
            }
        }
        public static ListaRezerwacji OdczytajXML(string nazwa)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListaRezerwacji));
            ListaRezerwacji rezerwacje;
            using (StreamReader reader = new StreamReader($"{nazwa}.xml"))
            {
                rezerwacje = (ListaRezerwacji)serializer.Deserialize(reader);
            }
            return rezerwacje;
        }
    }
}
