namespace ChatOnCom
{
    partial class frmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.cobBaudRate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cobStopbits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cobDataBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cobParity = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cobPortName = new System.Windows.Forms.ComboBox();
            this.btnDefaultSetting = new System.Windows.Forms.Button();
            this.ucHeader1 = new HQ_CONTROLS.ucHeader();
            this.SuspendLayout();
            // 
            // cobBaudRate
            // 
            this.cobBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobBaudRate.FormattingEnabled = true;
            this.cobBaudRate.Location = new System.Drawing.Point(96, 90);
            this.cobBaudRate.Name = "cobBaudRate";
            this.cobBaudRate.Size = new System.Drawing.Size(183, 21);
            this.cobBaudRate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Baud Rate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Stop Bits:";
            // 
            // cobStopbits
            // 
            this.cobStopbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobStopbits.FormattingEnabled = true;
            this.cobStopbits.Location = new System.Drawing.Point(96, 117);
            this.cobStopbits.Name = "cobStopbits";
            this.cobStopbits.Size = new System.Drawing.Size(183, 21);
            this.cobStopbits.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Data Bits";
            // 
            // cobDataBits
            // 
            this.cobDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobDataBits.FormattingEnabled = true;
            this.cobDataBits.Location = new System.Drawing.Point(96, 144);
            this.cobDataBits.Name = "cobDataBits";
            this.cobDataBits.Size = new System.Drawing.Size(183, 21);
            this.cobDataBits.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parity";
            // 
            // cobParity
            // 
            this.cobParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobParity.FormattingEnabled = true;
            this.cobParity.Location = new System.Drawing.Point(96, 171);
            this.cobParity.Name = "cobParity";
            this.cobParity.Size = new System.Drawing.Size(183, 21);
            this.cobParity.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(204, 208);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(123, 208);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu lại";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Port Name:";
            // 
            // cobPortName
            // 
            this.cobPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobPortName.FormattingEnabled = true;
            this.cobPortName.Location = new System.Drawing.Point(96, 63);
            this.cobPortName.Name = "cobPortName";
            this.cobPortName.Size = new System.Drawing.Size(183, 21);
            this.cobPortName.TabIndex = 11;
            // 
            // btnDefaultSetting
            // 
            this.btnDefaultSetting.Location = new System.Drawing.Point(15, 208);
            this.btnDefaultSetting.Name = "btnDefaultSetting";
            this.btnDefaultSetting.Size = new System.Drawing.Size(75, 23);
            this.btnDefaultSetting.TabIndex = 13;
            this.btnDefaultSetting.Text = "Mặc định";
            this.btnDefaultSetting.UseVisualStyleBackColor = true;
            this.btnDefaultSetting.Click += new System.EventHandler(this.btnDefaultSetting_Click);
            // 
            // ucHeader1
            // 
            this.ucHeader1.BackColor = System.Drawing.Color.Transparent;
            this.ucHeader1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucHeader1.BackgroundImage")));
            this.ucHeader1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.pictureHeader = ((System.Drawing.Image)(resources.GetObject("ucHeader1.pictureHeader")));
            this.ucHeader1.Size = new System.Drawing.Size(305, 61);
            this.ucHeader1.TabIndex = 0;
            this.ucHeader1.TextHeader = "Cấu hình kết nối";
            this.ucHeader1.TextHeaderColor = System.Drawing.Color.DarkBlue;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(305, 248);
            this.Controls.Add(this.btnDefaultSetting);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cobPortName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cobParity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cobDataBits);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cobStopbits);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cobBaudRate);
            this.Controls.Add(this.ucHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình kết nối";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HQ_CONTROLS.ucHeader ucHeader1;
        private System.Windows.Forms.ComboBox cobBaudRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cobStopbits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cobDataBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobParity;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cobPortName;
        private System.Windows.Forms.Button btnDefaultSetting;
    }
}