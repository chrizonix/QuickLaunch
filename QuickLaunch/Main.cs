using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using QuickLaunch.Data;
using QuickLaunch.Utils;
using QuickLaunch.Libraries;

using Shortcut = QuickLaunch.Data.Shortcut;

namespace QuickLaunch
{
    public partial class Main : Form
    {
        // Main Context Menu for Shortcut Links
        private ContextMenuStrip contextMenu = null;

        // Create Global Application Config File
        private readonly ConfigFile config = new ConfigFile();

        // Ignore List for Shortcut Files (e.g. Hidden System Files)
        private readonly HashSet<String> ignoredFiles = new HashSet<String> { "desktop.ini" };

        public Main()
        {
            // Initialize Components
            InitializeComponent();

            // Parse and Set Command Line Arguments
            CommandLineArgs.ParseCommandLine();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Check Config File
            if (config.FileExists())
            {
                // Load Config File
                config.LoadFile();
            }
            // No Config -> Check Command Line Startup Mode
            else if (CommandLineArgs.Mode == AppMode.ShowContextMenu)
            {
                // In "Show Context Menu" Mode, but No Config -> Show Warning Message
                MessageBox.Show("No Config -> Please Create Config first!", "Config Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // No Config -> Exit Application
                Application.Exit();
            }

            // Check Startup Mode
            switch (CommandLineArgs.Mode)
            {
                case AppMode.Normal:
                    // Normal Start
                    StartNormal();
                    break;
                case AppMode.ShowContextMenu:
                    // Start Context Menu
                    StartContextMenu();
                    break;
            }
        }

        private void StartNormal()
        {
            // Start Window in Normal State
            WindowState = FormWindowState.Normal;

            // Show Application in Taskbar
            ShowInTaskbar = true;
            ShowIcon = true;
        }

        private void StartContextMenu()
        {
            // Start Window in Minimized State
            WindowState = FormWindowState.Minimized;

            // Don't Show Anything in Taskbar
            ShowInTaskbar = false;
            ShowIcon = false;

            // Build or Load Context Menu using Shortcut Folder of Config
            BuildContextMenu(LoadOrCreateShortcuts(config.GetShortcutFolder()));

            // Add Event Handler for Context Menu Closing
            contextMenu.Closed += ContextMenu_Closed;

            // Show Context Menu using Location and Offset Values of Config File
            ShowContextMenu(config.GetScreenSelector(), config.GetLocation());
        }

        private List<Shortcut> LoadOrCreateShortcuts(string shortcutFolder)
        {
            // Check Cache File for Deserialization Exists
            if (SerializationUtils.CacheFileExists())
            {
                // Deserialize Shortcuts from Cache File
                return SerializationUtils.DeserializeShortcuts();
            }

            // Generate / Create List of Shortcuts from Folder
            return LoadShortcuts(shortcutFolder);
        }

        private void ContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // Check Command Line Startup Parameter
            if (CommandLineArgs.Mode == AppMode.ShowContextMenu)
            {
                // Always Exit Application in Context Menu Mode (using Timeout)
                Task.Delay(config.GetDefaultTimeout()).ContinueWith(task => Application.Exit());
            }
        }

        private void BuildContextMenu(List<Shortcut> shortcuts)
        {
            // Create New Context Menu
            contextMenu = new ContextMenuStrip();

            // Build Menu Items of Context Menu Recursively
            contextMenu.Items.AddRange(BuildContextMenuItems(shortcuts));
        }

