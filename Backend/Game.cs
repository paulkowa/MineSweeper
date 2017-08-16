using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Game
    {
        public bool firstClick;
        private MainWindow window;
        private Difficulty difficulty;
        private Board board;
        public Game(MainWindow window)
        {
            this.window = window;
            difficulty = new Difficulty();
        }
        /// <summary>
        /// Start a new instance of the game
        /// </summary>
        public void NewGame()
        {
            board = new Board(difficulty);
            window.ResetBoard();
        }
        /// <summary>
        /// Get a read only board
        /// </summary>
        /// <returns></returns>
        public Board GetBoard()
        {
            return board;
        }
        /// <summary>
        /// Get a read only diffculty
        /// </summary>
        /// <returns></returns>
        public Difficulty GetDifficulty()
        {
            return difficulty;
        }
        /// <summary>
        /// Return a reference to the gui class
        /// </summary>
        /// <returns></returns>
        public MainWindow GetMainWindow()
        {
            return window;
        }
    }
}
