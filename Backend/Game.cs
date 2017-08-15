using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Game
    {
        private MainWindow window;
        private Difficulty difficulty;
        private Board board;
        public Game(MainWindow window)
        {
            this.window = window;
            difficulty = new Difficulty();
            board = new Board(difficulty);
        }
        public void updateTimer(int seconds)
        {

        }
        /// <summary>
        /// Get a read only board
        /// </summary>
        /// <returns></returns>
        public Board getBoard()
        {
            return board;
        }
        /// <summary>
        /// Get a read only diffculty
        /// </summary>
        /// <returns></returns>
        public Difficulty getDifficulty()
        {
            return difficulty;
        }
    }
}
