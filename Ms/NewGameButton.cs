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

namespace Ms
{
    public class NewGameButton : Button
    {
        private Game game;
        private Image smile, loss, win;
        public NewGameButton(Game game)
        {
            // Link to game class
            this.game = game;
            // Set button values
            setProperties();
        }

        private void setProperties()
        {
            Background = Brushes.DodgerBlue;
            BorderBrush = Brushes.White;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            Height = 40;
            Width = 50;

            smile = new Image();
            smile.Source = new BitmapImage(new Uri(@"/Resources/smile.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;
            RenderOptions.SetBitmapScalingMode(smile, BitmapScalingMode.HighQuality);
            RenderOptions.SetEdgeMode(smile, EdgeMode.Aliased);
            Content = smile;

            win = new Image();
            win.Source = new BitmapImage(new Uri(@"/Resources/victory.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;
            RenderOptions.SetBitmapScalingMode(win, BitmapScalingMode.HighQuality);
            RenderOptions.SetEdgeMode(win, EdgeMode.Aliased);

            loss = new Image();
            loss.Source = new BitmapImage(new Uri(@"/Resources/loss.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;
            RenderOptions.SetBitmapScalingMode(loss, BitmapScalingMode.HighQuality);
            RenderOptions.SetEdgeMode(loss, EdgeMode.Aliased);
        }

        /// <summary>
        /// Start a new game on left mouse click, if at least one tile has already been selected
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Content = smile;
            if (!game.firstClick)
            {
                game.newGame();
                IsEnabled = false;
            }
            else { game.removeFlags(); }
            IsEnabled = true;
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
        }

        public void gameLost()
        {
            Content = loss;
        }

        public void gameWon()
        {
            Content = win;
        }
    }
}
