using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
namespace lab5
{
    public partial class Form1 : Form
    {
        private MultiplicativeCongruentialGenerator generator;
        private int yesCount = 0;
        private int noCount = 0;
        public Form1()
        {
            InitializeComponent();
            generator = new MultiplicativeCongruentialGenerator();
            trackProbability.Value = 50;
            lblProbValue.Text = "50%";
            for (int i = 0; i < 40; i++)
            {
                generator.NextDouble();
            }
        }
        private void trackProbability_Scroll(object sender, EventArgs e)
        {
            lblProbValue.Text = $"{trackProbability.Value}%";
        }

        private void btnGetAnswer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                MessageBox.Show("Сначала задайте вопрос!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            double alpha = generator.NextDouble();     
            double probability = trackProbability.Value / 100.0;  

            string answer;
            if (alpha < probability)
            {
                answer = "ДА";
                lblAnswer.ForeColor = Color.Green;
                yesCount++;
            }
            else
            {
                answer = "НЕТ";
                lblAnswer.ForeColor = Color.Red;
                noCount++;
            }

            lblAnswer.Text = answer;

            this.Text = $"Да или Нет | ДА: {yesCount} | НЕТ: {noCount}";
        }

        private void lblAnswer_Click(object sender, EventArgs e)
        {

        }
        private void btnToBall_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
