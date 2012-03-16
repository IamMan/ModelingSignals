using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private const int STEP = 20;
        private MethodResult _p1 = null;
        private MethodResult _p2 = null;
        private MethodResult _p3 = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void drawOs(Panel can, string binStr, int height)
        {
            Graphics g = can.CreateGraphics();
            g.Clear(Color.White);
            Pen linePen = new Pen(new HatchBrush(HatchStyle.DarkVertical, Color.Black), 1);
            float[] dashValues = {5, 2};
            linePen.DashPattern = dashValues;
            Brush textBrush = new SolidBrush(Color.Black);
            Console.WriteLine(linePen.PenType);
            for (int i = 0; i < binStr.Length; i++)
            {
                if (can.Width < (i + 1) * STEP) can.Width += STEP * 2;
                g.DrawLine(linePen, (i + 1) * STEP - STEP, 0, (i + 1) * STEP - STEP, height);
                g.DrawString(Char.ToString(binStr[i]), new Font("Calibri",10), textBrush, i * STEP+5, (float)(height/2.0)-10);
                g.DrawLine(linePen, (i + 1) * STEP, 0, (i + 1) * STEP, height);
            }
        }
        


        
       
        


        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if(_p1 == null) return;
            Results tmp = new Results(_p1);
            tmp.ShowDialog();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (_p2 == null) return;
            Results tmp = new Results(_p2);
            tmp.ShowDialog();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (_p3 == null) return;
            Results tmp = new Results(_p3);
            tmp.ShowDialog();
        }
    }
}
