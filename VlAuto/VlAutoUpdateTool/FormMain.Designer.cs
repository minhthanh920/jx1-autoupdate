namespace VlAutoUpdateTool
{
    partial class FormMain
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
            label1 = new Label();
            textBoxFolderPath = new TextBox();
            buttonFileSelect = new Button();
            button1 = new Button();
            buttonSaveConfig = new Button();
            treeViewPath = new TreeView();
            buttonCreateChecksum = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 0;
            label1.Text = "Thư mục:";
            // 
            // textBoxFolderPath
            // 
            textBoxFolderPath.Location = new Point(75, 6);
            textBoxFolderPath.Name = "textBoxFolderPath";
            textBoxFolderPath.ReadOnly = true;
            textBoxFolderPath.Size = new Size(599, 23);
            textBoxFolderPath.TabIndex = 1;
            // 
            // buttonFileSelect
            // 
            buttonFileSelect.Location = new Point(680, 6);
            buttonFileSelect.Name = "buttonFileSelect";
            buttonFileSelect.Size = new Size(75, 23);
            buttonFileSelect.TabIndex = 2;
            buttonFileSelect.Text = "Chọn File";
            buttonFileSelect.UseVisualStyleBackColor = true;
            buttonFileSelect.Click += buttonFileSelect_Click;
            // 
            // button1
            // 
            button1.Location = new Point(75, 39);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Mở file";
            button1.UseVisualStyleBackColor = true;
            // 
            // buttonSaveConfig
            // 
            buttonSaveConfig.Location = new Point(168, 39);
            buttonSaveConfig.Name = "buttonSaveConfig";
            buttonSaveConfig.Size = new Size(75, 23);
            buttonSaveConfig.TabIndex = 2;
            buttonSaveConfig.Text = "Lưu file";
            buttonSaveConfig.UseVisualStyleBackColor = true;
            buttonSaveConfig.Click += buttonSaveConfig_Click;
            // 
            // treeViewPath
            // 
            treeViewPath.CheckBoxes = true;
            treeViewPath.Location = new Point(12, 79);
            treeViewPath.Name = "treeViewPath";
            treeViewPath.Size = new Size(760, 470);
            treeViewPath.TabIndex = 3;
            treeViewPath.AfterCheck += treeViewPath_AfterCheck;
            // 
            // buttonCreateChecksum
            // 
            buttonCreateChecksum.Location = new Point(265, 39);
            buttonCreateChecksum.Name = "buttonCreateChecksum";
            buttonCreateChecksum.Size = new Size(127, 23);
            buttonCreateChecksum.TabIndex = 4;
            buttonCreateChecksum.Text = "Tạo file checksum";
            buttonCreateChecksum.UseVisualStyleBackColor = true;
            buttonCreateChecksum.Click += buttonCreateChecksum_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(buttonCreateChecksum);
            Controls.Add(treeViewPath);
            Controls.Add(buttonSaveConfig);
            Controls.Add(button1);
            Controls.Add(buttonFileSelect);
            Controls.Add(textBoxFolderPath);
            Controls.Add(label1);
            Name = "FormMain";
            Text = "FormMain";
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxFolderPath;
        private Button buttonFileSelect;
        private Button buttonSaveConfig;
        private Button button1;
        private TreeView treeViewPath;
        private Button buttonCreateChecksum;
    }
}