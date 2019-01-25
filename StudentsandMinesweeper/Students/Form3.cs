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
    public partial class Form3 : Form
    {
        Form2 frm2;
        public Form3(Form2 sender)
        {
            InitializeComponent();
            this.frm2 = sender;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var listView = frm2.student.lessons.Select(x => new
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
