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
    public partial class LessonShow : Form
    {
        Student student = new Student();
        
        public LessonShow(Student student)
        {
            InitializeComponent();
            this.student = student;
            
        }
        

        private void LessonShow_Load(object sender, EventArgs e)
        {
           var listView = student.lessons.Select(x => new
            {
                Lesson = x.lessonName,
                Grade1 = x.grades[0].grade,
                Grade2 = x.grades[1].grade,
                Grade3 = x.grades[2].grade
            }).ToList();

            dataGridView1.DataSource = listView;
        }
    }
}
