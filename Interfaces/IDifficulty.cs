using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Interfaces
{
    public interface IDifficulty
    {
        /// <summary>
        /// Swtich difficulty level to next highest
        /// </summary>
        void SwitchDifficulty();
        /// <summary>
        /// Get the tile count based on the current difficulty
        /// </summary>
        /// <returns>An integer representing the number of tiles</returns>
        int GetTileCount();
        /// <summary>
        /// Get the number of tiles which are mines based on the current difficulty
        /// </summary>
        /// <returns>An integer representing the number of tiles which are mines</returns>
        int GetMineCount();
    }
}
