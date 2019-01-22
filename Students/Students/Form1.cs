using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Students
{
    public partial class Form1 : Form
    {
        List<Student> students = new List<Student>();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            var form2 = new Form2(this);
            form2.Show();
        }

        private void generateStudents()
        {
            var student = new Student();
            for (int i = 1; i <= 100; i++)
            {
                students.Add(student.generateStudent(i));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(800, 600);

            generateStudents();
            dataGridView1.DataSource = students;
            dataGridView1.Columns[0].HeaderText = "Student ID";
            dataGridView1.Columns[1].HeaderText = "Name";
            dataGridView1.Columns[2].HeaderText = "Surname";
            dataGridView1.Columns[3].HeaderText = "Phone";
            dataGridView1.Columns[4].HeaderText = "Email";
            dataGridView1.Columns[5].HeaderText = "State";
            dataGridView1.Columns[6].HeaderText = "Grade";
            fillStateCombobox();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var studentId = (int)dataGridView1.CurrentRow.Cells[0].Value;
            var student = (Student)students.Find(x => x.studentId == studentId);
            var form2 = new Form2(this, student);
            form2.Show();
        }

        public void insertStudent(Student student)
        {
            student.studentId = students.Count+1;
            students.Add(student);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
        }
        public void deleteStudent(Student student)
        {
            students.Remove(student);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                dataGridView1.DataSource = students;
            }
            else
            {
                var searchedStudents = students.FindAll(x => (x.name.ToLower().Contains(textBox1.Text.ToLower()) || x.surname.ToLower().Contains(textBox1.Text.ToLower())));
                dataGridView1.DataSource = searchedStudents;
            }
        }
        public List<string> seherler = new List<string>();
        private void fillStateCombobox()
        {

            foreach (var sehir in students)
            {
                if (!seherler.Contains(sehir.state))
                {
                    seherler.Add(sehir.state);
                }
            }
            seherler.Sort();
            comboBox1.Items.Add("ALL");
            foreach (var item in seherler)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "ALL")

            {
                dataGridView1.DataSource = students;
            }
            else
            {
                var sehirler = students.FindAll(x => (x.state.ToLower().Contains(comboBox1.Text.ToLower())));
                dataGridView1.DataSource = sehirler;
            }
        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("naber naptın");
        }
        bool normalsirala = true;
        private void dataGridView1_ColumnHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    if (sortedColumnIndex == e.ColumnIndex)
                    {
                        dataGridView1.DataSource = students.OrderByDescending(x => x.studentId).ToList();
                        sortedColumnIndex = -1;
                    }
                    else
                    {
                        dataGridView1.DataSource = students.OrderBy(x => x.studentId).ToList();
                        sortedColumnIndex = e.ColumnIndex;
                    }
                    break;
                case 1:
                    if (sortedColumnIndex == e.ColumnIndex)
                    {
                        dataGridView1.DataSource = students.OrderByDescending(x => x.name).ToList();
                        sortedColumnIndex = -1;
                    }
                    else
                    {
                        dataGridView1.DataSource = students.OrderBy(x => x.name).ToList();
                        sortedColumnIndex = e.ColumnIndex;
                    }
                    break;
                case 2:
                    if (sortedColumnIndex == e.ColumnIndex)
                    {
                        dataGridView1.DataSource = students.OrderByDescending(x => x.surname).ToList();
                        sortedColumnIndex = -1;
                    }
                    else
                    {
                        dataGridView1.DataSource = students.OrderBy(x => x.surname).ToList();
                        sortedColumnIndex = e.ColumnIndex;
                    }
                    break;
                case 3:
                    if (sortedColumnIndex == e.ColumnIndex)
                    {
                        dataGridView1.DataSource = students.OrderByDescending(x => x.phone).ToList();
                        sortedColumnIndex = -1;
                    }
                    else
                    {
                        dataGridView1.DataSource = students.OrderBy(x => x.phone).ToList();
                        sortedColumnIndex = e.ColumnIndex;
                    }
                    break;
                case 4:
                    if (sortedColumnIndex == e.ColumnIndex)
                    {
                        dataGridView1.DataSource = students.OrderByDescending(x => x.email).ToList();
                        sortedColumnIndex = -1;
                    }
                    else
                    {
                        dataGridView1.DataSource = students.OrderBy(x => x.email).ToList();
                        sortedColumnIndex = e.ColumnIndex;
                    }
                    break;
                case 5:
                    if (sortedColumnIndex == e.ColumnIndex)
                    {
                        dataGridView1.DataSource = students.OrderByDescending(x => x.state).ToList();
                        sortedColumnIndex = -1;
                    }
                    else
                    {
                        dataGridView1.DataSource = students.OrderBy(x => x.state).ToList();
                        sortedColumnIndex = e.ColumnIndex;
                    }
                    break;
                case 6:
                    if (sortedColumnIndex == e.ColumnIndex)
                    {
                        dataGridView1.DataSource = students.OrderByDescending(x => x.grade).ToList();
                        sortedColumnIndex = -1;
                    }
                    else
                    {
                        dataGridView1.DataSource = students.OrderBy(x => x.grade).ToList();
                        sortedColumnIndex = e.ColumnIndex;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
