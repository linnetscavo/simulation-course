using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    public partial class Form2 : Form
    {
        private MultiplicativeCongruentialGenerator generator;

        private string[] positiveAnswers = {
            "Безусловно да", "Решительно да", "Можешь быть уверен",
            "Мне кажется — да", "Наиболее вероятно"
        };

        private string[] neutralAnswers = {
            "Ответ неясен, попробуй снова", "Спроси позже",
            "Лучше не говорить", "Сейчас нельзя предсказать",
            "Сконцентрируйся и спроси опять"
        };

        private string[] negativeAnswers = {
            "Не рассчитывай на это", "Мой ответ — нет",
            "По моим данным — нет", "Перспективы не очень",
            "Источники говорят — нет"
        };

        private double[] probabilities = { 0.50, 0.25, 0.25 };

        public Form2()
        {
            InitializeComponent();
            generator = new MultiplicativeCongruentialGenerator();
            for (int i = 0; i < 50; i++)
            {
                generator.NextDouble();
            }
        }
        private void btnAsk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                MessageBox.Show("Сначала задайте вопрос!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            double alpha = generator.NextDouble(); 


            string answer;

            if (alpha < 0.50)  
            {
                int index = (int)(generator.NextDouble() * positiveAnswers.Length);
                answer = positiveAnswers[index];
                lblAnswer.ForeColor = Color.Green;
            }
            else if (alpha < 0.75) 
            {
                int index = (int)(generator.NextDouble() * neutralAnswers.Length);
                answer = neutralAnswers[index];
                lblAnswer.ForeColor = Color.Orange;
            }
            else  
            {
                int index = (int)(generator.NextDouble() * negativeAnswers.Length);
                answer = negativeAnswers[index];
                lblAnswer.ForeColor = Color.Red;
            }

            lblAnswer.Text = answer;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void txtQuestion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
