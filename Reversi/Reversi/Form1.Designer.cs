namespace Reversi
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
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblCurrentPlayer = new System.Windows.Forms.Label();
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.txtPlayer1 = new System.Windows.Forms.TextBox();
            this.txtPlayer2 = new System.Windows.Forms.TextBox();
            this.btNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.lblHumanColor = new System.Windows.Forms.Label();
            this.cmbHumanColor = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "blank.jpg");
            this.imgList.Images.SetKeyName(1, "ply1.jpg");
            this.imgList.Images.SetKeyName(2, "ply2.jpg");
            this.imgList.Images.SetKeyName(3, "hover.jpg");
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(422, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(75, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            // 
            // lblCurrentPlayer
            // 
            this.lblCurrentPlayer.AutoSize = true;
            this.lblCurrentPlayer.Location = new System.Drawing.Point(419, 166);
            this.lblCurrentPlayer.Name = "lblCurrentPlayer";
            this.lblCurrentPlayer.Size = new System.Drawing.Size(72, 13);
            this.lblCurrentPlayer.TabIndex = 1;
            this.lblCurrentPlayer.Text = "Current player";
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.AutoSize = true;
            this.lblPlayer1.Location = new System.Drawing.Point(419, 277);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(69, 13);
            this.lblPlayer1.TabIndex = 2;
            this.lblPlayer1.Text = "Player 1 (red)";
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.AutoSize = true;
            this.lblPlayer2.Location = new System.Drawing.Point(419, 326);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(74, 13);
            this.lblPlayer2.TabIndex = 3;
            this.lblPlayer2.Text = "Player 2 (blue)";
            // 
            // txtPlayer1
            // 
            this.txtPlayer1.Location = new System.Drawing.Point(422, 293);
            this.txtPlayer1.Name = "txtPlayer1";
            this.txtPlayer1.Size = new System.Drawing.Size(42, 20);
            this.txtPlayer1.TabIndex = 4;
            this.txtPlayer1.TabStop = false;
            // 
            // txtPlayer2
            // 
            this.txtPlayer2.Location = new System.Drawing.Point(422, 342);
            this.txtPlayer2.Name = "txtPlayer2";
            this.txtPlayer2.Size = new System.Drawing.Size(42, 20);
            this.txtPlayer2.TabIndex = 5;
            this.txtPlayer2.TabStop = false;
            // 
            // btNew
            // 
            this.btNew.Location = new System.Drawing.Point(422, 38);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(75, 23);
            this.btNew.TabIndex = 6;
            this.btNew.Text = "New game";
            this.btNew.UseVisualStyleBackColor = true;
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mode";
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            "2 players",
            "Minimax",
            "αβ Pruning"});
            this.cmbMode.Location = new System.Drawing.Point(422, 94);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(88, 21);
            this.cmbMode.TabIndex = 8;
            this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.cmbMode_SelectedIndexChanged);
            // 
            // lblHumanColor
            // 
            this.lblHumanColor.AutoSize = true;
            this.lblHumanColor.Location = new System.Drawing.Point(419, 118);
            this.lblHumanColor.Name = "lblHumanColor";
            this.lblHumanColor.Size = new System.Drawing.Size(68, 13);
            this.lblHumanColor.TabIndex = 9;
            this.lblHumanColor.Text = "Human Color";
            // 
            // cmbHumanColor
            // 
            this.cmbHumanColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHumanColor.FormattingEnabled = true;
            this.cmbHumanColor.Items.AddRange(new object[] {
            "red",
            "blue"});
            this.cmbHumanColor.Location = new System.Drawing.Point(422, 134);
            this.cmbHumanColor.Name = "cmbHumanColor";
            this.cmbHumanColor.Size = new System.Drawing.Size(88, 21);
            this.cmbHumanColor.TabIndex = 10;
            this.cmbHumanColor.SelectedIndexChanged += new System.EventHandler(this.cmbHumanColor_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 441);
            this.Controls.Add(this.cmbHumanColor);
            this.Controls.Add(this.lblHumanColor);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btNew);
            this.Controls.Add(this.txtPlayer2);
            this.Controls.Add(this.txtPlayer1);
            this.Controls.Add(this.lblPlayer2);
            this.Controls.Add(this.lblPlayer1);
            this.Controls.Add(this.lblCurrentPlayer);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Reversi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblCurrentPlayer;
        private System.Windows.Forms.Label lblPlayer1;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.TextBox txtPlayer1;
        private System.Windows.Forms.TextBox txtPlayer2;
        private System.Windows.Forms.Button btNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Label lblHumanColor;
        private System.Windows.Forms.ComboBox cmbHumanColor;

    }
}

