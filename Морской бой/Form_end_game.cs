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

namespace Морской_бой
{
    public partial class Form_end_game : Form
    {
        public Form_end_game(string s)
        {
            InitializeComponent();
            lbl_text.Text = s;
        }

        private void but_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
