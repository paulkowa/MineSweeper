using MineSweeper.GUI.Butons;
using MineSweeper.GUI.Buttons;
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
        private List<MineButton> mineButtons;
        public MainWindow()
        {
            InitializeComponent();
            mineButtons = new List<MineButton>();
            game = new Game(this);
            addGameControlButton(game);
            resetBoard();
        }

        public void resetBoard()
        {
            UniformGrid boardGrid = createGrid();
            addMineButtons(boardGrid, game.getDifficulty());
            this.GameBoardGrid.Children.Add(boardGrid);
        }

        private void addGameControlButton(Game game)
        {
            GameControlButton gameControlButton = new GameControlButton(game);
            this.TopGamePanel.Children.Add(gameControlButton);
            Grid.SetColumn(gameControlButton, 2);
        }

        private UniformGrid createGrid()
        {
            UniformGrid boardGrid = new UniformGrid();
            boardGrid.VerticalAlignment = VerticalAlignment.Top;
            boardGrid.HorizontalAlignment = HorizontalAlignment.Center;
            boardGrid.Columns = game.getBoard().x;
            boardGrid.Rows = game.getBoard().y;
            return boardGrid;
        }

        private void addMineButtons(UniformGrid boardGrid, Difficulty difficulty)
        {
            for (int i = 0; i < difficulty.getTileCount(); i++)
            {
                MineButton mb = new MineButton(game, game.getBoard().getTile(i));
                mineButtons.Add(mb);
                boardGrid.Children.Add(mb);
            }
        }
    }
}
