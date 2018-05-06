namespace _8bitVonNeiman.Controller.View {
    partial class ComponentsForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.editorButton = new System.Windows.Forms.Button();
            this.memoryButton = new System.Windows.Forms.Button();
            this.cpuButton = new System.Windows.Forms.Button();
            this.studentsButton = new System.Windows.Forms.Button();
            this.tasksButton = new System.Windows.Forms.Button();
            this.marksButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // editorButton
            // 
            this.editorButton.Location = new System.Drawing.Point(12, 12);
            this.editorButton.Name = "editorButton";
            this.editorButton.Size = new System.Drawing.Size(95, 23);
            this.editorButton.TabIndex = 0;
            this.editorButton.Text = "Компилятор";
            this.editorButton.UseVisualStyleBackColor = true;
            this.editorButton.Click += new System.EventHandler(this.editorButton_Click);
            // 
            // memoryButton
            // 
            this.memoryButton.Location = new System.Drawing.Point(113, 12);
            this.memoryButton.Name = "memoryButton";
            this.memoryButton.Size = new System.Drawing.Size(75, 23);
            this.memoryButton.TabIndex = 1;
            this.memoryButton.Text = "Память";
            this.memoryButton.UseVisualStyleBackColor = true;
            this.memoryButton.Click += new System.EventHandler(this.memoryButton_Click);
            // 
            // cpuButton
            // 
            this.cpuButton.Location = new System.Drawing.Point(12, 41);
            this.cpuButton.Name = "cpuButton";
            this.cpuButton.Size = new System.Drawing.Size(95, 23);
            this.cpuButton.TabIndex = 2;
            this.cpuButton.Text = "Процессор";
            this.cpuButton.UseVisualStyleBackColor = true;
            this.cpuButton.Click += new System.EventHandler(this.cpuButton_Click);
            // 
            // studentsButton
            // 
            this.studentsButton.Location = new System.Drawing.Point(12, 152);
            this.studentsButton.Name = "studentsButton";
            this.studentsButton.Size = new System.Drawing.Size(95, 23);
            this.studentsButton.TabIndex = 4;
            this.studentsButton.Text = "Студенты";
            this.studentsButton.UseVisualStyleBackColor = true;
            this.studentsButton.Click += new System.EventHandler(this.studentsButton_Click);
            // 
            // tasksButton
            // 
            this.tasksButton.Location = new System.Drawing.Point(12, 210);
            this.tasksButton.Name = "tasksButton";
            this.tasksButton.Size = new System.Drawing.Size(95, 23);
            this.tasksButton.TabIndex = 5;
            this.tasksButton.Text = "Задания";
            this.tasksButton.UseVisualStyleBackColor = true;
            this.tasksButton.Click += new System.EventHandler(this.tasksButton_Click);
            // 
            // marksButton
            // 
            this.marksButton.Location = new System.Drawing.Point(12, 181);
            this.marksButton.Name = "marksButton";
            this.marksButton.Size = new System.Drawing.Size(95, 23);
            this.marksButton.TabIndex = 6;
            this.marksButton.Text = "Оценки";
            this.marksButton.UseVisualStyleBackColor = true;
            this.marksButton.Click += new System.EventHandler(this.marksButton_Click);
            // 
            // ComponentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.marksButton);
            this.Controls.Add(this.tasksButton);
            this.Controls.Add(this.studentsButton);
            this.Controls.Add(this.cpuButton);
            this.Controls.Add(this.memoryButton);
            this.Controls.Add(this.editorButton);
            this.Location = new System.Drawing.Point(80, 40);
            this.Name = "ComponentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AddTaskForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ComponentsForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button editorButton;
        private System.Windows.Forms.Button memoryButton;
        private System.Windows.Forms.Button cpuButton;
        private System.Windows.Forms.Button studentsButton;
        private System.Windows.Forms.Button tasksButton;
        private System.Windows.Forms.Button marksButton;
    }
}

