namespace Lab7;

partial class FormMain
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Label labelName;
    private System.Windows.Forms.TextBox textBoxName;

    private System.Windows.Forms.Label labelOccupiedRooms;
    private System.Windows.Forms.TextBox textBoxOccupiedRooms;

    private System.Windows.Forms.Label labelTotalRooms;
    private System.Windows.Forms.TextBox textBoxTotalRooms;

    private System.Windows.Forms.Label labelPricePerDay;
    private System.Windows.Forms.TextBox textBoxPricePerDay;

    private System.Windows.Forms.Label labelAddress;
    private System.Windows.Forms.TextBox textBoxAddress;

    private System.Windows.Forms.Label labelRating;
    private System.Windows.Forms.TextBox textBoxRating;

    private System.Windows.Forms.Label labelHasFreeWiFi;
    private System.Windows.Forms.CheckBox checkBoxHasFreeWiFi;

    private System.Windows.Forms.Button buttonCreate;
    private System.Windows.Forms.Button buttonApply;
    private System.Windows.Forms.Button buttonCancel;

    private System.Windows.Forms.FlowLayoutPanel flowHotels;
    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel1;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        labelName = new Label();
        textBoxName = new TextBox();
        labelOccupiedRooms = new Label();
        textBoxOccupiedRooms = new TextBox();
        labelTotalRooms = new Label();
        textBoxTotalRooms = new TextBox();
        labelPricePerDay = new Label();
        textBoxPricePerDay = new TextBox();
        labelAddress = new Label();
        textBoxAddress = new TextBox();
        labelRating = new Label();
        textBoxRating = new TextBox();
        labelHasFreeWiFi = new Label();
        checkBoxHasFreeWiFi = new CheckBox();
        buttonCreate = new Button();
        buttonApply = new Button();
        buttonCancel = new Button();
        flowHotels = new FlowLayoutPanel();
        buttonExit = new Button();
        tableLayoutPanel1 = new TableLayoutPanel();
        panel1 = new Panel();
        tableLayoutPanel1.SuspendLayout();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // labelName
        // 
        labelName.AutoSize = true;
        labelName.Location = new Point(30, 40);
        labelName.Name = "labelName";
        labelName.Size = new Size(249, 32);
        labelName.TabIndex = 0;
        labelName.Text = "Название гостиницы:";
        // 
        // textBoxName
        // 
        textBoxName.Location = new Point(280, 36);
        textBoxName.MaxLength = 15;
        textBoxName.Name = "textBoxName";
        textBoxName.Size = new Size(268, 39);
        textBoxName.TabIndex = 1;
        // 
        // labelOccupiedRooms
        // 
        labelOccupiedRooms.AutoSize = true;
        labelOccupiedRooms.Location = new Point(30, 175);
        labelOccupiedRooms.Name = "labelOccupiedRooms";
        labelOccupiedRooms.Size = new Size(180, 32);
        labelOccupiedRooms.TabIndex = 2;
        labelOccupiedRooms.Text = "Заселено мест:";
        // 
        // textBoxOccupiedRooms
        // 
        textBoxOccupiedRooms.Location = new Point(280, 171);
        textBoxOccupiedRooms.MaxLength = 15;
        textBoxOccupiedRooms.Name = "textBoxOccupiedRooms";
        textBoxOccupiedRooms.Size = new Size(268, 39);
        textBoxOccupiedRooms.TabIndex = 3;
        // 
        // labelTotalRooms
        // 
        labelTotalRooms.AutoSize = true;
        labelTotalRooms.Location = new Point(30, 130);
        labelTotalRooms.Name = "labelTotalRooms";
        labelTotalRooms.Size = new Size(228, 32);
        labelTotalRooms.TabIndex = 4;
        labelTotalRooms.Text = "Общее число мест:";
        // 
        // textBoxTotalRooms
        // 
        textBoxTotalRooms.Location = new Point(280, 126);
        textBoxTotalRooms.MaxLength = 15;
        textBoxTotalRooms.Name = "textBoxTotalRooms";
        textBoxTotalRooms.Size = new Size(268, 39);
        textBoxTotalRooms.TabIndex = 5;
        // 
        // labelPricePerDay
        // 
        labelPricePerDay.AutoSize = true;
        labelPricePerDay.Location = new Point(30, 265);
        labelPricePerDay.Name = "labelPricePerDay";
        labelPricePerDay.Size = new Size(248, 32);
        labelPricePerDay.TabIndex = 6;
        labelPricePerDay.Text = "Оплата за день (руб):";
        // 
        // textBoxPricePerDay
        // 
        textBoxPricePerDay.Location = new Point(280, 261);
        textBoxPricePerDay.MaxLength = 15;
        textBoxPricePerDay.Name = "textBoxPricePerDay";
        textBoxPricePerDay.Size = new Size(268, 39);
        textBoxPricePerDay.TabIndex = 7;
        // 
        // labelAddress
        // 
        labelAddress.AutoSize = true;
        labelAddress.Location = new Point(30, 85);
        labelAddress.Name = "labelAddress";
        labelAddress.Size = new Size(85, 32);
        labelAddress.TabIndex = 8;
        labelAddress.Text = "Адрес:";
        // 
        // textBoxAddress
        // 
        textBoxAddress.Location = new Point(280, 81);
        textBoxAddress.MaxLength = 15;
        textBoxAddress.Name = "textBoxAddress";
        textBoxAddress.Size = new Size(268, 39);
        textBoxAddress.TabIndex = 9;
        // 
        // labelRating
        // 
        labelRating.AutoSize = true;
        labelRating.Location = new Point(30, 220);
        labelRating.Name = "labelRating";
        labelRating.Size = new Size(106, 32);
        labelRating.TabIndex = 10;
        labelRating.Text = "Рейтинг:";
        // 
        // textBoxRating
        // 
        textBoxRating.Location = new Point(280, 216);
        textBoxRating.MaxLength = 15;
        textBoxRating.Name = "textBoxRating";
        textBoxRating.Size = new Size(268, 39);
        textBoxRating.TabIndex = 11;
        // 
        // labelHasFreeWiFi
        // 
        labelHasFreeWiFi.AutoSize = true;
        labelHasFreeWiFi.Location = new Point(30, 310);
        labelHasFreeWiFi.Name = "labelHasFreeWiFi";
        labelHasFreeWiFi.Size = new Size(214, 32);
        labelHasFreeWiFi.TabIndex = 12;
        labelHasFreeWiFi.Text = "Бесплатный Wi-Fi:";
        // 
        // checkBoxHasFreeWiFi
        // 
        checkBoxHasFreeWiFi.Location = new Point(280, 310);
        checkBoxHasFreeWiFi.Name = "checkBoxHasFreeWiFi";
        checkBoxHasFreeWiFi.Size = new Size(30, 32);
        checkBoxHasFreeWiFi.TabIndex = 13;
        // 
        // buttonCreate
        // 
        buttonCreate.Location = new Point(30, 390);
        buttonCreate.Name = "buttonCreate";
        buttonCreate.Size = new Size(280, 40);
        buttonCreate.TabIndex = 14;
        buttonCreate.Text = "Создать гостиницу";
        buttonCreate.UseVisualStyleBackColor = true;
        buttonCreate.Click += buttonCreate_Click;
        // 
        // buttonApply
        // 
        buttonApply.Location = new Point(30, 448);
        buttonApply.Name = "buttonApply";
        buttonApply.Size = new Size(280, 40);
        buttonApply.TabIndex = 15;
        buttonApply.Text = "Применить изменения";
        buttonApply.Visible = false;
        buttonApply.Click += buttonApply_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.Location = new Point(320, 448);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new Size(228, 40);
        buttonCancel.TabIndex = 16;
        buttonCancel.Text = "Отменить";
        buttonCancel.Visible = false;
        buttonCancel.Click += buttonCancel_Click;
        // 
        // flowHotels
        // 
        flowHotels.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        flowHotels.AutoScroll = true;
        flowHotels.FlowDirection = FlowDirection.TopDown;
        flowHotels.Location = new Point(554, 22);
        flowHotels.Name = "flowHotels";
        flowHotels.Size = new Size(420, 466);
        flowHotels.TabIndex = 17;
        flowHotels.WrapContents = false;
        // 
        // buttonExit
        // 
        buttonExit.Location = new Point(320, 390);
        buttonExit.Name = "buttonExit";
        buttonExit.Size = new Size(228, 40);
        buttonExit.TabIndex = 18;
        buttonExit.Text = "Выход";
        buttonExit.UseVisualStyleBackColor = true;
        buttonExit.Click += buttonExit_Click;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1000F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(panel1, 1, 1);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 3;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 560F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(1087, 670);
        tableLayoutPanel1.TabIndex = 19;
        // 
        // panel1
        // 
        panel1.Anchor = AnchorStyles.None;
        panel1.Controls.Add(flowHotels);
        panel1.Controls.Add(textBoxName);
        panel1.Controls.Add(buttonExit);
        panel1.Controls.Add(textBoxOccupiedRooms);
        panel1.Controls.Add(labelName);
        panel1.Controls.Add(textBoxTotalRooms);
        panel1.Controls.Add(labelOccupiedRooms);
        panel1.Controls.Add(textBoxPricePerDay);
        panel1.Controls.Add(labelTotalRooms);
        panel1.Controls.Add(textBoxAddress);
        panel1.Controls.Add(labelAddress);
        panel1.Controls.Add(textBoxRating);
        panel1.Controls.Add(labelPricePerDay);
        panel1.Controls.Add(labelHasFreeWiFi);
        panel1.Controls.Add(labelRating);
        panel1.Controls.Add(buttonCreate);
        panel1.Controls.Add(checkBoxHasFreeWiFi);
        panel1.Controls.Add(buttonApply);
        panel1.Controls.Add(buttonCancel);
        panel1.Location = new Point(46, 58);
        panel1.Name = "panel1";
        panel1.Size = new Size(994, 554);
        panel1.TabIndex = 0;
        // 
        // FormMain
        // 
        ClientSize = new Size(1087, 670);
        Controls.Add(tableLayoutPanel1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormMain";
        Text = "Создание гостиницы";
        tableLayoutPanel1.ResumeLayout(false);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
    }

}

