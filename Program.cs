using System;
using System.Windows.Forms;

namespace cPicker
{
    public class colorPicker : Form
    {
        private ColorDialog colorDialog;
        private Button button;
        private Label label;

        public colorPicker()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            colorDialog = new ColorDialog
            {
                FullOpen = true
            };
            button = new Button
            {
                Text = "Choose Color"
            };
            button.Click += new EventHandler(Button_Click);

            label = new Label
            {
                Text = "No color selected",
                AutoSize = true,
                Left = button.Right + 10,
                Top = button.Top
            };

            Controls.Add(button);
            Controls.Add(label);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog.Color;
                label.Text = colorDialog.Color.Name;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new colorPicker());
        }
    }
}
