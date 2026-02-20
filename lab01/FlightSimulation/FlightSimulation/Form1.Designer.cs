namespace FlightSimulation
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelHeight = new System.Windows.Forms.Label();
            this.inputHeight = new System.Windows.Forms.NumericUpDown();
            this.labelAngle = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.inputSize = new System.Windows.Forms.NumericUpDown();
            this.labelSize = new System.Windows.Forms.Label();
            this.inputSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelWeight = new System.Windows.Forms.Label();
            this.labelStep = new System.Windows.Forms.Label();
            this.inputAngle = new System.Windows.Forms.NumericUpDown();
            this.inputWeight = new System.Windows.Forms.NumericUpDown();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comboBoxStep = new System.Windows.Forms.ComboBox();
            this.gridResults = new System.Windows.Forms.DataGridView();
            this.dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.range = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finalV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elapsedMs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.inputHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(106, 162);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(41, 13);
            this.labelHeight.TabIndex = 0;
            this.labelHeight.Text = "Height:";
            // 
            // inputHeight
            // 
            this.inputHeight.Location = new System.Drawing.Point(108, 178);
            this.inputHeight.Name = "inputHeight";
            this.inputHeight.Size = new System.Drawing.Size(131, 20);
            this.inputHeight.TabIndex = 1;
            this.inputHeight.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // labelAngle
            // 
            this.labelAngle.AutoSize = true;
            this.labelAngle.Location = new System.Drawing.Point(283, 162);
            this.labelAngle.Name = "labelAngle";
            this.labelAngle.Size = new System.Drawing.Size(37, 13);
            this.labelAngle.TabIndex = 2;
            this.labelAngle.Text = "Angle:";
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(217, 340);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(86, 31);
            this.btnLaunch.TabIndex = 3;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(106, 217);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(41, 13);
            this.labelSpeed.TabIndex = 4;
            this.labelSpeed.Text = "Speed:";
            // 
            // inputSize
            // 
            this.inputSize.DecimalPlaces = 1;
            this.inputSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.inputSize.Location = new System.Drawing.Point(285, 233);
            this.inputSize.Name = "inputSize";
            this.inputSize.Size = new System.Drawing.Size(131, 20);
            this.inputSize.TabIndex = 6;
            this.inputSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(283, 217);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(30, 13);
            this.labelSize.TabIndex = 5;
            this.labelSize.Text = "Size:";
            // 
            // inputSpeed
            // 
            this.inputSpeed.Location = new System.Drawing.Point(108, 233);
            this.inputSpeed.Name = "inputSpeed";
            this.inputSpeed.Size = new System.Drawing.Size(131, 20);
            this.inputSpeed.TabIndex = 8;
            this.inputSpeed.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // labelWeight
            // 
            this.labelWeight.AutoSize = true;
            this.labelWeight.Location = new System.Drawing.Point(106, 281);
            this.labelWeight.Name = "labelWeight";
            this.labelWeight.Size = new System.Drawing.Size(44, 13);
            this.labelWeight.TabIndex = 7;
            this.labelWeight.Text = "Weight:";
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Location = new System.Drawing.Point(283, 281);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(50, 13);
            this.labelStep.TabIndex = 9;
            this.labelStep.Text = "Step (dt):";
            // 
            // inputAngle
            // 
            this.inputAngle.Location = new System.Drawing.Point(285, 178);
            this.inputAngle.Name = "inputAngle";
            this.inputAngle.Size = new System.Drawing.Size(131, 20);
            this.inputAngle.TabIndex = 11;
            this.inputAngle.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            // 
            // inputWeight
            // 
            this.inputWeight.Location = new System.Drawing.Point(108, 297);
            this.inputWeight.Name = "inputWeight";
            this.inputWeight.Size = new System.Drawing.Size(131, 20);
            this.inputWeight.TabIndex = 12;
            this.inputWeight.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(506, 1);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(765, 475);
            this.chart1.TabIndex = 13;
            this.chart1.Text = "chart1";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboBoxStep
            // 
            this.comboBoxStep.FormattingEnabled = true;
            this.comboBoxStep.Items.AddRange(new object[] {
            "1",
            "0,1",
            "0,01",
            "0,001",
            "0,0001"});
            this.comboBoxStep.Location = new System.Drawing.Point(286, 296);
            this.comboBoxStep.Name = "comboBoxStep";
            this.comboBoxStep.Size = new System.Drawing.Size(130, 21);
            this.comboBoxStep.TabIndex = 14;
            // 
            // gridResults
            // 
            this.gridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dt,
            this.range,
            this.maxH,
            this.finalV,
            this.elapsedMs});
            this.gridResults.Location = new System.Drawing.Point(527, 482);
            this.gridResults.Name = "gridResults";
            this.gridResults.Size = new System.Drawing.Size(730, 161);
            this.gridResults.TabIndex = 15;
            this.gridResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dt
            // 
            this.dt.HeaderText = "Шаг, с";
            this.dt.Name = "dt";
            // 
            // range
            // 
            this.range.HeaderText = "Дальность, м";
            this.range.Name = "range";
            // 
            // maxH
            // 
            this.maxH.HeaderText = "Макс. высота, м";
            this.maxH.Name = "maxH";
            // 
            // finalV
            // 
            this.finalV.HeaderText = "Скорость в конце, м/с";
            this.finalV.Name = "finalV";
            // 
            // elapsedMs
            // 
            this.elapsedMs.HeaderText = "Время, мс";
            this.elapsedMs.Name = "elapsedMs";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 646);
            this.Controls.Add(this.gridResults);
            this.Controls.Add(this.comboBoxStep);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.inputWeight);
            this.Controls.Add(this.inputAngle);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.inputSpeed);
            this.Controls.Add(this.labelWeight);
            this.Controls.Add(this.inputSize);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.labelAngle);
            this.Controls.Add(this.inputHeight);
            this.Controls.Add(this.labelHeight);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.inputHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.NumericUpDown inputHeight;
        private System.Windows.Forms.Label labelAngle;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.NumericUpDown inputSize;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.NumericUpDown inputSpeed;
        private System.Windows.Forms.Label labelWeight;
        private System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.NumericUpDown inputAngle;
        private System.Windows.Forms.NumericUpDown inputWeight;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBoxStep;
        private System.Windows.Forms.DataGridView gridResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn range;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxH;
        private System.Windows.Forms.DataGridViewTextBoxColumn finalV;
        private System.Windows.Forms.DataGridViewTextBoxColumn elapsedMs;
    }
}

