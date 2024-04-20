# OTreeView

Rearrangeable TreeView for Windows Forms.

## Quick Start Example

```cs
treeView = new OTreeView();

treeView.CanHaveChildren += (node) => node?.Text.StartsWith("Node") ?? false;

for (int i = 0; i < 10; i++) {
  var node = new TreeNode() { Name = $"Node {i}", Text = $"Node {i}" };
  node.Nodes.Add(new TreeNode() { Name = $"Leaf {i}", Text = $"Leaf {i}" });
  treeView.Nodes.Add(node);
}
```

[![Nuget](https://img.shields.io/nuget/v/OTreeView)](https://www.nuget.org/packages/OTreeView)
