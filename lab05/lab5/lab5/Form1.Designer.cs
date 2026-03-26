namespace lab5
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
            lblQuestion = new Label();
            lblProbValue = new Label();
            lblAnswer = new Label();
            txtQuestion = new TextBox();
            btnGetAnswer = new Button();
            btnToBall = new Button();
            trackProbability = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)trackProbability).BeginInit();
            SuspendLayout();
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Location = new Point(30, 78);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(96, 15);
            lblQuestion.TabIndex = 0;
            lblQuestion.Text = "Задайте вопрос:";
            // 
            // lblProbValue
            // 
            lblProbValue.AutoSize = true;
            lblProbValue.Location = new Point(258, 173);
            lblProbValue.Name = "lblProbValue";
            lblProbValue.Size = new Size(29, 15);
            lblProbValue.TabIndex = 1;
            lblProbValue.Text = "50%";
            // 
            // lblAnswer
            // 
            lblAnswer.AutoSize = true;
            lblAnswer.Location = new Point(258, 210);
            lblAnswer.Name = "lblAnswer";
            lblAnswer.Size = new Size(10, 15);
            lblAnswer.TabIndex = 2;
            lblAnswer.Text = " ";
            lblAnswer.Click += lblAnswer_Click;
            // 
            // txtQuestion
            // 
            txtQuestion.Location = new Point(30, 96);
            txtQuestion.Name = "txtQuestion";
            txtQuestion.Size = new Size(502, 23);
            txtQuestion.TabIndex = 3;
            // 
            // btnGetAnswer
            // 
            btnGetAnswer.Location = new Point(30, 245);
            btnGetAnswer.Name = "btnGetAnswer";
            btnGetAnswer.Size = new Size(202, 50);
            btnGetAnswer.TabIndex = 4;
            btnGetAnswer.Text = "Получить ответ";
            btnGetAnswer.UseVisualStyleBackColor = true;
            btnGetAnswer.Click += btnGetAnswer_Click;
            // 
            // btnToBall
            // 
            btnToBall.Location = new Point(375, 272);
            btnToBall.Name = "btnToBall";
            btnToBall.Size = new Size(157, 23);
            btnToBall.TabIndex = 5;
            btnToBall.Text = "Перейти к Шару";
            btnToBall.UseVisualStyleBackColor = true;
            btnToBall.Click += btnToBall_Click;
            // 
            // trackProbability
            // 
            trackProbability.Location = new Point(41, 125);
            trackProbability.Maximum = 100;
            trackProbability.Name = "trackProbability";
            trackProbability.Size = new Size(467, 45);
            trackProbability.TabIndex = 6;
            trackProbability.Value = 50;
            trackProbability.Scroll += trackProbability_Scroll;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(556, 347);
            Controls.Add(trackProbability);
            Controls.Add(btnToBall);
            Controls.Add(btnGetAnswer);
            Controls.Add(txtQuestion);
            Controls.Add(lblAnswer);
            Controls.Add(lblProbValue);
            Controls.Add(lblQuestion);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackProbability).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblQuestion;
        private Label lblProbValue;
        private Label lblAnswer;
        private TextBox txtQuestion;
        private Button btnGetAnswer;
        private Button btnToBall;
        private TrackBar trackProbability;
    }
}
