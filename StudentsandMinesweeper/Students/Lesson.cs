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
        Random rnd = new Random();
        public Lesson()
        {

        }
        public Lesson(int lessonId, string lessonName)
        {
            this.lessonId = lessonId;
            this.lessonName = lessonName;        
           
        }
        public Lesson(int lessonId, string lessonName,int studentId,int examCount,Random rnd)
        {
            this.lessonId = lessonId;
            this.lessonName = lessonName;
            var grades = new List<Grade>();
            for (int i = 0; i < examCount; i++)
            {
                grades.Add(new Grade(studentId, lessonId, i, rnd.Next(0, 101)));
            }
            this.grades = grades;
        }
        public List<Lesson> GenerateLesson(int studentId, int examCount,Random rnd)
        {
            var lessons =new  List<Lesson>();
            lessons.Add(new Lesson(1, "Maths",studentId,examCount,rnd));
            lessons.Add(new Lesson(2, "Physics", studentId, examCount, rnd));
            lessons.Add(new Lesson(3, "Chemistry", studentId, examCount, rnd));
            lessons.Add(new Lesson(4, "Biology", studentId, examCount, rnd));
            lessons.Add(new Lesson(5, "Geometry", studentId, examCount, rnd));
            lessons.Add(new Lesson(6, "History", studentId, examCount, rnd));
            return lessons;
        }
        public List<Lesson> GenerateLesson()
        {
            var lessons = new List<Lesson>();
            lessons.Add(new Lesson(1, "Maths"));
            lessons.Add(new Lesson(2, "Physics"));
            lessons.Add(new Lesson(3, "Chemistry"));
            lessons.Add(new Lesson(4, "Biology"));
            lessons.Add(new Lesson(5, "Geometry"));
            lessons.Add(new Lesson(6, "History"));
            return lessons;
        }
    }
}
