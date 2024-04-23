using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;

using QuickLaunch.Data;
using Shortcut = QuickLaunch.Data.Shortcut;

namespace QuickLaunch.Utils
{
    public class CollectionUtils
    {
        /// <summary>
        /// Flatten Recursive Shortcut List
        /// </summary>
        public static List<Shortcut> FlatMap(IEnumerable<Shortcut> collection)
        {
            // Create Empty Result List
            List<Shortcut> result = new List<Shortcut>();

            // For Each Shortcut in Collection
            foreach (Shortcut item in collection)
            {
                // Add Shortcut to Result
                result.Add(item);

                // Check Shortcut Type is Folder
                if (item.Type == ShortcutType.Folder)
                {
                    // Recursive Call for Sub-Directory
                    result.AddRange(FlatMap(item.Shortcuts));
                }
            }

            // Return Flattened List
            return result;
        }

        /// <summary>
        /// Flatten Recursive TreeView Nodes into a List
        /// </summary>
        public static List<TreeNode> FlatMapTreeNodes(TreeNodeCollection treeNodes)
        {
            // Create Empty TreeNode Result List
            List<TreeNode> result = new List<TreeNode>();

            // For Each Node in TreeNodeCollection
            foreach (TreeNode node in treeNodes)
            {
                // Add Node to Result
                result.Add(node);

                // Check Tree Node is a Folder
                if (node.Nodes.Count > 0)
                {
                    // Flat Map Sub Items of Current Tree Node
                    result.AddRange(FlatMapTreeNodes(node.Nodes));
                }
            }

            // Return Flattened List
            return result;
        }
    }
}
