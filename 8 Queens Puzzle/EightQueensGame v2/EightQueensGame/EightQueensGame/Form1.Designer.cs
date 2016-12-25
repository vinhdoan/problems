namespace EightQueensGame
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tácGiảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Solve = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.btn_New = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Load = new System.Windows.Forms.Button();
            this.btn_Help = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioInstant = new System.Windows.Forms.RadioButton();
            this.radioFast = new System.Windows.Forms.RadioButton();
            this.radioMedium = new System.Windows.Forms.RadioButton();
            this.radioSlow = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(734, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.fileToolStripMenuItem.Text = "Tùy &chọn";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.newToolStripMenuItem.Text = "Trò chơi &mới";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveToolStripMenuItem.Text = "&Lưu trò chơi";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.loadToolStripMenuItem.Text = "&Tiếp tục chơi";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Th&oát";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.tácGiảToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.helpToolStripMenuItem.Text = "&Giúp đỡ";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.aboutToolStripMenuItem.Text = "&Hướng dẫn";
            // 
            // tácGiảToolStripMenuItem
            // 
            this.tácGiảToolStripMenuItem.Name = "tácGiảToolStripMenuItem";
            this.tácGiảToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tácGiảToolStripMenuItem.Text = "Thô&ng tin";
            // 
            // btn_Solve
            // 
            this.btn_Solve.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Solve.Location = new System.Drawing.Point(621, 256);
            this.btn_Solve.Name = "btn_Solve";
            this.btn_Solve.Size = new System.Drawing.Size(101, 33);
            this.btn_Solve.TabIndex = 5;
            this.btn_Solve.Text = "&Giải";
            this.btn_Solve.UseVisualStyleBackColor = true;
            this.btn_Solve.Click += new System.EventHandler(this.btn_Solve_Click);
            this.btn_Solve.MouseHover += new System.EventHandler(this.btn_Solve_MouseHover);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "0.jpg");
            this.imageList2.Images.SetKeyName(1, "1.jpg");
            this.imageList2.Images.SetKeyName(2, "2.jpg");
            this.imageList2.Images.SetKeyName(3, "3.jpg");
            this.imageList2.Images.SetKeyName(4, "4.jpg");
            this.imageList2.Images.SetKeyName(5, "5.jpg");
            // 
            // btn_New
            // 
            this.btn_New.Location = new System.Drawing.Point(621, 112);
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(101, 33);
            this.btn_New.TabIndex = 2;
            this.btn_New.Text = "Trò chơi &mới";
            this.btn_New.UseVisualStyleBackColor = true;
            this.btn_New.Click += new System.EventHandler(this.btn_New_Click);
            this.btn_New.MouseHover += new System.EventHandler(this.btn_New_MouseHover);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(621, 151);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(101, 33);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "&Lưu trò chơi";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            this.btn_Save.MouseHover += new System.EventHandler(this.btn_Save_MouseHover);
            // 
            // btn_Load
            // 
            this.btn_Load.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Load.Location = new System.Drawing.Point(621, 190);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(101, 33);
            this.btn_Load.TabIndex = 4;
            this.btn_Load.Text = "&Tiếp tục chơi";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            this.btn_Load.MouseHover += new System.EventHandler(this.btn_Load_MouseHover);
            // 
            // btn_Help
            // 
            this.btn_Help.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Help.Location = new System.Drawing.Point(621, 42);
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.Size = new System.Drawing.Size(101, 33);
            this.btn_Help.TabIndex = 1;
            this.btn_Help.Text = "&Hướng dẫn";
            this.btn_Help.UseVisualStyleBackColor = true;
            this.btn_Help.Click += new System.EventHandler(this.btn_Help_Click);
            this.btn_Help.MouseHover += new System.EventHandler(this.btn_Help_MouseHover);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 637);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "...";
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = global::EightQueensGame.Properties.Resources.Nền;
            this.groupBox1.Controls.Add(this.radioInstant);
            this.groupBox1.Controls.Add(this.radioFast);
            this.groupBox1.Controls.Add(this.radioMedium);
            this.groupBox1.Controls.Add(this.radioSlow);
            this.groupBox1.Location = new System.Drawing.Point(640, 295);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(82, 118);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tốc độ";
            // 
            // radioInstant
            // 
            this.radioInstant.AutoSize = true;
            this.radioInstant.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioInstant.BackgroundImage")));
            this.radioInstant.Location = new System.Drawing.Point(12, 90);
            this.radioInstant.Name = "radioInstant";
            this.radioInstant.Size = new System.Drawing.Size(61, 17);
            this.radioInstant.TabIndex = 3;
            this.radioInstant.TabStop = true;
            this.radioInstant.Text = "Lập tức";
            this.radioInstant.UseVisualStyleBackColor = true;
            this.radioInstant.CheckedChanged += new System.EventHandler(this.radioInstant_CheckedChanged);
            this.radioInstant.MouseHover += new System.EventHandler(this.radioInstant_MouseHover);
            // 
            // radioFast
            // 
            this.radioFast.AutoSize = true;
            this.radioFast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioFast.BackgroundImage")));
            this.radioFast.Location = new System.Drawing.Point(12, 67);
            this.radioFast.Name = "radioFast";
            this.radioFast.Size = new System.Drawing.Size(57, 17);
            this.radioFast.TabIndex = 2;
            this.radioFast.Text = "Nhanh";
            this.radioFast.UseVisualStyleBackColor = true;
            this.radioFast.CheckedChanged += new System.EventHandler(this.radioFast_CheckedChanged);
            this.radioFast.MouseHover += new System.EventHandler(this.radioFast_MouseHover);
            // 
            // radioMedium
            // 
            this.radioMedium.AutoSize = true;
            this.radioMedium.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioMedium.BackgroundImage")));
            this.radioMedium.Checked = true;
            this.radioMedium.Location = new System.Drawing.Point(12, 43);
            this.radioMedium.Name = "radioMedium";
            this.radioMedium.Size = new System.Drawing.Size(44, 17);
            this.radioMedium.TabIndex = 1;
            this.radioMedium.TabStop = true;
            this.radioMedium.Text = "Vừa";
            this.radioMedium.UseVisualStyleBackColor = true;
            this.radioMedium.CheckedChanged += new System.EventHandler(this.radioMedium_CheckedChanged);
            this.radioMedium.MouseHover += new System.EventHandler(this.radioMedium_MouseHover);
            // 
            // radioSlow
            // 
            this.radioSlow.AutoSize = true;
            this.radioSlow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioSlow.BackgroundImage")));
            this.radioSlow.Location = new System.Drawing.Point(12, 19);
            this.radioSlow.Name = "radioSlow";
            this.radioSlow.Size = new System.Drawing.Size(52, 17);
            this.radioSlow.TabIndex = 0;
            this.radioSlow.Text = "Chậm";
            this.radioSlow.UseVisualStyleBackColor = true;
            this.radioSlow.CheckedChanged += new System.EventHandler(this.radioSlow_CheckedChanged);
            this.radioSlow.MouseHover += new System.EventHandler(this.radioSlow_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(621, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Chế độ tự động";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(621, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tùy chọn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::EightQueensGame.Properties.Resources.Nền;
            this.ClientSize = new System.Drawing.Size(734, 662);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Help);
            this.Controls.Add(this.btn_Load);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_New);
            this.Controls.Add(this.btn_Solve);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trò chơi Bát Hậu";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btn_Solve;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Button btn_New;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Load;
        private System.Windows.Forms.Button btn_Help;
        private System.Windows.Forms.ToolStripMenuItem tácGiảToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioFast;
        private System.Windows.Forms.RadioButton radioMedium;
        private System.Windows.Forms.RadioButton radioSlow;
        private System.Windows.Forms.RadioButton radioInstant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

