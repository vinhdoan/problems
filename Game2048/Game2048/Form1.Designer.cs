namespace Game2048
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
            this.txtScore = new System.Windows.Forms.TextBox();
            this.selectInput = new System.Windows.Forms.ComboBox();
            this.dfs = new System.Windows.Forms.Button();
            this.bfs = new System.Windows.Forms.Button();
            this.hc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtScore
            // 
            this.txtScore.Enabled = false;
            this.txtScore.Location = new System.Drawing.Point(426, 27);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(86, 20);
            this.txtScore.TabIndex = 0;
            this.txtScore.Text = "0";
            // 
            // selectInput
            // 
            this.selectInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.selectInput.Location = new System.Drawing.Point(426, 203);
            this.selectInput.Name = "selectInput";
            this.selectInput.Size = new System.Drawing.Size(86, 21);
            this.selectInput.TabIndex = 1;
            this.selectInput.TabStop = false;
            this.selectInput.SelectedIndexChanged += new System.EventHandler(this.selectInput_SelectedIndexChanged);
            // 
            // dfs
            // 
            this.dfs.Location = new System.Drawing.Point(426, 319);
            this.dfs.Name = "dfs";
            this.dfs.Size = new System.Drawing.Size(75, 23);
            this.dfs.TabIndex = 2;
            this.dfs.Text = "DFS";
            this.dfs.UseVisualStyleBackColor = true;
            this.dfs.Click += new System.EventHandler(this.dfs_Click);
            // 
            // bfs
            // 
            this.bfs.Location = new System.Drawing.Point(426, 348);
            this.bfs.Name = "bfs";
            this.bfs.Size = new System.Drawing.Size(75, 23);
            this.bfs.TabIndex = 3;
            this.bfs.Text = "BFS";
            this.bfs.UseVisualStyleBackColor = true;
            this.bfs.Click += new System.EventHandler(this.bfs_Click);
            // 
            // hc
            // 
            this.hc.Location = new System.Drawing.Point(426, 377);
            this.hc.Name = "hc";
            this.hc.Size = new System.Drawing.Size(75, 23);
            this.hc.TabIndex = 4;
            this.hc.Text = "HC";
            this.hc.UseVisualStyleBackColor = true;
            this.hc.Click += new System.EventHandler(this.hc_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 441);
            this.Controls.Add(this.hc);
            this.Controls.Add(this.bfs);
            this.Controls.Add(this.dfs);
            this.Controls.Add(this.selectInput);
            this.Controls.Add(this.txtScore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "2048";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtScore;
        private System.Windows.Forms.ComboBox selectInput;
        private System.Windows.Forms.Button dfs;
        private System.Windows.Forms.Button bfs;
        private System.Windows.Forms.Button hc;
    }
}

