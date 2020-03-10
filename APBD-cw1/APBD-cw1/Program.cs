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
		private static HttpClient client = new HttpClient();
		public static async Task Main(string[] args)
		{
			try
			{
				//nazwy metod i zmiennych z duzej litery
				//prywatne pola dawac z podkreslnikiem
				//if (args.Length == 0) return;
				//nie zagniezdza nam sie kod

				string url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";

				await RecMailSearch(url);
			   
			   
				//Task <T>
				// ThreadPool() daje nam to dostep do puli watkow
				//

				

				var zbiory = new HashSet<string>();
				var list = new List<String>();
				var slownik = new Dictionary<string, int>();

				var znalezione = from e in list
								 where e.StartsWith("a")
								 select e;

			   //zadanie przechodzi rekurencyjnie po url na stronie i wypisywac z nich adresy email

				var st = new Student();
				st.Imie = "Jan";
			}
			catch (Exception exc) {
				//string.Format("wystapil blad: {0}", exc.toString());
				Console.WriteLine($"wystapil blad: {exc.ToString()}");
			}


			Console.WriteLine("Koniec");

		}

		public static async Task RecMailSearch(string url) {
			try
			{
				var result = await client.GetAsync(url);

				if (!result.IsSuccessStatusCode) return;

				string html = await result.Content.ReadAsStringAsync();

				var mailRegex = new Regex("[a-z]+[a-z0-9]*@[a-z.]+", RegexOptions.IgnoreCase);
				//regex do czytania maili

				var matchesMail = mailRegex.Matches(html);
				foreach (var m in matchesMail)
				{
					Console.WriteLine(m);
				}

				var urlRegex = new Regex("((([A-Za-z]{3,9}:(?:\\/\\/)?)(?:[-;:&=\\+\\$,\\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\\+\\$,\\w]+@)[A-Za-z0-9.-]+)((?:\\/[\\+~%\\/.\\w-_]*)?\\??(?:[-\\+=&;%@.\\w_]*)#?(?:[\\w]*))?)");

				var matchesUrl = urlRegex.Matches(html);

				foreach (var u in matchesUrl)
				{
					await RecMailSearch(u.ToString());
				}
			}
			catch (Exception exc) {
				Console.WriteLine("blad");
			}
		

		}
	}
}
