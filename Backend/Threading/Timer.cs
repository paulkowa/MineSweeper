using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Timer : BackgroundWorker
    {
        double seconds;
        MainWindow window;
        public Timer(MainWindow window)
        {
            seconds = 0.0;
            this.window = window;
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
            ProgressChanged += Time_ProgressChanged;
            RunWorkerCompleted += Time_RunWorkerCompleted;
            DoWork += Time_DoWork;
        }
        /// <summary>
        /// GUI call for ascyn thread to update the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            seconds = seconds + 0.1;
            window.UpdateTimer(seconds);
        }
        /// <summary>
        /// Wait 1 second, then report progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!CancellationPending)
            {
                Thread.Sleep(1);
                if (!CancellationPending) ReportProgress(0);
            }

            e.Cancel = true;
            return;
        }

        private void Time_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
