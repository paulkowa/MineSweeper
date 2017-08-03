using Ms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls.Primitives;

namespace Ms
{
    /// <summary>
    /// Game handles the GUI operations and the general game controls such as starting a new game and ending the current game
    /// </summary>
    public class Game
    {
        private List<MineButton> mineButtons;
        public MainWindow window { get; private set; }
        public Board board { get; private set; }
        public NewGameButton newGameButton { get; private set; }
        public bool firstClick, gameActive;
        private Queue<Tile> update;
        private const int size = 576;
        private Timer time;
        //private NewGameButton newGameButton;

        public Game(MainWindow window)
        {
            // Initalize variable
            this.window = window;
            // Add new button GUI element
            createNewGameButton(this);
            // Start game
            newGame();
        }

        /// <summary>
        /// Method called upon creating a new game or pressing the new game button
        /// </summary>
        public void newGame()
        {
            stopTimer();
            clearTimer();
            clearBoard();
            fillBoard();
            window.MineCounter.Text = Convert.ToString(board.mineTotal);
        }

        /// <summary>
        /// Ends the game for the user
        /// Reveals all the tiles on the board
        /// </summary>
        public void gameLost()
        {
            newGameButton.gameLost();
            newGameButton.IsEnabled = false;
            Queue<MineButton> setToX = new Queue<MineButton>();
            BackgroundWorker disableTiles = new BackgroundWorker();
            disableTiles.WorkerReportsProgress = false;
            disableTiles.DoWork += (sender, e) =>
            {
                for (int i = 0; i < mineButtons.Count; i++)
                {
                    if (mineButtons[i].tile.isMine == false) { mineButtons[i].tile.isActive = true; }
                    if (mineButtons[i].tile.isFlagged && mineButtons[i].tile.isMine == false) { setToX.Enqueue(mineButtons[i]); }
                }
            };
            disableTiles.RunWorkerCompleted += (sender, e) =>
            {
                while (setToX.Count > 0) { setToX.Dequeue().setX(); }
            };

            disableTiles.RunWorkerAsync();
            stopTimer();
            revealBoard();
        }

        public void gameWon()
        {

        }

        /// <summary>
        /// Add a newGameButton to the GUI
        /// </summary>
        private void createNewGameButton(Game game)
        {
            newGameButton = new NewGameButton(game);
            window.TopGrid.Children.Add(newGameButton);
            Grid.SetColumn(newGameButton, 3);
        }
        /// <summary>
        /// Creates an async thread to track the time and update the GUI
        /// </summary>
        public void startTimer()
        {
            firstClick = false;
            time = new Timer(window);
            time.RunWorkerAsync();
        }

