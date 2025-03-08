namespace Interfaces_lab1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonTest1 = new System.Windows.Forms.Button();
            this.buttonMain = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelAvg_time = new System.Windows.Forms.Label();
            this.buttonTest2 = new System.Windows.Forms.Button();
            this.buttonTest3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTest1
            // 
            this.buttonTest1.Location = new System.Drawing.Point(422, 579);
            this.buttonTest1.Name = "buttonTest1";
            this.buttonTest1.Size = new System.Drawing.Size(111, 53);
            this.buttonTest1.TabIndex = 0;
            this.buttonTest1.Text = "Test 1";
            this.buttonTest1.UseVisualStyleBackColor = true;
            this.buttonTest1.Click += new System.EventHandler(this.buttonTest1_Click);
            // 
            // buttonMain
            // 
            this.buttonMain.Location = new System.Drawing.Point(169, 47);
            this.buttonMain.Name = "buttonMain";
            this.buttonMain.Size = new System.Drawing.Size(75, 23);
            this.buttonMain.TabIndex = 1;
            this.buttonMain.Text = "Click Me";
            this.buttonMain.UseVisualStyleBackColor = true;
            this.buttonMain.Visible = false;
            this.buttonMain.Click += new System.EventHandler(this.buttonMain_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(7, 609);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(143, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Visible = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStartClick);
            // 
            // labelAvg_time
            // 
            this.labelAvg_time.AutoSize = true;
            this.labelAvg_time.Location = new System.Drawing.Point(156, 614);
            this.labelAvg_time.Name = "labelAvg_time";
            this.labelAvg_time.Size = new System.Drawing.Size(35, 13);
            this.labelAvg_time.TabIndex = 3;
            this.labelAvg_time.Text = "label1";
            this.labelAvg_time.Visible = false;
            // 
            // buttonTest2
            // 
            this.buttonTest2.Location = new System.Drawing.Point(539, 579);
            this.buttonTest2.Name = "buttonTest2";
            this.buttonTest2.Size = new System.Drawing.Size(111, 53);
            this.buttonTest2.TabIndex = 4;
            this.buttonTest2.Text = "Test2";
            this.buttonTest2.UseVisualStyleBackColor = true;
            this.buttonTest2.Click += new System.EventHandler(this.buttonTest2_Click);
            // 
            // buttonTest3
            // 
            this.buttonTest3.Location = new System.Drawing.Point(656, 579);
            this.buttonTest3.Name = "buttonTest3";
            this.buttonTest3.Size = new System.Drawing.Size(111, 53);
            this.buttonTest3.TabIndex = 5;
            this.buttonTest3.Text = "Test3";
            this.buttonTest3.UseVisualStyleBackColor = true;
            this.buttonTest3.Click += new System.EventHandler(this.buttonTest3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1005, 644);
            this.Controls.Add(this.buttonTest3);
            this.Controls.Add(this.buttonTest2);
            this.Controls.Add(this.labelAvg_time);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonMain);
            this.Controls.Add(this.buttonTest1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTest1;
        private System.Windows.Forms.Button buttonMain;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelAvg_time;
        private System.Windows.Forms.Button buttonTest2;
        private System.Windows.Forms.Button buttonTest3;
    }
}

