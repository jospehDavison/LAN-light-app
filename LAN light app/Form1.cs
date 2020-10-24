using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace LAN_light_app
{
    public partial class Form1 : Form
    {
        private string ip;
        public LightClient LC;

        public Form1()
        {
            InitializeComponent();
            LC = new LightClient(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InputBox ipInput = new InputBox("ip");
            while(ip == null)
            {
                if (ipInput.ShowDialog() == DialogResult.OK)
                {
                    ip = ipInput.result;
                    if(!(ip == null))
                    {
                        try
                        {
                            IPAddress _IP = IPAddress.Parse(ip);
                            LC.serverIP = _IP;
                            LC.connect();

                            MessageBox.Show("Connected: @" + ip);
                        }
                        catch
                        {
                            MessageBox.Show("error close and reopen, retype ip");
                        }
                    }
                }
            }
        }

        public void changeColor(string colorHex)
        {
            BackColor = ColorTranslator.FromHtml(colorHex);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LC.logout();
            Application.Exit();
        }
    }
}

