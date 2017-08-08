using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ms
{
    public class Tile
    {
        public bool isActive { get; private set; }
        public bool isMine { get; set; }
        public bool isFlagged { get; set; }
        public bool isSet { get; set; }
        public int nearbyMines { get; set; }
        public int index { get; private set; }

        /// <summary>
        /// Container to hold all the information that a tile in the board needs
        /// </summary>
        /// <param name="index"></param>
        public Tile(int index)
        {
            isActive = false;
            isMine = false;
            isFlagged = false;
            isSet = false;
            this.index = index;
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

        public void setActive()
        {
            if (!isActive) { isActive = !isActive; }
            
        }
    }
}
