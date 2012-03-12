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
        
        private string textType(string str)
        {
            string type = "";
            foreach (char c in str)
            {
                if (c == '0' || c == '1' && type != "hex")
                {
                    type = "bin";
                    continue;
                }
                if (('A' <= c && c <= 'F') || ('0' <= c && c <= '9'))
                {
                    type = "hex";
                }
                else
                {
                    return "bad";
                }
            }
            return type;
        }
        private string hexToBin(string hex)
        {
            string ans = "";
            foreach (char c in hex)
            {
                switch (c)
                {
                    case '0':
                        ans += "0000";
                        break;
                    case '1':
                        ans += "0001";
                        break;
                    case '2':
                        ans += "0010";
                        break;
                    case '3':
                        ans += "0011";
                        break;
                    case '4':
                        ans += "0100";
                        break;
                    case '5':
                        ans += "0101";
                        break;
                    case '6':
                        ans += "0110";
                        break;
                    case '7':
                        ans += "0111";
                        break;
                    case '8':
                        ans += "1000";
                        break;
                    case '9':
                        ans += "1001";
                        break;
                    case 'A':
                        ans += "1010";
                        break;
                    case 'B':
                        ans += "1011";
                        break;
                    case 'C':
                        ans += "1100";
                        break;
                    case 'D':
                        ans += "1101";
                        break;
                    case 'E':
                        ans += "1110";
                        break;
                    case 'F':
                        ans += "1111";
                        break;
                }
            }
            return ans;
        }
        private string binToHex(string bin)
        {
            string tmp = "";
            string ans = "";
            for (int i = bin.Length - 1; i >= 0; i--)
            {
                tmp = bin[i] + tmp;
                if (tmp.Length == 4)
                {
                    ans = binToChar(tmp) + ans;
                    tmp = "";
                }
            }
            while (tmp.Length < 4 && tmp.Length != 0)
            {
                tmp = '0' + tmp;
            }
            if (tmp.Length != 0) ans += binToChar(tmp) + ans;
            return ans;
        }
        private char binToChar(string bin)
        {
            Int32 num = 0;
            for (int i = 3; i >= 0; i--)
            {
                if (bin[i] == '1')
                {
                    num += (int) Math.Pow(2, 3 - i);
                }
            }
            if (num >= 0 && num <= 9)
            {
                return (char)('0' + num);
            }

            return (char)('A' + (num - 10));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string type = textType(textBox1.Text.Replace(" ", ""));
            string binStr = "";
            int c = int.Parse(textBox3.Text) * 1000000;
            if (type.Equals("bad"))
            {
                Application.Exit();
            }
            if (type.Equals("hex"))
            {
                textBox2.Text = hexToBin(textBox1.Text);
                binStr = textBox2.Text.Replace(" ", "");
            }
            if (type.Equals("bin"))
            {
                textBox2.Text = binToHex(textBox1.Text);
                binStr = textBox1.Text.Replace(" ", "");
            }
            labelBytes.Text = Convert.ToString(binStr.Length / 8.0);
            drawOs(panelOs, binStr, panelOs.Height);
            CodingMethod manchMethod = new ManchesterMethod(STEP);
            _p1 = manchMethod.drawSignal(panel1, binStr, 0, 10, 67, c);
            CodingMethod amiMethod = new AmiMethod(STEP);
            _p2 = amiMethod.drawSignal(panel2, binStr, 0, 10, 67, c);
            CodingMethod potentialMethod = new PotentialMethod(STEP);
            _p3 = potentialMethod.drawSignal(panel3, binStr, 0, 10, 67, c);
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
