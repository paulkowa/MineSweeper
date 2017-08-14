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
            setProperties();
            setImages();
        }
        /// <summary>
        /// Right click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            setFlag();
        }
        /// <summary>
        /// Left click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            setMine();
        }
        /// <summary>
        /// Double left click event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Set the content image to a flag
        /// </summary>
        private void setFlag()
        {
            Content = flag;
        }
        /// <summary>
        /// Set the content to null
        /// </summary>
        private void setEmpty()
        {
            Content = null;
        }
        /// <summary>
        /// Set the content image to a mine
        /// </summary>
        private void setMine()
        {
            Content = mine;
        }
        /// <summary>
        /// Set the content image to a cross
        /// </summary>
        private void setCross()
        {
            Content = cross;
        }
        /// <summary>
        /// Set images to their resources
        /// </summary>
        private void setImages()
        {
            flag = new Image();
            flag.Source = new BitmapImage(new Uri(@"/Images/sleeping.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            mine = new Image();
            mine.Source = new BitmapImage(new Uri(@"/Images/sleeping.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            cross = new Image();
            cross.Source = new BitmapImage(new Uri(@"/Images/sleeping.png", UriKind.RelativeOrAbsolute));
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

        }
        /// <summary>
        /// Set default properties
        /// </summary>
        private void setProperties()
        {
            Height = 29;
            Width = 29;
            Background = Brushes.DodgerBlue;
            BorderBrush = Brushes.White;
        }
    }
}
