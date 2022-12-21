using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MotsMeles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random randomize = new Random();  // Declaration of an instance of the Random class in order to generate random values

        bool blank = false;  // To restrict the clicking of a cell
        bool isGenerated = false;  // To determine whether the grid was generated
        bool isPlaced = false;  // To determine whether a word can be placed in a location

        string wordForming = "";  // To retrieve the letters of the word being formed by the user
        string player1Name;  // To save the first player's name
        string player2Name;  // To save the second player's name

        int score;  // To save a player's score
        int counterForDirection = 0;  // To restrict the clicking of a cell to imposed directions
        int counterForRandomWords = 0;  // To manage the placement of the words to find in the left grid
        int counterForRounds = 0;  // To save the number of rounds

        DictionaryWords dictionaryWords;  // To manage French and English wordlists in dictionary form

        List<Label> labelsClicked = new List<Label>();  // To restrict the clicking of a cell to imposed directions
        List<string> randomWords = new List<string>();  // Contains the random words generated
        List<string> wordsFound = new List<string>();  // Contains the words found by the user
        List<string> names = new List<string>();  // Contains the players names
        List<int> scores = new List<int>();  // Contains the players scores

        Label[] wordsLabel;  // Allows to manage the list of words to find

        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// The GenerateClicked method is the main method of this program, because it allows to generate the grid and place the words in it randomly.
        /// </summary>
        public void GenerateClicked(object sender, RoutedEventArgs e)
        {
            score = 0;
            player1Name = Player1Name.Text;
            player2Name = Player2Name.Text;
            names.Add(player1Name);
            names.Add(player2Name);

            // The following line contains all the labels in the grid to allow the grid to be trimmed and then iterate over all the labels
            Label[] labelsGeneralList = { G000, G001, G002, G003, G004, G005, G006, G007, G008, G009, G010, G011, G012, G013, G014, G015, G016, G017, G018, G019, G020, G021, G022, G023, G024, G025, G026, G027, G028, G029, G030, G031, G032, G033, G034, G035, G036, G037, G038, G039, G040, G041, G042, G043, G044, G045, G046, G047, G048, G049, G050, G051, G052, G053, G054, G055, G056, G057, G058, G059, G060, G061, G062, G063, G064, G065, G066, G067, G068, G069, G070, G071, G072, G073, G074, G075, G076, G077, G078, G079, G080, G081, G082, G083, G084, G085, G086, G087, G088, G089, G090, G091, G092, G093, G094, G095, G096, G097, G098, G099, G100, G101, G102, G103, G104, G105, G106, G107, G108, G109, G110, G111, G112, G113, G114, G115, G116, G117, G118, G119, G120, G121, G122, G123, G124, G125, G126, G127, G128, G129, G130, G131, G132, G133, G134, G135, G136, G137, G138, G139, G140, G141, G142, G143, G144, G145, G146, G147, G148, G149, G150, G151, G152, G153, G154, G155, G156, G157, G158, G159, G160, G161, G162, G163, G164, G165, G166, G167, G168, G169, G170, G171, G172, G173, G174, G175, G176, G177, G178, G179, G180, G181, G182, G183, G184, G185, G186, G187, G188, G189, G190, G191, G192, G193, G194, G195, G196, G197, G198, G199, G200, G201, G202, G203, G204, G205, G206, G207, G208, G209, G210, G211, G212, G213, G214, G215, G216, G217, G218, G219, G220, G221, G222, G223, G224, G225, G226, G227, G228, G229, G230, G231, G232, G233, G234, G235, G236, G237, G238, G239, G240, G241, G242, G243, G244, G245, G246, G247, G248, G249, G250, G251, G252, G253, G254, G255, G256, G257, G258, G259, G260, G261, G262, G263, G264, G265, G266, G267, G268, G269, G270, G271, G272, G273, G274, G275, G276, G277, G278, G279, G280, G281, G282, G283, G284, G285, G286, G287, G288, G289, G290, G291, G292, G293, G294, G295, G296, G297, G298, G299, G300, G301, G302, G303, G304, G305, G306, G307, G308, G309, G310, G311, G312, G313, G314, G315, G316, G317, G318, G319, G320, G321, G322, G323, G324, G325, G326, G327, G328, G329, G330, G331, G332, G333, G334, G335, G336, G337, G338, G339, G340, G341, G342, G343, G344, G345, G346, G347, G348, G349, G350, G351, G352, G353, G354, G355, G356, G357, G358, G359, G360, G361, G362, G363, G364, G365, G366, G367, G368, G369, G370, G371, G372, G373, G374, G375, G376, G377, G378, G379, G380, G381, G382, G383, G384, G385, G386, G387, G388, G389, G390, G391, G392, G393, G394, G395, G396, G397, G398, G399 };

            int difficultySelected = ListDifficulties.SelectedIndex + 1;  // Level of difficulty selected by the user
            object languageSelected = LanguageSelection.SelectedItem;  // Language selected by the user

            string messageBoxText = $"Difficulty selected: {difficultySelected}\nLanguage selected: {languageSelected.ToString().Split(" ")[1]}";
            string caption = "Grid generation";

            MessageBoxButton button = MessageBoxButton.OKCancel;  // MessageBoxButton with two options: 'ok' to confirm the settings chosen or 'cancel' to quit
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon);  // Display the MessageBox

            switch (result)
            {
                // If the user select the 'OK' option, the settings buttons are disabled and the 'Verify' button is enabled
                case MessageBoxResult.OK:
                    ListDifficulties.IsEnabled = false;
                    LanguageSelection.IsEnabled = false;
                    GenerateWordSearchGrid.IsEnabled = false;
                    isGenerated = true;
                    VerifyWord.IsEnabled = true;
                    Player1Name.IsEnabled = false;
                    Player2Name.IsEnabled = false;

                    ChangeGrid(difficultySelected);
                    PlaceRandomLetters();
                    AddWordsToGrid(difficultySelected);
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }

            void ChangeGrid(int difficulty)
            {
                // Declaration of 8 Label[] of the first 4 rows and columns of the grid in order to be able to crop them according to the difficulty
                Label[] firstColumnArray = { G000, G001, G002, G003, G004, G005, G006, G007, G008, G009, G010, G011, G012, G013, G014, G015, G016, G017, G018, G019 };
                Label[] secondColumnArray = { G020, G021, G022, G023, G024, G025, G026, G027, G028, G029, G030, G031, G032, G033, G034, G035, G036, G037, G038, G039 };
                Label[] thirdColumnArray = { G040, G041, G042, G043, G044, G045, G046, G047, G048, G049, G050, G051, G052, G053, G054, G055, G056, G057, G058, G059 };
                Label[] fourthColumnArray = { G060, G061, G062, G063, G064, G065, G066, G067, G068, G069, G070, G071, G072, G073, G074, G075, G076, G077, G078, G079 };
                Label[] firstRowArray = { G000, G020, G040, G060, G080, G100, G120, G140, G160, G180, G200, G220, G240, G260, G280, G300, G320, G340, G360, G380 };
                Label[] secondRowArray = { G001, G021, G041, G061, G081, G101, G121, G141, G161, G181, G201, G221, G241, G261, G281, G301, G321, G341, G361, G381 };
                Label[] thirdRowArray = { G002, G022, G042, G062, G082, G102, G122, G142, G162, G182, G202, G222, G242, G262, G282, G302, G322, G342, G362, G382 };
                Label[] fourthRowArray = { G003, G023, G043, G063, G083, G103, G123, G143, G163, G183, G203, G223, G243, G263, G283, G303, G323, G343, G363, G383 };

                List<Label[]> generalListToCrop = new List<Label[]> { firstColumnArray, firstRowArray, secondColumnArray, secondRowArray, thirdColumnArray, thirdRowArray, fourthColumnArray, fourthRowArray };

                for (int i = 0; i < 20; i++)
                {
                    foreach (Label[] rowOrColumnToCrop in generalListToCrop.SkipLast(difficulty * 2 - 2))
                    {
                        rowOrColumnToCrop[i].Visibility = Visibility.Collapsed;
                        labelsGeneralList = labelsGeneralList.Except(rowOrColumnToCrop).ToArray();
                    }
                }
            }

            void PlaceRandomLetters()
            {
                string lettersList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                foreach (Label labelLetter in labelsGeneralList)
                {
                    labelLetter.Content = lettersList[randomize.Next(0, lettersList.Length)];
                    labelLetter.Foreground = Brushes.Black;
                }
            }

            void AddWordsToGrid(int difficulty)
            {
                switch (languageSelected.ToString().Split(" ")[1])
                {
                    case "French":
                        dictionaryWords = new DictionaryWords("French", "french_dictionary.txt");
                        break;
                    case "English":
                        dictionaryWords = new DictionaryWords("English", "english_dictionary.txt");
                        break;
                }

                wordsLabel = new Label[] { W0, W1, W2, W3, W4, W5, W6, W7, W8, W9, W10, W11, W12, W13, W14 };

                for (int i = 0; i < difficulty + 10; i++)
                {
                    randomWords.Add(dictionaryWords.GetRandomWord(difficulty));
                }

                // The following code block displays the words to be found to the user
                randomWords = randomWords.OrderBy(word => word.Length).ThenBy(word => word).ToList();

                foreach (Label wordLabel in wordsLabel.SkipLast(5 - difficulty))
                {
                    wordLabel.Content = randomWords[counterForRandomWords];
                    wordLabel.Foreground = Brushes.White;
                    counterForRandomWords++;
                }

                // The following class allows to generate a random value more quickly than with the Random class
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // The following block of code is the most important of the whole program because it allows to place the words randomly in the grid
                foreach (string wordToPlace in randomWords)
                {
                    isPlaced = false;
                    while (!isPlaced)
                    {
                        byte[] randomNumbers = new byte[2];
                        rng.GetBytes(randomNumbers);

                        int randomWordDirection = 0;

                        // The following code block allows to restrict the orientation of words according to difficulty
                        switch (difficulty)
                        {
                            case 1:
                                randomWordDirection = (int)((randomNumbers[0] / (double)255) * 2);
                                break;
                            case 2:
                                randomWordDirection = (int)((randomNumbers[0] / (double)255) * 4);
                                break;
                            case 3:
                                randomWordDirection = (int)((randomNumbers[0] / (double)255) * 5);
                                break;
                            case 4:
                                randomWordDirection = (int)((randomNumbers[0] / (double)255) * 6);
                                break;
                            case 5:
                                randomWordDirection = (int)((randomNumbers[0] / (double)255) * 8);
                                break;
                        }

                        int randomLabelIndex = (int)((randomNumbers[1] / (double)255) * (labelsGeneralList.Length - 1));

                        void PlaceWordsInCertainDirection(int indexForDirection, bool isHorizontalOrDiagonal, int indexForDiagonal)
                        {
                            if (labelsGeneralList[randomLabelIndex].Background != Brushes.Silver || labelsGeneralList[randomLabelIndex].Content.ToString() == wordToPlace[0].ToString())
                            {
                                try
                                {
                                    for (int i = 1; i < wordToPlace.Length; i++)
                                    {
                                        if (isHorizontalOrDiagonal)
                                        {
                                            if (Grid.GetRow(labelsGeneralList[randomLabelIndex + indexForDirection * (wordToPlace.Length - 1)]) != Grid.GetRow(labelsGeneralList[randomLabelIndex]) + indexForDiagonal)
                                            {
                                                throw new Exception();
                                            }
                                            else if (labelsGeneralList[randomLabelIndex + indexForDirection * i].Background == Brushes.Silver)
                                            {
                                                if (labelsGeneralList[randomLabelIndex + indexForDirection * i].Content.ToString() != wordToPlace[i].ToString())
                                                {
                                                    throw new Exception();
                                                }
                                            }
                                        }
                                        else if (!isHorizontalOrDiagonal)
                                        {
                                            if (Grid.GetColumn(labelsGeneralList[randomLabelIndex + indexForDirection * (wordToPlace.Length - 1)]) != Grid.GetColumn(labelsGeneralList[randomLabelIndex]))
                                            {
                                                throw new Exception();
                                            }
                                            else if (labelsGeneralList[randomLabelIndex + indexForDirection * i].Background == Brushes.Silver)
                                            {
                                                if (labelsGeneralList[randomLabelIndex + indexForDirection * i].Content.ToString() != wordToPlace[i].ToString())
                                                {
                                                    throw new Exception();
                                                }
                                            }
                                        }
                                    }
                                    for (int j = 0; j < wordToPlace.Length; j++)
                                    {
                                        labelsGeneralList[randomLabelIndex + indexForDirection * j].Content = wordToPlace[j];
                                        labelsGeneralList[randomLabelIndex + indexForDirection * j].Background = Brushes.Silver;
                                    }
                                    isPlaced = true;
                                }
                                catch
                                {
                                    isPlaced = false;
                                }
                            }
                        }

                        switch (randomWordDirection)
                        {
                            // Top to bottom
                            case 0:
                                PlaceWordsInCertainDirection(1, false, 0);
                                break;
                            // Left to right
                            case 1:
                                PlaceWordsInCertainDirection(15 + difficulty, true, 0);
                                break;
                            // Bottom to top
                            case 2:
                                PlaceWordsInCertainDirection(-1, false, 0);
                                break;
                            // Right to left
                            case 3:
                                PlaceWordsInCertainDirection(-15 - difficulty, true, 0);
                                break;
                            // To lower right
                            case 4:
                                PlaceWordsInCertainDirection(16 + difficulty, true, wordToPlace.Length - 1);
                                break;
                            // To lower left
                            case 5:
                                PlaceWordsInCertainDirection(-14 - difficulty, true, wordToPlace.Length - 1);
                                break;
                            // To upper right
                            case 6:
                                PlaceWordsInCertainDirection(14 + difficulty, true, -wordToPlace.Length + 1);
                                break;
                            // To upper left
                            case 7:
                                PlaceWordsInCertainDirection(-16 - difficulty, true, -wordToPlace.Length + 1);
                                break;
                        }
                    }

                }

                // Blank all labels
                foreach (Label labelToBlank in labelsGeneralList)
                {
                    labelToBlank.Background = Brushes.White;
                }

                // Create a timer with an interval of one second
                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };

                // Create a counter to keep track of the remaining countdown time
                int counter = -1;
                switch (difficulty)
                {
                    case 1:
                        counter = 60;
                        break;
                    case 2:
                        counter = 90;
                        break;
                    case 3:
                        counter = 120;
                        break;
                    case 4:
                        counter = 150;
                        break;
                    case 5:
                        counter = 180;
                        break;
                }

                // Define the event handler for the Tick event of the timer
                timer.Tick += (sender, args) =>
                {
                    // Decrease the counter by one second
                    counter--;

                    // If the counter reaches 0, then the player has lost
                    if (counter == -1 && counterForRounds == 0)
                    {
                        timer.Stop();

                        foreach (Label box in labelsGeneralList)
                        {
                            box.IsEnabled = false;
                            if (box.Background != Brushes.SteelBlue)
                            {
                                box.Background = Brushes.White;
                            }
                            box.BorderBrush = Brushes.RoyalBlue;
                        }

                        foreach (string wordFoundByPlayer in wordsFound)
                        {
                            score += wordFoundByPlayer.Length;
                        }

                        scores.Add(score + (int)timerDecrementing.Content);

                        MessageBox.Show("Time's up!", $"Score: {scores[0]}", MessageBoxButton.OK, MessageBoxImage.Warning);
                        counterForRounds++;

                        foreach(Label box in labelsGeneralList)
                        {
                            box.IsEnabled = true;
                            box.Background = Brushes.White;
                            box.BorderBrush = Brushes.RoyalBlue;
                        }

                        foreach (Label wordLabel in wordsLabel.SkipLast(5 - difficulty))
                        {
                            wordLabel.Foreground = Brushes.White;
                        }

                        wordForming = "";
                        counterForDirection = 0;
                        labelsClicked.Clear();
                        blank = false;
                        wordsFound.Clear();
                        score = 0;

                        switch (difficulty)
                        {
                            case 1:
                                counter = 60;
                                break;
                            case 2:
                                counter = 90;
                                break;
                            case 3:
                                counter = 120;
                                break;
                            case 4:
                                counter = 150;
                                break;
                            case 5:
                                counter = 180;
                                break;
                        }
                        timer.Start();

                    }
                    else if (counter == -1 && counterForRounds == 1)
                    {
                        foreach (string wordFoundByPlayer in wordsFound)
                        {
                            score += wordFoundByPlayer.Length;
                        }
                        scores.Add(score + (int)timerDecrementing.Content);

                        if (scores[0] > scores[1])
                        {
                            foreach (Label box in labelsGeneralList)
                            {
                                box.IsEnabled = false;
                                if (box.Background != Brushes.SteelBlue)
                                {
                                    box.Background = Brushes.White;
                                }
                                box.BorderBrush = Brushes.RoyalBlue;
                            }

                            MessageBox.Show($"End of the game!\n{names[0]}: {scores[0]}\n{names[1]}: {scores[1]}", $"{names[0]} won the game!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            foreach (Label box in labelsGeneralList)
                            {
                                box.IsEnabled = false;
                                if (box.Background != Brushes.SteelBlue)
                                {
                                    box.Background = Brushes.White;
                                }
                                box.BorderBrush = Brushes.RoyalBlue;
                            }
                            MessageBox.Show($"End of the game!\n{names[0]}: {scores[0]}\n{names[1]}: {scores[1]}", $"{names[1]} won the game!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        Application.Current.Shutdown();
                    }

                    // If the player finds all the words within the time limit, he wins the round
                    if (wordsFound.Count == randomWords.Count)
                    {
                        timer.Stop();

                        foreach (Label box in labelsGeneralList)
                        {
                            box.IsEnabled = false;
                            if (box.Background != Brushes.SteelBlue)
                            {
                                box.Background = Brushes.White;
                                box.BorderBrush = Brushes.RoyalBlue;
                            }
                        }

                        VerifyWord.IsEnabled = false;

                        foreach (string wordFoundByPlayer in wordsFound)
                        {
                            score += wordFoundByPlayer.Length;
                        }

                        scores.Add(score + (int)timerDecrementing.Content);

                        MessageBox.Show($"You won!\nScore: {scores[0]}", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                        Application.Current.Shutdown();
                    }

                    // Use of the Dispatcher.Invoke method to execute the UI update code on the UI thread
                    timerDecrementing.Dispatcher.Invoke(() =>
                    {
                        timerDecrementing.Content = counter;
                    });
                };

                // Starting the timer
                timer.Start();

            }
        }

        // The following method is called when the user clicks on a box in the grid
        void G_MouseLeftButtonDown(object sender, EventArgs e)
        {
            if (isGenerated)
            {
                Label nameLabel = (sender as Label);
                if (counterForDirection == 0)
                {
                    if (nameLabel.Background != Brushes.SteelBlue)
                    {
                        nameLabel.Background = Brushes.LightGray;
                        nameLabel.BorderBrush = Brushes.LightBlue;
                    }
                    else
                    {
                        nameLabel.BorderBrush = Brushes.LightBlue;
                    }
                    wordForming += nameLabel.Content;
                    labelsClicked.Add(nameLabel);
                    counterForDirection++;
                }
                else if (counterForDirection == 1)
                {
                    string nameBefore = labelsClicked[counterForDirection - 1].Name;
                    int intNameBefore = Int32.Parse(nameBefore.Substring(nameBefore.Length - 3));
                    int intCurrentName = Int32.Parse(nameLabel.Name.Substring(nameLabel.Name.Length - 3));

                    if (intCurrentName == intNameBefore + 1 || intCurrentName == intNameBefore + 19 || intCurrentName == intNameBefore + 21 || intCurrentName == intNameBefore - 1 || intCurrentName == intNameBefore - 19 || intCurrentName == intNameBefore - 21 || intCurrentName == intNameBefore + 20 || intCurrentName == intNameBefore - 20)
                    {
                        if (nameLabel.Background != Brushes.SteelBlue)
                        {
                            nameLabel.Background = Brushes.LightGray;
                            nameLabel.BorderBrush = Brushes.LightBlue;
                        }
                        else
                        {
                            nameLabel.BorderBrush = Brushes.LightBlue;
                        }
                        wordForming += nameLabel.Content;
                        labelsClicked.Add(nameLabel);
                        counterForDirection++;
                    }
                    else
                    {
                        labelsClicked.Add(nameLabel);
                        blank = true;
                        VerifyWord_Click(null, null);
                    }
                }
                else
                {
                    string nameFirstBefore = labelsClicked[counterForDirection - 2].Name;
                    string nameBefore = labelsClicked[counterForDirection - 1].Name;
                    int intNameFirstBefore = Int32.Parse(nameFirstBefore.Substring(nameFirstBefore.Length - 3));
                    int intNameBefore = Int32.Parse(nameBefore.Substring(nameBefore.Length - 3));
                    int intCurrentName = Int32.Parse(nameLabel.Name.Substring(nameLabel.Name.Length - 3));

                    if (intCurrentName == intNameBefore + 1 && intCurrentName == intNameFirstBefore + 2 || intCurrentName == intNameBefore + 19 && intCurrentName == intNameFirstBefore + 38 || intCurrentName == intNameBefore + 21 && intCurrentName == intNameFirstBefore + 42 || intCurrentName == intNameBefore - 1 && intCurrentName == intNameFirstBefore - 2 || intCurrentName == intNameBefore - 19 && intCurrentName == intNameFirstBefore - 38 || intCurrentName == intNameBefore - 21 && intCurrentName == intNameFirstBefore - 42 || intCurrentName == intNameBefore + 20 && intCurrentName == intNameFirstBefore + 40 || intCurrentName == intNameBefore - 20 && intCurrentName == intNameFirstBefore - 40)
                    {
                        if (nameLabel.Background != Brushes.SteelBlue)
                        {
                            nameLabel.Background = Brushes.LightGray;
                            nameLabel.BorderBrush = Brushes.LightBlue;
                        }
                        else
                        {
                            nameLabel.BorderBrush = Brushes.LightBlue;
                        }
                        wordForming += nameLabel.Content;
                        labelsClicked.Add(nameLabel);
                        counterForDirection++;
                    }
                    else
                    {
                        labelsClicked.Add(nameLabel);
                        blank = true;
                        VerifyWord_Click(null, null);
                    }
                }
            }
        }

        // The following method is called when the user clicks on the 'Verify' button
        void VerifyWord_Click(object sender, EventArgs e)
        {
            if (isGenerated)
            {
                if (wordForming.Length > 1 && randomWords.Contains(wordForming) && !blank && !wordsFound.Contains(wordForming))
                {
                    wordsFound.Add(wordForming);
                    foreach (Label wordToFind in wordsLabel)
                    {
                        if (wordToFind.Content.ToString() == wordForming)
                        {
                            wordToFind.Foreground = Brushes.Blue;
                        }
                    }

                    foreach (Label labelClicked in labelsClicked)
                    {
                        labelClicked.Background = Brushes.SteelBlue;
                        labelClicked.BorderBrush = Brushes.RoyalBlue;
                    }
                }
                else
                {
                    foreach (Label labelClicked in labelsClicked)
                    {
                        if (labelClicked.Background != Brushes.SteelBlue)
                        {
                            labelClicked.Background = Brushes.White;
                            labelClicked.BorderBrush = Brushes.RoyalBlue;
                        }
                        else
                        {
                            labelClicked.BorderBrush = Brushes.RoyalBlue;
                        }
                    }
                }
                wordForming = "";
                counterForDirection = 0;
                labelsClicked.Clear();
                blank = false;
            }

        }

        // The method is called when the cursor enter a box in the grid
        void G_MouseEnter(object sender, EventArgs e)
        {
            bool mouseIsDown = Mouse.LeftButton == MouseButtonState.Pressed;
            Label nameLabel = sender as Label;
            if (isGenerated && mouseIsDown)
            {
                G_MouseLeftButtonDown(sender, e);
            }
        }
    }
}