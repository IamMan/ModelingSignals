using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class AmiMethod:CodingMethod
    {
        public AmiMethod(int step) : base(step)
        {
        }
        
        public MethodResult calcSignal(string signal, int c)
        {
            if (signal.Length == 1)
            {
                return new MethodResult(0,0,0,0,0);
            }
            IDictionary<int, int> terms = new SortedDictionary<int, int>();
            int term00 = 0;
            int term01 = 4;
            int term11 = 2;
            var nowTerm = -1;
            for (var i = 1; i < signal.Length; i++)
            {
                if ((signal[i] == '1' && signal[i - 1] == '0') || (signal[i] == '0' && signal[i - 1] == '1'))
                {
                    if (nowTerm != 1)
                    {
                        if(nowTerm == 0)
                        {
                            term00 *= 4;
                            if (!terms.ContainsKey(term00))
                            {
                                terms.Add(term00, 0);
                            }
                            terms[term00]++;
                            term00 = 0;
                        }
                        if(!terms.ContainsKey(term01))
                        {
                            terms.Add(term01, 0);
                        }
                        terms[term01]++;
                        nowTerm = 1;
                    }
                    continue;   
                }
                if (signal[i] == '0' && signal[i - 1] == '0')
                {
                    nowTerm = 0;
                    term00++;
                    continue;
                }
                if (signal[i] == '1' && signal[i - 1] == '1')
                {
                    if (nowTerm != 3)
                    {
                        if (nowTerm == 0)
                        {
                            term00 *= 4;
                            if (!terms.ContainsKey(term00))
                            {
                                terms.Add(term00, 0);
                            }
                            terms[term00]++;
                            term00 = 0;
                        }
                        if (!terms.ContainsKey(term11))
                        {
                            terms.Add(term11, 0);
                        }
                        terms[term11]++;
                        nowTerm = 3;
                    }
                    continue;
                }
            }
            double bit = 1.0/c;
            double fa = terms.Sum(term => 1.0 / (term.Key * bit) * term.Value) / terms.Sum(term => term.Value);
            double f0 = 1.0 / terms.Keys.Min() / bit;
            double fn = 1.0 / terms.Keys.Max() / bit;
            double fb = 7.0*f0;
            return new MethodResult(f0, fn, fb, fb - fn, fa);
        }

        public override MethodResult drawSignal(Panel can, string signal, int x, int minLevel, int height, int c)
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
                    g.DrawLine(linePen, beginX, midleLevel, beginX + STEP, midleLevel);
                }
                if (signal[i] == '1')
                {
                    if (up)
                    {
                        g.DrawLine(linePen, beginX, midleLevel, beginX, maxLevel);
                        g.DrawLine(linePen, beginX, maxLevel, beginX + STEP, maxLevel);
                        g.DrawLine(linePen, beginX + STEP, maxLevel, beginX + STEP, midleLevel);
                        up = false;
                    }
                    else
                    {
                        g.DrawLine(linePen, beginX, midleLevel, beginX, minLevel);
                        g.DrawLine(linePen, beginX, minLevel, beginX + STEP, minLevel);
                        g.DrawLine(linePen, beginX + STEP, minLevel, beginX + STEP, midleLevel);
                        up = true;
                    }
                }
                drawLines(g, beginX, height);
                if (can.Width < (i + 1) * STEP) can.Width += STEP * 2;
            }
            return calcSignal(signal, c);
        }
    }
}