        private ToolStripItem[] BuildContextMenuItems(List<Shortcut> shortcuts)
        {
            // Build Menu Items for Shortcuts
            List<ToolStripItem> items = new List<ToolStripItem>();

            // For Each Shortcut in List
            foreach (Shortcut shortcut in shortcuts)
            {
                // Create Empty Tool Strip Item using Display Name of Shortcut
                ToolStripMenuItem item = new ToolStripMenuItem(shortcut.DisplayName);

                // Restore Bytes from Base64 after Deserialization
                shortcut.Picture.RestoreBytesFromBase64();

                // Get Image of Shortcut Picture
                item.Image = shortcut.Picture.GetImage();

                // Use File Path as Tag
                item.Tag = shortcut.FilePath;

                // Build Menu Items for Sub-Folder
                if (shortcut.Type == ShortcutType.Folder)
                {
                    // Add Context Menu Items of Sub-Folder to Current DropDown Items
                    item.DropDownItems.AddRange(BuildContextMenuItems(shortcut.Shortcuts));
                }
                else if (CommandLineArgs.Mode == AppMode.ShowContextMenu)
                {
                    // Add Event Handler for Current Shortcut
                    item.Click += ContextMenuItem_Click;
                }

                // Add Menu Item to List
                items.Add(item);
            }

            // Return Menu Items
            return items.ToArray();
        }

        private void ContextMenuItem_Click(object sender, EventArgs e)
        {
            // Check and Cast Sender to Tool Strip Menu Item
            if (sender is ToolStripMenuItem toolStripItem)
            {
                // Extract Shortcut File Path in Tag Name
                String filePath = toolStripItem.Tag.ToString();

                // Launch Shortcut via Shell Execute Run Command via Shell API
                WindowsShellAPI.ShellExecuteRun(filePath, WindowsShellAPI.ShowWindowCommands.SW_NORMAL);
            }

            // Check Command Line Startup Parameter
            if (CommandLineArgs.Mode == AppMode.ShowContextMenu)
            {
                // Always Exit Application in Context Menu Mode (using Timeout)
                Task.Delay(config.GetDefaultTimeout()).ContinueWith(task => Application.Exit());
            }
        }

        private void BuildTreeView()
        {
            // Load Shortcuts and Icons from Disk and Config
            List<Shortcut> shortcuts = LoadShortcuts(txtShortcutFolder.Text);

            // Initialize Tree Image List with 32 Bit Color Depth
            treeViewLinks.ImageList = new ImageList()
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(16, 16)
            };

            // Get Flat List of All Shortcuts
            List<Shortcut> flatMap = CollectionUtils.FlatMap(shortcuts);

            // Clear Image List of Tree View
            treeViewLinks.ImageList.Images.Clear();

            // Build Image List for Tree View
            foreach (Shortcut shortcut in flatMap)
            {
                // Add Icon of Shortcut Picture to Image List with FilePath as Key
                treeViewLinks.ImageList.Images.Add(shortcut.FilePath, shortcut.Picture.Icon);
            }

            // Begin Tree View Update
            treeViewLinks.BeginUpdate();

            // Clear Nodes of Tree View
            treeViewLinks.Nodes.Clear();

            // Build Nodes of Tree View Recursively
            treeViewLinks.Nodes.AddRange(BuildTreeViewNodes(shortcuts));

            // End Tree View Update
            treeViewLinks.EndUpdate();

            // Refresh Tree View
            treeViewLinks.Refresh();
        }

        private TreeNode[] BuildTreeViewNodes(List<Shortcut> shortcuts)
        {
            // Build Tree Nodes for Shortcuts
            List<TreeNode> nodes = new List<TreeNode>();

            // For Each Shortcut in List
            foreach (Shortcut shortcut in shortcuts)
            {
                // Create Empty Tree Node
                TreeNode node = new TreeNode(shortcut.DisplayName);

                // Set Image Key of Node
                node.ImageKey = shortcut.FilePath;
                node.SelectedImageKey = shortcut.FilePath;

                // Use File Path as Tag
                node.Tag = shortcut.FilePath;

                // Build Tree Nodes for Sub-Folder
                if (shortcut.Type == ShortcutType.Folder)
                {
                    // Add Tree Nodes of Sub-Folder Shortcuts
                    node.Nodes.AddRange(BuildTreeViewNodes(shortcut.Shortcuts));
                }

                // Add Tree Node to List
                nodes.Add(node);
            }

            // Return Tree Nodes
            return nodes.ToArray();
        }

