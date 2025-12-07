using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace pwm_user_interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            String[] Baud_Rate = { "1200", "2400", "4800", "9600", "115200" };
            cmbBaudrate.Items.AddRange(Baud_Rate);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCOM.DataSource = SerialPort.GetPortNames();
            cmbCOM.Text = "Undefined";
            cmbBaudrate.Text = "Undefined";
        }


        private void btnCONNECT_Click(object sender, EventArgs e)
        {
          if ((cmbCOM.Text == "Undefined") || (cmbBaudrate.Text == "Undefined"))
            {
                MessageBox.Show("Please set configuration first");
                return;
            }
            try
            {
                if (!serCom.IsOpen)
                {
                    btnCONNECT.Text = "DISCONNECT";
                    serCom.PortName = cmbCOM.Text;
                    serCom.BaudRate = Convert.ToInt32(cmbBaudrate.Text);
                    serCom.Open();
                }
                else
                {
                    serCom.Write("000");
                    trackBar1.Value = 0;
                    lbSpeed.Text = "Duty cycle: 0%";
                    btnCONNECT.Text = "CONNECT";
                    serCom.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror " + ex.Message);
            }
        }

        private void btnEXIT_Click(object sender, EventArgs e)
        {
            if (serCom.IsOpen)
            {
                serCom.Write("000");
                serCom.Close();
            }
            Application.Exit();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (serCom.IsOpen)
            {
                string value;
                value = trackBar1.Value.ToString("D3");

                int pwmValue = int.Parse(value);

                lbSpeed.Text = "Duty cycle: " + pwmValue.ToString() + "%";

                serCom.Write(value);
            }
            else
            {
                MessageBox.Show("Please connect Com port!");
            }
        }

        private void cmbCOM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serCom.IsOpen)
            {
                serCom.Write("000");
                serCom.Close();
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (serCom.IsOpen)
            {
                serCom.Write("NGH"); 
            }
            else
            {
                MessageBox.Show("Please connect Com port!");
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (serCom.IsOpen)
            {
                serCom.Write("THN"); 
            }
            else
            {
                MessageBox.Show("Please connect Com port!");
            }
        }

        private void serCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serCom.IsOpen)
                {                       
                    string dataReceived = serCom.ReadLine();

                    this.Invoke(new MethodInvoker(() =>
                    {
                        txtSpeedValue.Text = dataReceived + " RPS";
                    }));
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (serCom.IsOpen)
            {
                
                serCom.Write("000");                
                trackBar1.Value = 0;
                lbSpeed.Text = "Duty cycle: 0%";
            }
            else
            {
                MessageBox.Show("Com port not connected!");
            }
        }

        private void txtSpeedValue_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
