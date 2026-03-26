namespace lab5
{
    partial class Form2
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
            pictureBoxBall = new PictureBox();
            lblQuestion = new Label();
            lblAnswer = new Label();
            btnAsk = new Button();
            btnBack = new Button();
            txtQuestion = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBall).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxBall
            // 
            pictureBoxBall.ImageLocation = "images.jpg";
            pictureBoxBall.Location = new Point(42, 12);
            pictureBoxBall.Name = "pictureBoxBall";
            pictureBoxBall.Size = new Size(331, 307);
            pictureBoxBall.TabIndex = 0;
            pictureBoxBall.TabStop = false;
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Location = new Point(442, 87);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(96, 15);
            lblQuestion.TabIndex = 1;
            lblQuestion.Text = "Задайте вопрос:";
            // 
            // lblAnswer
            // 
            lblAnswer.AutoSize = true;
            lblAnswer.Location = new Point(413, 166);
            lblAnswer.Name = "lblAnswer";
            lblAnswer.Size = new Size(0, 15);
            lblAnswer.TabIndex = 2;
            // 
            // btnAsk
            // 
            btnAsk.Location = new Point(413, 248);
            btnAsk.Name = "btnAsk";
            btnAsk.Size = new Size(146, 57);
            btnAsk.TabIndex = 3;
            btnAsk.Text = "Спросить шар";
            btnAsk.UseVisualStyleBackColor = true;
            btnAsk.Click += btnAsk_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(596, 270);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(135, 35);
            btnBack.TabIndex = 4;
            btnBack.Text = "Вернуться к Да/Нет";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // txtQuestion
            // 
            txtQuestion.Location = new Point(403, 127);
            txtQuestion.Name = "txtQuestion";
            txtQuestion.Size = new Size(307, 23);
            txtQuestion.TabIndex = 5;
            txtQuestion.TextChanged += txtQuestion_TextChanged;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(749, 326);
            Controls.Add(txtQuestion);
            Controls.Add(btnBack);
            Controls.Add(btnAsk);
            Controls.Add(lblAnswer);
            Controls.Add(lblQuestion);
            Controls.Add(pictureBoxBall);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)pictureBoxBall).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxBall;
        private Label lblQuestion;
        private Label lblAnswer;
        private Button btnAsk;
        private Button btnBack;
        private TextBox txtQuestion;
    }
}