using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotsMeles
{
    public class Player
    {
        private string name;
        private List<string> wordsFound = new List<string>() { };
        private int score = 0;

        public Player (string name)
        {
            this.name = name;
        }

        public List<string> WordsFound
        {
            get => wordsFound;
        }

        public string Name
        {
            get => name;
        }

        public int Score
        {
            get => score;
        }

        public void AddWord(string word)
        {
            wordsFound.Add(word);
        }

        public override string ToString()
        {
            return $"Name: {name}\nWords found: {wordsFound.Count}\nScore: {score}\n";
        }

        public void AddScore(int points)
        {
            score += points;
        }
    }
}
