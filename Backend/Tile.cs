using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Tile
    {
        public bool isActive { get; private set; }
        public bool isMine { get; private set; }
        public bool isFlag { get; private set; }
        public int index { get; private set; }
        private int nearMines;

        public Tile(int index)
        {
            this.index = index;
            isActive = false;
            isMine = false;
            isFlag = false;
            nearMines = 0;
        }

        /// <summary>
        /// Sets isActive to the opposite of its current state
        /// </summary>
        public void SetActive()
        {
            isActive = !isActive;
        }
        /// <summary>
        /// Sets isFlag is the opposite of its current state
        /// </summary>
        public void SetFlag()
        {
            isFlag = !isFlag;
        }
        /// <summary>
        /// Sets isMine to the opposite of its current state
        /// </summary>
        public void SetMine()
        {
            isMine = !isMine;
        }
        public void SetNearMines(int mines)
        {
            nearMines = mines;
        }
        /// <summary>
        /// Gets the number of mines adjacent to this tile
        /// </summary>
        /// <returns>Int number of mines adjacent to this tile</returns>
        public int GetNearMines()
        {
            return nearMines;
        }
        /// <summary>
        /// Convert tile to an integer value
        /// </summary>
        /// <returns>1 if the tile is a mine, 0 otherwise</returns>
        public int ToInt()
        {
            if (isMine) { return 1; }
            return 0;
        }
    }
}