        /// <summary>
        /// Cancel the time async thread
        /// </summary>
        private void stopTimer()
        {
            if (gameActive)
            {
                gameActive = false;
                time.CancelAsync();
                time.Dispose();
                time = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// Set the timer value to 0
        /// </summary>
        private void clearTimer()
        {
            window.Timer.Text = "0";
        }

        /// <summary>
        /// Remove flags from every tile already flagged
        /// </summary>
        public void removeFlags()
        {
            foreach (MineButton mb in mineButtons)
            {
                if (mb.tile.isFlagged) { mb.setFlag(); }
            }
        }

        /// <summary>
        /// Clear all values from the GUI board and background datastructures
        /// </summary>
        private void clearBoard()
        {
            if (window.UniGrid.Children.Count > 0)
            {
                for (int i = window.UniGrid.Children.Count - 1; i >= 0; i--)
                {
                    window.UniGrid.Children.RemoveAt(i);
                }
            }
            update = new Queue<Tile>();
            firstClick = true;
            gameActive = true;
            board = new Board(size);
            mineButtons = new List<MineButton>();
        }

        /// <summary>
        /// Fills the UI with new buttons representing the tiles for the game
        /// </summary>
        private void fillBoard()
        {
            window.UniGrid.Columns = 24;
            window.UniGrid.Rows = 24;

            int index = 0;
            for (int row = 0; row < 24; row++)
            {
                for (int column = 0; column < 24; column++)
                {
                    MineButton mineButton = new MineButton(board.board[index], this, index);
                    mineButtons.Add(mineButton);
                    window.UniGrid.Children.Add(mineButton);
                    index++;
                }
            }
        }

        /// <summary>
        /// Reveals all the tiles in the board
        /// </summary>
        private void revealBoard()
        {
            // Create worker to reveal mines
            MineButton mb = null;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (sender, e) =>
            {
                for (int i = 0; i < mineButtons.Count; i++)
                {
                    if (mineButtons[i].tile.isMine && mineButtons[i].tile.isActive == false)
                    {
                        mb = mineButtons[i];
                        worker.ReportProgress(10);
                        Thread.Sleep(5);
                    }
                }
            };

            worker.ProgressChanged += (sender, e) =>
            {
                if (mb.tile.isFlagged && mb.tile.isMine)
                {
                    mb.setFlag();
                    mb.setTile();
                }
                mb.setTile();
            };

            worker.RunWorkerCompleted += (sender, e) =>
            {
                newGameButton.IsEnabled = true;
            };

            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Create a worker thread to check surrounding tiles and reveal all empty tiles in a chain
        /// </summary>
        /// <param name="t"></param>
        public void checkTiles(Tile t)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.DoWork += (sender, e) =>
            {
                update = checkQueue(checkNeighbors(t), new List<Tile>());
            };

            worker.RunWorkerCompleted += (sender, e) =>
            {
                setTiles(update);
            };
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// GUI call to update buttons once asycn searching thread is completed
        /// </summary>
        /// <param name="queue"></param>
        public void setTiles(Queue<Tile> queue)
        {
            for (int i = queue.Count; i >= 0; i--)
            {
                Tile t = queue.Dequeue();
                if (!t.isFlagged)
                {
                    mineButtons[t.index].setTile();
                }
                queue.Enqueue(t);
            }

            bool endGame = false;
            BackgroundWorker testForMines = new BackgroundWorker();
            testForMines.WorkerReportsProgress = false;
            testForMines.DoWork += (sender, e) =>
            {
                while (queue.Count > 0)
                {
                    Tile temp = queue.Dequeue();
                    if (!temp.isFlagged && temp.isMine) { endGame = true; }
                }
            };

            testForMines.RunWorkerCompleted += (sender, e) =>
            {
                if (endGame) { gameLost(); }
            };

            testForMines.RunWorkerAsync();

        }
        /// <summary>
        /// Check a queue of tiles for all adjacent empty tiles
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="doNotCheck"></param>
        /// <returns>A queue of tiles in a cascade chain</returns>
        private Queue<Tile> checkQueue(Queue<Tile> queue, List<Tile> doNotCheck)
        {
            int size = queue.Count;
            Queue<Tile> temp = new Queue<Tile>();

            while (size > 0)
            {
                Tile t = queue.Dequeue();
                if (t.nearbyMines == 0 && !doNotCheck.Contains(t))
                {
                    doNotCheck.Add(t);
                    temp = checkQueue(checkNeighbors(t), doNotCheck);
                    queue.Enqueue(t);
                    while (temp.Count > 0)
                    {
                        queue.Enqueue(temp.Dequeue());
                    }
                }
                if (!doNotCheck.Contains(t)) { doNotCheck.Add(t); }
                queue.Enqueue(t);
                size--;
            }
            return queue;
        }

        /// <summary>
        /// Create a queue of adjacent tiles to the given tile
        /// Needs to be updated to dynamically change with board size
        /// </summary>
        /// <param name="t"></param>
        /// <returns>A queue of neighboring tiles, and the original tile</returns>
        private Queue<Tile> checkNeighbors(Tile t)
        {
            int index = t.index;
            Queue<Tile> tiles = new Queue<Tile>();
            tiles.Enqueue(t);
            if (index == 0 || index == 23 || index == 552 || index == 575)
            {
                switch (index)
                {
                    case 0:
                        tiles.Enqueue(board.board[index + 1]);
                        tiles.Enqueue(board.board[index + 25]);
                        tiles.Enqueue(board.board[index + 24]);
                        break;
                    case 23:
                        tiles.Enqueue(board.board[index - 1]);
                        tiles.Enqueue(board.board[index + 23]);
                        tiles.Enqueue(board.board[index + 24]);
                        break;
                    case 552:
                        tiles.Enqueue(board.board[index + 1]);
                        tiles.Enqueue(board.board[index - 23]);
                        tiles.Enqueue(board.board[index - 24]);
                        break;
                    case 575:
                        tiles.Enqueue(board.board[index - 1]);
                        tiles.Enqueue(board.board[index - 25]);
                        tiles.Enqueue(board.board[index - 24]);
                        break;
                }
            }
            else if (index % 24 == 0)
            {
                tiles.Enqueue(board.board[index + 1]);
                tiles.Enqueue(board.board[index + 25]);
                tiles.Enqueue(board.board[index + 24]);
                tiles.Enqueue(board.board[index - 24]);
                tiles.Enqueue(board.board[index - 23]);
            }
            else if (index % 24 == 23)
            {
                tiles.Enqueue(board.board[index + 24]);
                tiles.Enqueue(board.board[index + 23]);
                tiles.Enqueue(board.board[index - 1]);
                tiles.Enqueue(board.board[index - 25]);
                tiles.Enqueue(board.board[index - 24]);
            }
            else if (index < 23)
            {
                tiles.Enqueue(board.board[index + 1]);
                tiles.Enqueue(board.board[index + 25]);
                tiles.Enqueue(board.board[index + 24]);
                tiles.Enqueue(board.board[index + 23]);
                tiles.Enqueue(board.board[index - 1]);
            }
            else if (index > 552)
            {
                tiles.Enqueue(board.board[index + 1]);
                tiles.Enqueue(board.board[index - 1]);
                tiles.Enqueue(board.board[index - 25]);
                tiles.Enqueue(board.board[index - 24]);
                tiles.Enqueue(board.board[index - 23]);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    tiles.Enqueue(board.board[index + 1]);
                    tiles.Enqueue(board.board[index + 25]);
                    tiles.Enqueue(board.board[index + 24]);
                    tiles.Enqueue(board.board[index + 23]);
                    tiles.Enqueue(board.board[index - 1]);
                    tiles.Enqueue(board.board[index - 25]);
                    tiles.Enqueue(board.board[index - 24]);
                    tiles.Enqueue(board.board[index - 23]);
                }
            }
            return tiles;
        }
    }
}