        private List<Shortcut> LoadShortcuts(String folderPath)
        {
            // Get List of Shortcut Files in Config File (Keep Order of Items)
            List<String> configFiles = config.GetShortcuts().Keys.OfType<String>().ToList();

            // Call Recursive Function and Build Shortcut Icons
            return LoadShortcuts(folderPath, configFiles);
        }

        private List<Shortcut> LoadShortcuts(String folderPath, List<String> configFiles)
        {
            // Build List of Extracted Shortcuts
            List<Shortcut> shortcuts = new List<Shortcut>();

            // Build HashSet of Files including Files in All Sub-Directories (Ignoring System Files)
            HashSet<String> diskFiles = FileUtils.ListFiles(folderPath, "*").Except(ignoredFiles).ToHashSet();

            // Add Shortcuts of Disk Files that are not in Config yet
            configFiles.AddRange(diskFiles.Except(configFiles.ToHashSet()));

            // Build Lookup Table of Folders and Sub-Directories in Current Config File List
            Dictionary<String, List<String>> configFolders = configFiles.GroupBy(file => FileUtils.GetPathRoot(file, NotFoundMode.EmptyString))
                .ToDictionary(group => group.Key, group => group.Select(file => FileUtils.RemovePathRoot(file)).ToList());

            // Build Unique List of Files and Folders including only the Top Level Shortcut Files in Config
            OrderedDictionary uniqueConfigFiles = FileUtils.BuildUniqueDictionaryTopLevel(configFiles);

            // For Each Shortcut in Config (Keep Order)
            foreach (String shortcutFile in uniqueConfigFiles.Keys)
            {
                // Check Directory in Lookup Table
                if (configFolders.ContainsKey(shortcutFile))
                {
                    // Build Full Path to Shortcut File
                    String fullPath = Path.Combine(folderPath, shortcutFile);

                    // Extract Shortcut Display Name from Config or Use Shortcut File Name
                    String lookupKey = FileUtils.RemoveBaseFolderPath(fullPath, config.GetShortcutFolder());
                    String displayName = config.GetShortcutName(lookupKey, shortcutFile);

                    // Extract Icon and Generate Shortcut Object for Current Folder
                    Shortcut shortcut = new Shortcut(fullPath, displayName, ShortcutType.Folder)
                    {
                        // Extract Shortcuts from Sub-Folder and Add them to Folder Shortcut
                        Shortcuts = LoadShortcuts(fullPath, configFolders[shortcutFile]),
                        Picture = IconHelper.GetIcon(fullPath, IconSize.Small)
                    };

                    // Add Shortcut to Shortcuts
                    shortcuts.Add(shortcut);
                }
                // We have found a Shortcut -> Check if Shortcut Exists on Disk
                else if (diskFiles.Contains(shortcutFile))
                {
                    // Build Shortcut for Current File in Current Path
                    shortcuts.Add(BuildShortcut(folderPath, shortcutFile));
                }

                // Remove Shortcut File from Disk Files (Marked them as handled)
                diskFiles.Remove(shortcutFile);
            }

            // Return Extracted Shortcuts
            return shortcuts;
        }

        private Shortcut BuildShortcut(String folderPath, String shortcutFile)
        {
            // Build Full Path to Shortcut File
            String fullPath = Path.Combine(folderPath, shortcutFile);

            // Build Lookup Key for Config and Get Default Display Name from Shortcut File Name
            String lookupKey = FileUtils.RemoveBaseFolderPath(fullPath, config.GetShortcutFolder());
            String displayName = Path.GetFileNameWithoutExtension(shortcutFile);

            // Extract Shortcut Display Name from Config or Use Shortcut File Name
            displayName = config.GetShortcutName(lookupKey, displayName);

            // Extract Icon from Shortcut and Build Shortcut Object
            return new Shortcut(fullPath, displayName, ShortcutType.Shortcut)
            {
                // Extract Icon using Shortcut File Path and Icon Helper
                Picture = IconHelper.GetIcon(fullPath, IconSize.Small)
            };
        }

        private void CreateOrLoadConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Re-Load Config File
            config.LoadFile();

