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
    public partial class Form_help : Form
    {
        public Form_help()
        {
            InitializeComponent();
            //in last version use GetCurrentDirectory()
            string content_help = File.ReadAllText("D:/Учёба/Летняя практика (2016)/Морской бой/Морской бой/Resources/Rules.txt", Encoding.GetEncoding(1251));
            lbl_text.Text = content_help;
        }

        private void but_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
