using CSLib.BackEnd.EnDecryption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.EnDecryption
{
    public partial class FrmEnDecryption : Form
    {
        public FrmEnDecryption()
        {
            InitializeComponent();
        }

        private void btnEnCryption_Click(object sender, EventArgs e)
        {
            DES lDes = new DES();
            lDes.KEY_64 = txtKey.Text;
            lDes.IV_64 = txtIV.Text;

            txtCiphertext.Text = lDes.Encode(txtPlaintext.Text);
        }

        private void btnDeCryption_Click(object sender, EventArgs e)
        {
            DES lDes = new DES();
            lDes.KEY_64 = txtKey.Text;
            lDes.IV_64 = txtIV.Text;

            txtPlaintext.Text = lDes.Decode(txtCiphertext.Text);
        }
    }
}
