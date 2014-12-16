namespace CalCal
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFile1 = new System.Windows.Forms.TextBox();
            this.textBoxFile2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCal = new System.Windows.Forms.Button();
            this.buttonSet1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Файл программы MOONSUN";
            // 
            // textBoxFile1
            // 
            this.textBoxFile1.Location = new System.Drawing.Point(176, 10);
            this.textBoxFile1.Name = "textBoxFile1";
            this.textBoxFile1.Size = new System.Drawing.Size(333, 22);
            this.textBoxFile1.TabIndex = 1;
            // 
            // textBoxFile2
            // 
            this.textBoxFile2.Location = new System.Drawing.Point(176, 36);
            this.textBoxFile2.Name = "textBoxFile2";
            this.textBoxFile2.Size = new System.Drawing.Size(333, 22);
            this.textBoxFile2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Файл фильтров";
            // 
            // buttonCal
            // 
            this.buttonCal.Location = new System.Drawing.Point(161, 62);
            this.buttonCal.Name = "buttonCal";
            this.buttonCal.Size = new System.Drawing.Size(240, 37);
            this.buttonCal.TabIndex = 4;
            this.buttonCal.Text = "Вычислить";
            this.buttonCal.UseVisualStyleBackColor = true;
            this.buttonCal.Click += new System.EventHandler(this.buttonCal_Click);
            // 
            // buttonSet1
            // 
            this.buttonSet1.Location = new System.Drawing.Point(515, 7);
            this.buttonSet1.Name = "buttonSet1";
            this.buttonSet1.Size = new System.Drawing.Size(26, 23);
            this.buttonSet1.TabIndex = 6;
            this.buttonSet1.Text = "...";
            this.buttonSet1.UseVisualStyleBackColor = true;
            this.buttonSet1.Click += new System.EventHandler(this.buttonSet1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 106);
            this.Controls.Add(this.buttonCal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSet1);
            this.Controls.Add(this.textBoxFile2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxFile1);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFile1;
        private System.Windows.Forms.TextBox textBoxFile2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCal;
        private System.Windows.Forms.Button buttonSet1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFile;

    }
}

