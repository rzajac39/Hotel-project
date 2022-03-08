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
    public class KadraHotelu
    {
        Kierownik kierownikGlowny;
        List<List<Pracownik>> kadraPracownikow;
        List<Kierownik> kadraKierownikow;

        public Kierownik KierownikGlowny { get => kierownikGlowny; set => kierownikGlowny = value; }
        public List<List<Pracownik>> KadraPracownikow { get => kadraPracownikow; set => kadraPracownikow = value; }
        public List<Kierownik> KadraKierownikow { get => kadraKierownikow; set => kadraKierownikow = value; }

        public KadraHotelu()
        {
            KierownikGlowny = null;
            KadraPracownikow = new List<List<Pracownik>>();
            KadraKierownikow = new List<Kierownik>();
        }
        public KadraHotelu(Kierownik kierownikGlowny) : this()
        {
            KierownikGlowny = kierownikGlowny;
        }
       
      
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(KierownikGlowny.ToString());
            sb.AppendLine("Kierownicy zmiany");
            foreach (var kierownik in KadraKierownikow)
            {
                sb.AppendLine(kierownik.ToString());
            }

            foreach (var pracownicy in KadraPracownikow)
            {
                foreach (var item in pracownicy)
                {
                    sb.AppendLine(item.ToString());
                }
            }

            return sb.ToString();
        }

        public static void ZapiszXML(string nazwa, KadraHotelu kh)
        {
            XmlSerializer sr = new XmlSerializer(typeof(KadraHotelu));
            using (StreamWriter sw = new StreamWriter($"{nazwa}.xml"))
            {
                sr.Serialize(sw, kh);
            }
        }
        public static KadraHotelu OdczytajXML(string nazwa)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(KadraHotelu));
            KadraHotelu kh;
            using (StreamReader reader = new StreamReader($"{nazwa}.xml"))
            {
                kh = (KadraHotelu)serializer.Deserialize(reader);
            }
            return kh;
        }
    }
}
