using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Difficulty : Interfaces.IDifficulty
    {
        private int difficulty;
        public Difficulty()
        {
            difficulty = 0;
        }
        /// <summary>
        /// Swtich difficulty level to next highest
        /// </summary>
        public void SwitchDifficulty()
        {
            switch (difficulty)
            {
                case 0:
                    difficulty = 1;
                    break;
                case 1:
                    difficulty = 2;
                    break;
                case 2:
                    difficulty = 0;
                    break;
            }
        }
        /// <summary>
        /// Get the tile count based on the current difficulty
        /// </summary>
        /// <returns>An integer representing the number of tiles</returns>
        public int GetTileCount()
        {
            switch (difficulty)
            {
                case 0:
                    return 64;
                case 1:
                    return 256;
                case 2:
                    return 480;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// Get the number of tiles which are mines based on the current difficulty
        /// </summary>
        /// <returns>An integer representing the number of tiles which are mines</returns>
        public int GetMineCount()
        {
            switch (difficulty)
            {
                case 0:
                    return 10;
                case 1:
                    return 40;
                case 2:
                    return 99;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// Get the number of pixels required for the board size based on difficulty
        /// </summary>
        /// <returns>Double representing the pixel width of the board</returns>
        public double GetXPixels()
        {
            switch (difficulty)
            {
                case 0:
                    return 232;
                case 1:
                    return 464;
                case 2:
                    return 870;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// Get the number of pixels required for the board size based on difficulty
        /// </summary>
        /// <returns>Double representing the pixel width of the board</returns>
        public double GetYPixels()
        {
            switch (difficulty)
            {
                case 0:
                    return 232;
                case 1:
                    return 464;
                case 2:
                    return 464;
                default:
                    return 0;
            }
        }
        public int GetDifficulty()
        {
            return difficulty;
        }
    }
}
