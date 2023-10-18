using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace U3_13_Nuotraukos
{
    //KLASE SKIRTA NUOTRAUKOS DUOMENIMS SAUGOTI
    class Nuotrauka
    {
        private string pavadinimas;//nuotraukos pavadinimas
        private string tipas;//nuotraukos tipas
        private int aukstis;//nuotraukos aukstis
        private int plotis;//nuotraukos plotis

        public Nuotrauka(string pavadinimas, string tipas,
            int aukstis, int plotis)
        {
            this.pavadinimas = pavadinimas;
            this.tipas = tipas;
            this.aukstis = aukstis;
            this.plotis = plotis;
        }

        //grazina nuotraukos pavadinima
        public string ImtiPavadinima() { return pavadinimas; }

        //grazina nuotraukos tipa
        public string ImtiTipa() { return tipas; }

        //grazina nuotraukos auksti
        public int ImtiAuksti() { return aukstis; }

        //grazina nuotraukos ploti
        public int ImtiPloti() { return plotis; }
    }

    internal class Program
    {
        const int Cn = 100;
        const string PD = "Duomenys.txt";
        const string PD1 = "Duomenys2.txt";
        const string RZ = "Rezultatai.txt";

        static void Main(string[] args)
        {
            //Pirmos kolekcijos nuotrauku duomenys
            Nuotrauka[] N = new Nuotrauka[Cn];
            int kiek;
            string vardas;
            int k;
            if(File.Exists(RZ))
                File.Delete(RZ);
            Skaityti(N, PD, out kiek, out vardas);
            Spausdinti(N, RZ, kiek, vardas);

            //pirmos kolekcijos didziausia nuotrauka
            int didziausiaNuotrauka = DidziausiaNuotrauka(N, kiek);
            
            //ivedamas norimas nuotraukos tipas
            string tipas;
            Console.WriteLine("Iveskite norima nuotraukos tipa" +
                "(spalvota, nespalvota ar stereo): ");
            tipas = Console.ReadLine();

            //pirmos kolekcijos nuordyto tipo kiekis
            int kiekTipo = KiekNurodytoTipo(N, kiek, tipas);

            using (var fr = File.AppendText(RZ))
            {
                //randama pirmos kolekcijos didziausia nuotrauka
                fr.WriteLine("Didziausia pirmos kolekcijos nuotrauka:");
                fr.WriteLine("Pavadinimas: {0}.",
                    N[didziausiaNuotrauka].ImtiPavadinima());
                fr.WriteLine("Tipas: {0}.", 
                    N[didziausiaNuotrauka].ImtiTipa());
                fr.WriteLine("");

                //randamas ivestos nuotraukos tipo kiekis
                //pirmoje kolekcijoje
                if (kiekTipo > 0)
                    fr.WriteLine("'{0}' tipo nuotrauku kiekis yra:" +
                        " {1,2:d} nuotrauka/os.", tipas, kiekTipo);
                else
                    fr.WriteLine("'{0}' tipo nuotraukos nera sarase.",
                        tipas);
                fr.WriteLine("");
            }

            //Antros kolekcijos nuotrauku duomenys
            Nuotrauka[] N1 = new Nuotrauka[Cn];
            int kiek1;
            string vardas1;
            int k1;
            Skaityti(N1, PD1, out kiek1, out vardas1);
            Spausdinti(N1, RZ, kiek1, vardas1);

            //antros kolekcijos didziausia nuotrauka
            int didziausiaNuotrauka1 = 
                DidziausiaNuotrauka(N1, kiek1);

            //antros kolekcijos nurodyto tipo nuotraukos
            int kiekTipo1 = 
                KiekNurodytoTipo(N1, kiek1, tipas);

            //nuotraukos dydis abejoms kolekcijoms
            int dydis = 0;
            int maxNuotrauka = 
                NuotraukosDydis(N, kiek, dydis);
            int maxNuotrauka1 = 
                NuotraukosDydis(N1, kiek1, dydis);

            using (var fr = File.AppendText(RZ))
            {
                //randama antros kolekcijos didziausia nuotrauka
                fr.WriteLine("Didziausia antros kolekcijos nuotrauka:");
                fr.WriteLine("Pavadinimas: {0}.", 
                    N1[didziausiaNuotrauka1].ImtiPavadinima());
                fr.WriteLine("Tipas: {0}.", 
                    N1[didziausiaNuotrauka1].ImtiTipa());
                fr.WriteLine("");

                //randama, kurioje kolekcijoje nuotrauka yra didesne
                if (maxNuotrauka > maxNuotrauka1)
                    fr.WriteLine("Didziausia nuotrauka " +
                        "yra tik pirmoje kolekcijoje");
                else
                    if (maxNuotrauka < maxNuotrauka1)
                    fr.WriteLine("Didziausia nuotrauka " +
                        "yra tik antroje kolekcijoje");
                else
                    if (maxNuotrauka == maxNuotrauka1)
                    fr.WriteLine("Didziausios nuotraukos " +
                        "yra abejose kolekcijose");
                fr.WriteLine("");

                //randamas ivestos nuotraukos tipo kiekis
                //antroje kolekcijoje
                if (KiekNurodytoTipo(N1, kiek1, tipas) > 0)
                    fr.WriteLine("'{0}' tipo nuotrauku kiekis yra: {1,2:d} " +
                        "nuotrauka/os.", tipas, kiekTipo1);
                else
                    fr.WriteLine("'{0}' tipo nuotraukos nera sarase.", tipas);

                //randa, kurioje kolekcijoje yra daugiau nurodyto tipo nuotrauku
                if (kiekTipo > kiekTipo1)
                    fr.WriteLine("'{0}' tipo nuotrauku yra daugiau pirmoje" +
                        " kolekcijoje: {1,2:d} nuotrauka/os.", tipas, kiekTipo);
                else
                    if (kiekTipo < kiekTipo1)
                    fr.WriteLine("'{0}' tipo nuotrauku yra daugiau antroje" +
                        " kolekcijoje: {1,2:d} nuotrauka/os.", tipas, kiekTipo1);
                else
                    if (kiekTipo == kiekTipo1)
                    fr.WriteLine("'{0}' tipo nuotrauku kiekis yra toks pats" +
                        " abejose kolekcijose.", tipas);
                else
                    fr.WriteLine("'{0}' tipo nuotrauka neegzistuoja sarasuose.");
                fr.WriteLine("");
            }

            //Nespalvotu nuotrauku sarasas
            Nuotrauka[] Nespalvotos = new Nuotrauka[Cn];
            int kiekNespalvotu = 0;
            FormuotiNespalvotas(N, kiek, Nespalvotos, ref kiekNespalvotu);
            FormuotiNespalvotas(N1, kiek1, Nespalvotos, ref kiekNespalvotu);

            Spausdinti(Nespalvotos, RZ, kiekNespalvotu, "Naujas sarasas");
        }

        /// <summary>
        /// Metodas skaito duomenis is failo(be nurodyto kiekio)
        /// </summary>
        /// <param name="N">nuotrauku objektas</param>
        /// <param name="fv">failo vardas</param>
        /// <param name="kiek">nuotrauku kiekis</param>
        /// <param name="vardas">savininko vardas</param>
        static void Skaityti(Nuotrauka[] N, string fv, 
            out int kiek, out string vardas)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                int i = 0;
                line = reader.ReadLine();
                vardas = line;
                while((line = reader.ReadLine()) != null && (i < Cn))
                {
                    string[] parts = line.Split(';');
                    string pavadinimas = parts[0];
                    string tipas = parts[1];
                    int aukstis = int.Parse(parts[2]);
                    int plotis = int.Parse(parts[3]);
                    N[i++] = new Nuotrauka(pavadinimas, tipas, aukstis
                        , plotis);
                }
                kiek = i;
            }
        }

        /// <summary>
        /// Metodas spausdina duomenis i faila
        /// </summary>
        /// <param name="N">nuotrauku objektas</param>
        /// <param name="fv">failo vardas</param>
        /// <param name="kiek">nuotrauku kiekis</param>
        /// <param name="vardas">savininko vardas</param>
        static void Spausdinti(Nuotrauka[] N, string fv, int kiek, string vardas)
        {
            const string top =
       "|-----------------|----------------|-----------------|------------|\r\n"
      +"| Pavadinimas     | Tipas          | Aukstis         | Plotis     | \r\n"
      +"|-----------------|----------------|-----------------|------------|";

            using (var fr = File.AppendText(fv))
            {
                Nuotrauka tarp;
                fr.WriteLine("{0}", vardas);
                fr.WriteLine(top);
                for (int i = 0; i < kiek; i++)
                {
                    tarp = N[i];
                    fr.WriteLine("|{0,-15}  |{1,-15} |{2,17:f2}|{3,12:f2}|",
                        tarp.ImtiPavadinima(), tarp.ImtiTipa(), tarp.ImtiAuksti(),
                        tarp.ImtiPloti());
                }
                fr.WriteLine("|----------------------------" +
                    "-------------------------------------|");
                fr.WriteLine("");
            }
        }
        
        /// <summary>
        /// Suranda, kuri nuotrauka yra didziausia(indeksu)
        /// </summary>
        /// <param name="N">nuotrauku objektas</param>
        /// <param name="kiek">nuotrauku kiekis</param>
        /// <returns></returns>
        static int DidziausiaNuotrauka(Nuotrauka[] N, int kiek)
        {
            int k = 0;
            for (int i = 0; i < kiek; i++)
            {
                if ((N[i].ImtiAuksti() * N[i].ImtiPloti()) > 
                    (N[k].ImtiAuksti() * N[k].ImtiPloti()))
                {
                    k = i;
                }

            }
            return k;
        }

        /// <summary>
        /// Suskaiciuojamas nuotraukos dydis
        /// </summary>
        /// <param name="N">nuotrauku objektas</param>
        /// <param name="kiek">nuotrauku kiekis</param>
        /// <returns></returns>
        static int NuotraukosDydis(Nuotrauka[] N, int kiek, int dydis)
        {
            dydis = 0;
            int k = 0;
            for (int i = 0; i < kiek; i++)
            {
                if ((N[i].ImtiAuksti() * N[i].ImtiPloti()) >
                    (N[k].ImtiAuksti() * N[k].ImtiPloti()))
                    dydis = N[i].ImtiAuksti() * N[i].ImtiPloti();
            }
            return dydis;
        }

        /// <summary>
        /// Kiek yra nurodyto tipo nuotrauku
        /// </summary>
        /// <param name="N">nuotrauku objektas</param>
        /// <param name="kiek">nuotrauku kiekis</param>
        /// <returns></returns>
        static int KiekNurodytoTipo(Nuotrauka[] N, int kiek,
            string tipas)
        {
            int k = 0;
            for (int i = 0; i < kiek; i++)
                if (N[i].ImtiTipa().Trim() == 
                    tipas.ToLower().Trim())
                    k++;
            return k;
        }

        /// <summary>
        /// Formuoja nauja nespalvotu nuotruku sarasa
        /// </summary>
        /// <param name="N">nuotrauku objektas</param>
        /// <param name="kiek">kiek nuotrauka</param>
        /// <param name="Nespalvota">nespalvotu nuotrauku objektas</param>
        /// <param name="kiekNespalvotu">nespalvotos nuotraukos</param>
        static void FormuotiNespalvotas(Nuotrauka[] N, int kiek, 
            Nuotrauka[] Nespalvota, ref int kiekNespalvotu)
        {
            string nespalvota = "nespalvotos";
            for(int i = 0; i < kiek; i++)
            {
                if (N[i].ImtiTipa().ToLower().Trim() == 
                    nespalvota.ToLower().Trim())
                {
                    Nespalvota[kiekNespalvotu] = N[i];
                    kiekNespalvotu++;
                }    
            }
        }
    }
}
