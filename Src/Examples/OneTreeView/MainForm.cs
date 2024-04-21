using System.Windows.Forms;

namespace OneTreeView {
  public partial class MainForm : Form {
    public MainForm() {
      InitializeComponent();

      treeView.CanHaveChildren += (node) => node?.Text.StartsWith("Node") ?? false;

      for (int i = 0; i < 10; i++) {
        var node = new TreeNode() { Text = $"Node {i}" };
        node.Nodes.Add(new TreeNode() { Text = $"Leaf {i}" });
        treeView.Nodes.Add(node);
      }
    }
  }
}
