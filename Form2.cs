using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public string EnteredTask;
        public Form2()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            EnteredTask = TaskInputBox.Text;
            if (EnteredTask.Length == 0)
                label1.Text = "Please Enter some Task!!";

            if(EnteredTask.Length>0)
                this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}