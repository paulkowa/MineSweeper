using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Board
    {
        private int tileCount, mineCount;
        public int x { get; private set; }
        public int y { get; private set; }
        private List<Tile> board;
        public Board(Difficulty difficulty)
        {
            tileCount = difficulty.getTileCount();
            mineCount = difficulty.getMineCount();
            board = new List<Tile>();
            setXY();
        }
        /// <summary>
        /// Set x and y dimensions based on current tileCount
        /// </summary>
        private void setXY()
        {
            switch (tileCount)
            {
                case 64:
                    x = y = 8;
                    break;
                case 256:
                    x = y = 16;
                    break;
                case 480:
                    x = 30;
                    y = 16;
                    break;
            }
        }
        /*
         * 
         * Methods to create a board, and tiles and set tile content
         * 
         */
        /// <summary>
        /// Run all methods necessary to create a new board
        /// </summary>
        private void setUpBoard()
        {
            generateTiles();
            generateMines();
            setTileMineCount();
        }
        /// <summary>
        /// Create all the tile objects
        /// </summary>
        private void generateTiles()
        {
            for (int i = 0; i < tileCount; i++) { board.Add(new Tile(i)); }
        }
        /// <summary>
        /// Assign which tiles are mines
        /// </summary>
        private void generateMines()
        {
            int mines = mineCount;
            Random rand = new Random();
            while (mines > 0)
            {
                int i = rand.Next(1, tileCount - 1);
                if (!board[i].isMine)
                {
                    board[i].setMine();
                    mines--;
                }
            }
        }
        /// <summary>
        /// Sets the tiles to their proper mine counts
        /// </summary>
        private void setTileMineCount()
        {
            for (int i = 0; i < tileCount; i++)
            {
                if (board[i].isMine) { board[i].setNearMines(-1); }
                else if (i == 0 || i == x - 1 || i == (x * (y - 1)) || i == (x * y) - 1) { setCornerTiles(i); }
                else { setRegularTiles(i); }

            }
        }
        /// <summary>
        /// Sets the corner tiles mine counts
        /// </summary>
        /// <param name="i"></param>
        private void setCornerTiles(int i)
        {
            if (i == 0) { board[i].setNearMines(getRightOf(i).toInt() + getLowerRightOf(i).toInt() + getLowerOf(i).toInt()); }
            else if (i == x - 1) { board[i].setNearMines(getLeftOf(i).toInt() + getLowerLeftOf(i).toInt() + getLowerOf(i).toInt()); }
            else if (i == (x * (y - 1))) { board[i].setNearMines(getUpperOf(i).toInt() + getUpperRightOf(i).toInt() + getRightOf(i).toInt()); }
            else { board[i].setNearMines(getUpperOf(i).toInt() + getUpperLeftOf(i).toInt() + getLeftOf(i).toInt()); }
        }
        /// <summary>
        /// Set all tile mine counts, except corners
        /// </summary>
        /// <param name="i"></param>
        private void setRegularTiles(int i)
        {
            if (i % x == x - 1) { board[i].setNearMines(getUpperOf(i).toInt() + getUpperRightOf(i).toInt() + getLeftOf(i).toInt() + getLowerLeftOf(i).toInt() + getLowerOf(i).toInt()); }
            else if (i % x == 0) { board[i].setNearMines(getUpperOf(i).toInt() +  getUpperRightOf(i).toInt() + getRightOf(i).toInt() + getLowerRightOf(i).toInt() + getLowerOf(i).toInt()); }
            else if (i < x) { board[i].setNearMines(getLeftOf(i).toInt() + getLowerLeftOf(i).toInt() + getLowerOf(i).toInt() + getLowerRightOf(i).toInt() + getRightOf(i).toInt()); }
            else if (i > x * (y - 1)) { board[i].setNearMines(getLeftOf(i).toInt() + getUpperLeftOf(i).toInt() + getUpperOf(i).toInt() + getUpperRightOf(i).toInt() + getRightOf(i).toInt()); }
            else { board[i].setNearMines(getUpperOf(i).toInt() + getUpperRightOf(i).toInt() + getRightOf(i).toInt() + getLowerRightOf(i).toInt() + getLowerOf(i).toInt() + getLowerLeftOf(i).toInt() + getLeftOf(i).toInt() + getUpperLeftOf(i).toInt()); }
        }
        /*
         * 
         * Methods to interact with the board and its contents
         * 
         */
        /// <summary>
        /// Swap a given tile to the location 0 in the board
        /// </summary>
        /// <param name="t"></param>
        public void moveMine(Tile t)
        {
            board[t.index].setMine();
            board[0].setMine();
            setTileMineCount();
        }
        /// <summary>
        /// Get tile at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index</returns>
        public Tile getTile(int index)
        {
            if (checkValidIndex(index)) { return board[index]; }
            return null;
        }
        /// <summary>
        /// Get tile left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - 1</returns>
        public Tile getLeftOf(int index)
        {
            if (checkValidIndex(index)) { return board[index - 1]; }
            return null;
        }
        /// <summary>
        /// Get tile upper left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - (x - 1)</returns>
        public Tile getUpperLeftOf(int index)
        {
            if (checkValidIndex(index)) { return board[index - (x - 1)]; }
            return null;
        }
        /// <summary>
        /// Get tile upper of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - x</returns>
        public Tile getUpperOf(int index)
        {
            if (checkValidIndex(index)) { return board[index - x]; }
            return null;
        }
        /// <summary>
        /// Get tile upper right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - (x + 1)</returns>
        public Tile getUpperRightOf(int index)
        {
            if (checkValidIndex(index)) { return board[index - (x + 1)]; }
            return null;
        }
        /// <summary>
        /// Get tile right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + 1</returns>
        public Tile getRightOf(int index)
        {
            if (checkValidIndex(index)) { return board[index + 1]; }
            return null;
        }
        /// <summary>
        /// Get tile lower right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + (x + 1)</returns>
        public Tile getLowerRightOf(int index)
        {
            if (checkValidIndex(index)) { return board[index + (x + 1)]; }
            return null;
        }
        /// <summary>
        /// Get tile lower of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + x</returns>
        public Tile getLowerOf(int index)
        {
            if (checkValidIndex(index)) { return board[index + x]; }
            return null;
        }
        /// <summary>
        /// Get tile lower left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + (x - 1)</returns>
        public Tile getLowerLeftOf(int index)
        {
            if (checkValidIndex(index)) { return board[index + (x - 1)]; }
            return null;
        }
        /// <summary>
        /// Check if index is a valid location on the board
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool checkValidIndex(int index)
        {
            if (index < 0 || index > board.Count - 1) { return false; }
            return true;
        }
    }
}
