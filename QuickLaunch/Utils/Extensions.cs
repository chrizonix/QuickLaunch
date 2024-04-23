using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickLaunch.Utils
{
    public static class Extensions
    {
        public static void MoveUp(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;

            // Begin Update
            view.BeginUpdate();

            // Check Parent Node
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                }
            }
            else if (node.TreeView.Nodes.Contains(node))
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index - 1, node);
                }
            }

            // End Update
            view.EndUpdate();

            // Select Current Node
            view.SelectedNode = node;
        }

        public static void MoveDown(this TreeNode node)
        {
            // Get Parent Node and View
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;

            // Begin Update
            view.BeginUpdate();

            // Check Parent Node
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                }
            }
            else if (view != null && view.Nodes.Contains(node))
            {
                int index = view.Nodes.IndexOf(node);
                if (index < view.Nodes.Count - 1)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index + 1, node);
                }
            }

            // End Update
            view.EndUpdate();

            // Select Current Node
            view.SelectedNode = node;
        }
    }
}
