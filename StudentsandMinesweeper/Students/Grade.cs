using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students
{
    public class Grade
    {
        public int studentId { get; set; }
        public int lessonId { get; set; }
        public int examNo { get; set; }
        public int grade { get; set; }
        
        public Grade()
        {

        }
        public Grade(int studentId, int lessonId, int examNo, int grade)
        {
            this.studentId = studentId;
            this.lessonId = lessonId;
            this.examNo = examNo;
            this.grade = grade;
        }

        public List<Grade> GenerateGrades(int studentCount,int examCount)
        {
            Random rnd = new Random();
            var grades = new List<Grade>();
            var lesson = new Lesson();
            var lessons = lesson.GenerateLesson();
            for (int i = 0; i < studentCount; i++)
            {
                for (int j = 0; j < lessons.Count; j++)
                {
                    for (int k = 0; k < examCount; k++)
                    {
                        grades.Add(new Grade(i, j, k, rnd.Next(0, 100)));
                    }
                }
            }
            return grades;
        }
    }
}
