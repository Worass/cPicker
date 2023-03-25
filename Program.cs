using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace cPicker
{
    public class colorPicker : Form
    {
        private ColorDialog colorDialog;
        private TextBox hexTextBox;
        private Label label;
        private PictureBox colorCircle;

        public colorPicker()
        {
            InitializeComponent();

            // Play beeping sound when opening app for the first time. //
            Console.Beep(440, 500);
        }

        private void InitializeComponent()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false;

            colorDialog = new ColorDialog
            {
                FullOpen = true
            };
            hexTextBox = new TextBox
            {
                Text = "#000000",
                Width = 100
            };
            hexTextBox.TextChanged += new EventHandler(HexTextBox_TextChanged);

            label = new Label
            {
                Text = "No color selected",
                AutoSize = true,
                Left = hexTextBox.Right + 10,
                Top = hexTextBox.Top
            };

            colorCircle = new PictureBox
            {
                Width = 200,
                Height = 200,
                Left = 10,
                Top = hexTextBox.Bottom + 10
            };
            colorCircle.Paint += new PaintEventHandler(ColorCircle_Paint);

            Controls.Add(hexTextBox);
            Controls.Add(label);
            Controls.Add(colorCircle);
        }

        private void ColorCircle_Paint(object sender, PaintEventArgs e)
{
    Rectangle rect = new Rectangle(0, 0, colorCircle.Width, colorCircle.Height);

    // Create a linear gradient brush with four gradient stops
    LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Red, Color.Green, 0f);
    brush.InterpolationColors = new ColorBlend
    {
        Colors = new Color[] { Color.Red, Color.Yellow, Color.Lime, Color.Green },
        Positions = new float[] { 0f, 0.33f, 0.67f, 1f }
    };

    // Draw a circle with a gradient fill
    using (GraphicsPath path = new GraphicsPath())
    {
        path.AddEllipse(rect);
        using (PathGradientBrush fillBrush = new PathGradientBrush(path))
        {
            fillBrush.CenterPoint = new PointF(rect.Width / 2f, rect.Height / 2f);
            fillBrush.CenterColor = Color.White;
            fillBrush.SurroundColors = new Color[] { colorDialog.Color };
            e.Graphics.FillEllipse(fillBrush, rect);
        }
    }

    // Draw a white border
    using (Pen borderPen = new Pen(Color.White))
    {
        e.Graphics.DrawEllipse(borderPen, rect);
    }
}


        private void HexTextBox_TextChanged(object sender, EventArgs e)
        {
            string hex = hexTextBox.Text.Replace("#", "").Trim();

            // Limit input to 6 characters
            if (hex.Length > 6)
            {
                hex = hex.Substring(0, 6);
                hexTextBox.Text = "#" + hex;
                hexTextBox.SelectionStart = 7;
            }

            // Remove any non-numeric characters from the input
            hex = new string(hex.Where(c => char.IsDigit(c)).ToArray());

            if (hex.Length == 6)
            {
                Color color = Color.FromArgb(
                    int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)
                );
                colorDialog.Color = color;
                colorCircle.Invalidate();
                BackColor = color;
                label.Text = color.Name + " (RGB: " + color.R + ", " + color.G + ", " + color.B + ") (HEX: #" + hex.ToUpper() + ")";

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
