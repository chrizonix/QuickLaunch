using System;
using System.Collections.Generic;

using static QuickLaunch.Utils.IconHelper;

namespace QuickLaunch.Data
{
    public enum ShortcutType
    {
        Shortcut,
        Folder
    }

    public struct Shortcut
    {
        public IconStruct Picture;
        
        public String FilePath;
        public String DisplayName;

        public ShortcutType Type;
        public List<Shortcut> Shortcuts;

        public Shortcut(String FilePath, String DisplayName, ShortcutType Type)
        {
            // Create Empty Icon Struct for Picture
            this.Picture = new IconStruct();

            // Set FilePath and DisplayName
            this.FilePath = FilePath; 
            this.DisplayName = DisplayName;

            // Set Shortcut Type
            this.Type = Type;
            this.Shortcuts = new List<Shortcut>();
        }
    }
}
