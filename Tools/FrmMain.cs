using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.EnDecryption;

namespace Tools
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnEnDecryption_Click(object sender, EventArgs e)
        {
            FrmEnDecryption lFrm = new FrmEnDecryption();
            lFrm.ShowDialog();
            lFrm.Dispose();
        }
    }
}
