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
            board = new List<Tile>();
            tileCount = difficulty.GetTileCount();
            mineCount = difficulty.GetMineCount();
            SetXY();
            GenerateTiles();
            GenerateMines();
            SetTileMineCount();
        }
        /// <summary>
        /// Set x and y dimensions based on current tileCount
        /// </summary>
        private void SetXY()
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
         * Methods to create a board, and tiles and Set tile content
         * 
         */
        /// <summary>
        /// Run all methods necessary to create a new board
        /// </summary>
        private void SetUpBoard()
        {
            GenerateTiles();
            GenerateMines();
            SetTileMineCount();
        }
        /// <summary>
        /// Create all the tile objects
        /// </summary>
        private void GenerateTiles()
        {
            for (int i = 0; i < tileCount; i++) { board.Add(new Tile(i)); }
        }
        /// <summary>
        /// Assign which tiles are mines
        /// </summary>
        private void GenerateMines()
        {
            int mines = mineCount;
            Random rand = new Random();
            while (mines > 0)
            {
                int i = rand.Next(1, tileCount - 1);
                if (!board[i].isMine)
                {
                    board[i].SetMine();
                    mines--;
                }
            }
        }
        /// <summary>
        /// Sets the tiles to their proper mine counts
        /// </summary>
        private void SetTileMineCount()
        {
            for (int i = 0; i < tileCount; i++)
            {
                if (board[i].isMine) { board[i].SetNearMines(-1); }
                else if (i == 0 || i == x - 1 || i == (x * (y - 1)) || i == (x * y) - 1) { SetCornerTiles(i); }
                else { SetRegularTiles(i); }
                //if(i == 0) { board[0].SetNearMines(GetLowerRightOf(i).ToInt() + GetLowerOf(i).ToInt() + GetRightOf(i).ToInt()); }

            }
        }
        /// <summary>
        /// Sets the corner tiles mine counts
        /// </summary>
        /// <param name="i"></param>
        private void SetCornerTiles(int i)
        {
            if (i == 0) { board[i].SetNearMines(GetRightOf(i).ToInt() + GetLowerRightOf(i).ToInt() + GetLowerOf(i).ToInt()); }
            else if (i == x - 1) { board[i].SetNearMines(GetLeftOf(i).ToInt() + GetLowerLeftOf(i).ToInt() + GetLowerOf(i).ToInt()); }
            else if (i == (x * (y - 1))) { board[i].SetNearMines(GetUpperOf(i).ToInt() + GetUpperRightOf(i).ToInt() + GetRightOf(i).ToInt()); }
            else { board[i].SetNearMines(GetUpperOf(i).ToInt() + GetUpperLeftOf(i).ToInt() + GetLeftOf(i).ToInt()); }
        }
        /// <summary>
        /// Set all tile mine counts, except corners
        /// </summary>
        /// <param name="i"></param>
        private void SetRegularTiles(int i)
        {
            if (i % x == x - 1) { board[i].SetNearMines(GetUpperOf(i).ToInt() + GetUpperRightOf(i).ToInt() + GetLeftOf(i).ToInt() + GetLowerLeftOf(i).ToInt() + GetLowerOf(i).ToInt()); }
            else if (i % x == 0) { board[i].SetNearMines(GetUpperOf(i).ToInt() +  GetUpperRightOf(i).ToInt() + GetRightOf(i).ToInt() + GetLowerRightOf(i).ToInt() + GetLowerOf(i).ToInt()); }
            else if (i < x) { board[i].SetNearMines(GetLeftOf(i).ToInt() + GetLowerLeftOf(i).ToInt() + GetLowerOf(i).ToInt() + GetLowerRightOf(i).ToInt() + GetRightOf(i).ToInt()); }
            else if (i > x * (y - 1)) { board[i].SetNearMines(GetLeftOf(i).ToInt() + GetUpperLeftOf(i).ToInt() + GetUpperOf(i).ToInt() + GetUpperRightOf(i).ToInt() + GetRightOf(i).ToInt()); }
            else { board[i].SetNearMines(GetUpperOf(i).ToInt() + GetUpperRightOf(i).ToInt() + GetRightOf(i).ToInt() + GetLowerRightOf(i).ToInt() + GetLowerOf(i).ToInt() + GetLowerLeftOf(i).ToInt() + GetLeftOf(i).ToInt() + GetUpperLeftOf(i).ToInt()); }
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
        public void MoveMine(Tile t)
        {
            board[t.index].SetMine();
            board[0].SetMine();
            SetTileMineCount();
        }
        /// <summary>
        /// Get tile at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index</returns>
        public Tile GetTile(int index)
        {
            if (CheckValidIndex(index)) { return board[index]; }
            return null;
        }
        /// <summary>
        /// Get tile left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - 1</returns>
        public Tile GetLeftOf(int index)
        {
            if (CheckValidIndex(index)) { return board[index - 1]; }
            return null;
        }
        /// <summary>
        /// Get tile upper left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - (x - 1)</returns>
        public Tile GetUpperLeftOf(int index)
        {
            if (CheckValidIndex(index)) { return board[index - (x + 1)]; }
            return null;
        }
        /// <summary>
        /// Get tile upper of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - x</returns>
        public Tile GetUpperOf(int index)
        {
            if (CheckValidIndex(index)) { return board[index - x]; }
            return null;
        }
        /// <summary>
        /// Get tile upper right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - (x + 1)</returns>
        public Tile GetUpperRightOf(int index)
        {
            if (CheckValidIndex(index)) { return board[index - (x - 1)]; }
            return null;
        }
        /// <summary>
        /// Get tile right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + 1</returns>
        public Tile GetRightOf(int index)
        {
            if (CheckValidIndex(index)) { return board[index + 1]; }
            return null;
        }
        /// <summary>
        /// Get tile lower right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + (x + 1)</returns>
        public Tile GetLowerRightOf(int index)
        {
            if (CheckValidIndex(index)) { return board[index + x + 1]; }
            return null;
        }
        /// <summary>
        /// Get tile lower of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + x</returns>
        public Tile GetLowerOf(int index)
        {
            if (CheckValidIndex(index)) { return board[index + x]; }
            return null;
        }
        /// <summary>
        /// Get tile lower left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + (x - 1)</returns>
        public Tile GetLowerLeftOf(int index)
        {
            if (CheckValidIndex(index)) { return board[index + (x - 1)]; }
            return null;
        }
        /// <summary>
        /// Check if index is a valid location on the board
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool CheckValidIndex(int index)
        {
            if (index < 0 || index > board.Count - 1) { return false; }
            return true;
        }
    }
}
