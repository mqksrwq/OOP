namespace Lab6
{
    partial class AsyncMethodsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelInfoTop = new System.Windows.Forms.Label();
            this.labelVector = new System.Windows.Forms.Label();
            this.textBoxVectorInput = new System.Windows.Forms.TextBox();
            this.buttonCalculateAverage = new System.Windows.Forms.Button();
            this.labelAverageResult = new System.Windows.Forms.Label();
            this.labelTimeResult = new System.Windows.Forms.Label();
            this.buttonThirdAction = new System.Windows.Forms.Button();
            this.labelThirdActionInfo = new System.Windows.Forms.Label();
            this.labelThirdActionResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelInfoTop
            // 
            this.labelInfoTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfoTop.AutoSize = false;
            this.labelInfoTop.Location = new System.Drawing.Point(28, 20);
            this.labelInfoTop.Name = "labelInfoTop";
            this.labelInfoTop.Size = new System.Drawing.Size(660, 50);
            this.labelInfoTop.TabIndex = 0;
            this.labelInfoTop.Text = "На форме запускаются 3 асинхронных действия: расчет среднего, получение времени и третье действие";
            // 
            // labelVector
            // 
            this.labelVector.AutoSize = true;
            this.labelVector.Location = new System.Drawing.Point(28, 66);
            this.labelVector.Name = "labelVector";
            this.labelVector.Size = new System.Drawing.Size(326, 25);
            this.labelVector.TabIndex = 1;
            this.labelVector.Text = "Вектор чисел (через пробел/запятую)";
            // 
            // textBoxVectorInput
            // 
            this.textBoxVectorInput.Location = new System.Drawing.Point(33, 95);
            this.textBoxVectorInput.Name = "textBoxVectorInput";
            this.textBoxVectorInput.Size = new System.Drawing.Size(913, 31);
            this.textBoxVectorInput.TabIndex = 2;
            this.textBoxVectorInput.Text = "10 15 20 25";
            // 
            // buttonCalculateAverage
            // 
            this.buttonCalculateAverage.Location = new System.Drawing.Point(33, 146);
            this.buttonCalculateAverage.Name = "buttonCalculateAverage";
            this.buttonCalculateAverage.Size = new System.Drawing.Size(328, 49);
            this.buttonCalculateAverage.TabIndex = 3;
            this.buttonCalculateAverage.Text = "Рассчитать среднее";
            this.buttonCalculateAverage.UseVisualStyleBackColor = true;
            this.buttonCalculateAverage.Click += new System.EventHandler(this.buttonCalculateAverage_Click);
            // 
            // labelAverageResult
            // 
            this.labelAverageResult.AutoSize = true;
            this.labelAverageResult.Location = new System.Drawing.Point(28, 210);
            this.labelAverageResult.Name = "labelAverageResult";
            this.labelAverageResult.Size = new System.Drawing.Size(108, 25);
            this.labelAverageResult.TabIndex = 4;
            this.labelAverageResult.Text = "Результат: -";
            // 
            // labelTimeResult
            // 
            this.labelTimeResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTimeResult.AutoSize = true;
            this.labelTimeResult.Location = new System.Drawing.Point(696, 20);
            this.labelTimeResult.Name = "labelTimeResult";
            this.labelTimeResult.Size = new System.Drawing.Size(236, 25);
            this.labelTimeResult.TabIndex = 5;
            this.labelTimeResult.Text = "Серверное время: 00:00:00";
            // 
            // buttonThirdAction
            // 
            this.buttonThirdAction.Location = new System.Drawing.Point(33, 366);
            this.buttonThirdAction.Name = "buttonThirdAction";
            this.buttonThirdAction.Size = new System.Drawing.Size(328, 49);
            this.buttonThirdAction.TabIndex = 7;
            this.buttonThirdAction.Text = "Третье действие";
            this.buttonThirdAction.UseVisualStyleBackColor = true;
            this.buttonThirdAction.Click += new System.EventHandler(this.buttonThirdAction_Click);
            // 
            // labelThirdActionInfo
            // 
            this.labelThirdActionInfo.AutoSize = true;
            this.labelThirdActionInfo.Location = new System.Drawing.Point(28, 428);
            this.labelThirdActionInfo.Name = "labelThirdActionInfo";
            this.labelThirdActionInfo.Size = new System.Drawing.Size(838, 25);
            this.labelThirdActionInfo.TabIndex = 8;
            this.labelThirdActionInfo.Text = "Третье действие: асинхронно генерирует случайное число и показывает, четное оно или нет";
            // 
            // labelThirdActionResult
            // 
            this.labelThirdActionResult.AutoSize = true;
            this.labelThirdActionResult.Location = new System.Drawing.Point(28, 464);
            this.labelThirdActionResult.Name = "labelThirdActionResult";
            this.labelThirdActionResult.Size = new System.Drawing.Size(195, 25);
            this.labelThirdActionResult.TabIndex = 9;
            this.labelThirdActionResult.Text = "Третье действие: -";
            // 
            // AsyncMethodsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.labelInfoTop);
            this.Controls.Add(this.labelThirdActionInfo);
            this.Controls.Add(this.labelThirdActionResult);
            this.Controls.Add(this.buttonThirdAction);
            this.Controls.Add(this.labelTimeResult);
            this.Controls.Add(this.labelAverageResult);
            this.Controls.Add(this.buttonCalculateAverage);
            this.Controls.Add(this.textBoxVectorInput);
            this.Controls.Add(this.labelVector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsyncMethodsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Асинхронные методы";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfoTop;
        private System.Windows.Forms.Label labelVector;
        private System.Windows.Forms.TextBox textBoxVectorInput;
        private System.Windows.Forms.Button buttonCalculateAverage;
        private System.Windows.Forms.Label labelAverageResult;
        private System.Windows.Forms.Label labelTimeResult;
        private System.Windows.Forms.Button buttonThirdAction;
        private System.Windows.Forms.Label labelThirdActionInfo;
        private System.Windows.Forms.Label labelThirdActionResult;
    }
}
