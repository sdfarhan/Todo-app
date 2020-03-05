using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TaskForm : Form
    {
        private DateTime FormDate;
        public TaskForm()
        {
            InitializeComponent();
            FormDate = DateTime.Now;
        }
        private void TaskForm_Load(object sender, EventArgs e)
        {
            DisplayTaskInTextArea(FormDate);
        }
        private void TodaysScheduleButton_Click(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Now;
            DisplayTaskInTextArea(FormDate);
        }
        private void YesterdaysScheduleButton_Click(object sender, EventArgs e)
        {
                dateTimePicker.Value = DateTime.Now.AddDays(-1);
                DisplayTaskInTextArea(FormDate);
        }
        private void AddTaskButton_Click(object sender, EventArgs e)
        { 
            Form2 AddTaskEventHandler = new Form2(FormDate); // creating new form for taking input
            AddTaskEventHandler.ShowDialog(); // this will display the form
            try
            {
                using (Task task = Task.GetRequiredTasksObject(FormDate))
                {
                    task.AddTask(AddTaskEventHandler.EnteredTask, AddTaskEventHandler.SelectedTime);
                }
                DisplayTaskInTextArea(FormDate);
            }
            catch (ConflictingScheduledTimeException CSTE)
            {
                MessageBox.Show(CSTE.Message);
            }
            catch (NullReferenceException)
            {
                
            }
        }
        private void DisplayTaskInTextArea(DateTime date)
        {
            TaskListArea.Clear();
            DateLabel.Text = date.ToShortDateString();
            DayLabel.Text = date.DayOfWeek.ToString();
            try
            {
                using (Task task = new Task(date))
                {
                    if (task.Tasks.Count() == 0)
                    {
                        DisplayWhenNoTask();
                    }
                    else
                    {
                        DeleteTaskButton.Enabled = true;
                        int i = 0;
                        foreach (SingleTask EachTask in task.Tasks)
                        {
                            TaskListArea.AppendText(
                                ++i 
                                + EachTask.TimeCreated.ToString().Substring(0, 8).PadLeft(9 + 15) 
                                + EachTask.Task.PadLeft(EachTask.Task.Length + 25)
                                + "\n"
                                );
                        }
                    }
                }
            }
            catch(FileNotFoundException)
            {
               DisplayWhenNoTask();
            }
        }
        private void DeleteTaskButton_Click(object sender, EventArgs e)
        {
            try
            { 
                using (Task task = new Task(FormDate))
                {
                    DeleteForm DeleteTaskEvent = new DeleteForm();
                    DeleteTaskEvent.ShowDialog();
                    int index = int.Parse(DeleteTaskEvent.IndexofTask);
                    task.DeleteTask(index);
                    DisplayTaskInTextArea(FormDate);
                }   
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("No Taks To delete!!");
            }
            catch (Exception E)
            {
                MessageBox.Show("Index can be in range ["+1+"-"+E.Message+"]");
            }
        }
        private void DisplayWhenNoTask()
        {
            DeleteTaskButton.Enabled = false;
            TaskListArea.AppendText("Hurray we don't habe any task!!!!");
        }
        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FormDate = dateTimePicker.Value;
            if (FormDate.Date < DateTime.Now.Date)
            {
                AddTaskButton.Enabled = false;
                DeleteTaskButton.Enabled = false;
                Debug.WriteLine(DeleteTaskButton.Enabled);
            }
            else
            {
                AddTaskButton.Enabled = true;
                DeleteTaskButton.Enabled = true;
            }
            DisplayTaskInTextArea(FormDate);
        }
    }
}