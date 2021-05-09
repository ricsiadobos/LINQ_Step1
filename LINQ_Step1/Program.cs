using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQ_Step1
{
    

        class Program
        {
            public class Data
            {
                public string Name;
                public int Age;
                public string Sex;
                public double Height;
                public DateTime Date;
                public int Money;
                public int Upmoney;
                public int Person = 1;

                #region Constructor

                public Data()
                {
                }

                public Data(string name, int age, string sex, double height, DateTime date, int money)
                {
                    Name = name;
                    Age = age;
                    Sex = sex;
                    Height = height;
                    Date = date;
                    Money = money;
                }

                #endregion



            }

            //Distinct válogtáshoz kell
            #region MyRegion Distinct

            // Distinct metódushoz lista esetén
            class NameComparer : IEqualityComparer<Data>
            {
                public bool Equals(Data x, Data y)
                {
                    return x.Name.Equals(y.Name);
                }

                public int GetHashCode(Data obj)
                {
                    return obj.Name.GetHashCode();
                }

            }

            #endregion


            static void Main(string[] args)
            {
                List<Data> list = new List<Data>();


                #region Beolvasás

                FileStream fileStream = new FileStream(@"adat.csv", FileMode.Open);
                StreamReader r = new StreamReader(fileStream);


                string elsoSor = r.ReadLine();
                string sor = "";
                string[] db;
                while (!r.EndOfStream)
                {
                    sor = r.ReadLine();
                    db = sor.Split(';');


                    Data adat = new Data(db[0], Convert.ToInt32(db[1]), db[2],
                    Convert.ToDouble(db[3]), Convert.ToDateTime(db[4]), 
                    Convert.ToInt32(db[5]));
                    list.Add(adat);
                }
                #endregion

                #region Var and Obj.

                #region List Even - Odd

                List<Data> szül = list.Where(x => x.Age % 2 == 0).ToList();

                foreach (Data item in szül)
                {
                    Console.Write(item.Name + " kor: ");
                    Console.WriteLine(item.Age);
                }

                List<int> onlyOddAgeList = new List<int>();
                List<Data> ageOddList = list.Where(x => x.Age % 2 == 1).ToList();
                foreach (Data item in ageOddList)
                {
                    onlyOddAgeList.Add(item.Age);
                }
                #endregion

                #region Min - Max

                Console.WriteLine();
                Console.Write("Minimum érték: ");
                int min = list.Min(x => x.Age);
                Console.WriteLine(min);

                Console.WriteLine();
                Console.Write("Minimum érték: ");
                Console.WriteLine(min);
                var minList = list.First(x => x.Age == list.Min(y => y.Age));


                Console.Write("Minimum érték: ");
                int max = list.Max(x => x.Age);
                Console.WriteLine(max);

                #endregion

                #region Min obj (implementált ComaterTo-val)

                /*
                Data MinObj = list.Min();

                #endregion

                #region Max obj
                Data MaxObj = list.Max();
                Console.ReadLine();

                */
                #endregion

                #region Max-Min obj.

                Data MaxObj = list.First(x => x.Money == list.Max(y => y.Money));
                Data MinObj = list.First(x => x.Money == list.Min(y => y.Money));
                Data MinObjHeight = list.FirstOrDefault(x => x.Height == list.Max(y => y.Height));

                #endregion

                #region Money Sum

                int moneySum = list.Sum(x => x.Money);
                Console.WriteLine("");

                //Console.WriteLine("Összes pénz: " + moneySum);
                #endregion

                #region Money Avg

                double moneyAvg = Convert.ToDouble(list.Average(x => x.Money));
                Console.WriteLine("");

                Console.WriteLine("Átlag pénz: " + Math.Round(moneyAvg, 2));
                #endregion

                #region First

                int elsoIdosebb40nél = onlyOddAgeList.FirstOrDefault(x => x > 70);
                if (elsoIdosebb40nél == 0)
                {
                    Console.WriteLine("\nElső 40 évnél idősebb: Nincs ilyen találat!");
                }
                else
                {
                    Console.WriteLine("\n Első 40 évnél idősebb: " + elsoIdosebb40nél);
                }
                #endregion

                #region Contains

                var contains = from szemely in list where szemely.Name.Contains("Peti") select szemely;
                var contains2 = list.Where(x => x.Name.Contains("Peti"));
                Console.WriteLine();
                foreach (var item in contains)
                {
                    Console.Write("Listában lévő Peti: " + item.Name);
                    Console.Write("\t" + item.Age);
                    Console.Write("\t" + item.Height);
                    Console.Write("\t" + item.Date.ToShortDateString());

                }


                #endregion

                #region Between

                var contains30to50 = list.Where(x => x.Age > 30 && x.Age < 50).ToList();

                foreach (var item in contains30to50)
                {
                    Console.Write("30 és 50 év közöttiek: " + item.Name);
                    Console.Write("\t" + item.Age);
                    Console.Write("\t" + item.Height);
                    Console.Write("\t" + item.Date.ToShortDateString());
                    Console.WriteLine();
                }

                #endregion

                #region Contains-Like

                List<Data> LikeList = list.Where(x => x.Name.Contains("a")).ToList();
                Console.WriteLine();
                foreach (var item in LikeList)
                {
                    Console.Write("Listában lévő Xamarol: " + item.Name);
                    Console.Write("\t" + item.Age);
                    Console.Write("\t" + item.Height);
                    Console.Write("\t" + item.Date.ToShortDateString());

                }


                #endregion

                #region Equals 

                List<Data> equalsList = list.Where(x => x.Name.Equals("Tomi")).ToList();
                List<string> Csaka1985List = list.Where(x => x.Date.Year.Equals(1985)).Select(y => y.Name).ToList();
                Console.WriteLine();

                foreach (var item in LikeList)
                {
                    Console.Write("Listában lévő Tomi: " + item.Name);
                    Console.Write("\t" + item.Age);
                    Console.Write("\t" + item.Height);
                    Console.Write("\t" + item.Date.ToShortDateString());

                }


                #endregion

                #region MoneyList

                List<int> moneyList = list.Select(x => x.Money).ToList();
            foreach (var item in moneyList)
            {
                Console.WriteLine(item);
            }

                #endregion

                #region Distinct (kölöndöző elemek) - CompratTo kell hozzá

                List<string> DistinctStringList = list.Distinct().Select(x => x.Name).ToList();
                List<Data> DistinctList = list.Distinct(new NameComparer()).ToList();


                /*  main-en kívül kell egy ilyen osztály az elemek összehasonlításához
                class NameComparer : IEqualityComparer<Data>
            {
                public bool Equals(Data x, Data y)
                {
                    return x.Name.Equals(y.Name);
                }

                public int GetHashCode(Data obj)
                {
                    return obj.Name.GetHashCode();
                }

            }
                */


                #endregion


                #endregion

                #region List


                #region OrderBY

                Console.WriteLine("\nKor szerint redezett lista:");
                List<Data> OrderList = list.OrderBy(x => x.Money).ToList();

                #endregion

                #region Contains

                List<Data> ContainsA = list.Where(x => x.Name.Contains("a")).ToList();

                #endregion

                #region List SeachMoreParameter
                List<Data> OrderMindenList = list.Where(x => x.Date.Year % 2 == 0 & x.Age >= 20 & x.Money >= 50).OrderByDescending(x => x.Age).ToList();

                #endregion

                #region StringParameterList+OrderBy

                List<string> nevekList = list.OrderBy(x => x.Name).Select(x => x.Name).ToList();

                #endregion

                #region FizuEmelés
                List<Data> UpFizulist = new List<Data>(list);
                // előtte le kell másolni a listát és a köv példányt módosítani
                UpFizulist.ForEach(y => { y.Upmoney = (int)Math.Round(y.Money * 1.2, 0); });

                #endregion

                #region Dictionary (kulcs-érték) 

                Dictionary<string, int> nemekSzamolasaList = list.GroupBy(nemek => nemek.Sex).Select(nemek_nemekSzamolasaLista => new
                {
                    nemek_nemekSzamolasaLista.Key,
                    Value = nemek_nemekSzamolasaLista.Sum(nemek => nemek.Person)
                }).ToDictionary(t => t.Key, t => t.Value);
                //value-nak int változót lehet megadni.  key -> miből   value -> mennyi
                Dictionary<string, int> nemekPenzeList = list.GroupBy(nemek => nemek.Sex).Select(nemek_nemekSzamolasaLista => new
                {
                    nemek_nemekSzamolasaLista.Key,
                    Value = nemek_nemekSzamolasaLista.Sum(nemek => nemek.Money)
                }).ToDictionary(t => t.Key, t => t.Value);


                foreach (var item in nemekSzamolasaList)
                {
                    Console.WriteLine();
                    Console.Write("Nem: \t" + item.Key);
                    Console.WriteLine();
                    Console.Write("Fő: \t" + item.Value);
                }
                #endregion
                #endregion

                Console.ReadLine();


            }


        }
    

}
