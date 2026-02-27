using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab2
{
    public partial class Form1 : Form
    {

        const double rho = 8900.0;   
        const double c = 385.0;      
        const double lambda = 400.0; 

        const double L = 0.1;        
        const double T_left = 100.0; 
        const double T_right = 0.0;  
        const double T_initial = 20.0; 
        const double t_model = 9.0;  
        public Form1()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            chart1.ChartAreas.Clear();
            var area = new ChartArea("MainArea");
            area.AxisX.Title = "Координата x, м";
            area.AxisY.Title = "Температура T, °C";
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = L;
            chart1.ChartAreas.Add(area);

            chart1.Series.Clear();
            var series = new Series("TemperatureProfile")
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.Blue,
                BorderWidth = 2
            };
            chart1.Series.Add(series);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(comboBoxTau.Text, out double tau))
            {
                MessageBox.Show("Ошибка parsing шага по времени", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(comboBoxH.Text, out double h))
            {
                MessageBox.Show("Ошибка parsing шага по пространству", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (h <= 0 || tau <= 0)
            {
                MessageBox.Show("Шаги должны быть больше нуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            labelParams.Text =
                $"Материал: медь\n" +
                $"ρ = {rho}\n" +
                $"c = {c}\n" +
                $"λ = {lambda}\n" +
                $"L = {L}\n" +
                $"τ = {tau}\n" +
                $"h = {h}\n" +
                $"T_left = {T_left}\n" +
                $"T_right = {T_right}\n" +
                $"T_initial = {T_initial}\n" +
                $"t_model = {t_model}";

            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                double tCenter = SolveHeatEquation(tau, h, out double[] T_final, out double[] xCoords);
                sw.Stop();

                labelParams.Text += $"\nВремя расчета: {sw.Elapsed.TotalMilliseconds:F2} мс";

                int rowIndex = dataGridView1.Rows.Add();
                dataGridView1["colTau", rowIndex].Value = tau;
                dataGridView1["colH", rowIndex].Value = h;
                dataGridView1["colCenterTemp", rowIndex].Value = tCenter.ToString("F4"); // 4 знака после запятой

                DrawGraph(xCoords, T_final);

                MessageBox.Show($"Расчет завершен!\nТемпература в центре: {tCenter:F4} °C", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расчете: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private double SolveHeatEquation(double tau, double h, out double[] T_final, out double[] xCoords)
        {
            int N = (int)Math.Round(L / h);
            if (N == 0) N = 1;
            h = L / N;

            int nodesCount = N + 1;

            xCoords = new double[nodesCount];
            double[] T = new double[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                xCoords[i] = i * h;
                T[i] = T_initial;
            }

            T[0] = T_left;
            T[N] = T_right;

            double coeff_A_C = lambda / (h * h);
            double coeff_B_base = 2.0 * lambda / (h * h);

            int timeSteps = (int)Math.Round(t_model / tau);
            if (timeSteps == 0) timeSteps = 1;
            tau = t_model / timeSteps;

            double coeff_F_factor = rho * c / tau;

            double[] alpha = new double[nodesCount];
            double[] beta = new double[nodesCount];

            for (int step = 0; step < timeSteps; step++)
            {
                alpha[0] = 0.0;
                beta[0] = T_left;

                for (int i = 1; i < N; i++)
                {
                    double B_i = coeff_B_base + coeff_F_factor;
                    double denominator = B_i - coeff_A_C * alpha[i - 1];
                    
                    alpha[i] = coeff_A_C / denominator;
                    beta[i] = (coeff_A_C * beta[i - 1] + coeff_F_factor * T[i]) / denominator;
                }

                T[N] = T_right;
                for (int i = N - 1; i >= 1; i--)
                {
                    T[i] = alpha[i] * T[i + 1] + beta[i];
                }
                T[0] = T_left;
            }
            T_final = T;

            int centerIndex = N / 2; 
            return T_final[centerIndex];
        }
        private void DrawGraph(double[] x, double[] T)
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < x.Length; i++)
            {
                chart1.Series[0].Points.AddXY(x[i], T[i]);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            chart1.Series[0].Points.Clear();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
