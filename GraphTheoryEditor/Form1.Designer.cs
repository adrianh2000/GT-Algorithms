namespace GraphTheoryEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGraphScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGracefulResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDistancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depthFirstSearchDFSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findGracefulLabelingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonCreateCircle = new System.Windows.Forms.Button();
            this.buttonSelection = new System.Windows.Forms.Button();
            this.buttonDrawEdge = new System.Windows.Forms.Button();
            this.buttonDrawVertex = new System.Windows.Forms.Button();
            this.labelMouseCoordinates = new System.Windows.Forms.Label();
            this.buttonDeleteGraph = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxMain.Location = new System.Drawing.Point(7, 87);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(842, 363);
            this.pictureBoxMain.TabIndex = 0;
            this.pictureBoxMain.TabStop = false;
            this.pictureBoxMain.Click += new System.EventHandler(this.pictureBoxMain_Click);
            this.pictureBoxMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMain_Paint);
            this.pictureBoxMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseMove);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.algorithmToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(861, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveasToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveasToolStripMenuItem
            // 
            this.saveasToolStripMenuItem.Name = "saveasToolStripMenuItem";
            this.saveasToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveasToolStripMenuItem.Text = "Save &as...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(118, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveGraphScreenshotToolStripMenuItem,
            this.loadGracefulResultsToolStripMenuItem});
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.graphToolStripMenuItem.Text = "&Graph";
            // 
            // saveGraphScreenshotToolStripMenuItem
            // 
            this.saveGraphScreenshotToolStripMenuItem.Enabled = false;
            this.saveGraphScreenshotToolStripMenuItem.Name = "saveGraphScreenshotToolStripMenuItem";
            this.saveGraphScreenshotToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.saveGraphScreenshotToolStripMenuItem.Text = "Save Graph &Screenshot";
            this.saveGraphScreenshotToolStripMenuItem.Click += new System.EventHandler(this.saveGraphScreenshotToolStripMenuItem_Click);
            // 
            // loadGracefulResultsToolStripMenuItem
            // 
            this.loadGracefulResultsToolStripMenuItem.Name = "loadGracefulResultsToolStripMenuItem";
            this.loadGracefulResultsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.loadGracefulResultsToolStripMenuItem.Text = "&Load Graceful Results";
            this.loadGracefulResultsToolStripMenuItem.Click += new System.EventHandler(this.loadGracefulResultsToolStripMenuItem_Click);
            // 
            // algorithmToolStripMenuItem
            // 
            this.algorithmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDistancesToolStripMenuItem,
            this.depthFirstSearchDFSToolStripMenuItem,
            this.findGracefulLabelingsToolStripMenuItem});
            this.algorithmToolStripMenuItem.Name = "algorithmToolStripMenuItem";
            this.algorithmToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.algorithmToolStripMenuItem.Text = "&Algorithm";
            this.algorithmToolStripMenuItem.Click += new System.EventHandler(this.algorithmToolStripMenuItem_Click);
            // 
            // showDistancesToolStripMenuItem
            // 
            this.showDistancesToolStripMenuItem.Enabled = false;
            this.showDistancesToolStripMenuItem.Name = "showDistancesToolStripMenuItem";
            this.showDistancesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.showDistancesToolStripMenuItem.Text = "Show &Distances";
            this.showDistancesToolStripMenuItem.Click += new System.EventHandler(this.showDistancesToolStripMenuItem_Click);
            // 
            // depthFirstSearchDFSToolStripMenuItem
            // 
            this.depthFirstSearchDFSToolStripMenuItem.Enabled = false;
            this.depthFirstSearchDFSToolStripMenuItem.Name = "depthFirstSearchDFSToolStripMenuItem";
            this.depthFirstSearchDFSToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.depthFirstSearchDFSToolStripMenuItem.Text = "Depth &First Search (DFS)";
            this.depthFirstSearchDFSToolStripMenuItem.Click += new System.EventHandler(this.depthFirstSearchDFSToolStripMenuItem_Click);
            // 
            // findGracefulLabelingsToolStripMenuItem
            // 
            this.findGracefulLabelingsToolStripMenuItem.Name = "findGracefulLabelingsToolStripMenuItem";
            this.findGracefulLabelingsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.findGracefulLabelingsToolStripMenuItem.Text = "Find &Graceful Labelings";
            this.findGracefulLabelingsToolStripMenuItem.Click += new System.EventHandler(this.findGracefulLabelingsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultFolderToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // defaultFolderToolStripMenuItem
            // 
            this.defaultFolderToolStripMenuItem.Name = "defaultFolderToolStripMenuItem";
            this.defaultFolderToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.defaultFolderToolStripMenuItem.Text = "&Default folder...";
            this.defaultFolderToolStripMenuItem.Click += new System.EventHandler(this.defaultFolderToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMain.Controls.Add(this.buttonDeleteGraph);
            this.panelMain.Controls.Add(this.buttonCreateCircle);
            this.panelMain.Controls.Add(this.buttonSelection);
            this.panelMain.Controls.Add(this.buttonDrawEdge);
            this.panelMain.Controls.Add(this.buttonDrawVertex);
            this.panelMain.Location = new System.Drawing.Point(7, 27);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(849, 54);
            this.panelMain.TabIndex = 3;
            // 
            // buttonCreateCircle
            // 
            this.buttonCreateCircle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCreateCircle.BackgroundImage")));
            this.buttonCreateCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCreateCircle.Location = new System.Drawing.Point(196, 0);
            this.buttonCreateCircle.Name = "buttonCreateCircle";
            this.buttonCreateCircle.Size = new System.Drawing.Size(56, 47);
            this.buttonCreateCircle.TabIndex = 3;
            this.buttonCreateCircle.UseVisualStyleBackColor = true;
            this.buttonCreateCircle.Click += new System.EventHandler(this.buttonCreateCircle_Click);
            // 
            // buttonSelection
            // 
            this.buttonSelection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSelection.BackgroundImage")));
            this.buttonSelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSelection.Location = new System.Drawing.Point(10, 0);
            this.buttonSelection.Name = "buttonSelection";
            this.buttonSelection.Size = new System.Drawing.Size(56, 47);
            this.buttonSelection.TabIndex = 2;
            this.buttonSelection.UseVisualStyleBackColor = true;
            this.buttonSelection.Click += new System.EventHandler(this.buttonSelection_Click);
            // 
            // buttonDrawEdge
            // 
            this.buttonDrawEdge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDrawEdge.BackgroundImage")));
            this.buttonDrawEdge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDrawEdge.Location = new System.Drawing.Point(134, -1);
            this.buttonDrawEdge.Name = "buttonDrawEdge";
            this.buttonDrawEdge.Size = new System.Drawing.Size(56, 47);
            this.buttonDrawEdge.TabIndex = 1;
            this.buttonDrawEdge.UseVisualStyleBackColor = true;
            this.buttonDrawEdge.Click += new System.EventHandler(this.buttonDrawEdge_Click);
            // 
            // buttonDrawVertex
            // 
            this.buttonDrawVertex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDrawVertex.BackgroundImage")));
            this.buttonDrawVertex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDrawVertex.Location = new System.Drawing.Point(72, 0);
            this.buttonDrawVertex.Name = "buttonDrawVertex";
            this.buttonDrawVertex.Size = new System.Drawing.Size(56, 47);
            this.buttonDrawVertex.TabIndex = 0;
            this.buttonDrawVertex.UseVisualStyleBackColor = true;
            this.buttonDrawVertex.Click += new System.EventHandler(this.buttonDrawVertex_Click);
            // 
            // labelMouseCoordinates
            // 
            this.labelMouseCoordinates.AutoSize = true;
            this.labelMouseCoordinates.Location = new System.Drawing.Point(345, 9);
            this.labelMouseCoordinates.Name = "labelMouseCoordinates";
            this.labelMouseCoordinates.Size = new System.Drawing.Size(117, 13);
            this.labelMouseCoordinates.TabIndex = 4;
            this.labelMouseCoordinates.Text = "labelMouseCoordinates";
            // 
            // buttonDeleteGraph
            // 
            this.buttonDeleteGraph.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDeleteGraph.BackgroundImage")));
            this.buttonDeleteGraph.Location = new System.Drawing.Point(793, -2);
            this.buttonDeleteGraph.Name = "buttonDeleteGraph";
            this.buttonDeleteGraph.Size = new System.Drawing.Size(47, 49);
            this.buttonDeleteGraph.TabIndex = 4;
            this.buttonDeleteGraph.UseVisualStyleBackColor = true;
            this.buttonDeleteGraph.Click += new System.EventHandler(this.buttonDeleteGraph_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 462);
            this.Controls.Add(this.labelMouseCoordinates);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.pictureBoxMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Graph Theory Algorithms by Adrian Heinz";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelMouseCoordinates;
        private System.Windows.Forms.Button buttonDrawEdge;
        private System.Windows.Forms.Button buttonDrawVertex;
        private System.Windows.Forms.Button buttonSelection;
        private System.Windows.Forms.ToolStripMenuItem algorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDistancesToolStripMenuItem;
        private System.Windows.Forms.Button buttonCreateCircle;
        private System.Windows.Forms.ToolStripMenuItem depthFirstSearchDFSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findGracefulLabelingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGraphScreenshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadGracefulResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultFolderToolStripMenuItem;
        private System.Windows.Forms.Button buttonDeleteGraph;
    }
}

