namespace Course_Project_OOP_3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            addNewItemButton = new Button();
            textBox1 = new TextBox();
            ProcessALLItemsInList = new Button();
            ClearAllTablesButton = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(1, 126);
            label1.Name = "label1";
            label1.Size = new Size(301, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new Point(440, 59);
            label2.Name = "label2";
            label2.Size = new Size(268, 20);
            label2.TabIndex = 1;
            label2.Text = "label2";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // addNewItemButton
            // 
            addNewItemButton.Location = new Point(525, 169);
            addNewItemButton.Name = "addNewItemButton";
            addNewItemButton.Size = new Size(94, 29);
            addNewItemButton.TabIndex = 4;
            addNewItemButton.Text = "Add item";
            addNewItemButton.UseVisualStyleBackColor = true;
            addNewItemButton.Click += AddNewItemButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(440, 107);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(268, 27);
            textBox1.TabIndex = 5;
            textBox1.Text = "Enter item number(12 numbers)";
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.MouseClick += textBox1_MouseClick;
            // 
            // ProcessALLItemsInList
            // 
            ProcessALLItemsInList.Location = new Point(76, 169);
            ProcessALLItemsInList.Name = "ProcessALLItemsInList";
            ProcessALLItemsInList.Size = new Size(153, 29);
            ProcessALLItemsInList.TabIndex = 6;
            ProcessALLItemsInList.Text = "Process ALL items";
            ProcessALLItemsInList.UseVisualStyleBackColor = true;
            ProcessALLItemsInList.Click += ProcessALLItemsInListButton_Click;
            // 
            // ClearAllTablesButton
            // 
            ClearAllTablesButton.Location = new Point(249, 372);
            ClearAllTablesButton.Name = "ClearAllTablesButton";
            ClearAllTablesButton.Size = new Size(153, 29);
            ClearAllTablesButton.TabIndex = 7;
            ClearAllTablesButton.Text = "Clear All tables";
            ClearAllTablesButton.UseVisualStyleBackColor = true;
            ClearAllTablesButton.Click += ClearAllTablesButton_Click;
            // 
            // label3
            // 
            label3.Location = new Point(169, 332);
            label3.Name = "label3";
            label3.Size = new Size(325, 20);
            label3.TabIndex = 8;
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 470);
            Controls.Add(label3);
            Controls.Add(ClearAllTablesButton);
            Controls.Add(ProcessALLItemsInList);
            Controls.Add(textBox1);
            Controls.Add(addNewItemButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button addNewItemButton;
        private TextBox textBox1;
        private Button ProcessALLItemsInList;
        private Button ClearAllTablesButton;
        private Label label3;
    }
}
