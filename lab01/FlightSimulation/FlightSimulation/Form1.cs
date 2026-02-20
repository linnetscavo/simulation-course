using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlightSimulation
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        string activeSeries;
        const decimal g = 9.81M;
        const decimal C = 0.15M; 
        const decimal rho = 1.29M;

        decimal t, x, y, vx, vy, v;
        decimal k; 

        decimal maxHeight = 0;
        decimal finalSpeed = 0;
        bool isFlying = false;
        int stepCounter = 0;
        HashSet<decimal> usedSteps = new HashSet<decimal>();
        Stopwatch sw = new Stopwatch();


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Title = "Дальность, м";
            chart1.ChartAreas[0].AxisY.Title = "Высота, м";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (isFlying)
            {
                MessageBox.Show("Симуляция уже выполняется. Подождите окончания текущего запуска.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal h0 = inputHeight.Value; 
            decimal v0 = inputSpeed.Value;  
            decimal angleDeg = inputAngle.Value; 
            decimal S = inputSize.Value;          
            decimal m = inputWeight.Value;        
            
            decimal dt = decimal.Parse(comboBoxStep.Text);

            if (usedSteps.Contains(dt))
            {
                MessageBox.Show("Этот шаг моделирования уже был использован");
                return;
            }
            usedSteps.Add(dt);

            if (m <= 0)
            {
                MessageBox.Show("Масса должна быть больше нуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (S <= 0)
            {
                MessageBox.Show("Площадь сечения должна быть больше нуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double angleRad = (double)angleDeg * Math.PI / 180.0;
            decimal cosa = (decimal)Math.Cos(angleRad);
            decimal sina = (decimal)Math.Sin(angleRad);

            k = 0.5M * C * rho * S / m;

            t = 0;
            x = 0;
            y = h0;
            vx = v0 * cosa;
            vy = v0 * sina;

            maxHeight = y;
            isFlying = true;

            activeSeries = $"dt = {dt} с";

            var newSeries = new System.Windows.Forms.DataVisualization.Charting.Series(activeSeries);
            newSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            newSeries.Color = GetRandomColor(); 
            chart1.Series.Add(newSeries);

            chart1.Series[activeSeries].Points.AddXY((double)x, (double)y);

            timer1.Interval = 50;
            timer1.Tag = dt;      
            sw.Restart();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isFlying) return;

            decimal dt = (decimal)timer1.Tag;

            v = (decimal)Math.Sqrt((double)(vx * vx + vy * vy));

            vx = vx - k * vx * v * dt;
            vy = vy - (g + k * vy * v) * dt;
            x = x + vx * dt;
            y = y + vy * dt;
            t += dt;

            if (y > maxHeight) maxHeight = y;

            if (dt >= 0.1M) 
            {
                chart1.Series[activeSeries].Points.AddXY((double)x, (double)y);
            }
            else 
            {
                if (stepCounter % 1000 == 0) 
                    chart1.Series[activeSeries].Points.AddXY((double)x, (double)y);
            }

            if (y <= 0)
            {
                timer1.Stop();
                isFlying = false;


                if (chart1.Series[activeSeries].Points.Count >= 2)
                {
                    int lastIdx = chart1.Series[activeSeries].Points.Count - 1;
                    int prevIdx = lastIdx - 1;

                    var p1 = chart1.Series[activeSeries].Points[prevIdx]; 
                    var p2 = chart1.Series[activeSeries].Points[lastIdx];

                    double x1 = (double)p1.XValue;
                    double y1 = (double)p1.YValues[0];
                    double x2 = (double)p2.XValue;
                    double y2 = (double)p2.YValues[0];

                    if (Math.Abs(y2 - y1) > 1e-9)
                    {
                        double correctedX = x1 + (0 - y1) * (x2 - x1) / (y2 - y1);

                        chart1.Series[activeSeries].Points.RemoveAt(lastIdx);
                        chart1.Series[activeSeries].Points.AddXY(correctedX, 0);

                        x = (decimal)correctedX;
                        y = 0;
                    }
                }

                stepCounter = 0;
                finalSpeed = (decimal)Math.Sqrt((double)(vx * vx + vy * vy));
                sw.Stop();
                TimeSpan elapsed = sw.Elapsed;

                ShowResults(dt, x, maxHeight, finalSpeed, elapsed.TotalMilliseconds);
            }
        }
        private void ShowResults(decimal dt, decimal range, decimal maxH, decimal finalV, double elapsedMs)
        {
            gridResults.Rows.Add(dt, range, maxH, finalV, elapsedMs);
            string message = $"Шаг моделирования: {dt} с\n" +
                             $"Дальность полета: {range:F4} м\n" +
                             $"Максимальная высота: {maxH:F4} м\n" +
                             $"Скорость в конечной точке: {finalV:F4} м/с";
            MessageBox.Show(message);
        }
        private System.Drawing.Color GetRandomColor()
        {
            return System.Drawing.Color.FromArgb(rand.Next(50, 255), rand.Next(50, 255), rand.Next(50, 255));
        }
    }
}
