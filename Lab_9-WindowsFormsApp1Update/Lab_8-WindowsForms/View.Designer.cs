namespace Lab_8_WindowsForms
{
    partial class View
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.syncFirstDirectoryButton = new System.Windows.Forms.Button();
            this.syncSecondDirectoryButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Путь первой директории";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Путь второй директории";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1169, 31);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "\\\\Mac\\Home\\Desktop\\Директории для теста\\1_Директория";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(34, 161);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(1169, 31);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "\\\\Mac\\Home\\Desktop\\Директории для теста\\2_Директория";
            // 
            // syncFirstDirectoryButton
            // 
            this.syncFirstDirectoryButton.Location = new System.Drawing.Point(34, 223);
            this.syncFirstDirectoryButton.Name = "syncFirstDirectoryButton";
            this.syncFirstDirectoryButton.Size = new System.Drawing.Size(352, 208);
            this.syncFirstDirectoryButton.TabIndex = 4;
            this.syncFirstDirectoryButton.Text = "Синхронизировать первую директорию на основе второй";
            this.syncFirstDirectoryButton.UseVisualStyleBackColor = true;
            // 
            // syncSecondDirectoryButton
            // 
            this.syncSecondDirectoryButton.Location = new System.Drawing.Point(34, 447);
            this.syncSecondDirectoryButton.Name = "syncSecondDirectoryButton";
            this.syncSecondDirectoryButton.Size = new System.Drawing.Size(352, 208);
            this.syncSecondDirectoryButton.TabIndex = 5;
            this.syncSecondDirectoryButton.Text = "Синхронизировать вторую директорию на основе первой";
            this.syncSecondDirectoryButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(423, 223);
            this.label3.MinimumSize = new System.Drawing.Size(780, 425);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(780, 425);
            this.label3.TabIndex = 6;
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(741, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(462, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Beta version: Пользоваться с осторожностью";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 667);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.syncSecondDirectoryButton);
            this.Controls.Add(this.syncFirstDirectoryButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "View";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button syncFirstDirectoryButton;
        private System.Windows.Forms.Button syncSecondDirectoryButton;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
    }
}

