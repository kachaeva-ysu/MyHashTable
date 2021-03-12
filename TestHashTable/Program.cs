using System;
using System.Collections.Generic;
using System.Text;
using HashTableForStudents;
using System.IO;
using System.Diagnostics;

namespace TestHashTable
{
    internal class Program
    {
        private static void Main()
        {
            //string[] words = GetWords();
            //var ts = new Stopwatch();
            //ts.Start();
            //UseDictionary(words);
            //ts.Stop();
            //Console.WriteLine("Dictionary " + ts.ElapsedMilliseconds);
            //ts.Reset();
            //ts.Start();
            //UseOpenAddressHashTable(words);
            //ts.Stop();
            //Console.WriteLine("Hash table " + ts.ElapsedMilliseconds);
            var dict = new Dictionary<int, int>();
            var table = new OpenAddressHashTable<int, int>();

            var s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 10000; i++)
                dict.Add(i, 1);
            s.Stop();
            Console.WriteLine(s.Elapsed);
            s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 10000; i++)
                table.Add(i, 1);
            s.Stop();
            Console.WriteLine(s.Elapsed);

            s = new Stopwatch();
            s.Start();
            for (int i = 5000; i < 15000; i++)
                dict.ContainsKey(i);
            s.Stop();
            Console.WriteLine(s.Elapsed);
            s = new Stopwatch();
            s.Start();
            for (int i = 5000; i < 15000; i++)
                table.Contains(i);
            s.Stop();
            Console.WriteLine(s.Elapsed);


            Console.ReadLine();
        }

        public static string[] GetWords()
        {
            string[] words;
            char[] delimitedchars =
            {
                    ',', ':', ' ', '.', '!', ';', '<', '?', '>', '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '/',
                    '"', '*', '(', ')', '\'','\n','\r','\\'
                };
            using (StreamReader f = new StreamReader("anna.txt", Encoding.Default))
            {
                words = f.ReadToEnd().ToLower().Split(delimitedchars, StringSplitOptions.RemoveEmptyEntries);
            }
            return words;
        }

        private static void UseOpenAddressHashTable(string[] words)
        {
            var htble = new OpenAddressHashTable<string, int>(340007);

            for (int i = 0; i < words.Length; i++)
            {
                var slovo = words[i];
                if (htble.Contains(slovo))
                {
                    htble[slovo]++;
                }
                else
                {
                    htble.Add(slovo, 1);
                }
            }
        }

        private static void UseDictionary(string[] words)
        {
            Dictionary<string, int> slovar = new Dictionary<string, int>();

            for (int i = 0; i < words.Length; i++)
            {
                var slovo = words[i];
                if (slovar.ContainsKey(slovo))
                {
                    slovar[slovo]++;
                }
                else
                    slovar.Add(slovo, 1);
            }

            //var table = new OpenAddressHashTable<int, string>();
            //table.Add(10, "sdf");
            //table.Add(22, "sdf");
            //table.Add(31, "wf");
            //table.Add(4, "wf");
            //table.Add(15, "sdf");
            //table.Add(28, "sdf");
            //table.Add(17, "wf");
            //table.Add(88, "wf");
            //table.Add(59, "wf");
            //table.Remove(4);
            //Console.WriteLine(table.ToString());
        }
    }
}