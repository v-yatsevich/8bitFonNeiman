namespace _8bitVonNeiman.Core.View {
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
            // ComponentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.editorButton);
            this.Name = "ComponentsForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ComponentsForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button editorButton;
    }
}

