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
        public int IndexofTask;
        public DeleteForm()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (IndexField.Text.Length != 0)
            {
                IndexofTask = Int32.Parse(IndexField.Text);
            }
            this.Dispose();
        }
    }
}
