using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ms
{
    class CheckVictory : BackgroundWorker
    {
        private Game game;
        private bool victory;
        public CheckVictory(Game game)
        {
            this.game = game;
            victory = true;
            WorkerSupportsCancellation = false;
            RunWorkerCompleted += CheckVictory_RunWorkerCompleted;
            DoWork += CheckVictory_DoWork;
            
        }
        /// <summary>
        /// GUI call for ascyn thread to update the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckVictory_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }
        /// <summary>
        /// Wait 1 second, then report progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckVictory_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            while(victory)
            {
                if(!game.board.board[i].isActive && !game.board.board[i].isMine) { victory = false; }
                i++;
            }
        }

        private void CheckVictory_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (victory) { game.gameWon(); }
        }
    }
}