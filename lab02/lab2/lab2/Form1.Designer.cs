namespace lab2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelTimeStep = new System.Windows.Forms.Label();
            this.labelSpaceStep = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.comboBoxTau = new System.Windows.Forms.ComboBox();
            this.comboBoxH = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colTau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCenterTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelParams = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTimeStep
            // 
            this.labelTimeStep.AutoSize = true;
            this.labelTimeStep.Location = new System.Drawing.Point(602, 356);
            this.labelTimeStep.Name = "labelTimeStep";
            this.labelTimeStep.Size = new System.Drawing.Size(119, 13);
            this.labelTimeStep.TabIndex = 0;
            this.labelTimeStep.Text = "Шаг по времени (τ), с:";
            // 
            // labelSpaceStep
            // 
            this.labelSpaceStep.AutoSize = true;
            this.labelSpaceStep.Location = new System.Drawing.Point(602, 414);
            this.labelSpaceStep.Name = "labelSpaceStep";
            this.labelSpaceStep.Size = new System.Drawing.Size(115, 13);
            this.labelSpaceStep.TabIndex = 1;
            this.labelSpaceStep.Text = "Шаг по простр. (h), м:";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(618, 473);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 25);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Запуск";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(618, 502);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Очистить таблицу";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // comboBoxTau
            // 
            this.comboBoxTau.FormattingEnabled = true;
            this.comboBoxTau.Items.AddRange(new object[] {
            "0,1",
            "0,01",
            "0,001",
            "0,0001"});
            this.comboBoxTau.Location = new System.Drawing.Point(600, 372);
            this.comboBoxTau.Name = "comboBoxTau";
            this.comboBoxTau.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTau.TabIndex = 4;
            // 
            // comboBoxH
            // 
            this.comboBoxH.FormattingEnabled = true;
            this.comboBoxH.Items.AddRange(new object[] {
            "0,1",
            "0,01",
            "0,001",
            "0,0001"});
            this.comboBoxH.Location = new System.Drawing.Point(600, 430);
            this.comboBoxH.Name = "comboBoxH";
            this.comboBoxH.Size = new System.Drawing.Size(121, 21);
            this.comboBoxH.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTau,
            this.colH,
            this.colCenterTemp});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(445, 519);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colTau
            // 
            this.colTau.HeaderText = "Шаг τ, с";
            this.colTau.Name = "colTau";
            // 
            // colH
            // 
            this.colH.HeaderText = "Шаг h, м";
            this.colH.Name = "colH";
            // 
            // colCenterTemp
            // 
            this.colCenterTemp.HeaderText = "T в центре, °C";
            this.colCenterTemp.Name = "colCenterTemp";
            this.colCenterTemp.Width = 200;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(463, 12);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(574, 338);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelParams);
            this.groupBox1.Location = new System.Drawing.Point(752, 356);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 181);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // labelParams
            // 
            this.labelParams.AutoSize = true;
            this.labelParams.Location = new System.Drawing.Point(6, 16);
            this.labelParams.Name = "labelParams";
            this.labelParams.Size = new System.Drawing.Size(82, 13);
            this.labelParams.TabIndex = 0;
            this.labelParams.Text = "Ждем запуска";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 542);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBoxH);
            this.Controls.Add(this.comboBoxTau);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.labelSpaceStep);
            this.Controls.Add(this.labelTimeStep);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTimeStep;
        private System.Windows.Forms.Label labelSpaceStep;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox comboBoxTau;
        private System.Windows.Forms.ComboBox comboBoxH;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTau;
        private System.Windows.Forms.DataGridViewTextBoxColumn colH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCenterTemp;
    }
}

