using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vector_Drawing_Application
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void githubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            githubLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/LuxuriaBob/Vector-Drawing-Application-with-Tree-Data-Structure");
        }
    }
}
