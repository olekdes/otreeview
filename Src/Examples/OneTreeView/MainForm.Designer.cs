using System.Drawing;
using System.Windows.Forms;
using OlekDes;

namespace OneTreeView {
  partial class MainForm {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      treeView = new OTreeView();
      SuspendLayout();
      // 
      // treeView
      // 
      treeView.Dock = DockStyle.Fill;
      treeView.Location = new Point(0, 0);
      treeView.Name = "treeView";
      treeView.Size = new Size(300, 450);
      treeView.TabIndex = 0;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(300, 450);
      Controls.Add(treeView);
      Name = "MainForm";
      Text = "One Tree View";
      ResumeLayout(false);
    }

    #endregion

    private OTreeView treeView;
  }
}
