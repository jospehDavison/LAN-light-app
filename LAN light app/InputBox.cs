using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAN_light_app
{
    public partial class InputBox : Form
    {
        public InputBox(string name)
        {
            InitializeComponent();
            this.Text = name;
            lblName.Text = name;
            lblResponse.Text = "";
        }

        public string result;
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            result = tbxInput.Text;
            this.DialogResult = DialogResult.OK;
        }

        public void ResponseText(string response)
        {
            lblResponse.Text = response;
        }
    }
}
