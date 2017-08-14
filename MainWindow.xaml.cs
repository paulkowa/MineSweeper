using MineSweeper.GUI.Butons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this);
            addGameControlButton(game);
        }

        public void resetBoard()
        {

        }

        private void addGameControlButton(Game game)
        {
            GameControlButton gameControlButton = new GameControlButton(game);
            this.TopGamePanel.Children.Add(gameControlButton);
            Grid.SetColumn(gameControlButton, 2);
        }

        private void addMineButtons(Game game)
        {
            GameBoardGrid.Children.Add(createGrid());
        }
        private UniformGrid createGrid()
        {
            UniformGrid boardGrid = new UniformGrid();
            boardGrid.VerticalAlignment = VerticalAlignment.Top;
            boardGrid.HorizontalAlignment = HorizontalAlignment.Center;
            return boardGrid;
        }
    }
}
