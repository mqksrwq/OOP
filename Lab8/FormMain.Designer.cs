using System.Drawing;
using System.Windows.Forms;

namespace Lab8;

public partial class FormMain
{
    private MenuStrip menuStrip;
    private ToolStripMenuItem menuItemMain;
    private ToolStripMenuItem menuItemHotel;
    private ToolStripMenuItem menuItemGroup;
    internal Panel panelContent = null!;

    private void InitializeComponent()
    {
        menuStrip = new MenuStrip();
        menuItemMain = new ToolStripMenuItem();
        menuItemHotel = new ToolStripMenuItem();
        menuItemGroup = new ToolStripMenuItem();
        panelContent = new Panel();
        menuStrip.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip
        // 
        menuStrip.ImageScalingSize = new Size(24, 24);
        menuStrip.Items.AddRange(new ToolStripItem[] { menuItemMain, menuItemHotel, menuItemGroup });
        menuStrip.Location = new Point(0, 0);
        menuStrip.Name = "menuStrip";
        menuStrip.Padding = new Padding(8, 4, 0, 4);
        menuStrip.Size = new Size(1038, 44);
        menuStrip.TabIndex = 0;
        // 
        // menuItemMain
        // 
        menuItemMain.Name = "menuItemMain";
        menuItemMain.Size = new Size(121, 36);
        menuItemMain.Text = "Главная";
        // 
        // menuItemHotel
        // 
        menuItemHotel.Name = "menuItemHotel";
        menuItemHotel.Size = new Size(148, 36);
        menuItemHotel.Text = "Гостиница";
        // 
        // menuItemGroup
        // 
        menuItemGroup.Name = "menuItemGroup";
        menuItemGroup.Size = new Size(111, 36);
        menuItemGroup.Text = "Группа";
        // 
        // panelContent
        // 
        panelContent.Dock = DockStyle.Fill;
        panelContent.Location = new Point(0, 44);
        panelContent.Margin = new Padding(4, 4, 4, 4);
        panelContent.Name = "panelContent";
        panelContent.Padding = new Padding(16, 15, 16, 15);
        panelContent.Size = new Size(1038, 724);
        panelContent.TabIndex = 1;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(13F, 32F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1038, 768);
        Controls.Add(panelContent);
        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        Margin = new Padding(4, 4, 4, 4);
        MinimumSize = new Size(668, 620);
        Name = "FormMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Управление гостиницами";
        menuStrip.ResumeLayout(false);
        menuStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}


