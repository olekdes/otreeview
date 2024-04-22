# OTreeView

Rearrangeable TreeView for Windows Forms.

[![Nuget](https://img.shields.io/nuget/v/OTreeView)](https://www.nuget.org/packages/OTreeView)

## Get Started

![Example](https://github.com/olekdes/otreeview/blob/master/example.png?raw=true)

```cs
// Use OTreeView control instead of regular TreeView

treeView.CanHaveChildren += (node) => node?.Text.StartsWith("Node") ?? false;

for (int i = 0; i < 5; i++) {
  var node = new TreeNode() { Text = $"Node {i}" };
  node.Nodes.Add(new TreeNode() { Text = $"Leaf {i}" });
  treeView.Nodes.Add(node);
}
```
