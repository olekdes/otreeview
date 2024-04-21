using OlekDes;

namespace TwoTreeViews {
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
      splitContainer = new System.Windows.Forms.SplitContainer();
      treeView1 = new OTreeView();
      treeView2 = new OTreeView();
      ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
      splitContainer.Panel1.SuspendLayout();
      splitContainer.Panel2.SuspendLayout();
      splitContainer.SuspendLayout();
      SuspendLayout();
      // 
      // splitContainer
      // 
      splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      splitContainer.Location = new System.Drawing.Point(0, 0);
      splitContainer.Name = "splitContainer";
      splitContainer.Panel1.Controls.Add(treeView1);
      splitContainer.Panel2.Controls.Add(treeView2);
      splitContainer.Size = new System.Drawing.Size(600, 450);
      splitContainer.SplitterDistance = 298;
      splitContainer.TabIndex = 0;
      // 
      // treeView1
      // 
      treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
      treeView1.Location = new System.Drawing.Point(0, 0);
      treeView1.Name = "treeView1";
      treeView1.Size = new System.Drawing.Size(298, 450);
      treeView1.TabIndex = 0;
      // 
      // treeView1
      // 
      treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
      treeView2.Location = new System.Drawing.Point(0, 0);
      treeView2.Name = "treeView2";
      treeView2.Size = new System.Drawing.Size(298, 450);
      treeView2.TabIndex = 0;
      // 
      // MainForm
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(600, 450);
      Controls.Add(splitContainer);
      Name = "MainForm";
      Text = "Two Tree Views";
      splitContainer.Panel1.ResumeLayout(false);
      splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
      splitContainer.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer;
    private OTreeView treeView1;
    private OTreeView treeView2;
  }
}
