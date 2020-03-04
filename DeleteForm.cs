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
    public partial class DeleteForm : Form
    {
        public string IndexofTask;
        public DeleteForm()
        {
            InitializeComponent();
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            IndexofTask = IndexField.Text;
            if (IndexofTask.Length == 0)
            {
                WarningLabel.Text = "Please Enter some Task!!";
            }
            else
            {
                this.Dispose();
            }
        }
    }
}
