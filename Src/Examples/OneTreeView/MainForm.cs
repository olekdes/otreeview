using System.Windows.Forms;

namespace OneTreeView {
  public partial class MainForm : Form {
    public MainForm() {
      InitializeComponent();

      treeView.CanHaveChildren += (node) => node?.Text.StartsWith("Node") ?? false;

      for (int i = 0; i < 10; i++) {
        var node = new TreeNode() { Name = $"Node {i}", Text = $"Node {i}" };
        node.Nodes.Add(new TreeNode() { Name = $"Leaf {i}", Text = $"Leaf {i}" });
        treeView.Nodes.Add(node);
      }
    }
  }
}
