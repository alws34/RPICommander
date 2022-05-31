using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPICommander.Forms
{
    public partial class frmAbout : Form
    {
        public frmAbout(string version)
        {
            InitializeComponent();
            CenterToScreen();
            lblVersion.Text = $"Version: {version}";
        }
    }
}
