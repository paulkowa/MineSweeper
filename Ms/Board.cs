using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ms
{
    /// <summary>
    /// Class to represent the board and handle filling it with tiles, assigning tile mine count values, generating mines and relocating mines
    /// </summary>
    public class Board
    {
        private int size;
        public double mineTotal { get; private set; }
        public List<Tile> board { get; private set; }
        public Board(int size)
        {
            // Initialize values
            this.size = size;
            board = new List<Tile>();
            // Fill board, assign mines, and assign tile mine count
            fillBoard(board, this.size);
            generateMines(board, size);
            mineCount(board, size);
        }
        /// <summary>
        /// Fill the board array with the correct number of tiles
        /// </summary>
        /// <param name="board"></param>
        /// <param name="size"></param>
        private void fillBoard(List<Tile> board, int size)
        {
            for (int i = 0; i < size; i++) { board.Add(new Tile(i)); }
        }

        /// <summary>
        /// Assign tiles as mines at random
        /// </summary>
        /// <param name="board"></param>
        /// <param name="size"></param>
        private void generateMines(List<Tile> board, int size)
        {
            Random rand = new Random();
            double mines = Math.Round(size * 0.21);
            mineTotal = mines;
            while (mines > 0)
            {
                int i = rand.Next(1, size - 1);
                if (board[i].isMine == false)
                {
                    board[i].isMine = true;
                    mines--;
                }
            }
        }

        /// <summary>
        /// Swap the location of a mine with another tile
        /// </summary>
        /// <param name="t"></param>
        public void moveMine(Tile t)
        {
            board[t.index].isMine = false;
            board[0].isMine = true;
            mineCount(board, size);
        }

        /// <summary>
        /// Refresh the counts for nearbyMines for every tile on the board
        /// Cannot currently be used, as running async breaks setTile()
        /// Causes mines when clicked first to not update properly with the gui
        /// </summary>
        /*
        public void refreshMineCounts()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.DoWork += (sender, e) =>
            {
                mineCount(board, size);
            };
            worker.RunWorkerAsync();
        } 
        */

        /// <summary>
        /// Assign tiles with their correct nearby mine values
        /// </summary>
        /// <param name="board"></param>
        /// <param name="size"></param>
        private void mineCount(List<Tile> board, int size)
        {
            for (int i = 0; i < board.Count; i++)
            {
                if (board[i].isMine) { board[i].nearbyMines = -1; }
                else if (i == 0 || i == 23 || i == 552 || i == 575)
                {
                    switch (i)
                    {
                        case 0:
                            board[i].nearbyMines = board[i + 1].toInt() + board[i + 25].toInt() + board[i + 24].toInt();
                            break;
                        case 23:
                            board[i].nearbyMines = board[i - 1].toInt() + board[i + 23].toInt() + board[i + 24].toInt();
                            break;
                        case 552:
                            board[i].nearbyMines = board[i + 1].toInt() + board[i - 23].toInt() + board[i - 24].toInt();
                            break;
                        case 575:
                            board[i].nearbyMines = board[i - 1].toInt() + board[i - 25].toInt() + board[i - 24].toInt();
                            break;
                    }
                }
                else
                {
                    if (i % 24 == 23) { board[i].nearbyMines = board[i + 24].toInt() + board[i + 23].toInt() + board[i - 1].toInt() + board[i - 25].toInt() + board[i - 24].toInt(); }
                    else if (i % 24 == 0) { board[i].nearbyMines = board[i + 1].toInt() + board[i + 25].toInt() + board[i + 24].toInt() + board[i - 24].toInt() + board[i - 23].toInt(); }
                    else if (i < 23) { board[i].nearbyMines = board[i + 1].toInt() + board[i + 25].toInt() + board[i + 24].toInt() + board[i + 23].toInt() + board[i - 1].toInt(); }
                    else if (i > 552) { board[i].nearbyMines = board[i + 1].toInt() + board[i - 1].toInt() + board[i - 25].toInt() + board[i - 24].toInt() + board[i - 23].toInt(); }
                    else { board[i].nearbyMines = board[i + 1].toInt() + board[i + 25].toInt() + board[i + 24].toInt() + board[i + 23].toInt() + board[i - 1].toInt() + board[i - 25].toInt() + board[i - 24].toInt() + board[i - 23].toInt(); }
                }
            }
        }
    }
}

