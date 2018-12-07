namespace Tools.EnDecryption
{
    partial class FrmEnDecryption
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtIV = new System.Windows.Forms.TextBox();
            this.btnEnCryption = new System.Windows.Forms.Button();
            this.btnDeCryption = new System.Windows.Forms.Button();
            this.txtPlaintext = new System.Windows.Forms.TextBox();
            this.txtCiphertext = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCiphertext);
            this.groupBox1.Controls.Add(this.txtPlaintext);
            this.groupBox1.Controls.Add(this.btnDeCryption);
            this.groupBox1.Controls.Add(this.btnEnCryption);
            this.groupBox1.Controls.Add(this.txtIV);
            this.groupBox1.Controls.Add(this.txtKey);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(792, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DES加密解密";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "键（Key）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "初始化向量（IV）";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(77, 18);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(132, 21);
            this.txtKey.TabIndex = 2;
            // 
            // txtIV
            // 
            this.txtIV.Location = new System.Drawing.Point(409, 18);
            this.txtIV.Name = "txtIV";
            this.txtIV.Size = new System.Drawing.Size(132, 21);
            this.txtIV.TabIndex = 3;
            // 
            // btnEnCryption
            // 
            this.btnEnCryption.Location = new System.Drawing.Point(625, 16);
            this.btnEnCryption.Name = "btnEnCryption";
            this.btnEnCryption.Size = new System.Drawing.Size(75, 23);
            this.btnEnCryption.TabIndex = 4;
            this.btnEnCryption.Text = "DES加密";
            this.btnEnCryption.UseVisualStyleBackColor = true;
            this.btnEnCryption.Click += new System.EventHandler(this.btnEnCryption_Click);
            // 
            // btnDeCryption
            // 
            this.btnDeCryption.Location = new System.Drawing.Point(705, 16);
            this.btnDeCryption.Name = "btnDeCryption";
            this.btnDeCryption.Size = new System.Drawing.Size(75, 23);
            this.btnDeCryption.TabIndex = 5;
            this.btnDeCryption.Text = "DES解密";
            this.btnDeCryption.UseVisualStyleBackColor = true;
            this.btnDeCryption.Click += new System.EventHandler(this.btnDeCryption_Click);
            // 
            // txtPlaintext
            // 
            this.txtPlaintext.Location = new System.Drawing.Point(14, 52);
            this.txtPlaintext.Multiline = true;
            this.txtPlaintext.Name = "txtPlaintext";
            this.txtPlaintext.Size = new System.Drawing.Size(378, 94);
            this.txtPlaintext.TabIndex = 6;
            // 
            // txtCiphertext
            // 
            this.txtCiphertext.Location = new System.Drawing.Point(409, 52);
            this.txtCiphertext.Multiline = true;
            this.txtCiphertext.Name = "txtCiphertext";
            this.txtCiphertext.Size = new System.Drawing.Size(371, 94);
            this.txtCiphertext.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(215, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "8位字符";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(547, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "8位字符";
            // 
            // FrmEnDecryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmEnDecryption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "加密解密";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtIV;
        private System.Windows.Forms.Button btnEnCryption;
        private System.Windows.Forms.Button btnDeCryption;
        private System.Windows.Forms.TextBox txtPlaintext;
        private System.Windows.Forms.TextBox txtCiphertext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}