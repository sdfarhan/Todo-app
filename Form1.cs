using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private DateTime FormDate;
        public Form1()
        {
            InitializeComponent();
            FormDate = DateTime.Now;
        }
        private void TodaysScheduleButton_Click(object sender, EventArgs e)
        {
            using (Task task = new Task(DateTime.Now))
            {
                displayTaskInTextArea(task.Date);
            }
        }
        private void YesterdaysScheduleButton_Click(object sender, EventArgs e)
        {
            using (Task task = new Task(DateTime.Now.AddDays(-1)))
            {
                displayTaskInTextArea(task.Date);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            displayTaskInTextArea(DateTime.Now);
        }
        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            Form2 popup = new Form2();
            popup.ShowDialog();
            using (Task task = new Task(FormDate))
            {
                task.AddTask(popup.EnteredTask);
            }
            displayTaskInTextArea(FormDate);
        }
        private void displayTaskInTextArea(DateTime date)
        {
            DateLabel.Text = date.ToShortDateString();
            DayLabel.Text = date.DayOfWeek.ToString();
            TaskListArea.Clear();
            using (Task task = new Task(date))
            {
                if (task.Tasks.Count() == 0)
                {
                    TaskListArea.AppendText("Hurray You don't have any task!!");
                }
                int i = 0;
                foreach(SingleTask eachtask in task.Tasks)
                {
                    TaskListArea.AppendText(++i + eachtask.Time.ToString().Substring(0,8).PadLeft(9+15) + eachtask.Task.PadLeft(eachtask.Task.Length+25) + "\n");
                }

            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FormDate = dateTimePicker1.Value;
            if(ENV.IsTaskFileExist(FormDate))
                displayTaskInTextArea(FormDate);
        }
    }
}
