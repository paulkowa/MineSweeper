using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MineSweeper.GUI.Butons
{
    public class GameControlButton : Button
    {
        private Game game;
        private Image easy, medium, hard, loss, win;
        public GameControlButton(Game game)
        {
            this.game = game;
            setImages();
            setProperties();
        }
        /// <summary>
        /// Event handler for left click
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }
        /// <summary>
        /// Event handler for right click
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            gameMedium();
        }
        /// <summary>
        /// Method to change appearence upon losing the game
        /// </summary>
        public void gameLost()
        {
            Content = loss;
        }
        /// <summary>
        /// Method to change appearence upon winning the game
        /// </summary>
        public void gameWon()
        {
            Content = win;
        }
        /// <summary>
        /// Change content image to easy
        /// </summary>
        private void gameEasy()
        {
            Content = easy;
        }
        /// <summary>
        /// Change content image to medium
        /// </summary>
        private void gameMedium()
        {
            Content = medium;
        }
        /// <summary>
        /// Change content image to hard
        /// </summary>
        private void gameHard()
        {
            Content = hard;
        }

        private void setDifficultyIcon(Game game)
        {
            
        }
        /// <summary>
        /// Set image variables to proper content
        /// </summary>
        private void setImages()
        {
            easy = new Image();
            easy.Source = new BitmapImage(new Uri(@"/Images/sleeping.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            medium = new Image();
            medium.Source = new BitmapImage(new Uri(@"/Images/unamused.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            hard = new Image();
            hard.Source = new BitmapImage(new Uri(@"/Images/fear.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            loss = new Image();
            loss.Source = new BitmapImage(new Uri(@"/Images/loss.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            win = new Image();
            win.Source = new BitmapImage(new Uri(@"/Images/victory.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

        }
        /// <summary>
        /// Set button properties
        /// </summary>
        private void setProperties()
        {
            Width = 60;
            Margin = new Thickness(0,3,0,3);
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;
            Background = Brushes.DodgerBlue;
            BorderBrush = Brushes.White;
            Content = easy;
        }
    }
}
