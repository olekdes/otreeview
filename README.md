# OTreeView

Rearrangeable TreeView for Windows Forms.

![Example](https://github.com/olekdes/otreeview/blob/master/example.png?raw=true)

[![License](https://img.shields.io/github/license/olekdes/otreeview.svg)](https://github.com/olekdes/uioc/blob/master/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/OTreeView)](https://www.nuget.org/packages/OTreeView) 
[![NuGet](https://img.shields.io/nuget/dt/OTreeView.svg)](https://www.nuget.org/packages/OTreeView)

## Installation

Install OTreeView using Package Manager with the `Install-Package OTreeView` command or get it from the [NuGet.org](https://www.nuget.org/packages/OTreeView).

## Get Started

```cs
// Use OTreeView control instead of regular TreeView

treeView.CanHaveChildren += (node) => node?.Text.StartsWith("Node") ?? false;

for (int i = 0; i < 5; i++) {
  var node = new TreeNode() { Text = $"Node {i}" };
  node.Nodes.Add(new TreeNode() { Text = $"Leaf {i}" });
  treeView.Nodes.Add(node);
}
```
