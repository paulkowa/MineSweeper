using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ms
{
    /// <summary>
    /// Handles keeping track of the time and updating the GUI timer
    /// </summary>
    class Timer : BackgroundWorker
    {
        int seconds;
        MainWindow window;
        public Timer(MainWindow window)
        {
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
            window.Timer.Text = Convert.ToString(++seconds);
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
                Thread.Sleep(1000);
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
