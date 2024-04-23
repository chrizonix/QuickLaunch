using System;
using System.IO;
using System.Windows.Forms;

namespace QuickLaunch.Libraries
{
    public enum AppMode
    {
        Normal,
        ShowContextMenu
    }

    public class CommandLineArgs
    {
        // Application Startup Mode
        public static AppMode Mode { get; private set; }

        // Hide Taskbar for "Show Context Menu" Mode
        public static bool HideTaskbar { get; private set; }

        private static void SetDefaultValues()
        {
            // Set Default Mode to Normal
            Mode = AppMode.Normal;

            // Don't Hide Taskbar by Default
            HideTaskbar = false;
        }

        public static void ParseCommandLine()
        {
            // Set Default Values
            SetDefaultValues();

            // Get Command Line Arguments
            String[] args = Environment.GetCommandLineArgs();

            // Check Number of Arguments
            if (args.Length > 1)
            {
                // Parse Application and Startup Mode from Command Line Arguments
                (String appName, String modeString) = (args[0], args[1]);

                // Parse Mode (First Parameter)
                if (modeString.ToLower() == "--normal")
                {
                    // Set App Mode to Normal
                    Mode = AppMode.Normal;
                }
                else if (modeString.ToLower() == "--show")
                {
                    // Set App Mode to Show Context Menu
                    Mode = AppMode.ShowContextMenu;

                    // Check Shift Key is Pressed during Startup
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        // Revert to Normal Mode
                        Mode = AppMode.Normal;
                    }
                }
                else
                {
                    // Cannot Parse Mode -> Show Help
                    ShowHelp(appName);
                }
            }

            // Check Additional Arguments
            if (args.Length > 2)
            {
                // Parse Mode for Hiding the Taskbar
                String hideTaskbar = args[2];

                // Check Command Line for Hide Taskbar Option
                if (args[2].ToLower() == "--hide-taskbar")
                {
                    // Hide Taskbar
                    HideTaskbar = true;
                }
            }
        }

        private static void ShowHelp(String appName)
        {
            // Get Filename of Application
            appName = Path.GetFileName(appName);

            // Write to Console / Standard Output
            Console.WriteLine("Invalid Command Line Arguments!");
            Console.WriteLine($"Usage: {appName} [--normal|--show] [--hide-taskbar]");

            // Show Message Box and Usage Info to User
            MessageBox.Show($"Usage: {appName} [--normal|--show] [--hide-taskbar]", "Invalid Command Line Arguments!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
