namespace _8bitVonNeiman.Memory.View {
    partial class MemoryForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.clearMemoryButton = new System.Windows.Forms.Button();
            this.memoryDataGridView = new System.Windows.Forms.DataGridView();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.checkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.memoryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // clearMemoryButton
            // 
            this.clearMemoryButton.Location = new System.Drawing.Point(13, 13);
            this.clearMemoryButton.Name = "clearMemoryButton";
            this.clearMemoryButton.Size = new System.Drawing.Size(113, 23);
            this.clearMemoryButton.TabIndex = 0;
            this.clearMemoryButton.Text = "Очистить память";
            this.clearMemoryButton.UseVisualStyleBackColor = true;
            this.clearMemoryButton.Click += new System.EventHandler(this.clearMemoryButton_Click);
            // 
            // memoryDataGridView
            // 
            this.memoryDataGridView.AllowUserToAddRows = false;
            this.memoryDataGridView.AllowUserToDeleteRows = false;
            this.memoryDataGridView.AllowUserToResizeColumns = false;
            this.memoryDataGridView.AllowUserToResizeRows = false;
            this.memoryDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.memoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memoryDataGridView.Location = new System.Drawing.Point(13, 42);
            this.memoryDataGridView.Name = "memoryDataGridView";
            this.memoryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.memoryDataGridView.Size = new System.Drawing.Size(548, 345);
            this.memoryDataGridView.TabIndex = 1;
            this.memoryDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.memoryDataGridView_CellEndEdit);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Location = new System.Drawing.Point(458, 12);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(103, 23);
            this.saveAsButton.TabIndex = 2;
            this.saveAsButton.Text = "Сохранить как";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(377, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(296, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "Загрузить";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // checkButton
            // 
            this.checkButton.Location = new System.Drawing.Point(215, 13);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(75, 23);
            this.checkButton.TabIndex = 5;
            this.checkButton.Text = "Проверить";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // MemoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 399);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.saveAsButton);
            this.Controls.Add(this.memoryDataGridView);
            this.Controls.Add(this.clearMemoryButton);
            this.Location = new System.Drawing.Point(390, 592);
            this.Name = "MemoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MemoryView";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MemoryForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.memoryDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clearMemoryButton;
        private System.Windows.Forms.DataGridView memoryDataGridView;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button checkButton;
    }
}