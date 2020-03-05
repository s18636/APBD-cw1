using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace cw1
{
    public class Student
    {
        public string Imie { get; set; }
        private string _nazwisko;
        //prywatne pola dawac z podkreslnikiem
        public string Nazwisko
        {
            get { return _nazwisko; }
            set
            {
                if (value == null) throw new ArgumentException();
                _nazwisko = value;
            }
        }
    }
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                //nazwy metod i zmiennych z duzej litery
                //prywatne pola dawac z podkreslnikiem
                //if (args.Length == 0) return;
                //nie zagniezdza nam sie kod

                string url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";

                var client = new HttpClient();
                var result = await client.GetAsync(url);
                //Task <T>
                // ThreadPool() daje nam to dostep do puli watkow
                //

                if (!result.IsSuccessStatusCode) return;

                var zbiory = new HashSet<string>();
                var list = new List<String>();
                var slownik = new Dictionary<string, int>();

                var znalezione = from e in list
                                 where e.StartsWith("a")
                                 select e;

               //zadanie przechodzi rekurencyjnie po url na stronie i wypisywac z nich adresy email


                string html = await result.Content.ReadAsStringAsync();

                var regex = new Regex("[a-z]+[a-z0-9]*@[a-z.]+", RegexOptions.IgnoreCase);
                //regex do czytania maili

                var matches = regex.Matches(html);
                foreach (var m in matches)
                {
                    Console.WriteLine(m);
                }

                var st = new Student();
                st.Imie = "Jan";
            }
            catch (Exception exc) {
                //string.Format("wystapil blad: {0}", exc.toString());
                Console.WriteLine($"wystapil blad: {exc.ToString()}");
            }


            Console.WriteLine("Koniec");

        }
    }
}
