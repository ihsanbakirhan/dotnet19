using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students
{
    public class Lesson
    {
        public int lessonId { get; set; }
        public string lessonName { get; set; }
        public List<Grade> grades { get; set; }

        public Lesson()
        {

        }
        Random rnd = new Random();
        public Lesson(int lessonId, string lessonName)
        {
            this.lessonId = lessonId;
            this.lessonName = lessonName;
            
            
        }
        public Lesson(int lessonId,string lessonName,int studentId,int examCount)
        {
            this.lessonId = lessonId;
            this.lessonName = lessonName;
            var grades = new List<Grade>();
            for(int i = 0; i < examCount; i++)
            {
                grades.Add(new Grade(studentId, lessonId, i, rnd.Next(0, 101)));
            }
            this.grades = grades;
        }

        public List<Lesson> GenerateLessons(int studentId,int examCount)
        {
            var lessons = new List<Lesson>();
            lessons.Add(new Lesson(1, "Math", studentId, examCount));
            lessons.Add(new Lesson(2, "Physics", studentId, examCount));
            lessons.Add(new Lesson(3, "Chemistry", studentId, examCount));
            lessons.Add(new Lesson(4, "Biology", studentId, examCount));
            lessons.Add(new Lesson(5, "Geometry", studentId, examCount));
            lessons.Add(new Lesson(6, "History", studentId, examCount));
            return lessons;
        }
    }
}
