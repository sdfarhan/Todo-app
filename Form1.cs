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
    public partial class Form1 : Form
    {
        private Todo todo;
        public Form1()
        {
            InitializeComponent();
            todo = new Todo();
        }
        private void TodaysScheduleButton_Click(object sender, EventArgs e)
        {
            displayTaskInTextArea(todo.TodayTasks);
        }
        private void YesterdaysScheduleButton_Click(object sender, EventArgs e)
        {
            displayTaskInTextArea(todo.YesterdayTasks);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            displayTaskInTextArea(todo.TodayTasks);
        }
        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            Form2 popup = new Form2();
            popup.ShowDialog();
            todo.TodayTasks.AddTask(todo.TodayTasks.Date,popup.EnteredTask);
            displayTaskInTextArea(todo.TodayTasks);
        }
        private void displayTaskInTextArea(Task task)
        {
            DateLabel.Text = task.Date.ToShortDateString();
            DayLabel.Text  =  task.Date.DayOfWeek.ToString();
            TaskListArea.Clear();
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
        private void ChangeDayButton_Click(object sender, EventArgs e)
        {
            todo.ChangeDay();
            displayTaskInTextArea(todo.TodayTasks);
        }
        private void DeleteTaskButton_Click(object sender, EventArgs e)
        {
            DeleteForm DeleteTask = new DeleteForm();
            DeleteTask.ShowDialog();
            if(DeleteTask.IndexofTask>0 && DeleteTask.IndexofTask <= todo.TodayTasks.Tasks.Count)
            {
                todo.TodayTasks.Tasks.RemoveAt(DeleteTask.IndexofTask - 1);
            }
            ENV.UpdateTodayTaskFile(todo.TodayTasks);
            displayTaskInTextArea(todo.TodayTasks);
        }
    }
}
