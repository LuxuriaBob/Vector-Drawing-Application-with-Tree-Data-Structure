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
            this.MoveButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.RectangleButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.UndoButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.ColorPickerButton = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.FillColorCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CircleButton = new System.Windows.Forms.Button();
            this.SquareButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LineButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MoveButton
            // 
            this.MoveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MoveButton.BackgroundImage")));
            this.MoveButton.Image = ((System.Drawing.Image)(resources.GetObject("MoveButton.Image")));
            this.MoveButton.Location = new System.Drawing.Point(299, 0);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(45, 45);
            this.MoveButton.TabIndex = 0;
            this.toolTip1.SetToolTip(this.MoveButton, "Move");
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClearButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ClearButton.BackgroundImage")));
            this.ClearButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearButton.Image")));
            this.ClearButton.Location = new System.Drawing.Point(385, 0);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(45, 45);
            this.ClearButton.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ClearButton, "Clear Canvas");
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
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
            // SelectButton
            // 
            this.SelectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SelectButton.BackgroundImage")));
            this.SelectButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectButton.Image")));
            this.SelectButton.Location = new System.Drawing.Point(256, 0);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(45, 45);
            this.SelectButton.TabIndex = 3;
            this.toolTip1.SetToolTip(this.SelectButton, "Select");
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteButton.BackgroundImage")));
            this.DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteButton.Image")));
            this.DeleteButton.Location = new System.Drawing.Point(342, 0);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(45, 45);
            this.DeleteButton.TabIndex = 4;
            this.toolTip1.SetToolTip(this.DeleteButton, "Erase");
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UndoButton.BackgroundImage")));
            this.UndoButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoButton.Image")));
            this.UndoButton.Location = new System.Drawing.Point(429, 0);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(45, 45);
            this.UndoButton.TabIndex = 5;
            this.toolTip1.SetToolTip(this.UndoButton, "Undo");
            this.UndoButton.UseVisualStyleBackColor = true;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SaveButton.BackgroundImage")));
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.Location = new System.Drawing.Point(517, 0);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(45, 45);
            this.SaveButton.TabIndex = 6;
            this.toolTip1.SetToolTip(this.SaveButton, "Save");
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LoadButton.BackgroundImage")));
            this.LoadButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadButton.Image")));
            this.LoadButton.Location = new System.Drawing.Point(561, 0);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(45, 45);
            this.LoadButton.TabIndex = 7;
            this.toolTip1.SetToolTip(this.LoadButton, "Load");
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // ColorPickerButton
            // 
            this.ColorPickerButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ColorPickerButton.BackgroundImage")));
            this.ColorPickerButton.Image = ((System.Drawing.Image)(resources.GetObject("ColorPickerButton.Image")));
            this.ColorPickerButton.Location = new System.Drawing.Point(473, 0);
            this.ColorPickerButton.Name = "ColorPickerButton";
            this.ColorPickerButton.Size = new System.Drawing.Size(45, 45);
            this.ColorPickerButton.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ColorPickerButton, "Pick Color");
            this.ColorPickerButton.UseVisualStyleBackColor = true;
            this.ColorPickerButton.Click += new System.EventHandler(this.ColorPickerButton_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(176, 18);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(81, 45);
            this.trackBar1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.trackBar1, "Change Pen Size");
            this.trackBar1.Value = 1;
            // 
            // FillColorCheckBox
            // 
            this.FillColorCheckBox.AutoSize = true;
            this.FillColorCheckBox.Location = new System.Drawing.Point(184, 3);
            this.FillColorCheckBox.Name = "FillColorCheckBox";
            this.FillColorCheckBox.Size = new System.Drawing.Size(65, 17);
            this.FillColorCheckBox.TabIndex = 4;
            this.FillColorCheckBox.Text = "Fill Color";
            this.FillColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LineButton);
            this.panel1.Controls.Add(this.CircleButton);
            this.panel1.Controls.Add(this.SquareButton);
            this.panel1.Controls.Add(this.LoadButton);
            this.panel1.Controls.Add(this.RectangleButton);
            this.panel1.Controls.Add(this.SelectButton);
            this.panel1.Controls.Add(this.FillColorCheckBox);
            this.panel1.Controls.Add(this.MoveButton);
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.DeleteButton);
            this.panel1.Controls.Add(this.ColorPickerButton);
            this.panel1.Controls.Add(this.UndoButton);
            this.panel1.Controls.Add(this.ClearButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 46);
            this.panel1.TabIndex = 10;
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
            // Vector_Drawing_Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(851, 522);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Vector_Drawing_Application";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Vector_Drawing_Application_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button MoveButton;
    private System.Windows.Forms.Button ClearButton;
    private System.Windows.Forms.Button RectangleButton;
    private System.Windows.Forms.Button SelectButton;
    private System.Windows.Forms.Button DeleteButton;
    private System.Windows.Forms.Button UndoButton;
    private System.Windows.Forms.Button SaveButton;
    private System.Windows.Forms.Button LoadButton;
    private System.Windows.Forms.Button ColorPickerButton;
    private System.Windows.Forms.TrackBar trackBar1;
    private System.Windows.Forms.CheckBox FillColorCheckBox;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SquareButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button CircleButton;
        private System.Windows.Forms.Button LineButton;
    }
}