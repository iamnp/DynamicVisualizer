using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace DynamicVisualizer
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.ThreadException += ApplicationOnThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Dispatcher.CurrentDispatcher.UnhandledException += CurrentDispatcherOnUnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void CurrentDispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            HandleUncaughtException(e.Exception);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                HandleUncaughtException(ex);
            }
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleUncaughtException(e.Exception);
        }

        private static void HandleUncaughtException(Exception ex)
        {
            if (ExceptionForm.Showing)
            {
                return;
            }
            var f = new ExceptionForm(ex);
            f.ShowDialog();
            f.Dispose();
            ExceptionForm.Showing = false;
        }
    }
}