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
            this.FillColorCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseLabel = new System.Windows.Forms.Label();
            this.rulerLabel = new System.Windows.Forms.Label();
            this.TextPanel = new System.Windows.Forms.Panel();
            this.WebViewerButton = new System.Windows.Forms.Button();
            this.DecryptionButton = new System.Windows.Forms.Button();
            this.CurveButton = new System.Windows.Forms.Button();
            this.EncryptionButton = new System.Windows.Forms.Button();
            this.RulerButton = new System.Windows.Forms.Button();
            this.Help_Button = new System.Windows.Forms.Button();
            this.StretchButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.MoveButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ColorPickerButton = new System.Windows.Forms.Button();
            this.UndoButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.PolygonButton = new System.Windows.Forms.Button();
            this.LineButton = new System.Windows.Forms.Button();
            this.CircleButton = new System.Windows.Forms.Button();
            this.SquareButton = new System.Windows.Forms.Button();
            this.RectangleButton = new System.Windows.Forms.Button();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBackgroundImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate90DegressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.TextPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FillColorCheckBox
            // 
            this.FillColorCheckBox.AutoSize = true;
            this.FillColorCheckBox.Location = new System.Drawing.Point(276, 3);
            this.FillColorCheckBox.Name = "FillColorCheckBox";
            this.FillColorCheckBox.Size = new System.Drawing.Size(65, 17);
            this.FillColorCheckBox.TabIndex = 4;
            this.FillColorCheckBox.Text = "Fill Color";
            this.FillColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolPanel
            // 
            this.toolPanel.AutoSize = true;
            this.toolPanel.BackColor = System.Drawing.Color.DarkOrange;
            this.toolPanel.Controls.Add(this.WebViewerButton);
            this.toolPanel.Controls.Add(this.DecryptionButton);
            this.toolPanel.Controls.Add(this.CurveButton);
            this.toolPanel.Controls.Add(this.EncryptionButton);
            this.toolPanel.Controls.Add(this.RulerButton);
            this.toolPanel.Controls.Add(this.Help_Button);
            this.toolPanel.Controls.Add(this.numericUpDown1);
            this.toolPanel.Controls.Add(this.StretchButton);
            this.toolPanel.Controls.Add(this.LoadButton);
            this.toolPanel.Controls.Add(this.SelectButton);
            this.toolPanel.Controls.Add(this.MoveButton);
            this.toolPanel.Controls.Add(this.SaveButton);
            this.toolPanel.Controls.Add(this.DeleteButton);
            this.toolPanel.Controls.Add(this.ColorPickerButton);
            this.toolPanel.Controls.Add(this.UndoButton);
            this.toolPanel.Controls.Add(this.ClearButton);
            this.toolPanel.Controls.Add(this.PolygonButton);
            this.toolPanel.Controls.Add(this.LineButton);
            this.toolPanel.Controls.Add(this.CircleButton);
            this.toolPanel.Controls.Add(this.SquareButton);
            this.toolPanel.Controls.Add(this.RectangleButton);
            this.toolPanel.Controls.Add(this.FillColorCheckBox);
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolPanel.Location = new System.Drawing.Point(0, 24);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(969, 49);
            this.toolPanel.TabIndex = 10;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(276, 23);
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
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.rotateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(969, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadBackgroundImageToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 24);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
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
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.rotateToolStripMenuItem.Text = "Rotate";
            this.rotateToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // mouseLabel
            // 
            this.mouseLabel.AutoSize = true;
            this.mouseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.mouseLabel.Location = new System.Drawing.Point(4, 31);
            this.mouseLabel.Name = "mouseLabel";
            this.mouseLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 5);
            this.mouseLabel.Size = new System.Drawing.Size(158, 27);
            this.mouseLabel.TabIndex = 12;
            this.mouseLabel.Text = "Mouse Position = ";
            // 
            // rulerLabel
            // 
            this.rulerLabel.AutoSize = true;
            this.rulerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rulerLabel.Location = new System.Drawing.Point(4, 4);
            this.rulerLabel.Name = "rulerLabel";
            this.rulerLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 5);
            this.rulerLabel.Size = new System.Drawing.Size(163, 27);
            this.rulerLabel.TabIndex = 13;
            this.rulerLabel.Text = "Selected Button = ";
            // 
            // TextPanel
            // 
            this.TextPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TextPanel.AutoSize = true;
            this.TextPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TextPanel.Controls.Add(this.rulerLabel);
            this.TextPanel.Controls.Add(this.mouseLabel);
            this.TextPanel.Location = new System.Drawing.Point(1, 442);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Size = new System.Drawing.Size(265, 77);
            this.TextPanel.TabIndex = 14;
            // 
            // WebViewerButton
            // 
            this.WebViewerButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("WebViewerButton.BackgroundImage")));
            this.WebViewerButton.Image = global::Vector_Drawing_Application.Properties.Resources.web_icon_40x40_32x32;
            this.WebViewerButton.Location = new System.Drawing.Point(920, 1);
            this.WebViewerButton.Name = "WebViewerButton";
            this.WebViewerButton.Size = new System.Drawing.Size(45, 45);
            this.WebViewerButton.TabIndex = 27;
            this.toolTip1.SetToolTip(this.WebViewerButton, "View on Web");
            this.WebViewerButton.UseVisualStyleBackColor = true;
            this.WebViewerButton.Click += new System.EventHandler(this.WebViewerButton_Click);
            // 
            // DecryptionButton
            // 
            this.DecryptionButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DecryptionButton.BackgroundImage")));
            this.DecryptionButton.Image = global::Vector_Drawing_Application.Properties.Resources.decryption_icon_32x32;
            this.DecryptionButton.Location = new System.Drawing.Point(876, 1);
            this.DecryptionButton.Name = "DecryptionButton";
            this.DecryptionButton.Size = new System.Drawing.Size(45, 45);
            this.DecryptionButton.TabIndex = 26;
            this.toolTip1.SetToolTip(this.DecryptionButton, "Decrypt File");
            this.DecryptionButton.UseVisualStyleBackColor = true;
            this.DecryptionButton.Click += new System.EventHandler(this.DecryptionButton_Click);
            // 
            // CurveButton
            // 
            this.CurveButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CurveButton.Image = global::Vector_Drawing_Application.Properties.Resources.curve_icon_32x32;
            this.CurveButton.Location = new System.Drawing.Point(221, 1);
            this.CurveButton.Name = "CurveButton";
            this.CurveButton.Size = new System.Drawing.Size(45, 45);
            this.CurveButton.TabIndex = 14;
            this.toolTip1.SetToolTip(this.CurveButton, "Draw Curve");
            this.CurveButton.UseVisualStyleBackColor = false;
            this.CurveButton.Click += new System.EventHandler(this.CurveButton_Click);
            // 
            // EncryptionButton
            // 
            this.EncryptionButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EncryptionButton.BackgroundImage")));
            this.EncryptionButton.Image = global::Vector_Drawing_Application.Properties.Resources.encryption_32x32;
            this.EncryptionButton.Location = new System.Drawing.Point(832, 1);
            this.EncryptionButton.Name = "EncryptionButton";
            this.EncryptionButton.Size = new System.Drawing.Size(45, 45);
            this.EncryptionButton.TabIndex = 25;
            this.toolTip1.SetToolTip(this.EncryptionButton, "Encrypte File");
            this.EncryptionButton.UseVisualStyleBackColor = true;
            this.EncryptionButton.Click += new System.EventHandler(this.EncryptionButton_Click);
            // 
            // RulerButton
            // 
            this.RulerButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RulerButton.BackgroundImage")));
            this.RulerButton.Image = global::Vector_Drawing_Application.Properties.Resources.ruler_icon_1_40x40;
            this.RulerButton.Location = new System.Drawing.Point(480, 1);
            this.RulerButton.Name = "RulerButton";
            this.RulerButton.Size = new System.Drawing.Size(45, 45);
            this.RulerButton.TabIndex = 25;
            this.toolTip1.SetToolTip(this.RulerButton, "Length and Area");
            this.RulerButton.UseVisualStyleBackColor = true;
            this.RulerButton.Click += new System.EventHandler(this.RulerButton_Click);
            // 
            // Help_Button
            // 
            this.Help_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Help_Button.BackgroundImage")));
            this.Help_Button.Image = global::Vector_Drawing_Application.Properties.Resources.question_icon_32x32;
            this.Help_Button.Location = new System.Drawing.Point(788, 1);
            this.Help_Button.Name = "Help_Button";
            this.Help_Button.Size = new System.Drawing.Size(45, 45);
            this.Help_Button.TabIndex = 24;
            this.toolTip1.SetToolTip(this.Help_Button, "Help");
            this.Help_Button.UseVisualStyleBackColor = true;
            this.Help_Button.Click += new System.EventHandler(this.Help_Button_Click);
            // 
            // StretchButton
            // 
            this.StretchButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StretchButton.BackgroundImage")));
            this.StretchButton.Image = ((System.Drawing.Image)(resources.GetObject("StretchButton.Image")));
            this.StretchButton.Location = new System.Drawing.Point(436, 1);
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
            this.LoadButton.Image = global::Vector_Drawing_Application.Properties.Resources.load_icon_32x32;
            this.LoadButton.Location = new System.Drawing.Point(744, 1);
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
            this.SelectButton.Image = global::Vector_Drawing_Application.Properties.Resources.select_icon;
            this.SelectButton.Location = new System.Drawing.Point(348, 1);
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
            this.MoveButton.Image = global::Vector_Drawing_Application.Properties.Resources.move_icon;
            this.MoveButton.Location = new System.Drawing.Point(392, 1);
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
            this.SaveButton.Image = global::Vector_Drawing_Application.Properties.Resources.save_icon_32x32;
            this.SaveButton.Location = new System.Drawing.Point(700, 1);
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
            this.DeleteButton.Image = global::Vector_Drawing_Application.Properties.Resources.eraser_icon_1_48x32;
            this.DeleteButton.Location = new System.Drawing.Point(524, 1);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(45, 45);
            this.DeleteButton.TabIndex = 20;
            this.toolTip1.SetToolTip(this.DeleteButton, "Delete");
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ColorPickerButton
            // 
            this.ColorPickerButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ColorPickerButton.BackgroundImage")));
            this.ColorPickerButton.Image = global::Vector_Drawing_Application.Properties.Resources.color_icon_32x32;
            this.ColorPickerButton.Location = new System.Drawing.Point(656, 1);
            this.ColorPickerButton.Name = "ColorPickerButton";
            this.ColorPickerButton.Size = new System.Drawing.Size(45, 45);
            this.ColorPickerButton.TabIndex = 17;
            this.toolTip1.SetToolTip(this.ColorPickerButton, "Pick Color");
            this.ColorPickerButton.UseVisualStyleBackColor = true;
            this.ColorPickerButton.Click += new System.EventHandler(this.ColorPickerButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UndoButton.BackgroundImage")));
            this.UndoButton.Image = global::Vector_Drawing_Application.Properties.Resources.undo_icon_2_48x32;
            this.UndoButton.Location = new System.Drawing.Point(612, 1);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(45, 45);
            this.UndoButton.TabIndex = 21;
            this.toolTip1.SetToolTip(this.UndoButton, "Undo");
            this.UndoButton.UseVisualStyleBackColor = true;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClearButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ClearButton.BackgroundImage")));
            this.ClearButton.Image = global::Vector_Drawing_Application.Properties.Resources.delete_icon_32x32;
            this.ClearButton.Location = new System.Drawing.Point(568, 1);
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
            this.PolygonButton.Image = global::Vector_Drawing_Application.Properties.Resources.polygon_icon_1_32x32;
            this.PolygonButton.Location = new System.Drawing.Point(177, 1);
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
            this.LineButton.Image = global::Vector_Drawing_Application.Properties.Resources.line_icon_v2_24x24;
            this.LineButton.Location = new System.Drawing.Point(133, 1);
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
            this.CircleButton.Image = global::Vector_Drawing_Application.Properties.Resources.circle_icon_32x32;
            this.CircleButton.Location = new System.Drawing.Point(89, 1);
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
            this.SquareButton.Image = global::Vector_Drawing_Application.Properties.Resources.square_icon_44x48;
            this.SquareButton.Location = new System.Drawing.Point(45, 1);
            this.SquareButton.Name = "SquareButton";
            this.SquareButton.Size = new System.Drawing.Size(45, 45);
            this.SquareButton.TabIndex = 8;
            this.toolTip1.SetToolTip(this.SquareButton, "Draw Square");
            this.SquareButton.UseVisualStyleBackColor = false;
            this.SquareButton.Click += new System.EventHandler(this.SquareButton_Click);
            // 
            // RectangleButton
            // 
            this.RectangleButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RectangleButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RectangleButton.BackgroundImage")));
            this.RectangleButton.Image = global::Vector_Drawing_Application.Properties.Resources.rectangle_icon_48x32;
            this.RectangleButton.Location = new System.Drawing.Point(1, 1);
            this.RectangleButton.Name = "RectangleButton";
            this.RectangleButton.Size = new System.Drawing.Size(45, 45);
            this.RectangleButton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.RectangleButton, "Draw Rectangle");
            this.RectangleButton.UseVisualStyleBackColor = false;
            this.RectangleButton.Click += new System.EventHandler(this.RectButton_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // loadBackgroundImageToolStripMenuItem
            // 
            this.loadBackgroundImageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadBackgroundImageToolStripMenuItem.Image")));
            this.loadBackgroundImageToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadBackgroundImageToolStripMenuItem.Name = "loadBackgroundImageToolStripMenuItem";
            this.loadBackgroundImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadBackgroundImageToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.loadBackgroundImageToolStripMenuItem.Text = "&Load Background Image";
            this.loadBackgroundImageToolStripMenuItem.Click += new System.EventHandler(this.LoadBackgroundImageToolStripMenuItem_Click);
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
            // Vector_Drawing_Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(969, 520);
            this.Controls.Add(this.TextPanel);
            this.Controls.Add(this.toolPanel);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 45);
            this.MinimumSize = new System.Drawing.Size(851, 212);
            this.Name = "Vector_Drawing_Application";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vector Drawing Application";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Vector_Drawing_Application_FormClosing);
            this.Load += new System.EventHandler(this.Vector_Drawing_Application_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.toolPanel.ResumeLayout(false);
            this.toolPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TextPanel.ResumeLayout(false);
            this.TextPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RectangleButton;
        private System.Windows.Forms.CheckBox FillColorCheckBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel toolPanel;
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
        private System.Windows.Forms.Button RulerButton;
        private System.Windows.Forms.Label mouseLabel;
        private System.Windows.Forms.Label rulerLabel;
        private System.Windows.Forms.Button CurveButton;
        private System.Windows.Forms.ToolStripMenuItem loadBackgroundImageToolStripMenuItem;
        private System.Windows.Forms.Panel TextPanel;
        private System.Windows.Forms.Button DecryptionButton;
        private System.Windows.Forms.Button EncryptionButton;
        private System.Windows.Forms.Button WebViewerButton;
    }
}