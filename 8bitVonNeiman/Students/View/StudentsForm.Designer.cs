namespace _8bitVonNeiman.Students.View {
    partial class StudentsForm {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.addButton = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeleteStudent = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ChangeStudent = new System.Windows.Forms.DataGridViewButtonColumn();
            this.LookStudent = new System.Windows.Forms.DataGridViewButtonColumn();
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
            this.name,
            this.group,
            this.DeleteStudent,
            this.ChangeStudent,
            this.LookStudent});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(645, 400);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(582, 418);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // name
            // 
            this.name.HeaderText = "Студент";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 240;
            // 
            // group
            // 
            this.group.HeaderText = "Группа";
            this.group.Name = "group";
            this.group.ReadOnly = true;
            this.group.Width = 60;
            // 
            // DeleteStudent
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = "Удалить";
            this.DeleteStudent.DefaultCellStyle = dataGridViewCellStyle7;
            this.DeleteStudent.HeaderText = "Удалить";
            this.DeleteStudent.Name = "DeleteStudent";
            this.DeleteStudent.ReadOnly = true;
            this.DeleteStudent.Text = "Удалить";
            // 
            // ChangeStudent
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = "Изменить";
            this.ChangeStudent.DefaultCellStyle = dataGridViewCellStyle8;
            this.ChangeStudent.HeaderText = "Изменить";
            this.ChangeStudent.Name = "ChangeStudent";
            this.ChangeStudent.ReadOnly = true;
            // 
            // LookStudent
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.NullValue = "Просмотреть";
            this.LookStudent.DefaultCellStyle = dataGridViewCellStyle9;
            this.LookStudent.HeaderText = "Просмотреть";
            this.LookStudent.Name = "LookStudent";
            this.LookStudent.ReadOnly = true;
            this.LookStudent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LookStudent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // StudentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 453);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "StudentsForm";
            this.Text = "StudentsForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StudentsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn group;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteStudent;
        private System.Windows.Forms.DataGridViewButtonColumn ChangeStudent;
        private System.Windows.Forms.DataGridViewButtonColumn LookStudent;
    }
}