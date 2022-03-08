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
    public class ListaPokojow 
    {
        int liczbaPokojow;
        
        List<Pokoj> pokoje;

        public int LiczbaPokojow { get => liczbaPokojow; set => liczbaPokojow = value; }
        
        public List<Pokoj> Pokoje { get => pokoje; set => pokoje = value; }

        public ListaPokojow()
        {
            LiczbaPokojow = 0;
            
            pokoje = new List<Pokoj>();
        }

        public void DodajPokoj(Pokoj pokoj)
        {
            if (Pokoje.Contains(pokoj) == false)
            {
                LiczbaPokojow++;
                Pokoje.Add(pokoj);
                
            }
        }

        public void UsunPokoj(Pokoj pokoj)
        {
            if (Pokoje.Contains(pokoj) == true)
            {
                LiczbaPokojow--;
                Pokoje.Remove(pokoj);
            }
        }

        public void Sortuj()
        {
            Pokoje.Sort();
        }

        public override string ToString()
        {
            string wyjsce = "";
            foreach (var pokoj in Pokoje)
            {
                wyjsce += pokoj.ToString() + Environment.NewLine;
            }
            return wyjsce;
        }
        public static void ZapiszXML(string nazwa, ListaPokojow z)
        {
            XmlSerializer sr = new XmlSerializer(typeof(ListaPokojow));
            using (StreamWriter sw = new StreamWriter($"{nazwa}.xml"))
            {
                sr.Serialize(sw, z);
            }
        }
        public static ListaPokojow OdczytajXML(string nazwa)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListaPokojow));
            ListaPokojow pokoje;
            using (StreamReader reader = new StreamReader($"{nazwa}.xml"))
            {
                pokoje = (ListaPokojow)serializer.Deserialize(reader);
            }
            return pokoje;
        }

        
    }
}
