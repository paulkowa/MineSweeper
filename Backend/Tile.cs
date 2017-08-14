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
        public int nearMines { get; private set; }
        public int index { get; private set; }

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
        public void setActive()
        {
            isActive = !isActive;
        }
        /// <summary>
        /// Sets isFlag is the opposite of its current state
        /// </summary>
        public void setFlag()
        {
            isFlag = !isFlag;
        }
        /// <summary>
        /// Sets isMine to the opposite of its current state
        /// </summary>
        public void setMine()
        {
            isMine = !isMine;
        }
        public void setNearMines(int mines)
        {
            nearMines = mines;
        }
        /// <summary>
        /// Convert tile to an integer value
        /// </summary>
        /// <returns>1 if the tile is a mine, 0 otherwise</returns>
        public int toInt()
        {
            if (isMine) { return 1; }
            return 0;
        }
    }
}
