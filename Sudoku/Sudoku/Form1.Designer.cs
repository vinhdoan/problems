namespace Sudoku
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
            this.dfs = new System.Windows.Forms.Button();
            this.bfs = new System.Windows.Forms.Button();
            this.hillclimbing = new System.Windows.Forms.Button();
            this.selectInput = new System.Windows.Forms.ComboBox();
            this.refreshbt = new System.Windows.Forms.Button();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblHV = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMemory = new System.Windows.Forms.Label();
            this.txtHV = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtMemory = new System.Windows.Forms.TextBox();
            this.hillclimbing2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dfs
            // 
            this.dfs.Location = new System.Drawing.Point(475, 289);
            this.dfs.Name = "dfs";
            this.dfs.Size = new System.Drawing.Size(75, 23);
            this.dfs.TabIndex = 4;
            this.dfs.Text = "DFS";
            this.dfs.UseVisualStyleBackColor = true;
            this.dfs.Click += new System.EventHandler(this.dfs_Click);
            // 
            // bfs
            // 
            this.bfs.Location = new System.Drawing.Point(475, 318);
            this.bfs.Name = "bfs";
            this.bfs.Size = new System.Drawing.Size(75, 23);
            this.bfs.TabIndex = 5;
            this.bfs.Text = "BFS";
            this.bfs.UseVisualStyleBackColor = true;
            this.bfs.Click += new System.EventHandler(this.bfs_Click);
            // 
            // hillclimbing
            // 
            this.hillclimbing.Location = new System.Drawing.Point(466, 347);
            this.hillclimbing.Name = "hillclimbing";
            this.hillclimbing.Size = new System.Drawing.Size(84, 23);
            this.hillclimbing.TabIndex = 6;
            this.hillclimbing.Text = "Hill Climbing 1\r\n";
            this.hillclimbing.UseVisualStyleBackColor = true;
            this.hillclimbing.Click += new System.EventHandler(this.hillclimbing_Click);
            // 
            // selectInput
            // 
            this.selectInput.FormattingEnabled = true;
            this.selectInput.Items.AddRange(new object[] {
            "Input0",
            "Input1",
            "Input2",
            "Input3",
            "Input4",
            "Input5",
            "Input6",
            "Input7",
            "Input8",
            "Input9"});
            this.selectInput.Location = new System.Drawing.Point(475, 217);
            this.selectInput.Name = "selectInput";
            this.selectInput.Size = new System.Drawing.Size(75, 21);
            this.selectInput.TabIndex = 7;
            this.selectInput.SelectedIndexChanged += new System.EventHandler(this.selectInput_SelectedIndexChanged);
            // 
            // refreshbt
            // 
            this.refreshbt.Location = new System.Drawing.Point(475, 244);
            this.refreshbt.Name = "refreshbt";
            this.refreshbt.Size = new System.Drawing.Size(75, 23);
            this.refreshbt.TabIndex = 8;
            this.refreshbt.Text = "Refresh";
            this.refreshbt.UseVisualStyleBackColor = true;
            this.refreshbt.Click += new System.EventHandler(this.refreshbt_Click);
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(519, 201);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(31, 13);
            this.lblInput.TabIndex = 11;
            this.lblInput.Text = "Input";
            // 
            // lblHV
            // 
            this.lblHV.AutoSize = true;
            this.lblHV.Location = new System.Drawing.Point(472, 31);
            this.lblHV.Name = "lblHV";
            this.lblHV.Size = new System.Drawing.Size(78, 13);
            this.lblHV.TabIndex = 12;
            this.lblHV.Text = "Heuristic Value";
            this.lblHV.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(472, 80);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(30, 13);
            this.lblTime.TabIndex = 13;
            this.lblTime.Text = "Time";
            this.lblTime.Visible = false;
            // 
            // lblMemory
            // 
            this.lblMemory.AutoSize = true;
            this.lblMemory.Location = new System.Drawing.Point(472, 133);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(44, 13);
            this.lblMemory.TabIndex = 14;
            this.lblMemory.Text = "Memory";
            this.lblMemory.Visible = false;
            // 
            // txtHV
            // 
            this.txtHV.Location = new System.Drawing.Point(475, 47);
            this.txtHV.Name = "txtHV";
            this.txtHV.Size = new System.Drawing.Size(75, 20);
            this.txtHV.TabIndex = 15;
            this.txtHV.Visible = false;
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(475, 96);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(75, 20);
            this.txtTime.TabIndex = 16;
            this.txtTime.Visible = false;
            // 
            // txtMemory
            // 
            this.txtMemory.Location = new System.Drawing.Point(475, 149);
            this.txtMemory.Name = "txtMemory";
            this.txtMemory.Size = new System.Drawing.Size(75, 20);
            this.txtMemory.TabIndex = 17;
            this.txtMemory.Visible = false;
            // 
            // hillclimbing2
            // 
            this.hillclimbing2.Location = new System.Drawing.Point(466, 376);
            this.hillclimbing2.Name = "hillclimbing2";
            this.hillclimbing2.Size = new System.Drawing.Size(84, 23);
            this.hillclimbing2.TabIndex = 18;
            this.hillclimbing2.Text = "Hill Climbing 2";
            this.hillclimbing2.UseVisualStyleBackColor = true;
            this.hillclimbing2.Click += new System.EventHandler(this.hillclimbing2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 414);
            this.Controls.Add(this.hillclimbing2);
            this.Controls.Add(this.txtMemory);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtHV);
            this.Controls.Add(this.lblMemory);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblHV);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.refreshbt);
            this.Controls.Add(this.selectInput);
            this.Controls.Add(this.hillclimbing);
            this.Controls.Add(this.bfs);
            this.Controls.Add(this.dfs);
            this.Name = "Form1";
            this.Text = "SUDOKU";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dfs;
        private System.Windows.Forms.Button bfs;
        private System.Windows.Forms.Button hillclimbing;
        private System.Windows.Forms.ComboBox selectInput;
        private System.Windows.Forms.Button refreshbt;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblHV;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblMemory;
        private System.Windows.Forms.TextBox txtHV;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtMemory;
        private System.Windows.Forms.Button hillclimbing2;

    }
}

