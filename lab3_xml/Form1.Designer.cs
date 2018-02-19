namespace lab3
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
            this.button1 = new System.Windows.Forms.Button();
            this.LocalRB = new System.Windows.Forms.RadioButton();
            this.TCPRB = new System.Windows.Forms.RadioButton();
            this.SQLRB = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButHash = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(488, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Рассчитать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LocalRB
            // 
            this.LocalRB.AutoSize = true;
            this.LocalRB.Location = new System.Drawing.Point(6, 19);
            this.LocalRB.Name = "LocalRB";
            this.LocalRB.Size = new System.Drawing.Size(74, 17);
            this.LocalRB.TabIndex = 1;
            this.LocalRB.TabStop = true;
            this.LocalRB.Text = "Из файла";
            this.LocalRB.UseVisualStyleBackColor = true;
            this.LocalRB.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // TCPRB
            // 
            this.TCPRB.AutoSize = true;
            this.TCPRB.Location = new System.Drawing.Point(6, 43);
            this.TCPRB.Name = "TCPRB";
            this.TCPRB.Size = new System.Drawing.Size(65, 17);
            this.TCPRB.TabIndex = 2;
            this.TCPRB.TabStop = true;
            this.TCPRB.Text = "По сети";
            this.TCPRB.UseVisualStyleBackColor = true;
            this.TCPRB.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // SQLRB
            // 
            this.SQLRB.AutoSize = true;
            this.SQLRB.Location = new System.Drawing.Point(6, 67);
            this.SQLRB.Name = "SQLRB";
            this.SQLRB.Size = new System.Drawing.Size(71, 17);
            this.SQLRB.TabIndex = 3;
            this.SQLRB.TabStop = true;
            this.SQLRB.Text = "из СУБД";
            this.SQLRB.UseVisualStyleBackColor = true;
            this.SQLRB.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar2);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.ButHash);
            this.groupBox1.Controls.Add(this.LocalRB);
            this.groupBox1.Controls.Add(this.SQLRB);
            this.groupBox1.Controls.Add(this.TCPRB);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 124);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Способ получения данных";
            // 
            // ButHash
            // 
            this.ButHash.AutoSize = true;
            this.ButHash.Enabled = false;
            this.ButHash.Location = new System.Drawing.Point(152, 43);
            this.ButHash.Name = "ButHash";
            this.ButHash.Size = new System.Drawing.Size(103, 17);
            this.ButHash.TabIndex = 4;
            this.ButHash.Text = "Проверять хэш";
            this.ButHash.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 169);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(660, 380);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(152, 67);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(261, 67);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(100, 23);
            this.progressBar2.TabIndex = 6;
            this.progressBar2.Visible = false;
            this.progressBar2.Click += new System.EventHandler(this.progressBar2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton LocalRB;
        private System.Windows.Forms.RadioButton TCPRB;
        private System.Windows.Forms.RadioButton SQLRB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ButHash;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;

    }
}