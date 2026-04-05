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
        private readonly Timer _clockTimer;

        public AsyncMethodsForm()
        {
            InitializeComponent();
            MinimumSize = Size;

            labelTimeResult.AutoSize = true;
            PositionServerTimeLabel();

            _clockTimer = new Timer { Interval = 1000 };
            _clockTimer.Tick += (s, e) => UpdateServerTime();
            UpdateServerTime();
            _clockTimer.Start();
            FormClosed += (s, e) => _clockTimer.Stop();
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

        private void UpdateServerTime()
        {
            labelTimeResult.Text = $"Серверное время: {DateTime.Now:HH:mm:ss}";
            PositionServerTimeLabel();
        }

        private void PositionServerTimeLabel()
        {
            labelTimeResult.Visible = true;
            labelTimeResult.BringToFront();
            labelTimeResult.Top = 20;
            labelTimeResult.Left = ClientSize.Width - labelTimeResult.PreferredWidth - 20;

            int maxInfoWidth = labelTimeResult.Left - labelInfoTop.Left - 12;
            if (maxInfoWidth > 100)
                labelInfoTop.Width = maxInfoWidth;
        }

        private async Task<string> GenerateRandomStatusAsync()
        {
            await Task.Delay(700);
            var number = _random.Next(1, 101);
            return $"Случайное число {number}, четное: {(number % 2 == 0 ? "да" : "нет")}";
        }
    }
}
