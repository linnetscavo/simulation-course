using System;
using System.Drawing;
using System.Windows.Forms;
namespace lab3
{
    public partial class Form1 : Form
    {
        bool isStopped = false;
        const int GRID_SIZE = 50; 
        const int CELL_SIZE = 10; 

        enum CellState { Empty, Tree, Fire, Water }

        CellState[,] grid = new CellState[GRID_SIZE, GRID_SIZE];
        CellState[,] nextGrid = new CellState[GRID_SIZE, GRID_SIZE];

        int currentStep = 0;
        int totalTrees = 0;
        int burnedTrees = 0;
        bool isRunning = false;

        Color colorEmpty = Color.White;
        Color colorTree = Color.Green;
        Color colorFire = Color.Orange;
        Color colorWater = Color.LightBlue;

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            InitializeMap();
        }
        private void InitializeMap()
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    grid[i, j] = CellState.Empty;
                }
            }
            currentStep = 0;
            burnedTrees = 0;
            totalTrees = 0;
            UpdateStats();
            DrawMap();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            isStopped = false;
            if (isRunning)
            {
                MessageBox.Show("Остановите симуляцию перед генерацией нового леса!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InitializeMap();
            totalTrees = 0;

            // 1. Генерируем лес (70% деревьев)
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (rand.NextDouble() < 0.7)
                    {
                        grid[i, j] = CellState.Tree;
                        totalTrees++;
                    }
                }
            }

            // 2. Генерируем реку (горизонтальная полоса в случайном месте)
            int riverRow = rand.Next(GRID_SIZE / 4, 3 * GRID_SIZE / 4);
            for (int j = 0; j < GRID_SIZE; j++)
            {
                if (grid[riverRow, j] == CellState.Tree)
                    totalTrees--; // Река уничтожила дерево
                grid[riverRow, j] = CellState.Water;

                // Иногда добавляем второй ряд для ширины
                if (rand.NextDouble() < 0.5 && riverRow + 1 < GRID_SIZE)
                {
                    if (grid[riverRow + 1, j] == CellState.Tree)
                        totalTrees--;
                    grid[riverRow + 1, j] = CellState.Water;
                }
            }

            // 3. Поджигаем несколько случайных деревьев для старта
            int fireCount = 5;
            while (fireCount > 0)
            {
                int fi = rand.Next(GRID_SIZE);
                int fj = rand.Next(GRID_SIZE);
                if (grid[fi, fj] == CellState.Tree)
                {
                    grid[fi, fj] = CellState.Fire;
                    fireCount--;
                }
            }

            burnedTrees = 0;
            currentStep = 0;
            UpdateStats();
            DrawMap();
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isStopped)
            {
                MessageBox.Show("Симуляция завершена. Сгенерируйте новый лес для продолжения.",
                               "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (btnPause.Text == "Продолжить")
            {
                return; 
            }

            if (!isRunning)
            {
                isRunning = true;
                timerSim.Start();
                btnStart.Enabled = false;
                btnStart.Text = "Идет...";
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                timerSim.Stop();
                btnPause.Text = "Продолжить"; 
            }
            else
            {
                isRunning = true;
                timerSim.Start();
                btnPause.Text = "Пауза";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isRunning = false;
            isStopped = true;
            timerSim.Stop();
            btnPause.Text = "Пауза";
            btnPause.Enabled = false;
            MessageBox.Show($"Симуляция завершена!\n{lblBurned.Text}\nВсего шагов: {currentStep}",
                    "Симуляция остановлена",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            isRunning = false;
            timerSim.Stop();
            InitializeMap();
            btnPause.Enabled = true;
            btnStart.Enabled = true;
            btnStart.Text = "Старт";
        }

        private void timerSim_Tick(object sender, EventArgs e)
        {
            SimulateStep();
            currentStep++;
            UpdateStats();
            DrawMap();
        }

        private void SimulateStep()
        {
            Array.Copy(grid, nextGrid, grid.Length);

            double f = trackLightning.Value / 1000.0; 
            double p = trackGrowth.Value / 100.0;     
            double humidity = trackHumidity.Value / 100.0; 
            double windForce = trackWindForce.Value / 100.0; 

            int windX = 0, windY = 0;
            string windDir = comboBoxWindDir.Text;
            if (windDir == "Север") windY = 1; 
            else if (windDir == "Юг") windY = -1; 
            else if (windDir == "Запад") windX = 1; 
            else if (windDir == "Восток") windX = -1;

            int newBurned = 0;

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    CellState current = grid[i, j];

                    if (current == CellState.Fire)
                    {
                        nextGrid[i, j] = CellState.Empty;
                        newBurned++;
                    }
                    else if (current == CellState.Tree)
                    {
                        bool hasBurningNeighbor = false;
                        double maxFireProb = 0.0;

                        for (int di = -1; di <= 1; di++)
                        {
                            for (int dj = -1; dj <= 1; dj++)
                            {
                                if (di == 0 && dj == 0) continue;

                                int ni = i + di;
                                int nj = j + dj;

                                if (ni >= 0 && ni < GRID_SIZE && nj >= 0 && nj < GRID_SIZE)
                                {
                                    if (grid[ni, nj] == CellState.Fire)
                                    {
                                        hasBurningNeighbor = true;

                                        double windFactor = (dj * windX + di * windY) * windForce;
                                        double fireProb = 0.85 + windFactor; 

                                        fireProb *= (1.0 - humidity * 0.5);

                                        if (fireProb > maxFireProb)
                                            maxFireProb = fireProb;
                                    }
                                }
                            }
                        }
                        if (hasBurningNeighbor && rand.NextDouble() < maxFireProb)
                        {
                            nextGrid[i, j] = CellState.Fire;
                        }
                        else if (rand.NextDouble() < f * (1.0 - humidity))
                        {
                            nextGrid[i, j] = CellState.Fire;
                        }
                    }
                    else if (current == CellState.Empty)
                    {
                        if (rand.NextDouble() < p * (1.0 - humidity * 0.3))
                        {
                            nextGrid[i, j] = CellState.Tree;
                            totalTrees++;
                        }
                    }
                }
            }

            burnedTrees += newBurned;
            Array.Copy(nextGrid, grid, grid.Length);
        }

        private void DrawMap()
        {
            Bitmap bmp = new Bitmap(GRID_SIZE * CELL_SIZE, GRID_SIZE * CELL_SIZE);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    for (int j = 0; j < GRID_SIZE; j++)
                    {
                        Color cellColor = colorEmpty;
                        switch (grid[i, j])
                        {
                            case CellState.Tree: cellColor = colorTree; break;
                            case CellState.Fire: cellColor = colorFire; break;
                            case CellState.Water: cellColor = colorWater; break;
                        }

                        using (SolidBrush brush = new SolidBrush(cellColor))
                        {
                            g.FillRectangle(brush, j * CELL_SIZE, i * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                        }
                    }
                }
            }
            pictureBoxMap.Image = bmp;
        }

        private void UpdateStats()
        {
            lblStep.Text = $"Шаг: {currentStep}";

            double burnedPercent = totalTrees > 0 ? (burnedTrees * 100.0 / totalTrees) : 0;
            lblBurned.Text = $"Сгорело: {burnedPercent:F1}%";
        }

        private void comboBoxSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            int speed = int.Parse(comboBoxSpeed.Text);
            timerSim.Interval = 550 - (speed * 100);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackLightning_Scroll(object sender, EventArgs e)
        {

        }

        private void trackGrowth_Scroll(object sender, EventArgs e)
        {

        }

        private void trackWindForce_Scroll(object sender, EventArgs e)
        {

        }

        private void trackHumidity_Scroll(object sender, EventArgs e)
        {

        }

        private void comboBoxWindDir_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
