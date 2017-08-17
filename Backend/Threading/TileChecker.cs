using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class TileChecker : BackgroundWorker
    {
        MainWindow window;
        public TileChecker(Queue<Tile> tiles)
        {
            WorkerReportsProgress = false;
            WorkerSupportsCancellation = false;
            RunWorkerCompleted += TileChecker_RunWorkerCompleted;
            DoWork += TileChecker_DoWork;
        }
        private void TileChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void TileChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}

