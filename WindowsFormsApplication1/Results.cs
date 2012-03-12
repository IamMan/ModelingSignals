using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Results : Form
    {
        public Results(MethodResult mr)
        {
            InitializeComponent();
            textBox1.Text = String.Format("{0:0.00}", mr.F0 / 1000000.0);
            textBox2.Text = String.Format("{0:0.00}", mr.Fn / 1000000.0);
            textBox3.Text = String.Format("{0:0.00}", mr.Fb / 1000000.0);
            textBox4.Text = String.Format("{0:0.00}", mr.S / 1000000.0);
            textBox5.Text = String.Format("{0:0.00}", mr.Fa / 1000000.0);
        }

        private void Results_Load(object sender, EventArgs e)
        {

        }
        
    }
}
