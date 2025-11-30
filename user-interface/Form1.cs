using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace pwm_user_interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void btnCONNECT_Click(object sender, EventArgs e)
        {
            if (!serCom.IsOpen)
            {
                btnCONNECT.Text = "DISCONNECT";
                serCom.PortName = "COM5";
                serCom.BaudRate = Convert.ToInt32("115200");

                serCom.Open();
            }
            else
            {
                btnCONNECT.Text = "CONNECT";
                serCom.Write("000");
                serCom.Close();
            }
        }

        private void btnEXIT_Click(object sender, EventArgs e)
        {
            serCom.Close();
            Application.Exit();
        }

        string value;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            value = trackBar1.Value.ToString("D3");

            int pwmValue = int.Parse(value);

            lbSpeed.Text = "Speed: " + pwmValue.ToString();

            serCom.Write(value); 

        }
    }
}
