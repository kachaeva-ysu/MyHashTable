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
            string[] words = GetWords();
            var ts = new Stopwatch();
            ts.Start();
            UseDictionary(words);
            ts.Stop();
            Console.WriteLine("Dictionary " + ts.ElapsedMilliseconds);
            ts.Reset();
            ts.Start();
            UseOpenAddressHashTable(words);
            ts.Stop();
            Console.WriteLine("Hash table " + ts.ElapsedMilliseconds);
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
            var htble = new OpenAddressHashTable<string, int>();
            List<string> itemsForRemoving = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                var slovo = words[i];
                if (htble.Contains(slovo))
                {
                    htble[slovo]++;
                    if (htble[slovo] == 27)
                        itemsForRemoving.Add(slovo);
                }
                else
                {
                    htble.Add(slovo, 1);
                }
            }
            foreach (var slovo in itemsForRemoving)
            {
                htble.Remove(slovo);
            }
        }

        private static void UseDictionary(string[] words)
        {
            Dictionary<string, int> slovar = new Dictionary<string, int>();
            List<string> itemsForRemoving = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                var slovo = words[i];
                if (slovar.ContainsKey(slovo))
                {
                    slovar[slovo]++;
                    if (slovar[slovo] == 27)
                        itemsForRemoving.Add(slovo);
                }
                else
                    slovar.Add(slovo, 1);
            }
            foreach (var slovo in itemsForRemoving)
            {
                slovar.Remove(slovo);
            }
        }
    }
}