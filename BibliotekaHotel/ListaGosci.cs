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
    public class SpisGosci
    {
        int liczbaGosci;
        List<Gosc> listaGosci;

        public int LiczbaGosci { get => liczbaGosci; set => liczbaGosci = value; }
        public List<Gosc> ListaGosci { get => listaGosci; set => listaGosci = value; }

        public SpisGosci()
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
        public void UsunGosica(Gosc gosc)
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
        public static void ZapiszXML(string nazwa, ListaGosci g)
        {
            XmlSerializer sr = new XmlSerializer(typeof(SpisGosci));
            using (StreamWriter sw = new StreamWriter($"{nazwa}.xml"))
            {
                sr.Serialize(sw, g);
            }
        }
        public static ListaGosci OdczytajXML(string nazwa)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SpisGosci));
            ListaGosci goscie;
            using (StreamReader reader = new StreamReader($"{nazwa}.xml"))
            {
                goscie = (ListaGosci)serializer.Deserialize(reader);
            }
            return goscie;
        }
    }
}
