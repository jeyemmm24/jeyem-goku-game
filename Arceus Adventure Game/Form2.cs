using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arceus_Adventure_Game
{
    public partial class Form2 : Form
    {
        int speed = 20;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    panel1.Top -= speed;
                    break;

                case Keys.Down:
                case Keys.S:
                    panel1.Top += speed;
                    break;

                case Keys.Left:
                case Keys.A:
                    panel1.Left -= speed;
                    break;

                case Keys.Right:
                case Keys.D:

                    panel1.Left += speed;
                    break;
            }

            Colllision();
        }

        private void Colllision()
        {
            if (panel1.Bounds.IntersectsWith(panel2.Bounds))
            {
                Form1 form = new Form1();
                form.Show();
                this.Hide();

            }
        }
    }
}
