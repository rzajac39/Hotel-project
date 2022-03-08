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
    public class Goscie
    {
        int liczbaGosci;
        List<Gosc> listaGosci;

        public int LiczbaGosci { get => liczbaGosci; set => liczbaGosci = value; }
        public List<Gosc> ListaGosci { get => listaGosci; set => listaGosci = value; }

        public Goscie()
        {
            LiczbaGosci = 0;
            ListaGosci = new List<Gosc>();
        }

        public void DodajGoscia(Gosc gosc)
        {
            if (ListaGosci.Contains(gosc) == false)
            {
                LiczbaGosci++;
                ListaGosci.Add(gosc);
            }
        }
        public void UsunGoscia(Gosc gosc)
        {
            if (ListaGosci.Contains(gosc) == false)
            {
                LiczbaGosci--;
                ListaGosci.Remove(gosc);
            }
        }

        public override string ToString()
        {
            string wyjsce = "";
            foreach (var gosc in ListaGosci)
            {
                wyjsce += gosc.ToString() + Environment.NewLine;
            }
            return wyjsce;
        }
        public static void ZapiszXML(string nazwa, Goscie g)
        {
            XmlSerializer sr = new XmlSerializer(typeof(Goscie));
            using (StreamWriter sw = new StreamWriter($"{nazwa}.xml"))
            {
                sr.Serialize(sw, g);
            }
        }
        public static Goscie OdczytajXML(string nazwa)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Goscie));
            Goscie goscie;
            using (StreamReader reader = new StreamReader($"{nazwa}.xml"))
            {
                goscie = (Goscie)serializer.Deserialize(reader);
            }
            return goscie;
        }
    }
}
