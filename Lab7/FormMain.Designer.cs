using System.Drawing;
using System.Windows.Forms;

namespace Lab5;

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
        SuspendLayout();
        // 
        // menuStrip
        // 
        menuStrip.ImageScalingSize = new Size(24, 24);
        menuStrip.Items.AddRange(new ToolStripItem[] { menuItemMain, menuItemHotel, menuItemGroup });
        menuStrip.Location = new Point(0, 0);
        menuStrip.Name = "menuStrip";
        menuStrip.Padding = new Padding(6, 3, 0, 3);
        menuStrip.Size = new Size(800, 35);
        menuStrip.TabIndex = 0;
        // 
        // menuItemMain
        // 
        menuItemMain.Name = "menuItemMain";
        menuItemMain.Size = new Size(92, 29);
        menuItemMain.Text = "Главная";
        // 
        // menuItemHotel
        // 
        menuItemHotel.Name = "menuItemHotel";
        menuItemHotel.Size = new Size(111, 29);
        menuItemHotel.Text = "Гостиница";
        // 
        // menuItemGroup
        // 
        menuItemGroup.Name = "menuItemGroup";
        menuItemGroup.Size = new Size(82, 29);
        menuItemGroup.Text = "Группа";
        // 
        // panelContent
        // 
        panelContent.Dock = DockStyle.Fill;
        panelContent.Location = new Point(0, 35);
        panelContent.Name = "panelContent";
        panelContent.Padding = new Padding(12);
        panelContent.Size = new Size(800, 565);
        panelContent.TabIndex = 1;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 600);
        Controls.Add(panelContent);
        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        MinimumSize = new Size(520, 500);
        Name = "FormMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Управление гостиницами";
        ResumeLayout(false);
        PerformLayout();
    }
}
