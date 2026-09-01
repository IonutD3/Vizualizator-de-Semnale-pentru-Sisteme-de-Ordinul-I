using System;
using System.Windows.Forms;

namespace SignalVisualizer;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        // Configurează aplicația WinForms și pornește fereastra principală.
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