            // Hide Info Label
            lblInfo.Visible = false;
            boxConfig.Visible = false;

            // Set Shortcut Folder
            txtShortcutFolder.Text = config.GetShortcutFolder();

            // Set Screen Selector
            switch (config.GetScreenSelector())
            {
                case ScreenSelector.PrimaryScreen:
                    rbPrimaryScreen.Checked = true;
                    break;
                case ScreenSelector.CurrentScreen:
                    rbCurrentScreen.Checked = true;
                    break;
                case ScreenSelector.MouseLocation:
                    rbMouseLocation.Checked = true;
                    break;
            }

            // Set Screen Location
            nudLeft.Value = config.GetLocation().X;
            nudBottom.Value = config.GetLocation().Y;

            // Build Tree View
            BuildTreeView();

            // Show Config GroupBox
            boxConfig.Visible = true;

            // Enable Save Config Tool Strip Menu
            saveConfigToolStripMenuItem.Enabled = true;
        }

        private void SaveConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set Shortcut Folder
            config.SetShortcutFolder(txtShortcutFolder.Text);

            // Set Screen Selector
            if (rbCurrentScreen.Checked)
            {
                config.SetScreenSelector(ScreenSelector.CurrentScreen);
            }
            else if (rbPrimaryScreen.Checked)
            {
                config.SetScreenSelector(ScreenSelector.PrimaryScreen);
            }
            else if (rbMouseLocation.Checked)
            {
                config.SetScreenSelector(ScreenSelector.MouseLocation);
            }

            // Set Screen Location
            config.SetLocation(new Point((int)nudLeft.Value, (int)nudBottom.Value));

            // Build Flat Map of Shortcuts in Tree View and Set Shortcuts in Config
            config.SetShortcuts(BuildShortcutsDictionary(treeViewLinks, txtShortcutFolder.Text));

            // Save Config
            config.SaveFile();

            // Load Shortcuts and Icons from Disk using Current Selected Folder
            List<Shortcut> shortcuts = LoadShortcuts(txtShortcutFolder.Text);

            // Serialize / Save Shortcuts to Cache File
            SerializationUtils.SerializeShortcuts(shortcuts);

            // Re-Build Context Menu
            BuildContextMenu(shortcuts);

            // Show Success Info
            ShowSuccessInfo();
        }

        private void ShowSuccessInfo()
        {
            // Show Info Message Box to User
            MessageBox.Show("Successfully Saved File!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Set Info Label
            lblInfo.Text = "To use the context menu, create a desktop/taskbar shortcut to this *.exe and use the --show command line argument!";

            // Show Info Label
            lblInfo.Visible = true;
        }

        private OrderedDictionary BuildShortcutsDictionary(TreeView treeView, String baseFolderPath)
        {
            // Build Flat Map of Shortcuts using the Nodes in the TreeView
            List<TreeNode> flatMap = CollectionUtils.FlatMapTreeNodes(treeView.Nodes);

            // Build Dictionary for Shortcuts in Config
            OrderedDictionary result = new OrderedDictionary();

            // Remove Base Folder Path For Each Shortcut
            foreach (TreeNode treeNode in flatMap)
            {
                // Use Node Text as Display Name and Node Tag Name as Shortcut File Path and Remove the Base Folder Path
                result.Add(FileUtils.RemoveBaseFolderPath(treeNode.Tag.ToString(), baseFolderPath), treeNode.Text);
            }

            // Return Dictionary for Config
            return result;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit Application
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Show About Box as Dialog
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void BtnFolder_Click(object sender, EventArgs e)
        {
            // Create Folder Browser Dialog
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            // Set Description for Folder Browser Dialog
            dialog.Description = "Select Folder containing the *.lnk Files ...";

            // Show Dialog
            DialogResult result = dialog.ShowDialog();

            // Check Dialog Result and Selected Path
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                // Set Path in TextBox
                txtShortcutFolder.Text = dialog.SelectedPath;

                // Build / Refresh Tree View
                BuildTreeView();
            }
        }

        private void NudBottom_ValueChanged(object sender, EventArgs e)
        {
            // Build & Preview Context Menu
            PreviewContextMenu();
        }

        private void NudLeft_ValueChanged(object sender, EventArgs e)
        {
            // Build & Preview Context Menu
            PreviewContextMenu();
        }

        private void PreviewContextMenu()
        {
            // Check Group Box Visible?
            if (!boxConfig.Visible)
            {
                // Don't Preview Context Menu
                return;
            }

            // Check Context Menu
            if (contextMenu == null)
            {
                // Build Context Menu using Current Selected Folder
                BuildContextMenu(LoadShortcuts(txtShortcutFolder.Text));
            }

            // Show Context Menu using Location and Offset Values of Controls
            ShowContextMenu(MapControlsToSelector(), MapControlsToPoint());
        }

        private Point MapControlsToPoint()
        {
            // Map Values of Left and Bottom Numeric Controls to Point (X, Y)
            return new Point((int)nudLeft.Value, (int)nudBottom.Value);
        }

        private ScreenSelector MapControlsToSelector()
        {
            // Check Radio Buttons
            if (rbCurrentScreen.Checked)
            {
                // Set Selector to Current Screen
                return ScreenSelector.CurrentScreen;
            }
            else if (rbPrimaryScreen.Checked)
            {
                // Set Selector to Primary Screen
                return ScreenSelector.PrimaryScreen;
            }
            else if (rbMouseLocation.Checked)
            {
                // Set Selector to Mouse Location
                return ScreenSelector.MouseLocation;
            }

            // Default Mode is Current Screen
            return ScreenSelector.CurrentScreen;
        }

        private void ShowContextMenu(ScreenSelector mode, Point offset)
        {
            // Screen Location for Context Menu
            Point screenLocation = new Point(0, 0);

            // Check Location Mode
            switch (mode)
            {
                case ScreenSelector.CurrentScreen:
                    // Get Working Area of Current Screen
                    Rectangle workingArea = Screen.GetWorkingArea(this);

                    // Calculate Location based on Current Screen
                    screenLocation = new Point(workingArea.X + offset.X,
                                               workingArea.Bottom - offset.Y);
                    break;
                case ScreenSelector.PrimaryScreen:
                    // Get Working Area of Primary Screen
                    Rectangle primaryScreen = Screen.PrimaryScreen.Bounds;

                    // Calculate Location based on Primary Screen
                    screenLocation = new Point(primaryScreen.X + offset.X,
                                               primaryScreen.Bottom - offset.Y);
                    break;
                case ScreenSelector.MouseLocation:
                    // Get Position of Mouse Cursor
                    Point cursorLocation = Cursor.Position;

                    // Calculate Location based on Mouse Location
                    screenLocation = new Point(cursorLocation.X + offset.X,
                                               cursorLocation.Y - offset.Y);
                    break;
            }

            // Check Hide Taskbar Option and Application Startup Mode for Context Menu
            if (CommandLineArgs.HideTaskbar && CommandLineArgs.Mode == AppMode.ShowContextMenu)
            {
                // Hide Taskbar for "Show Context Menu" Mode
                HideTaskbarForContextMenu(contextMenu);
            }

            // Show Context Menu at Screen Location
            contextMenu.Show(screenLocation);

            // Focus Context Menu
            contextMenu.Focus();
        }

        protected void HideTaskbarForContextMenu(ContextMenuStrip contextMenu)
        {
            // Assign Context Menu to Notify Icon to Hide Taskbar
            notifyIcon.ContextMenuStrip = contextMenu;

            // Set Foreground Window to Context Menu, otherwise the Taskbar Element is shown
            UnsafeNativeMethods.SetForegroundWindow(new HandleRef(contextMenu, contextMenu.Handle));
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            // Move Tree Node Up
            treeViewLinks.SelectedNode?.MoveUp();

            // Focus Tree View Again
            treeViewLinks.Focus();
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            // Move Tree Node Down
            treeViewLinks.SelectedNode?.MoveDown();

            // Focus Tree View Again
            treeViewLinks.Focus();
        }
    }
}
