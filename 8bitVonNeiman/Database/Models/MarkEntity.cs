using System;

namespace _8bitVonNeiman.Database.Models {
    public class MarkEntity {
        public int Id;
        public DateTime Date;
        public string Description;
        public TaskEntity Task;
        public StudentEntity Student;

        public MarkEntity(int id, DateTime date, string description, TaskEntity task, StudentEntity student) {
            Id = id;
            Date = date;
            Description = description;
            Task = task;
            Student = student;
        }

        public MarkEntity() {
            Id = -1;
            Date = DateTime.Now;
        }
    }
}
