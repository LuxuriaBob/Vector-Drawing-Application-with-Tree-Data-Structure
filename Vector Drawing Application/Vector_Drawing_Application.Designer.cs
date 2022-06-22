namespace Vector_Drawing_Application
{
    partial class Vector_Drawing_Application
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vector_Drawing_Application));
            this.RectangleButton = new System.Windows.Forms.Button();
            this.FillColorCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.StretchButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.MoveButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ColorPickerButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.PolygonButton = new System.Windows.Forms.Button();
            this.LineButton = new System.Windows.Forms.Button();
            this.CircleButton = new System.Windows.Forms.Button();
            this.SquareButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate90DegressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Help_Button = new System.Windows.Forms.Button();
            this.UndoButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RectangleButton
            // 
            this.RectangleButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RectangleButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RectangleButton.BackgroundImage")));
            this.RectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("RectangleButton.Image")));
            this.RectangleButton.Location = new System.Drawing.Point(0, 0);
            this.RectangleButton.Name = "RectangleButton";
            this.RectangleButton.Size = new System.Drawing.Size(45, 45);
            this.RectangleButton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.RectangleButton, "Draw Rectangle");
            this.RectangleButton.UseVisualStyleBackColor = false;
            this.RectangleButton.Click += new System.EventHandler(this.RectButton_Click);
            // 
            // FillColorCheckBox
            // 
            this.FillColorCheckBox.AutoSize = true;
            this.FillColorCheckBox.Location = new System.Drawing.Point(228, 3);
            this.FillColorCheckBox.Name = "FillColorCheckBox";
            this.FillColorCheckBox.Size = new System.Drawing.Size(65, 17);
            this.FillColorCheckBox.TabIndex = 4;
            this.FillColorCheckBox.Text = "Fill Color";
            this.FillColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Help_Button);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.StretchButton);
            this.panel1.Controls.Add(this.LoadButton);
            this.panel1.Controls.Add(this.SelectButton);
            this.panel1.Controls.Add(this.MoveButton);
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.DeleteButton);
            this.panel1.Controls.Add(this.ColorPickerButton);
            this.panel1.Controls.Add(this.UndoButton);
            this.panel1.Controls.Add(this.ClearButton);
            this.panel1.Controls.Add(this.PolygonButton);
            this.panel1.Controls.Add(this.LineButton);
            this.panel1.Controls.Add(this.CircleButton);
            this.panel1.Controls.Add(this.SquareButton);
            this.panel1.Controls.Add(this.RectangleButton);
            this.panel1.Controls.Add(this.FillColorCheckBox);
            this.panel1.Location = new System.Drawing.Point(3, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 46);
            this.panel1.TabIndex = 10;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(228, 23);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown1.TabIndex = 13;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // StretchButton
            // 
            this.StretchButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StretchButton.BackgroundImage")));
            this.StretchButton.Image = ((System.Drawing.Image)(resources.GetObject("StretchButton.Image")));
            this.StretchButton.Location = new System.Drawing.Point(388, 1);
            this.StretchButton.Name = "StretchButton";
            this.StretchButton.Size = new System.Drawing.Size(45, 45);
            this.StretchButton.TabIndex = 17;
            this.toolTip1.SetToolTip(this.StretchButton, "Stretch");
            this.StretchButton.UseVisualStyleBackColor = true;
            this.StretchButton.Click += new System.EventHandler(this.StretchButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LoadButton.BackgroundImage")));
            this.LoadButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadButton.Image")));
            this.LoadButton.Location = new System.Drawing.Point(651, 1);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(45, 45);
            this.LoadButton.TabIndex = 23;
            this.toolTip1.SetToolTip(this.LoadButton, "Load");
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SelectButton.BackgroundImage")));
            this.SelectButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectButton.Image")));
            this.SelectButton.Location = new System.Drawing.Point(300, 1);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(45, 45);
            this.SelectButton.TabIndex = 19;
            this.toolTip1.SetToolTip(this.SelectButton, "Select");
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // MoveButton
            // 
            this.MoveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MoveButton.BackgroundImage")));
            this.MoveButton.Image = ((System.Drawing.Image)(resources.GetObject("MoveButton.Image")));
            this.MoveButton.Location = new System.Drawing.Point(344, 1);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(45, 45);
            this.MoveButton.TabIndex = 16;
            this.toolTip1.SetToolTip(this.MoveButton, "Move");
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SaveButton.BackgroundImage")));
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.Location = new System.Drawing.Point(607, 1);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(45, 45);
            this.SaveButton.TabIndex = 22;
            this.toolTip1.SetToolTip(this.SaveButton, "Save");
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteButton.BackgroundImage")));
            this.DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteButton.Image")));
            this.DeleteButton.Location = new System.Drawing.Point(432, 1);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(45, 45);
            this.DeleteButton.TabIndex = 20;
            this.toolTip1.SetToolTip(this.DeleteButton, "Erase");
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ColorPickerButton
            // 
            this.ColorPickerButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ColorPickerButton.BackgroundImage")));
            this.ColorPickerButton.Image = ((System.Drawing.Image)(resources.GetObject("ColorPickerButton.Image")));
            this.ColorPickerButton.Location = new System.Drawing.Point(563, 1);
            this.ColorPickerButton.Name = "ColorPickerButton";
            this.ColorPickerButton.Size = new System.Drawing.Size(45, 45);
            this.ColorPickerButton.TabIndex = 17;
            this.toolTip1.SetToolTip(this.ColorPickerButton, "Pick Color");
            this.ColorPickerButton.UseVisualStyleBackColor = true;
            this.ColorPickerButton.Click += new System.EventHandler(this.ColorPickerButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClearButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ClearButton.BackgroundImage")));
            this.ClearButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearButton.Image")));
            this.ClearButton.Location = new System.Drawing.Point(475, 1);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(45, 45);
            this.ClearButton.TabIndex = 18;
            this.toolTip1.SetToolTip(this.ClearButton, "Clear Canvas");
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // PolygonButton
            // 
            this.PolygonButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PolygonButton.Image = ((System.Drawing.Image)(resources.GetObject("PolygonButton.Image")));
            this.PolygonButton.Location = new System.Drawing.Point(176, 0);
            this.PolygonButton.Name = "PolygonButton";
            this.PolygonButton.Size = new System.Drawing.Size(45, 45);
            this.PolygonButton.TabIndex = 13;
            this.toolTip1.SetToolTip(this.PolygonButton, "Draw Polygon");
            this.PolygonButton.UseVisualStyleBackColor = false;
            this.PolygonButton.Click += new System.EventHandler(this.PolygonButton_Click);
            // 
            // LineButton
            // 
            this.LineButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LineButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LineButton.BackgroundImage")));
            this.LineButton.Image = ((System.Drawing.Image)(resources.GetObject("LineButton.Image")));
            this.LineButton.Location = new System.Drawing.Point(132, 0);
            this.LineButton.Name = "LineButton";
            this.LineButton.Size = new System.Drawing.Size(45, 45);
            this.LineButton.TabIndex = 12;
            this.toolTip1.SetToolTip(this.LineButton, "Draw Line");
            this.LineButton.UseVisualStyleBackColor = false;
            this.LineButton.Click += new System.EventHandler(this.LineButton_Click);
            // 
            // CircleButton
            // 
            this.CircleButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CircleButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CircleButton.BackgroundImage")));
            this.CircleButton.Image = ((System.Drawing.Image)(resources.GetObject("CircleButton.Image")));
            this.CircleButton.Location = new System.Drawing.Point(88, 0);
            this.CircleButton.Name = "CircleButton";
            this.CircleButton.Size = new System.Drawing.Size(45, 45);
            this.CircleButton.TabIndex = 11;
            this.toolTip1.SetToolTip(this.CircleButton, "Draw Circle");
            this.CircleButton.UseVisualStyleBackColor = false;
            this.CircleButton.Click += new System.EventHandler(this.CircleButton_Click);
            // 
            // SquareButton
            // 
            this.SquareButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SquareButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SquareButton.BackgroundImage")));
            this.SquareButton.Image = ((System.Drawing.Image)(resources.GetObject("SquareButton.Image")));
            this.SquareButton.Location = new System.Drawing.Point(44, 0);
            this.SquareButton.Name = "SquareButton";
            this.SquareButton.Size = new System.Drawing.Size(45, 45);
            this.SquareButton.TabIndex = 8;
            this.toolTip1.SetToolTip(this.SquareButton, "Draw Square");
            this.SquareButton.UseVisualStyleBackColor = false;
            this.SquareButton.Click += new System.EventHandler(this.SquareButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.rotateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(144, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotate90DegressToolStripMenuItem,
            this.rotateHorizontallyToolStripMenuItem,
            this.rotateVerticallyToolStripMenuItem});
            this.rotateToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.rotateToolStripMenuItem.Text = "Rotate";
            this.rotateToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // rotate90DegressToolStripMenuItem
            // 
            this.rotate90DegressToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rotate90DegressToolStripMenuItem.Image")));
            this.rotate90DegressToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rotate90DegressToolStripMenuItem.Name = "rotate90DegressToolStripMenuItem";
            this.rotate90DegressToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.rotate90DegressToolStripMenuItem.Size = new System.Drawing.Size(242, 38);
            this.rotate90DegressToolStripMenuItem.Text = "Rotate 90 Degress";
            this.rotate90DegressToolStripMenuItem.Click += new System.EventHandler(this.Rotate90DegressToolStripMenuItem_Click);
            // 
            // rotateHorizontallyToolStripMenuItem
            // 
            this.rotateHorizontallyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rotateHorizontallyToolStripMenuItem.Image")));
            this.rotateHorizontallyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rotateHorizontallyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.rotateHorizontallyToolStripMenuItem.Name = "rotateHorizontallyToolStripMenuItem";
            this.rotateHorizontallyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.rotateHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(242, 38);
            this.rotateHorizontallyToolStripMenuItem.Text = "Rotate Horizontally";
            this.rotateHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.RotateHorizontallyToolStripMenuItem_Click);
            // 
            // rotateVerticallyToolStripMenuItem
            // 
            this.rotateVerticallyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rotateVerticallyToolStripMenuItem.Image")));
            this.rotateVerticallyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rotateVerticallyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.rotateVerticallyToolStripMenuItem.Name = "rotateVerticallyToolStripMenuItem";
            this.rotateVerticallyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.rotateVerticallyToolStripMenuItem.Size = new System.Drawing.Size(242, 38);
            this.rotateVerticallyToolStripMenuItem.Text = "Rotate Vertically";
            this.rotateVerticallyToolStripMenuItem.Click += new System.EventHandler(this.RotateVerticallyToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // Help_Button
            // 
            this.Help_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Help_Button.BackgroundImage")));
            this.Help_Button.Image = ((System.Drawing.Image)(resources.GetObject("Help_Button.Image")));
            this.Help_Button.Location = new System.Drawing.Point(695, 1);
            this.Help_Button.Name = "Help_Button";
            this.Help_Button.Size = new System.Drawing.Size(45, 45);
            this.Help_Button.TabIndex = 24;
            this.toolTip1.SetToolTip(this.Help_Button, "Load");
            this.Help_Button.UseVisualStyleBackColor = true;
            this.Help_Button.Click += new System.EventHandler(this.Help_Button_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UndoButton.BackgroundImage")));
            this.UndoButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoButton.Image")));
            this.UndoButton.Location = new System.Drawing.Point(519, 1);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(45, 45);
            this.UndoButton.TabIndex = 21;
            this.toolTip1.SetToolTip(this.UndoButton, "Undo");
            this.UndoButton.UseVisualStyleBackColor = true;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // Vector_Drawing_Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(761, 522);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 45);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Vector_Drawing_Application";
            this.Padding = new System.Windows.Forms.Padding(0, 45, 0, 0);
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Vector Drawing Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Vector_Drawing_Application_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RectangleButton;
        private System.Windows.Forms.CheckBox FillColorCheckBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SquareButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button CircleButton;
        private System.Windows.Forms.Button LineButton;
        private System.Windows.Forms.Button PolygonButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ColorPickerButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button StretchButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotate90DegressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateHorizontallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateVerticallyToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button Help_Button;
        private System.Windows.Forms.Button UndoButton;
    }
}