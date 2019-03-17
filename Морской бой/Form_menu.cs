using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Морской_бой
{
    public partial class Form_menu : Form
    {
        public Form_menu()
        {
            InitializeComponent();
        }
        
        private void but_start_game_Click(object sender, EventArgs e)
        {
            create_game();
        }

        private void but_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void but_help_Click(object sender, EventArgs e)
        {
            open_help();
        }

        private void open_help()
        {
            Form_help help = new Form_help();
            help.ShowDialog();
        }

        public void create_game()
        {
            Form_game game_screen = new Form_game();
            game_screen.ShowDialog();
        }
    }
}
