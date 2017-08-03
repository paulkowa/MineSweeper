using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ms
{
    public class MineButton : Button
    {
        public Tile tile { get; private set; }
        private Image flag, mine;
        private Game game;

        public MineButton(Tile tile, Game game, int index)
        {
            // Initalize values
            this.tile = tile;
            this.game = game;
            // Set flag image
            flag = new Image();
            flag.Source = new BitmapImage(new Uri(@"/Resources/flagIcon.png", UriKind.RelativeOrAbsolute));
            flag.VerticalAlignment = VerticalAlignment.Center;
            flag.HorizontalAlignment = HorizontalAlignment.Center;
            RenderOptions.SetBitmapScalingMode(flag, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(flag, EdgeMode.Aliased);
            // Set Mine image
            mine = new Image();
            mine.Source = new BitmapImage(new Uri(@"/Resources/mine.png", UriKind.RelativeOrAbsolute));
            mine.VerticalAlignment = VerticalAlignment.Center;
            mine.HorizontalAlignment = HorizontalAlignment.Center;
            RenderOptions.SetBitmapScalingMode(mine, BitmapScalingMode.HighQuality);
            RenderOptions.SetEdgeMode(mine, EdgeMode.Aliased);
            // Set button default background color
            Background = Brushes.DodgerBlue;
            this.BorderBrush = Brushes.White;

            // TEMPORARY FOR TESTING DELETE
            // TEMPORARY FOR TESTING DELETE
            // TEMPORARY FOR TESTING DELETE
            if (tile.isMine)
            {
                //Content = mine;
            }
            // TEMPORARY FOR TESTING DELETE
            // TEMPORARY FOR TESTING DELETE
            // TEMPORARY FOR TESTING DELETE

        }
        /// <summary>
        /// Right Click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            setFlag();

        }

        /// <summary>
        /// Left Click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (tile.isActive || tile.isFlagged) { return; }
            else if (game.firstClick)
            {
                if (tile.isMine) { game.board.moveMine(tile); }
                game.startTimer();
            }

            if (tile.nearbyMines == 0 && tile.isMine == false) { game.checkTiles(tile); }
            else if (tile.isMine)
            {
                setTile();
                game.gameLost();
            }
            else { setTile(); }

        }

        /// <summary>
        /// Double click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.ChangedButton == MouseButton.Left && tile.isActive && !tile.isMine && !tile.isSet) { game.checkTiles(tile); }
        }

        public void setX()
        {
            setFlag();
            tile.isActive = true;
            tile.isSet = true;
            Image x = new Image();
            x.Source = new BitmapImage(new Uri(@"/Resources/xmark.png", UriKind.RelativeOrAbsolute));
            x.VerticalAlignment = VerticalAlignment.Center;
            x.HorizontalAlignment = HorizontalAlignment.Center;
            RenderOptions.SetBitmapScalingMode(x, BitmapScalingMode.HighQuality);
            RenderOptions.SetEdgeMode(x, EdgeMode.Aliased);
            Background = Brushes.PowderBlue;
            Content = x;
        }

        /// <summary>
        /// Set or remove flag
        /// </summary>
        public void setFlag()
        {
            // Ignore if tile is already activated
            if (tile.isActive) { return; }
            else if (tile.isFlagged)
            {
                Content = null;
                Background = Brushes.DodgerBlue;
                game.window.MineCounter.Text = Convert.ToString(Convert.ToInt32(game.window.MineCounter.Text) + 1);
                tile.isFlagged = false;
            }

            // Set flag
            else
            {
                Content = flag;
                Background = Brushes.DodgerBlue;
                game.window.MineCounter.Text = Convert.ToString(Convert.ToInt32(game.window.MineCounter.Text) - 1);
                tile.isFlagged = true;
            }
        }
        /// <summary>
        /// Set the button to display the proper number or mine
        /// </summary>
        public void setTile()
        {
            if (tile.isFlagged || tile.isActive) { return; }
            else if (tile.isMine)
            {
                Content = mine;
                Background = Brushes.Red;
                tile.isActive = true;
            }
            else
            {
                if (tile.nearbyMines > 0)
                {
                    TextBlock t = new TextBlock();
                    t.Text = Convert.ToString(tile.nearbyMines);
                    setText(t, tile);
                    Content = t;
                    Background = Brushes.PowderBlue;
                    tile.isActive = true;
                }
                else
                {
                    Content = null;
                    Background = Brushes.PowderBlue;
                    tile.isActive = true;
                }
            }
        }

        /// <summary>
        /// Set the color of the text within the button to match the number of mines surrounding it
        /// </summary>
        /// <param name="t"></param>
        /// <param name="tile"></param>
        private void setText(TextBlock t, Tile tile)
        {
            t.FontWeight = FontWeights.Bold;
            t.FontSize = 16;
            t.VerticalAlignment = VerticalAlignment.Center;
            t.HorizontalAlignment = HorizontalAlignment.Center;
            switch (tile.nearbyMines)
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
    }
}

