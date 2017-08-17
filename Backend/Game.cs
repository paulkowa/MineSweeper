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
        
        public void checkTiles(Tile t)
        {
            TileChecker checker = new TileChecker(QueueNeighboringTiles(t));
        }
        private void setTiles(Queue<Tile> tiles)
        {
            bool gameOver = false;
            while (tiles.Count > 0)
            {
                Tile t = tiles.Dequeue();
                if(!t.isFlag)
                {
                    window.ActivateMineButton(t.index);
                    if(t.isMine) { gameOver = !gameOver; }
                }
            }
            if (gameOver) {; }
        }
        private Queue<Tile> GetTileQueue(Queue<Tile> tiles, List<Tile> doNotCheck)
        {
            int size = tiles.Count;
            Queue<Tile> temp = new Queue<Tile>();

            while (size > 0)
            {
                Tile t = tiles.Dequeue();
                if(t.GetNearMines().Equals(0) && !doNotCheck.Contains(t))
                {
                    doNotCheck.Add(t);
                    GetTileQueue(QueueNeighboringTiles(t), doNotCheck);
                    tiles.Enqueue(t);
                }
            }
            return null;
        }

        private Queue<Tile> QueueNeighboringTiles(Tile t)
        {
            Queue<Tile> tiles = new Queue<Tile>();
            int index = t.index;
            if (t.index == 0 || t.index == board.x - 1 || index == (board.x * (board.y - 1)) || index == ((board.x * board.y) - 1)) { GetCornerTiles(t.index, tiles); }
            else { GetRegularTiles(t.index, tiles); }
            return tiles;
        }
        /// <summary>
        /// Adds adjacent tiles to the given corner tile to the queue
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tiles"></param>
        private void GetCornerTiles(int index, Queue<Tile> tiles)
        {
            if (index == 0)
            {
                tiles.Enqueue(board.GetRightOf(index));
                tiles.Enqueue(board.GetLowerRightOf(index));
                tiles.Enqueue(board.GetLowerOf(index));
            }
            else if (index == board.x - 1)
            {
                tiles.Enqueue(board.GetLeftOf(index));
                tiles.Enqueue(board.GetLowerLeftOf(index));
                tiles.Enqueue(board.GetLowerOf(index));
            }
            else if (index == (board.x * (board.y - 1)))
            {
                tiles.Enqueue(board.GetUpperOf(index));
                tiles.Enqueue(board.GetUpperRightOf(index));
                tiles.Enqueue(board.GetRightOf(index));
            }
            else
            {
                tiles.Enqueue(board.GetUpperOf(index));
                tiles.Enqueue(board.GetUpperLeftOf(index));
                tiles.Enqueue(board.GetLeftOf(index));
            }
        }
        /// <summary>
        /// Adds adjacent tiles to the given non-corner tile to the queue
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tiles"></param>
        private void GetRegularTiles(int index, Queue<Tile> tiles)
        {
            if (index % board.x == board.x - 1)
            {
                tiles.Enqueue(board.GetUpperOf(index));
                tiles.Enqueue(board.GetUpperLeftOf(index));
                tiles.Enqueue(board.GetLeftOf(index));
                tiles.Enqueue(board.GetLowerLeftOf(index));
                tiles.Enqueue(board.GetLowerOf(index));
            }
            else if (index % board.x == 0)
            {
                tiles.Enqueue(board.GetUpperOf(index));
                tiles.Enqueue(board.GetUpperRightOf(index));
                tiles.Enqueue(board.GetRightOf(index));
                tiles.Enqueue(board.GetLowerRightOf(index));
                tiles.Enqueue(board.GetLowerOf(index));
            }
            else if (index < board.x)
            {
                tiles.Enqueue(board.GetLeftOf(index));
                tiles.Enqueue(board.GetLowerLeftOf(index));
                tiles.Enqueue(board.GetLowerOf(index));
                tiles.Enqueue(board.GetLowerRightOf(index));
                tiles.Enqueue(board.GetRightOf(index));
            }
            else if (index > board.x * (board.y - 1))
            {
                tiles.Enqueue(board.GetLeftOf(index));
                tiles.Enqueue(board.GetUpperLeftOf(index));
                tiles.Enqueue(board.GetUpperOf(index));
                tiles.Enqueue(board.GetUpperRightOf(index));
                tiles.Enqueue(board.GetRightOf(index));
            }
            else
            {
                tiles.Enqueue(board.GetLeftOf(index));
                tiles.Enqueue(board.GetUpperLeftOf(index));
                tiles.Enqueue(board.GetUpperOf(index));
                tiles.Enqueue(board.GetUpperRightOf(index));
                tiles.Enqueue(board.GetRightOf(index));
                tiles.Enqueue(board.GetLowerRightOf(index));
                tiles.Enqueue(board.GetLowerOf(index));
                tiles.Enqueue(board.GetLowerLeftOf(index));
            }
        }
    }
}
