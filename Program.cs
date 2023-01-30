using System;
using System.Windows.Forms;

namespace cPicker
{
    public class colorPicker : Form
    {
        private ColorDialog colorDialog;
        private Button button;

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

            Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog.Color;
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
