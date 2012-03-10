using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class BipolarRZ:CodingMethod
    {
        public BipolarRZ(int step) : base(step)
        {
        }

        public override void drawSignal(Panel can, string signal, int x, int minLevel, int height)
        {
            var linePen = new Pen((Color.Black), 1);
            minLevel = can.Height - minLevel;
            int maxLevel = minLevel - height;
            int midleLevel = minLevel - height/2;
            Graphics g = can.CreateGraphics();
            g.Clear(Color.White);
            bool up = true;
            for(int i = 0; i < signal.Length; i++)
            {
                int beginX = x + i * STEP;
                if (signal[i] == '0')
                {
                    g.DrawLine(linePen, beginX, midleLevel, beginX, minLevel);
                    g.DrawLine(linePen, beginX, minLevel, beginX + STEP / 2, minLevel);
                    g.DrawLine(linePen, beginX + STEP / 2, minLevel, beginX + STEP / 2, midleLevel);
                    g.DrawLine(linePen, beginX + STEP / 2, midleLevel, beginX + STEP, midleLevel);   
                }
                if (signal[i] == '1')
                {
                    g.DrawLine(linePen, beginX, midleLevel, beginX, maxLevel);
                    g.DrawLine(linePen, beginX, maxLevel, beginX + STEP / 2, maxLevel);
                    g.DrawLine(linePen, beginX + STEP / 2, maxLevel, beginX + STEP / 2, midleLevel);
                    g.DrawLine(linePen, beginX + STEP / 2, midleLevel, beginX + STEP, midleLevel);  
                }
                drawLines(g, beginX, height);
                if (can.Width < (i + 1) * STEP) can.Width += STEP * 2;
            }
        }
    }
}
