using System;
using System.Drawing;
using System.Windows.Forms;

namespace cPicker
{
    public class colorPicker : Form
    {
        private ColorDialog colorDialog;
        private Button button;
        private Label label;
        private FlowLayoutPanel colorPalette;

        public colorPicker()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

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

            colorPalette = new FlowLayoutPanel
            {
                Width = 200,
                Height = 50,
                Left = 10,
                Top = button.Bottom + 10
            };
            AddColorsToPalette();

            Controls.Add(button);
            Controls.Add(label);
            Controls.Add(colorPalette);
        }

        private void AddColorsToPalette()
        {
            Color[] colors = new Color[] {
                Color.Red, Color.Orange, Color.Yellow,
                Color.Green, Color.Blue, Color.Indigo,
                Color.Violet, Color.Pink, Color.Brown,
                Color.Black
            };

            foreach (Color color in colors)
            {
                Button colorButton = new Button
                {
                    BackColor = color,
                    Width = 40,
                    Height = 40,
                    Margin = new Padding(10)
                };
                colorButton.Click += new EventHandler((sender, e) => ColorButton_Click(sender, e, color));
                colorPalette.Controls.Add(colorButton);
            }
        }

        private void ColorButton_Click(object sender, EventArgs e, Color color)
        {
            BackColor = color;
            label.Text = color.Name;
            label.Text = color.Name + " (RGB: " + color.R + ", " + color.G + ", " + color.B + ") (HEX: #" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2") + ")";
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog.Color;
                label.Text = colorDialog.Color.Name;
                label.Text = colorDialog.Color.Name + " (RGB: " + colorDialog.Color.R + ", " + colorDialog.Color.G + ", " + colorDialog.Color.B + ") (HEX: #" + colorDialog.Color.R.ToString("X2") + colorDialog.Color.G.ToString("X2") + colorDialog.Color.B.ToString("X2") + ")";
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
