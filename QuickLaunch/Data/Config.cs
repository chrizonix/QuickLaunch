using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;

using GitHub.SimpleIni;
using static GitHub.SimpleIni.IniFile;

namespace QuickLaunch.Data
{
    public enum ScreenSelector
    {
        PrimaryScreen,
        CurrentScreen,
        MouseLocation
    }

    public struct Config
    {
        // Screen Location
        public Point Location;
        public ScreenSelector Screen;

        // Main Shortcut Folder Location
        public String ShortcutFolder;

        // Default Timeout for Launching Apps (in ms)
        public int DefaultTimeout;

        // Constructor
        public Config(ScreenSelector screen, Point location)
        {
            // Set Screen
            this.Screen = screen;
            this.Location = location;

            // Set Shortcut Folder
            this.ShortcutFolder = String.Empty;

            // Set Default Timeout (in ms)
            this.DefaultTimeout = 6000;
        }
    }

    public class ConfigFile
    {
        // Parse Object Flags for FromObject / ToObject Functions in IniFile Parser
        protected const ParseObjectFlags parseFlags = IniFile.ParseObjectFlags.AllowAdditionalKeys | IniFile.ParseObjectFlags.AllowMissingFields;

        // Config FileName
        protected const String FileName = "config.ini";

        // Config Section Names
        private const String SectionShortcuts = "Shortcuts";

        // Config and IniFile Object
        protected Config config;
        protected IniFile iniFile;

        // Default Constructor
        public ConfigFile()
        {
            // Create Empty Config (using Primary Screen and Default Location)
            this.config = new Config(ScreenSelector.CurrentScreen, new Point(100, 50));

            // Create IniFile from Empty Config Object
            this.iniFile = IniFile.FromObject(this.config, parseFlags);
        }

        public void DeleteFile()
        {
            new FileInfo(FileName).Delete();
        }

        public bool FileExists()
        {
            return new FileInfo(FileName).Exists;
        }

        public void LoadFile()
        {
            // Check Config File
            if (FileExists())
            {
                // Create Empty Config
                this.config = new Config();

                // Load IniFile from Disk
                this.iniFile = new IniFile(FileName);

                // Parse IniFile into Config Object
                IniFile.ToObject(ref this.config, this.iniFile, parseFlags);
            }
        }

        public void SaveFile()
        {
            // Fill / Generate IniFile from Config Object
            IniFile.FromObject<Config>(ref this.iniFile, this.config, parseFlags);

            // Save IniFile to Disk
            this.iniFile.SaveTo(FileName);
        }

        /**
         * Shortcut Display Name
         */
        public String GetShortcutName(String shortcutPath, String defaultValue)
        {
            // Use Value of Entry in Shortcut Section as Display Name and Shortcut Path as Key
            return this.iniFile.GetStr(SectionShortcuts, shortcutPath, defaultValue);
        }

        public void SetShortcutName(String shortcutPath, String shortcutName)
        {
            // Set Value in Shortcut Section as Display Name using Shortcut Path as Key
            this.iniFile.SetValue(SectionShortcuts, shortcutPath, shortcutName);
        }

        /**
         * Shortcut Links
         */
        public OrderedDictionary GetShortcuts()
        {
            // Check Shortcuts Section Exists
            if (!this.iniFile.ContainsSection(SectionShortcuts))
            {
                // Add Shortcuts Section
                this.iniFile.AddSection(SectionShortcuts);
            }

            // Return Shortcuts as Ordered Dictionary
            return this.iniFile.AsDictionary(SectionShortcuts);
        }

        public void SetShortcuts(OrderedDictionary shortcuts)
        {
            // Set Values in Shortcut Section of Config
            this.iniFile.SetValues(SectionShortcuts, shortcuts);
        }

        /**
         * Shortcut Folder
         */
        public String GetShortcutFolder()
        {
            return this.config.ShortcutFolder;
        }

        public void SetShortcutFolder(String shortcutFolder)
        {
            this.config.ShortcutFolder = shortcutFolder;
        }

        /**
         * Screen Location
         */
        public Point GetLocation()
        {
            return this.config.Location;
        }

        public void SetLocation(Point location)
        {
            this.config.Location = location;
        }

        /**
         * Screen Selector
         */
        public ScreenSelector GetScreenSelector()
        {
            return this.config.Screen;
        }

        public void SetScreenSelector(ScreenSelector screenSelector)
        {
            this.config.Screen = screenSelector;
        }

        /**
         * Default Timeout for Launching Apps (in ms)
         */
        public int GetDefaultTimeout()
        {
            return this.config.DefaultTimeout;
        }
    }
}
