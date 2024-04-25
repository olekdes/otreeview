using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OlekDes {

  public class OTreeView : TreeView {

    private TreeNode draggedNode;
    private TreeNode targetNode;
    private int cursorOffset;

    public event NodeMovedEventHandler NodeMoved;
    private void OnNodeMoved(TreeNode node) => NodeMoved?.Invoke(this, new NodeModedEventArgs() { Node = node });
    public CanHaveChildren CanHaveChildren { get; set; }

    public OTreeView() : base() {
      AllowDrop = true;

      HideSelection = false;
      ItemHeight += 3;
      Indent += 3;

      MouseDown += (s, e) => SelectedNode = GetNodeAt(e.X, e.Y);
      DragOver += ProcessDragOver;
      DragEnter += (s, e) => {
        e.Effect = DragDropEffects.Move;
        ResetState();
      };
      ItemDrag += (s, e) => DoDragDrop(e.Item, DragDropEffects.Move);
      DragDrop += ProcessDragDrop;
      DragLeave += (s, e) => ProcessDragOver(s, new DragEventArgs(null, 0, Cursor.Position.X, Cursor.Position.Y, 0, 0));
    }

    private void ProcessDragDrop(object sender, DragEventArgs e) {

      if (!e.Data.GetDataPresent(typeof(TreeNode)))
        return;

      var draggedNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
      var cursorPoint = PointToClient(new Point(e.X, e.Y));
      var targetNode = GetNodeAt(cursorPoint);
      var canHaveChildren = CanHaveChildren?.Invoke(targetNode) ?? false;
      var cursorOffset = GetCursorOffset(targetNode, cursorPoint, canHaveChildren);

      if (!CanDrop(draggedNode, targetNode, cursorOffset))
        return;

      var topNode = TopNode;

      draggedNode.Remove();
      if (targetNode == null) {
        if (cursorOffset < 0)
          Nodes.Insert(0, draggedNode);
        else
          Nodes.Add(draggedNode);
      }
      else {
        if (cursorOffset < 0)
          GetParentNodes(targetNode).Insert(GetParentNodes(targetNode).IndexOf(targetNode), draggedNode);
        else {
          if (cursorOffset == 0 || canHaveChildren && targetNode.Nodes.Count > 0)
            targetNode.Nodes.Insert(0, draggedNode);
          else
            GetParentNodes(targetNode).Insert(GetParentNodes(targetNode).IndexOf(targetNode) + 1, draggedNode);
        }
      }

      SelectedNode = draggedNode;
      TopNode = topNode;
      draggedNode.EnsureVisible();

      OnNodeMoved(draggedNode);
    }

    private void ProcessDragOver(object sender, DragEventArgs e) {

      var draggedNode = e.Data?.GetData(typeof(TreeNode)) as TreeNode;
      var cursorPoint = PointToClient(new Point(e.X, e.Y));
      var targetNode = GetNodeAt(cursorPoint);
      var canHaveChildren = CanHaveChildren?.Invoke(targetNode) ?? false;
      var cursorOffset = GetCursorOffset(targetNode, cursorPoint, canHaveChildren);

      if (this.draggedNode == draggedNode && this.targetNode == targetNode && this.cursorOffset == cursorOffset)
        return;

      this.draggedNode = draggedNode;
      this.targetNode = targetNode;
      this.cursorOffset = cursorOffset;

      targetNode?.PrevVisibleNode?.EnsureVisible();
      targetNode?.NextVisibleNode?.EnsureVisible();
      targetNode?.EnsureVisible();
      Refresh();

      if (!CanDrop(draggedNode, targetNode, cursorOffset)) {
        e.Effect = DragDropEffects.None;
        return;
      }

      e.Effect = DragDropEffects.Move;
      targetNode?.Expand();
      DrawPlaceholder(targetNode, cursorOffset);
    }

    private void ResetState() {
      draggedNode = null;
      targetNode = null;
      cursorOffset = 0;
    }

    private TreeNodeCollection GetParentNodes(TreeNode node) {

      if (node.Parent != null)
        return node.Parent.Nodes;
      if (Nodes.Contains(node))
        return Nodes;
      return null;
    }

    private int GetCursorOffset(TreeNode targetNode, Point targetPoint, bool useThreePoints = false) {

      if (targetNode == null) {
        if (targetPoint.X < 0 || targetPoint.X > ClientSize.Width)
          return 0;
        var cursorY = targetPoint.Y;
        var firstCenterY = Nodes[0].Bounds.Height / 2;
        return cursorY < firstCenterY ? -1 : 1;
      }

      var cursorRelativeY = targetPoint.Y - targetNode.Bounds.Y;

      if (useThreePoints) {
        var upThirdY = targetNode.Bounds.Height / 3;
        if (cursorRelativeY < upThirdY)
          return -1;
        var downTrirdY = targetNode.Bounds.Height / 1.5;
        if (cursorRelativeY > downTrirdY)
          return 1;
        return 0;
      }

      var centerY = targetNode.Bounds.Height / 2;
      return cursorRelativeY < centerY ? -1 : 1;
    }

    private bool CanDrop(TreeNode draggedNode, TreeNode targetNode, int cursorOffset) {

      // Can't drop nothing
      if (draggedNode == null)
        return false;

      // Can't drop outside
      if (targetNode == null)
        return cursorOffset < 0 && (draggedNode.Parent != null || draggedNode.PrevNode != null)
          || cursorOffset > 0 && (draggedNode.Parent != null || draggedNode.NextNode != null);

      // Can't drop to itself
      if (targetNode == draggedNode)
        return false;

      // Can't drop to the current place
      if (targetNode == draggedNode.NextNode && cursorOffset < 0)
        return false;

      // Can't drop to the current place
      if (targetNode.NextNode == draggedNode && targetNode.Nodes.Count == 0 && cursorOffset > 0)
        return false;

      // Can't drop to parent to its current position
      if (targetNode == draggedNode.Parent && draggedNode.Parent.Nodes.IndexOf(draggedNode) == 0 && cursorOffset >= 0)
        return false;

      // Can't drop parent to its own child
      if (IsChildOf(targetNode, draggedNode))
        return false;

      return true;
    }

    private TreeNode GetLastVisibleNode(TreeNode node = null) {

      if (node == null)
        node = Nodes.Cast<TreeNode>().First(n => n.Parent == null && n.NextNode == null);
      while (node.IsExpanded && node.Nodes.Count > 0)
        node = node.Nodes.Cast<TreeNode>().First(n => n.NextNode == null);
      return node;
    }

    private bool IsChildOf(TreeNode childNode, TreeNode parentNode) {

      while (childNode.Parent != null) {
        if (childNode.Parent == parentNode)
          return true;
        childNode = childNode.Parent;
      }
      return false;
    }

    private void DrawPlaceholder(TreeNode targetNode, int offset) {

      if (offset < 0) {
        var xNode = targetNode ?? Nodes[0];
        var xNearNode = targetNode?.PrevVisibleNode ?? xNode;
        var yNode = xNode;
        DrawLine(CreateGraphics(),
          x0: xNode.Bounds.X - GetImageWidth(xNode.ImageIndex),
          x1: Math.Max(xNode.Bounds.X + xNode.Bounds.Width, xNearNode.Bounds.X + xNearNode.Bounds.Width),
          y: yNode.Bounds.Y);
      }
      else if (offset > 0) {
        var xNode = targetNode != null
          ? CanHaveChildren?.Invoke(targetNode) ?? false && targetNode.Nodes.Count > 0 ? targetNode.Nodes[0] : targetNode
          : Nodes[Nodes.Count - 1];
        var xNearNode = targetNode != null
          ? targetNode.NextVisibleNode ?? xNode
          : GetLastVisibleNode();
        var yNode = targetNode ?? GetLastVisibleNode();
        DrawLine(CreateGraphics(),
          x0: xNode.Bounds.X - GetImageWidth(xNode.ImageIndex),
          x1: Math.Max(xNode.Bounds.X + xNode.Bounds.Width, xNearNode.Bounds.X + xNearNode.Bounds.Width),
          y: yNode.Bounds.Y + yNode.Bounds.Height);
      }
      else {
        DrawPointer(CreateGraphics(),
          x: targetNode.Bounds.X + targetNode.Bounds.Width,
          y: targetNode.Bounds.Y + targetNode.Bounds.Height / 2);
      }
    }

    private int GetImageWidth(int imageIndex) =>
      ImageList?.Images[imageIndex].Size.Width ?? 0;

    private void DrawPointer(Graphics g, int x, int y) {
      var brush = new SolidBrush(SystemColors.Highlight);
      g.FillPolygon(brush, new[] { new Point(x + 6, y - 4), new Point(x + 6, y + 4), new Point(x + 2, y), new Point(x + 2, y - 1), new Point(x + 6, y - 5) });
    }

    private void DrawLine(Graphics g, int x0, int x1, int y) {
      var pen = new Pen(SystemColors.Highlight, 2);
      g.DrawLine(pen, new Point(x0, y), new Point(x1, y));
    }
  }

  public class NodeModedEventArgs : EventArgs {
    public TreeNode Node { get; internal set; }
  }

  public delegate void NodeMovedEventHandler(object sender, NodeModedEventArgs e);

  public delegate bool CanHaveChildren(TreeNode node);
}
