﻿namespace Lab3;

partial class FormHotel
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
    
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        labelName = new System.Windows.Forms.Label();
        textBoxName = new System.Windows.Forms.TextBox();
        labelOccupiedRooms = new System.Windows.Forms.Label();
        textBoxOccupiedRooms = new System.Windows.Forms.TextBox();
        labelTotalRooms = new System.Windows.Forms.Label();
        textBoxTotalRooms = new System.Windows.Forms.TextBox();
        labelPricePerDay = new System.Windows.Forms.Label();
        textBoxPricePerDay = new System.Windows.Forms.TextBox();
        labelAddress = new System.Windows.Forms.Label();
        textBoxAddress = new System.Windows.Forms.TextBox();
        labelRating = new System.Windows.Forms.Label();
        textBoxRating = new System.Windows.Forms.TextBox();
        labelHasFreeWiFi = new System.Windows.Forms.Label();
        checkBoxHasFreeWiFi = new System.Windows.Forms.CheckBox();
        buttonCreate = new System.Windows.Forms.Button();
        buttonApply = new System.Windows.Forms.Button();
        buttonCancel = new System.Windows.Forms.Button();
        flowHotels = new System.Windows.Forms.FlowLayoutPanel();
        buttonExit = new System.Windows.Forms.Button();
        tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        panel1 = new System.Windows.Forms.Panel();
        listView1 = new System.Windows.Forms.ListView();
        buttonTest = new System.Windows.Forms.Button();
        tableLayoutPanel1.SuspendLayout();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // labelName
        // 
        labelName.AutoSize = true;
        labelName.Location = new System.Drawing.Point(5, 29);
        labelName.Name = "labelName";
        labelName.Size = new System.Drawing.Size(159, 20);
        labelName.TabIndex = 0;
        labelName.Text = "Название гостиницы:";
        // 
        // textBoxName
        // 
        textBoxName.Location = new System.Drawing.Point(166, 26);
        textBoxName.MaxLength = 15;
        textBoxName.Name = "textBoxName";
        textBoxName.Size = new System.Drawing.Size(200, 27);
        textBoxName.TabIndex = 1;
        // 
        // labelOccupiedRooms
        // 
        labelOccupiedRooms.AutoSize = true;
        labelOccupiedRooms.Location = new System.Drawing.Point(3, 128);
        labelOccupiedRooms.Name = "labelOccupiedRooms";
        labelOccupiedRooms.Size = new System.Drawing.Size(113, 20);
        labelOccupiedRooms.TabIndex = 2;
        labelOccupiedRooms.Text = "Заселено мест:";
        // 
        // textBoxOccupiedRooms
        // 
        textBoxOccupiedRooms.Location = new System.Drawing.Point(166, 125);
        textBoxOccupiedRooms.MaxLength = 15;
        textBoxOccupiedRooms.Name = "textBoxOccupiedRooms";
        textBoxOccupiedRooms.Size = new System.Drawing.Size(200, 27);
        textBoxOccupiedRooms.TabIndex = 4;
        // 
        // labelTotalRooms
        // 
        labelTotalRooms.AutoSize = true;
        labelTotalRooms.Location = new System.Drawing.Point(5, 95);
        labelTotalRooms.Name = "labelTotalRooms";
        labelTotalRooms.Size = new System.Drawing.Size(141, 20);
        labelTotalRooms.TabIndex = 4;
        labelTotalRooms.Text = "Общее число мест:";
        // 
        // textBoxTotalRooms
        // 
        textBoxTotalRooms.Location = new System.Drawing.Point(166, 92);
        textBoxTotalRooms.MaxLength = 15;
        textBoxTotalRooms.Name = "textBoxTotalRooms";
        textBoxTotalRooms.Size = new System.Drawing.Size(200, 27);
        textBoxTotalRooms.TabIndex = 3;
        // 
        // labelPricePerDay
        // 
        labelPricePerDay.AutoSize = true;
        labelPricePerDay.Location = new System.Drawing.Point(5, 194);
        labelPricePerDay.Name = "labelPricePerDay";
        labelPricePerDay.Size = new System.Drawing.Size(157, 20);
        labelPricePerDay.TabIndex = 6;
        labelPricePerDay.Text = "Оплата за день (руб):";
        // 
        // textBoxPricePerDay
        // 
        textBoxPricePerDay.Location = new System.Drawing.Point(166, 191);
        textBoxPricePerDay.MaxLength = 15;
        textBoxPricePerDay.Name = "textBoxPricePerDay";
        textBoxPricePerDay.Size = new System.Drawing.Size(200, 27);
        textBoxPricePerDay.TabIndex = 6;
        // 
        // labelAddress
        // 
        labelAddress.AutoSize = true;
        labelAddress.Location = new System.Drawing.Point(5, 62);
        labelAddress.Name = "labelAddress";
        labelAddress.Size = new System.Drawing.Size(54, 20);
        labelAddress.TabIndex = 8;
        labelAddress.Text = "Адрес:";
        // 
        // textBoxAddress
        // 
        textBoxAddress.Location = new System.Drawing.Point(166, 59);
        textBoxAddress.MaxLength = 15;
        textBoxAddress.Name = "textBoxAddress";
        textBoxAddress.Size = new System.Drawing.Size(200, 27);
        textBoxAddress.TabIndex = 2;
        // 
        // labelRating
        // 
        labelRating.AutoSize = true;
        labelRating.Location = new System.Drawing.Point(3, 161);
        labelRating.Name = "labelRating";
        labelRating.Size = new System.Drawing.Size(67, 20);
        labelRating.TabIndex = 10;
        labelRating.Text = "Рейтинг:";
        // 
        // textBoxRating
        // 
        textBoxRating.Location = new System.Drawing.Point(166, 158);
        textBoxRating.MaxLength = 15;
        textBoxRating.Name = "textBoxRating";
        textBoxRating.Size = new System.Drawing.Size(200, 27);
        textBoxRating.TabIndex = 5;
        // 
        // labelHasFreeWiFi
        // 
        labelHasFreeWiFi.AutoSize = true;
        labelHasFreeWiFi.Location = new System.Drawing.Point(5, 225);
        labelHasFreeWiFi.Name = "labelHasFreeWiFi";
        labelHasFreeWiFi.Size = new System.Drawing.Size(135, 20);
        labelHasFreeWiFi.TabIndex = 12;
        labelHasFreeWiFi.Text = "Бесплатный Wi-Fi:";
        // 
        // checkBoxHasFreeWiFi
        // 
        checkBoxHasFreeWiFi.Location = new System.Drawing.Point(166, 224);
        checkBoxHasFreeWiFi.Name = "checkBoxHasFreeWiFi";
        checkBoxHasFreeWiFi.Size = new System.Drawing.Size(20, 24);
        checkBoxHasFreeWiFi.TabIndex = 7;
        // 
        // buttonCreate
        // 
        buttonCreate.Location = new System.Drawing.Point(3, 298);
        buttonCreate.Name = "buttonCreate";
        buttonCreate.Size = new System.Drawing.Size(212, 30);
        buttonCreate.TabIndex = 8;
        buttonCreate.Text = "Создать гостиницу";
        buttonCreate.UseVisualStyleBackColor = true;
        buttonCreate.Click += buttonCreate_Click;
        // 
        // buttonApply
        // 
        buttonApply.Location = new System.Drawing.Point(3, 333);
        buttonApply.Name = "buttonApply";
        buttonApply.Size = new System.Drawing.Size(212, 30);
        buttonApply.TabIndex = 10;
        buttonApply.Text = "Применить изменения";
        buttonApply.Visible = false;
        buttonApply.Click += buttonApply_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.Location = new System.Drawing.Point(227, 333);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new System.Drawing.Size(139, 30);
        buttonCancel.TabIndex = 11;
        buttonCancel.Text = "Отменить";
        buttonCancel.Visible = false;
        buttonCancel.Click += buttonCancel_Click;
        // 
        // flowHotels
        // 
        flowHotels.AutoScroll = true;
        flowHotels.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        flowHotels.Location = new System.Drawing.Point(372, 14);
        flowHotels.Name = "flowHotels";
        flowHotels.Size = new System.Drawing.Size(306, 349);
        flowHotels.TabIndex = 17;
        flowHotels.WrapContents = false;
        // 
        // buttonExit
        // 
        buttonExit.Location = new System.Drawing.Point(227, 298);
        buttonExit.Name = "buttonExit";
        buttonExit.Size = new System.Drawing.Size(139, 29);
        buttonExit.TabIndex = 9;
        buttonExit.Text = "Выход";
        buttonExit.UseVisualStyleBackColor = true;
        buttonExit.Click += buttonExit_Click;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 700F));
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(panel1, 1, 1);
        tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 3;
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 561F));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new System.Drawing.Size(913, 830);
        tableLayoutPanel1.TabIndex = 19;
        // 
        // panel1
        // 
        panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
        panel1.Controls.Add(listView1);
        panel1.Controls.Add(buttonTest);
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
        panel1.Location = new System.Drawing.Point(115, 142);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(681, 545);
        panel1.TabIndex = 0;
        // 
        // listView1
        // 
        listView1.Location = new System.Drawing.Point(0, 384);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(675, 161);
        listView1.TabIndex = 1;
        listView1.UseCompatibleStateImageBehavior = false;
        // 
        // buttonTest
        // 
        buttonTest.Location = new System.Drawing.Point(5, 262);
        buttonTest.Name = "buttonTest";
        buttonTest.Size = new System.Drawing.Size(361, 30);
        buttonTest.TabIndex = 18;
        buttonTest.Text = "Тест";
        buttonTest.UseVisualStyleBackColor = true;
        buttonTest.Click += buttonTest_Click;
        // 
        // FormHotel
        // 
        ClientSize = new System.Drawing.Size(913, 830);
        Controls.Add(tableLayoutPanel1);
        Text = "Создание гостиницы";
        tableLayoutPanel1.ResumeLayout(false);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
    }

    private System.Windows.Forms.ListView listView1;

    private System.Windows.Forms.Button buttonTest;

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
}