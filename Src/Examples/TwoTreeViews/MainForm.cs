using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TwoTreeViews {
  public partial class MainForm : Form {
    public MainForm() {
      InitializeComponent();

      treeView1.CanHaveChildren += (node) => node?.Text.StartsWith("Node") ?? false;
      for (int i = 0; i < 10; i++) {
        var node = new TreeNode() { Text = $"Node 1-{i}" };
        node.Nodes.Add(new TreeNode() { Text = $"Leaf 1-{i}" });
        treeView1.Nodes.Add(node);
      }

      treeView2.CanHaveChildren += (node) => node?.Text.StartsWith("Node") ?? false;
      for (int i = 0; i < 10; i++) {
        var node = new TreeNode() { Text = $"Node 2-{i}" };
        node.Nodes.Add(new TreeNode() { Text = $"Leaf 2-{i}" });
        treeView2.Nodes.Add(node);
      }
    }
  }
}
