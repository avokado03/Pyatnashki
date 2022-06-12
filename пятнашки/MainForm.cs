using System;
using System.Windows.Forms;
using пятнашки.Properties;

namespace Pyatnashki
{
    public partial class MainForm : Form
    {
        Game game;
        public MainForm()
        {
            InitializeComponent();
            game = new Game(4);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int position = Convert.ToInt16(((Button)sender).Tag);
            game.Shift(position);
            refresh();
            if (game.CheckNumber())
            {
                MessageBox.Show("ПОБЕДА!");
                start_game();
            }
        }
        private Button GetButton(int position)
        {
            string buttonName = "button" + position.ToString();
            Control control = tablePanel.Controls[buttonName];
            return (Button)control;
        }

        private void menu_start_Click(object sender, EventArgs e)
        {
            tablePanel.Visible = true;
            start_game();
        }

        private void start_game()
        {
            game.Start();
            for (int j = 0; j < 100; j++)
                game.ShiftRandom();
            refresh();
        }

        private void refresh()
        {
            for (int position = 0; position < 16; position++)
            {
                int nr = game.GetNumber(position);
                Button button = GetButton(position);
                string imageName = "image_part_" + nr.ToString().PadLeft(3, '0');
                button.BackgroundImage = (System.Drawing.Bitmap)Resources.ResourceManager.GetObject(imageName);
            }

        }
    }
}
