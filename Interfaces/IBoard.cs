using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Interfaces
{
    public interface IBoard
    {
        /*
         * 
         * Methods to create a board, and tiles and set tile content
         * 
         */
        void MoveMine(Tile t);
        /// <summary>
        /// Get tile at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index</returns>
        Tile GetTile(int index);
        /// <summary>
        /// Get tile left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - 1</returns>
        Tile GetLeftOf(int index);
        /// <summary>
        /// Get tile upper left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - (x - 1)</returns>
        Tile GetUpperLeftOf(int index);
        /// <summary>
        /// Get tile upper of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - x</returns>
        Tile GetUpperOf(int index);
        /// <summary>
        /// Get tile upper right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index - (x + 1)</returns>
        Tile GetUpperRightOf(int index);
        /// <summary>
        /// Get tile right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + 1</returns>
        Tile GetRightOf(int index);
        /// <summary>
        /// Get tile lower right of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + (x + 1)</returns>
        Tile GetLowerRightOf(int index);
        /// <summary>
        /// Get tile lower of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + x</returns>
        Tile GetLowerOf(int index);
        /// <summary>
        /// Get tile lower left of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Tile at index + (x - 1)</returns>
        Tile GetLowerLeftOf(int index);
    }
}