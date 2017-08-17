using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace MineSweeper.GUI.Buttons
{
    public class MineButton : Button
    {
        private Game game;
        private Image flag, mine, cross;
        private Tile tile;
        public MineButton(Game game, Tile tile = null)
        {
            this.game = game;
            this.tile = tile;
            SetProperties();
            SetImages();
        }
        /*
         * 
         * Event handlers for this button
         * 
         */
        /// <summary>
        /// Right click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            FlagTile();
        }
        /// <summary>
        /// Left click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            SetTile();
        }
        /// <summary>
        /// Double left click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }
        /*
         * 
         * Methods used on events to change state of button
         * 
         */
        /// <summary>
        /// Flags the current tile if not flagged or active, if already flagged remove flag, if active do nothing.
        /// </summary>
        public void FlagTile()
        {
            if (tile.isActive) { return; }
            else if (tile.isFlag)
            {
                SetDefault();
                tile.SetFlag();
                game.GetMainWindow().IncrementTotalMines();
            }
            else
            {
                SetFlag();
                tile.SetFlag();
                game.GetMainWindow().DecrementTotalMines();
            }
        }
        /// <summary>
        /// Set the button to display either a mine or proper value of nearby mines
        /// </summary>
        public void SetTile()
        {
            if (tile.isFlag || tile.isActive) { return; }
            else if (tile.isMine)
            {
                SetMine();
                tile.SetActive();
            }
            else
            {
                if (tile.GetNearMines() > 0)
                {
                    Content = CreateButtonText();
                    Background = Brushes.PowderBlue;
                    tile.SetActive();
                    // Update victory progress
                }
                else
                {
                    SetEmpty();
                    tile.SetActive();
                    // Update victory progress
                }
            }
        }

        /*
         * 
         * Methods to set the gui properties of this button
         * 
         */
        private TextBlock CreateButtonText()
        {
            TextBlock t = new TextBlock();
            t.TextAlignment = TextAlignment.Center;
            t.VerticalAlignment = VerticalAlignment.Center;
            t.HorizontalAlignment = HorizontalAlignment.Center;
            SetText(t);
            return t;
        }
        /// <summary>
        /// Sets the properties of a text block and text to the int number of mines adjacent to this tile
        /// </summary>
        /// <param name="t"></param>
        private void SetText(TextBlock t)
        {
            t.FontWeight = FontWeights.Bold;
            t.FontSize = 16;
            t.Text = Convert.ToString(tile.GetNearMines());
            switch (tile.GetNearMines())
            {
                case 0:
                    break;
                case 1:
                    t.Foreground = Brushes.Blue;
                    break;
                case 2:
                    t.Foreground = Brushes.Green;
                    break;
                case 3:
                    t.Foreground = Brushes.Red;
                    break;
                case 4:
                    t.Foreground = Brushes.DarkBlue;
                    break;
                case 5:
                    t.Foreground = Brushes.DarkRed;
                    break;
                case 6:
                    t.Foreground = Brushes.DarkCyan;
                    break;
                case 7:
                    t.Foreground = Brushes.Black;
                    break;
                case 8:
                    t.Foreground = Brushes.Orange;
                    break;
            }
        }
        /// <summary>
        /// Set the content to null and background to default
        /// </summary>
        private void SetDefault()
        {
            Content = null;
            Background = Brushes.DodgerBlue;
        }
        private void SetEmpty()
        {
            Content = null;
            Background = Brushes.PowderBlue;
        }
        /// <summary>
        /// Set the content image to a flag
        /// </summary>
        private void SetFlag()
        {
            Content = flag;
            Background = Brushes.DodgerBlue;
        }
        /// <summary>
        /// Set the content image to a mine
        /// </summary>
        private void SetMine()
        {
            Content = mine;
            Background = Brushes.Red;
        }
        /// <summary>
        /// Set the content image to a cross
        /// </summary>
        private void SetCross()
        {
            Content = cross;
            Background = Brushes.PowderBlue;
        }
        /// <summary>
        /// Set images to their resources
        /// </summary>
        private void SetImages()
        {
            flag = new Image();
            flag.Source = new BitmapImage(new Uri(@"/Images/flagIcon.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            mine = new Image();
            mine.Source = new BitmapImage(new Uri(@"/Images/mine.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            cross = new Image();
            cross.Source = new BitmapImage(new Uri(@"/Images/xmark.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

        }
        /// <summary>
        /// Set default properties
        /// </summary>
        private void SetProperties()
        {
            Height = 29;
            Width = 29;
            Background = Brushes.DodgerBlue;
            BorderBrush = Brushes.White;
        }
    }
}
