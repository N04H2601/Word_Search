using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MotsMeles
{
    public class Board
    {
        Random randomize = new Random();
        private string language;
        private int difficulty;
        private Label[] lgl;

        public Board(string language, int difficulty, Label[] labelsGeneralList)
        {
            this.language = language;
            this.difficulty = difficulty;
            this.lgl = labelsGeneralList;
        }

        /* public override string ToString()
        {
            return $"Difficulty: {difficulty}\nWords to find: {string.Join(", ", wordsToFind)}";
        } */

        public int Difficulty
        {
            get => difficulty;
        }

        public string Language
        {
            get => language;
        }

        public void ChangeGrid()
        {
            // Declaration of 8 Label[] of the first 4 rows and columns of the grid in order to be able to crop them according to the difficulty
            Label[] firstColumnArray = lgl[0..20];
            Label[] secondColumnArray = lgl[20..40];
            Label[] thirdColumnArray = lgl[40..60];
            Label[] fourthColumnArray = lgl[60..80];
            Label[] firstRowArray = new Label[] { lgl[0], lgl[20], lgl[40], lgl[60], lgl[80], lgl[100], lgl[120], lgl[140], lgl[160], lgl[180], lgl[200], lgl[220], lgl[240], lgl[260], lgl[280], lgl[300], lgl[320], lgl[340], lgl[360], lgl[380] };
            Label[] secondRowArray = new Label[] { lgl[1], lgl[21], lgl[41], lgl[61], lgl[81], lgl[101], lgl[121], lgl[141], lgl[161], lgl[181], lgl[201], lgl[221], lgl[241], lgl[261], lgl[281], lgl[301], lgl[321], lgl[341], lgl[361], lgl[381] };
            Label[] thirdRowArray = new Label[] { lgl[2], lgl[22], lgl[42], lgl[62], lgl[82], lgl[102], lgl[122], lgl[142], lgl[162], lgl[182], lgl[202], lgl[222], lgl[242], lgl[262], lgl[282], lgl[302], lgl[322], lgl[342], lgl[362], lgl[382] };
            Label[] fourthRowArray = new Label[] { lgl[3], lgl[23], lgl[43], lgl[63], lgl[83], lgl[103], lgl[123], lgl[143], lgl[163], lgl[183], lgl[203], lgl[223], lgl[243], lgl[263], lgl[283], lgl[303], lgl[323], lgl[343], lgl[363], lgl[383] };

            List<Label[]> generalListToCrop = new List<Label[]> { firstColumnArray, firstRowArray, secondColumnArray, secondRowArray, thirdColumnArray, thirdRowArray, fourthColumnArray, fourthRowArray };

            for (int i = 0; i < 20; i++)
            {
                foreach (Label[] rowOrColumnToCrop in generalListToCrop.SkipLast(difficulty * 2 - 2))
                {
                    rowOrColumnToCrop[i].Visibility = Visibility.Collapsed;
                    lgl = lgl.Except(rowOrColumnToCrop).ToArray();
                }
            }
        }
        public void PlaceRandomLetters()
        {
            string lettersList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (Label labelLetter in lgl)
            {
                labelLetter.Content = lettersList[randomize.Next(0, lettersList.Length)];
                labelLetter.Foreground = Brushes.Black;
            }
        }
    }
}
