using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minefield
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void CreateMinefield(int width, int height)
        {
            int bs = 13;
            int margin = 1;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    var field = new Button();
                    field.Size = new Size(bs, bs);
                    field.Top = (bs + margin) * row;
                    field.Left = (bs + margin) * col;
                    this.Controls.Add(field);


                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateMinefield(50,50);
        }
       
    } 
}
