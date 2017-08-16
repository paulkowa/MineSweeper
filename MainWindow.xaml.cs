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
        private Timer timer;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this);
            mineButtons = new List<MineButton>();
            AddGameControlButton(game);
            
            game.NewGame();
        }
        /// <summary>
        /// Create and add the GameControlButton to the gui
        /// </summary>
        /// <param name="game"></param>
        private void AddGameControlButton(Game game)
        {
            GameControlButton gameControlButton = new GameControlButton(game);
            this.TopGamePanel.Children.Add(gameControlButton);
            Grid.SetColumn(gameControlButton, 2);
        }
        /// <summary>
        /// Resize the window and populate it with a new grid representing the boards
        /// </summary>
        public void ResetBoard()
        {
            SetTotalMines(game.GetDifficulty().GetMineCount());
            if (this.GameBoardGrid.Children.Count > 0) { this.GameBoardGrid.Children.RemoveAt(0); }
            mineButtons = new List<MineButton>();
            UniformGrid boardGrid = CreateGrid();
            AddMineButtons(boardGrid, game.GetDifficulty());
            this.GameBoardGrid.Children.Add(boardGrid);
        }
        /// <summary>
        /// Create a new grid, set its properties and set the underlying container to fit it
        /// </summary>
        /// <returns>A new uniformGrid representing the game board</returns>
        private UniformGrid CreateGrid()
        {
            UniformGrid grid = new UniformGrid();
            SetUniformGridProperties(grid);
            SetBoardGridProperties();
            return grid;
        }
        /// <summary>
        /// Set a UniformGrid's properties based on the game difficulty
        /// </summary>
        /// <param name="grid"></param>
        private void SetUniformGridProperties(UniformGrid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Columns = game.GetBoard().x;
            grid.Rows = game.GetBoard().y;
            grid.Height = game.GetDifficulty().GetYPixels();
            grid.Width = game.GetDifficulty().GetXPixels();
        }
        /// <summary>
        /// Set the container for the board grid properties based on the game difficulty
        /// </summary>
        private void SetBoardGridProperties()
        {
            this.GameBoardGrid.Height = game.GetDifficulty().GetYPixels();
            this.GameBoardGrid.Width = game.GetDifficulty().GetXPixels();
        }
        /// <summary>
        /// Populate a UniformGrid with mines based on the game difficulty
        /// </summary>
        /// <param name="boardGrid"></param>
        /// <param name="difficulty"></param>
        private void AddMineButtons(UniformGrid boardGrid, Difficulty difficulty)
        {
            for (int i = 0; i < difficulty.GetTileCount(); i++)
            {
                MineButton mb = new MineButton(game, game.GetBoard().GetTile(i));
                mineButtons.Add(mb);
                boardGrid.Children.Add(mb);
            }
        }
        /// <summary>
        /// Move the window to the current mouse position
        /// </summary>
        private void ResetWindowPosition()
        {

        }
        /*
         * 
         * Timer Control Methods
         * 
         */
        /// <summary>
        /// Create a new Timer and start it
        /// </summary>
        public void StartTimer()
        {
            timer = new Timer(this);
            timer.RunWorkerAsync();
        }
        /// <summary>
        /// Stop the timer
        /// </summary>
        public void StopTimer()
        {
            timer.CancelAsync();
            timer = null;
        }
        /// <summary>
        /// Set the Timer field in the gui to 0
        /// </summary>
        private void ResetTimer()
        {
            Timer.Text = "0";
        }
        /// <summary>
        /// Update the Timer field with given double value
        /// </summary>
        /// <param name="seconds"></param>
        public void UpdateTimer(double seconds)
        {
            if (seconds < 1000) { this.Timer.Text = string.Format("{0:0.0}", Math.Truncate(seconds * 10) / 10); }
            else { StopTimer(); }
        }
        /*
         * 
         * Methods to control the Mine Counter
         * 
         */
         /// <summary>
         /// Increase the value in the MineCounter by 1
         /// </summary>
        public void IncrementTotalMines()
        {
            this.MineCounter.Text = Convert.ToString(Convert.ToInt32(this.MineCounter.Text) + 1);
        }
        /// <summary>
        /// Decrease the value in the MineCounter by 1
        /// </summary>
        public void DecrementTotalMines()
        {
            this.MineCounter.Text = Convert.ToString(Convert.ToInt32(this.MineCounter.Text) - 1);
        }
        /// <summary>
        /// Set the MineCounter value to give integer
        /// </summary>
        /// <param name="mines"></param>
        public void SetTotalMines(int mines)
        {
            this.MineCounter.Text = Convert.ToString(mines);
        }
    }
}
