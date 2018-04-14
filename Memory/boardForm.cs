﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    public partial class boardForm : Form
    {
        public boardForm()
        {
            InitializeComponent();
        }

        #region Instance Variables
        const int NOT_PICKED_YET = -1;

        int firstCardNumber = NOT_PICKED_YET;
        int secondCardNumber = NOT_PICKED_YET;
        int matches = 0;
        #endregion

        #region Methods

        // This method finds a picture box on the board based on it's number (1 - 20)
        // It takes an integer as it's parameter and returns the picture box controls
        // that's name contains that number
        private PictureBox GetCard(int i)
        {
            PictureBox card = (PictureBox)this.Controls["card" + i];
            return card;
        }

        // This method gets the filename for the image displayed in a picture box given it's number
        // It takes an integer as it's parameter and returns a string containing the 
        // filename for the image in the corresponding picture box
        private string GetCardFilename(int i)
        {
            return GetCard(i).Tag.ToString();
        }

        // This method changes the filename for a given picture box
        // It takes an integer and a string that represents a filename as it's parameters
        // It doesn't return a value but stores the filename for the image to be displayed
        // in the picture box.  It doesn't actually display the new image
        private void SetCardFilename(int i, string filename)
        {
            GetCard(i).Tag = filename;
        }

        // These 2 methods get the value (and suit) of the card in a given picturebox
        // Both methods take an integer as the parameter and return a string
        private string GetCardValue(int index)
        {
            return GetCardFilename(index).Substring(4, 1);
        }

        private string GetCardSuit(int index)
        {
            return GetCardFilename(index).Substring(5, 1);
        }

        // TODO:  students should write this one
        private bool IsMatch(string firstCardNumber, string secondCardNumber)
        {
            if (firstCardNumber == secondCardNumber)
            {
                return true;
            }
            else return false;
        }

        // This method fills each picture box with a filename
        private void FillCardFilenames()
        {
            string[] values = { "a", "2", "j", "q", "k" };
            string[] suits = { "c", "d", "h", "s" };
            int i = 1;

            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 0; value <= 4; value++)
                {
                    SetCardFilename(i, "card" + values[value] + suits[suit] + ".jpg");
                    i++;
                }
            }
        }
            
        
        // TODO:  students should write this one
        private void ShuffleCards()
        {
            List<string> values = new List<string> { "a", "2", "j", "q", "k" };
            List<string> suits = new List<string> { "c", "d", "h", "s" };
            List<int> i = new List<int> { };

            for(int li = 1; li < 21; li++)
            {i.Add(li);}

            Random rand = new Random();

            for (int suit = 3; suit > -1; suit--)
            {
                for (int value = 4; value > -1; value--)
                {
                    int randi = rand.Next(i.Count);
                    SetCardFilename(i[randi], "card" + values[value].ToString() + suits[suit].ToString() + ".jpg");
                    i.RemoveAt(randi);
                }
            }
        }

        // This method loads (shows) an image in a picture box.  Assumes that filenames
        // have been filled in an earlier call to FillCardFilenames
        private void LoadCard(int i)
        {
            
            PictureBox card = GetCard(i);
            card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\" + GetCardFilename(i));
        
        }

        // This method loads the image for the back of a card in a picture box
        private void LoadCardBack(int i)
        {
            PictureBox card = GetCard(i);
            card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\black_back.jpg");
        }

        // TODO:  students should write all of these
        // shows (loads) the backs of all of the cards
        private void LoadAllCardBacks()
        {
            for(int i = 1; i < 21; i++)
            {
                PictureBox card = GetCard(i);
                card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\black_back.jpg");
            }
           
        }

        // Hides a picture box
        private void HideCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Visible = false;
        }

        private void HideAllCards()
        {
            for (int i = 1; i < 21; i++)
            {
                PictureBox card = GetCard(i);
                card.Visible = false;
            }
        }

        // shows a picture box
        private void ShowCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\" + GetCardFilename(i));
            
        }

        private void ShowAllCards()
        {
            for(int i = 1; i < 21; i++)
            {
                PictureBox card = GetCard(i);
                card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\" + GetCardFilename(i));
            }
        }

        // disables a picture box
        private void DisableCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Enabled = false;
        }

        private void DisableAllCards()
        {
            for (int i = 1; i < 21; i++)
            {
                PictureBox card = GetCard(i);
                card.Enabled = false;

            }
        }

        private void EnableCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Enabled = true;
        }

        private void EnableAllCards()
        {
            for (int i = 1; i < 21; i++)
            {
                PictureBox card = GetCard(i);
                card.Enabled = true;

            }
        }
    
        private void EnableAllVisibleCards()
        {
            for (int i = 1; i < 21; i++)
            {
                PictureBox card = GetCard(i);
                if(card.Visible == true)
                {
                    card.Enabled = true;
                }
            }
        }

        #endregion

        #region EventHandlers
        private void boardForm_Load(object sender, EventArgs e)
        {
            FillCardFilenames();
            ShuffleCards();
            LoadAllCardBacks();
            EnableAllCards();
            /* 
             * Fill the picture boxes with filenames
             * Shuffle the cards
             * Load all of the card backs - 
             *      While you're testing you might want to load all of card faces
             *      to make sure that the cards are loaded successfully and that
             *      they're shuffled.  If you get all 2s, something is wrong.
            */
        }

        private void card_Click(object sender, EventArgs e)
        {
            bool cardTwo = IsCardTwo();
            PictureBox card = (PictureBox)sender;
            int cardNumber = int.Parse(card.Name.Substring(4));
            if (cardTwo == false)
            
            {
                GetCard(cardNumber);
                LoadCard(cardNumber);
                GetCardSuit(cardNumber);
                GetCardValue(cardNumber);
                firstCardNumber = int.Parse(card.Name.Substring(4));
                DisableCard(cardNumber);
            }

            else if (cardTwo == true)
            {
                GetCard(cardNumber);
                LoadCard(cardNumber);
                GetCardSuit(cardNumber);
                GetCardValue(cardNumber);
                secondCardNumber= int.Parse(card.Name.Substring(4));
                DisableAllCards();
                flipTimer.Start();
            }


            
            /* 
             * if the first card isn't picked yet
             *      save the first card index
             *      load the card
             *      disable the card
             *  else (the user just picked the second card)
             *      save the second card index
             *      load the card
             *      disable all of the cards
             *      start the flip timer
             *  end if
            */
        }
        private bool IsCardTwo()
        {
            for (int i = 1; i < 21; i++)
            {
                PictureBox card = GetCard(i);
                if(card.Enabled == false && card.Visible == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void flipTimer_Tick(object sender, EventArgs e)
        {
            flipTimer.Stop();
            
            bool match = IsMatch((GetCardValue(firstCardNumber)), (GetCardValue(secondCardNumber)));

            if(match == true)
            {
                HideCard(firstCardNumber);
                HideCard(secondCardNumber);
                firstCardNumber = NOT_PICKED_YET;
                secondCardNumber = NOT_PICKED_YET;
                matches++;
                matchesLabel.Text = matches.ToString();
                if (matches == 10)
                {
                    MessageBox.Show("You Win!");
                }
                else
                {
                    EnableAllVisibleCards();
                }
            }
            else
            {
                LoadCardBack(firstCardNumber);
                LoadCardBack(secondCardNumber);
                firstCardNumber = NOT_PICKED_YET;
                secondCardNumber = NOT_PICKED_YET;
                EnableAllVisibleCards();

            }
            /*
             * stop the flip timer
             * if the first card and second card are a match
             *      increment the number of matches
             *      hide the first card
             *      hide the second card
             *      reset the first card number
             *      reset the second card number
             *      if the number of matches is 10
             *          show a message box
             *      else
             *          enable all of the cards left on the board
             *      end if
             * else
             *      flip the first card back over
             *      flip the second card back over
             *      reset the first card number
             *      reset the second card number
             *      enable all of the cards left on the board
             * end if
             */
        }

        #endregion

        private void restartLabel_Click(object sender, EventArgs e)
        {
            ShowAllCards();
            FillCardFilenames();
            ShuffleCards();
            LoadAllCardBacks();
            EnableAllCards();
            matches = 0;
        }

        private void exitLabel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
