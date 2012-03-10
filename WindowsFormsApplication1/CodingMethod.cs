using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    abstract class CodingMethod
    {
        protected readonly int STEP;

        protected CodingMethod(int step)
        {
            STEP = step;
        }

        public abstract void drawSignal(Panel can, string signal, int x, int minLevel, int height);
        protected void drawLines(Graphics g, int x, int panHeight)
        {
            Pen linePen = new Pen(Color.Black, 1);
            float[] dashValues = { 5, 2 };
            linePen.DashPattern = dashValues;    
            g.DrawLine(linePen, x, 0, x, panHeight);
            g.DrawLine(linePen, x+STEP, 0, x+STEP, panHeight);

        }

    }
}
