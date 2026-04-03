using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class AsyncMethodsForm : Form
    {
        private static readonly Random _random = new Random();

        public AsyncMethodsForm()
        {
            InitializeComponent();
            MinimumSize = Size;
        }

        private async void buttonCalculateAverage_Click(object sender, EventArgs e)
        {
            try
            {
                buttonCalculateAverage.Enabled = false;
                labelAverageResult.Text = "Результат: расчет...";

                var values = ParseVector(textBoxVectorInput.Text);
                var average = await CalculateAverageAsync(values);

                labelAverageResult.Text = $"Результат: {average:F3}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonCalculateAverage.Enabled = true;
            }
        }

        private async void buttonShowTime_Click(object sender, EventArgs e)
        {
            buttonShowTime.Enabled = false;
            labelTimeResult.Text = "Системное время: обновление...";

            var time = await GetSystemTimeAsync();
            labelTimeResult.Text = $"Системное время: {time}";

            buttonShowTime.Enabled = true;
        }

        private async void buttonThirdAction_Click(object sender, EventArgs e)
        {
            buttonThirdAction.Enabled = false;
            labelThirdActionResult.Text = "Третье действие: выполнение...";

            var result = await GenerateRandomStatusAsync();
            labelThirdActionResult.Text = $"Третье действие: {result}";

            buttonThirdAction.Enabled = true;
        }

        private static double[] ParseVector(string raw)
        {
            var parts = raw.Split(new[] { ' ', ';', ',', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                throw new InvalidOperationException("Введите значения вектора");

            return parts
                .Select(p => double.Parse(p, CultureInfo.InvariantCulture))
                .ToArray();
        }

        private async Task<double> CalculateAverageAsync(double[] values)
        {
            return await Task.Run(() => values.Average());
        }

        private async Task<string> GetSystemTimeAsync()
        {
            await Task.Delay(250);
            return DateTime.Now.ToString("HH:mm:ss");
        }

        private async Task<string> GenerateRandomStatusAsync()
        {
            await Task.Delay(700);
            var number = _random.Next(1, 101);
            return $"Случайное число {number}, четное: {(number % 2 == 0 ? "да" : "нет")}";
        }
    }
}
