using System.Collections.Generic;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Database {
    public interface IDatabaseManagerInput {
        /// Возвращает LoginEntity в соответствии с именем пользовател и паролем. 
        /// Если пользователя с такими данными нет в базе данных, будет вернут null
        LoginEntity GetUser(string login, string password);

        /// Возвращает список студентов
        List<StudentEntity> GetStudents();

        /// Возвращает список заданий
        List<TaskEntity> GetTasks();

        /// Добавляет или перезаписывает студента
        bool SetStudent(StudentEntity student);

        /// Удаляет студета
        void DeleteStudent(StudentEntity student);

        /// Добавляет или перезаписывает задание
        void SetTask(TaskEntity task);

        /// Удаляет щадание
        void DeleteTask(TaskEntity task);
    }
}
