namespace VlAutoUpdateClient
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
            buttonRetry = new Button();
            buttonOpenGame = new Button();
            buttonOpenAuto = new Button();
            buttonExit = new Button();
            progressBarAll = new ProgressBar();
            progressBarCurent = new ProgressBar();
            label1 = new Label();
            label2 = new Label();
            labelStatus = new Label();
            SuspendLayout();
            // 
            // buttonRetry
            // 
            buttonRetry.Location = new Point(237, 284);
            buttonRetry.Name = "buttonRetry";
            buttonRetry.Size = new Size(75, 23);
            buttonRetry.TabIndex = 0;
            buttonRetry.Text = "Thử lại";
            buttonRetry.UseVisualStyleBackColor = true;
            buttonRetry.Click += buttonRetry_Click;
            // 
            // buttonOpenGame
            // 
            buttonOpenGame.Location = new Point(318, 284);
            buttonOpenGame.Name = "buttonOpenGame";
            buttonOpenGame.Size = new Size(75, 23);
            buttonOpenGame.TabIndex = 1;
            buttonOpenGame.Text = "Mở Game";
            buttonOpenGame.UseVisualStyleBackColor = true;
            buttonOpenGame.Click += buttonOpenGame_Click;
            // 
            // buttonOpenAuto
            // 
            buttonOpenAuto.Location = new Point(399, 284);
            buttonOpenAuto.Name = "buttonOpenAuto";
            buttonOpenAuto.Size = new Size(75, 23);
            buttonOpenAuto.TabIndex = 2;
            buttonOpenAuto.Text = "Mở Auto";
            buttonOpenAuto.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(482, 284);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(75, 23);
            buttonExit.TabIndex = 3;
            buttonExit.Text = "Thoát";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // progressBarAll
            // 
            progressBarAll.Location = new Point(295, 263);
            progressBarAll.Name = "progressBarAll";
            progressBarAll.Size = new Size(262, 12);
            progressBarAll.TabIndex = 4;
            // 
            // progressBarCurent
            // 
            progressBarCurent.Location = new Point(295, 243);
            progressBarCurent.Name = "progressBarCurent";
            progressBarCurent.Size = new Size(262, 12);
            progressBarCurent.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(237, 242);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 6;
            label1.Text = "Hiện tại:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(237, 263);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 7;
            label2.Text = "Toàn bộ:";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(295, 224);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(229, 15);
            labelStatus.TabIndex = 8;
            labelStatus.Text = "Hãy click \"Mở game\" để bôn tẩu giang hồ";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            Controls.Add(labelStatus);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(progressBarCurent);
            Controls.Add(progressBarAll);
            Controls.Add(buttonExit);
            Controls.Add(buttonOpenAuto);
            Controls.Add(buttonOpenGame);
            Controls.Add(buttonRetry);
            MaximizeBox = false;
            Name = "FormMain";
            Text = "FormMain";
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonRetry;
        private Button buttonOpenGame;
        private Button buttonOpenAuto;
        private Button buttonExit;
        private ProgressBar progressBarAll;
        private ProgressBar progressBarCurent;
        private Label label1;
        private Label label2;
        private Label labelStatus;
    }
}