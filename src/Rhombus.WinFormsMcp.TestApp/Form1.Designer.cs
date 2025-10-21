namespace Rhombus.WinFormsMcp.TestApp;

partial class Form1
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
        this.components = new System.ComponentModel.Container();

        // Create layout panel
        var mainPanel = new System.Windows.Forms.Panel
        {
            Name = "mainPanel",
            Dock = System.Windows.Forms.DockStyle.Fill,
            AutoScroll = true
        };

        // Title label
        var titleLabel = new System.Windows.Forms.Label
        {
            Name = "titleLabel",
            Text = "fnWindowsMCP Test Application",
            Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
            Location = new System.Drawing.Point(10, 10),
            Size = new System.Drawing.Size(400, 30)
        };
        mainPanel.Controls.Add(titleLabel);

        // Section 1: TextBox
        var textBoxLabel = new System.Windows.Forms.Label
        {
            Text = "Text Input:",
            Location = new System.Drawing.Point(10, 50),
            Size = new System.Drawing.Size(100, 20)
        };
        mainPanel.Controls.Add(textBoxLabel);

        this.textBox = new System.Windows.Forms.TextBox
        {
            Name = "textBox",
            Location = new System.Drawing.Point(120, 50),
            Size = new System.Drawing.Size(200, 20),
            Text = "Type here..."
        };
        mainPanel.Controls.Add(this.textBox);

        // Section 2: Button
        this.clickButton = new System.Windows.Forms.Button
        {
            Name = "clickButton",
            Text = "Click Me",
            Location = new System.Drawing.Point(10, 80),
            Size = new System.Drawing.Size(100, 30)
        };
        this.clickButton.Click += (s, e) => MessageBox.Show("Button clicked!");
        mainPanel.Controls.Add(this.clickButton);

        // Section 3: CheckBox
        this.checkBox = new System.Windows.Forms.CheckBox
        {
            Name = "checkBox",
            Text = "Enable feature",
            Location = new System.Drawing.Point(120, 80),
            Size = new System.Drawing.Size(150, 30)
        };
        mainPanel.Controls.Add(this.checkBox);

        // Section 4: ComboBox
        var comboLabel = new System.Windows.Forms.Label
        {
            Text = "Select option:",
            Location = new System.Drawing.Point(10, 120),
            Size = new System.Drawing.Size(100, 20)
        };
        mainPanel.Controls.Add(comboLabel);

        this.comboBox = new System.Windows.Forms.ComboBox
        {
            Name = "comboBox",
            Location = new System.Drawing.Point(120, 120),
            Size = new System.Drawing.Size(150, 25),
            DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        };
        this.comboBox.Items.AddRange(new object[] { "Option 1", "Option 2", "Option 3", "Option 4" });
        mainPanel.Controls.Add(this.comboBox);

        // Section 5: DataGridView
        var gridLabel = new System.Windows.Forms.Label
        {
            Text = "Data Table:",
            Location = new System.Drawing.Point(10, 160),
            Size = new System.Drawing.Size(100, 20)
        };
        mainPanel.Controls.Add(gridLabel);

        this.dataGridView = new System.Windows.Forms.DataGridView
        {
            Name = "dataGridView",
            Location = new System.Drawing.Point(10, 185),
            Size = new System.Drawing.Size(500, 150),
            AllowUserToAddRows = false
        };
        this.dataGridView.Columns.Add("Column1", "Name");
        this.dataGridView.Columns.Add("Column2", "Value");
        this.dataGridView.Rows.Add("Row1", "Value1");
        this.dataGridView.Rows.Add("Row2", "Value2");
        this.dataGridView.Rows.Add("Row3", "Value3");
        mainPanel.Controls.Add(this.dataGridView);

        // Section 6: Status label
        this.statusLabel = new System.Windows.Forms.Label
        {
            Name = "statusLabel",
            Text = "Status: Ready",
            Location = new System.Drawing.Point(10, 345),
            Size = new System.Drawing.Size(300, 20),
            ForeColor = System.Drawing.Color.Green
        };
        mainPanel.Controls.Add(this.statusLabel);

        // Section 7: List Box
        var listLabel = new System.Windows.Forms.Label
        {
            Text = "List Items:",
            Location = new System.Drawing.Point(520, 50),
            Size = new System.Drawing.Size(100, 20)
        };
        mainPanel.Controls.Add(listLabel);

        this.listBox = new System.Windows.Forms.ListBox
        {
            Name = "listBox",
            Location = new System.Drawing.Point(520, 75),
            Size = new System.Drawing.Size(250, 100)
        };
        this.listBox.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
        mainPanel.Controls.Add(this.listBox);

        // Form configuration
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 400);
        this.Text = "fnWindowsMCP Test Application";
        this.Name = "TestForm";
        this.Controls.Add(mainPanel);
    }

    #endregion

    private System.Windows.Forms.TextBox textBox;
    private System.Windows.Forms.Button clickButton;
    private System.Windows.Forms.CheckBox checkBox;
    private System.Windows.Forms.ComboBox comboBox;
    private System.Windows.Forms.DataGridView dataGridView;
    private System.Windows.Forms.Label statusLabel;
    private System.Windows.Forms.ListBox listBox;
}
