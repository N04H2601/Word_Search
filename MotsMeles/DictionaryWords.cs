using System;
using System.Collections.Generic;
using System.IO;

namespace MotsMeles
{
    public class DictionaryWords
    {
        Random rand = new Random();
        private string language;
        private Dictionary<int, string[]> words;

        public DictionaryWords(string language, string filepath)
        {
            this.language = language;
            words = new Dictionary<int, string[]>();
            try
            {
                using StreamReader sr = new StreamReader(filepath);
                string lineNumber;
                string lineWords;

                while ((lineNumber = sr.ReadLine()) != null && (lineWords = sr.ReadLine()) != null)
                {
                    int numberLetters = int.Parse(lineNumber);
                    string[] wordsList = lineWords.Split(' ');
                    words.Add(numberLetters, wordsList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        public override string ToString()
        {
            string s = $"Language: {language}\n";
            foreach (KeyValuePair<int, string[]> element in words)
            {
                s += $"Number of words containing {element.Key} letters: {element.Value.Length}\n";
            }
            return s;
        }

        public string GetRandomWord(int difficulty)
        {
            int randomIndex = rand.Next(2, 11 + difficulty);
            int c = 2;
            string randomWord = "";
            foreach (KeyValuePair<int, string[]> element in words)
            {
                int randomValue = rand.Next(element.Value.Length);
                if (c == randomIndex)
                {
                    randomWord = element.Value[randomValue];
                }
                c++;
            }
            return randomWord;
        }
        public bool DichotomicRecursiveSearch(string word, int min = 0, int max = -1)
        {
            int middle = (min + max) / 2;
            if (max == -1)
            {
                max = words[word.Length].Length - 1;
            }
            if (min > max)
            {
                return false;
            }
            else
            {
                if (words[word.Length][middle] == word)
                {
                    return true;
                }
                else
                {
                    if (word.CompareTo(words[word.Length][middle]) > 0)
                    {
                        return DichotomicRecursiveSearch(word, middle + 1, max);
                    }
                    else
                    {
                        return DichotomicRecursiveSearch(word, min, middle - 1);
                    }
                }
            }
        }
    }
}
