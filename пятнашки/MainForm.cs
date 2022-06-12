using System;
using System.Drawing;
using System.Windows.Forms;
using Pyatnashki.Properties;

namespace Pyatnashki
{
    public partial class MainForm : Form
    {
        private Game game;
        public MainForm()
        {
            InitializeComponent();
            game = new Game(4);
            menu.Renderer = new MenuRenderer();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int position = Convert.ToInt16(((Button)sender).Tag);
            game.Shift(position);
            RefreshButtons();
            if (game.CheckNumber())
            {
                menuStart.Image = Resources.LightSwordOff;
                MessageBox.Show("ПОБЕДА!");
                StartGame();
            }
        }
        private Button GetButton(int position)
        {
            string buttonName = "button" + position.ToString();
            Control control = tablePanel.Controls[buttonName];
            return (Button)control;
        }

        private void menuStart_Click(object sender, EventArgs e)
        {
            menuStart.Image = Resources.LightSword;
            foreach(var b in tablePanel.Controls)
            {
                var btn = b as Button;
                btn.BackColor = Color.Transparent;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = Color.ForestGreen;
                btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            }
            StartGame();
            tablePanel.Visible = true;
        }

        private void StartGame()
        {
            game.Start();
            for (int j = 0; j < 100; j++)
                game.ShiftRandom();
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            for (int position = 0; position < 16; position++)
            {
                int nr = game.GetNumber(position);
                Button button = GetButton(position);
                string imageName = "image_part_" + nr.ToString().PadLeft(3, '0');
                button.BackgroundImage = (Bitmap)Resources.ResourceManager.GetObject(imageName);
            }

        }

        

        /// <summary>
        /// Переопределение стилей рендера для меню
        /// (убирает базовые стили при наведении на айтемы)
        /// </summary>
        private class MenuRenderer : ToolStripProfessionalRenderer
        {
            public MenuRenderer() : base(new MenuHoverColors()) { }
        }

        private class MenuHoverColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.Transparent; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.Transparent; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.White; }
            }
        }
    }
}
