namespace lab3
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
            components = new System.ComponentModel.Container();
            pictureBoxMap = new PictureBox();
            btnStart = new Button();
            btnPause = new Button();
            btnStop = new Button();
            btnClear = new Button();
            btnGenerate = new Button();
            comboBoxSpeed = new ComboBox();
            comboBoxWindDir = new ComboBox();
            trackHumidity = new TrackBar();
            trackWindForce = new TrackBar();
            trackGrowth = new TrackBar();
            trackLightning = new TrackBar();
            timerSim = new System.Windows.Forms.Timer(components);
            lblStep = new Label();
            lblBurned = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMap).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackHumidity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackWindForce).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackGrowth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackLightning).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxMap
            // 
            pictureBoxMap.Location = new Point(8, 12);
            pictureBoxMap.Name = "pictureBoxMap";
            pictureBoxMap.Size = new Size(500, 500);
            pictureBoxMap.TabIndex = 0;
            pictureBoxMap.TabStop = false;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(621, 391);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(107, 48);
            btnStart.TabIndex = 1;
            btnStart.Text = "Старт";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnPause
            // 
            btnPause.Location = new Point(621, 460);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(105, 48);
            btnPause.TabIndex = 2;
            btnPause.Text = "Пауза";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(762, 391);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(105, 48);
            btnStop.TabIndex = 3;
            btnStop.Text = "Стоп";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(762, 460);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(105, 48);
            btnClear.TabIndex = 4;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(633, 327);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(220, 46);
            btnGenerate.TabIndex = 5;
            btnGenerate.Text = "Сгенерировать лес";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // comboBoxSpeed
            // 
            comboBoxSpeed.FormattingEnabled = true;
            comboBoxSpeed.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            comboBoxSpeed.Location = new Point(551, 243);
            comboBoxSpeed.Name = "comboBoxSpeed";
            comboBoxSpeed.Size = new Size(121, 23);
            comboBoxSpeed.TabIndex = 6;
            comboBoxSpeed.Text = "3";
            comboBoxSpeed.SelectedIndexChanged += comboBoxSpeed_SelectedIndexChanged;
            // 
            // comboBoxWindDir
            // 
            comboBoxWindDir.FormattingEnabled = true;
            comboBoxWindDir.Items.AddRange(new object[] { "Север", "Юг", "Запад", "Восток" });
            comboBoxWindDir.Location = new Point(798, 243);
            comboBoxWindDir.Name = "comboBoxWindDir";
            comboBoxWindDir.Size = new Size(121, 23);
            comboBoxWindDir.TabIndex = 7;
            comboBoxWindDir.Text = "Север";
            comboBoxWindDir.SelectedIndexChanged += comboBoxWindDir_SelectedIndexChanged;
            // 
            // trackHumidity
            // 
            trackHumidity.LargeChange = 20;
            trackHumidity.Location = new Point(768, 170);
            trackHumidity.Maximum = 100;
            trackHumidity.Name = "trackHumidity";
            trackHumidity.Size = new Size(199, 45);
            trackHumidity.TabIndex = 8;
            trackHumidity.Scroll += trackHumidity_Scroll;
            // 
            // trackWindForce
            // 
            trackWindForce.LargeChange = 30;
            trackWindForce.Location = new Point(768, 87);
            trackWindForce.Maximum = 100;
            trackWindForce.Name = "trackWindForce";
            trackWindForce.Size = new Size(200, 45);
            trackWindForce.TabIndex = 9;
            trackWindForce.Scroll += trackWindForce_Scroll;
            // 
            // trackGrowth
            // 
            trackGrowth.Location = new Point(514, 170);
            trackGrowth.Maximum = 100;
            trackGrowth.Name = "trackGrowth";
            trackGrowth.Size = new Size(200, 45);
            trackGrowth.TabIndex = 10;
            trackGrowth.Scroll += trackGrowth_Scroll;
            // 
            // trackLightning
            // 
            trackLightning.Location = new Point(514, 87);
            trackLightning.Maximum = 100;
            trackLightning.Name = "trackLightning";
            trackLightning.Size = new Size(200, 45);
            trackLightning.TabIndex = 11;
            trackLightning.Scroll += trackLightning_Scroll;
            // 
            // timerSim
            // 
            timerSim.Tick += timerSim_Tick;
            // 
            // lblStep
            // 
            lblStep.AutoSize = true;
            lblStep.Location = new Point(514, 27);
            lblStep.Name = "lblStep";
            lblStep.Size = new Size(41, 15);
            lblStep.TabIndex = 12;
            lblStep.Text = "Шаг: 0";
            lblStep.Click += label1_Click;
            // 
            // lblBurned
            // 
            lblBurned.AutoSize = true;
            lblBurned.Location = new Point(514, 12);
            lblBurned.Name = "lblBurned";
            lblBurned.Size = new Size(76, 15);
            lblBurned.TabIndex = 13;
            lblBurned.Text = "Сгорело: 0%";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(544, 69);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 14;
            label1.Text = "Вероятность молнии (f)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(544, 152);
            label2.Name = "label2";
            label2.Size = new Size(128, 15);
            label2.TabIndex = 15;
            label2.Text = "Вероятность роста (p)";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(798, 69);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 16;
            label3.Text = "Сила ветра";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(798, 152);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 17;
            label4.Text = "Влажность";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(551, 225);
            label5.Name = "label5";
            label5.Size = new Size(118, 15);
            label5.TabIndex = 18;
            label5.Text = "Скорость анимации";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(798, 225);
            label6.Name = "label6";
            label6.Size = new Size(114, 15);
            label6.TabIndex = 19;
            label6.Text = "Направление ветра";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 524);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblBurned);
            Controls.Add(lblStep);
            Controls.Add(trackLightning);
            Controls.Add(trackGrowth);
            Controls.Add(trackWindForce);
            Controls.Add(trackHumidity);
            Controls.Add(comboBoxWindDir);
            Controls.Add(comboBoxSpeed);
            Controls.Add(btnGenerate);
            Controls.Add(btnClear);
            Controls.Add(btnStop);
            Controls.Add(btnPause);
            Controls.Add(btnStart);
            Controls.Add(pictureBoxMap);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxMap).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackHumidity).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackWindForce).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackGrowth).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackLightning).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxMap;
        private Button btnStart;
        private Button btnPause;
        private Button btnStop;
        private Button btnClear;
        private Button btnGenerate;
        private ComboBox comboBoxSpeed;
        private ComboBox comboBoxWindDir;
        private TrackBar trackHumidity;
        private TrackBar trackWindForce;
        private TrackBar trackGrowth;
        private TrackBar trackLightning;
        private System.Windows.Forms.Timer timerSim;
        private Label lblStep;
        private Label lblBurned;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
