namespace _8bitVonNeiman.Debugger.View {
    partial class DebugForm {
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.deleteAllButton = new System.Windows.Forms.Button();
            this.BreakpointColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BreakpointColumn,
            this.addressColumn,
            this.commandColumn,
            this.codeColumn});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(400, 558);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAllButton.Location = new System.Drawing.Point(231, 586);
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(181, 23);
            this.deleteAllButton.TabIndex = 1;
            this.deleteAllButton.Text = "Удалить все точки останова";
            this.deleteAllButton.UseVisualStyleBackColor = true;
            this.deleteAllButton.Click += new System.EventHandler(this.deleteAllButton_Click);
            // 
            // BreakpointColumn
            // 
            this.BreakpointColumn.Frozen = true;
            this.BreakpointColumn.HeaderText = "";
            this.BreakpointColumn.Name = "BreakpointColumn";
            this.BreakpointColumn.ReadOnly = true;
            this.BreakpointColumn.Width = 20;
            // 
            // addressColumn
            // 
            this.addressColumn.Frozen = true;
            this.addressColumn.HeaderText = "Адрес";
            this.addressColumn.Name = "addressColumn";
            this.addressColumn.ReadOnly = true;
            this.addressColumn.Width = 50;
            // 
            // commandColumn
            // 
            this.commandColumn.Frozen = true;
            this.commandColumn.HeaderText = "Команда";
            this.commandColumn.Name = "commandColumn";
            this.commandColumn.ReadOnly = true;
            this.commandColumn.Width = 70;
            // 
            // codeColumn
            // 
            this.codeColumn.Frozen = true;
            this.codeColumn.HeaderText = "Код";
            this.codeColumn.Name = "codeColumn";
            this.codeColumn.ReadOnly = true;
            this.codeColumn.Width = 200;
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 621);
            this.Controls.Add(this.deleteAllButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DebugForm";
            this.Text = "Отладка";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DebugForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button deleteAllButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn BreakpointColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeColumn;
    }
}