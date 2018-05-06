using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Database {
    public class DatabaseManager: IDatabaseManagerInput {
        private static DatabaseManager _instance;

        public static IDatabaseManagerInput Instance => _instance ?? (_instance = new DatabaseManager());

        private readonly SQLiteConnection _connection;
        private readonly SQLiteCommand _command;
        private byte[] _salt = { 20, 214, 235, 141, 250, 249, 57, 96, 75, 115, 83, 16, 106, 245, 156, 119 };

        private DatabaseManager() {
            _connection = new SQLiteConnection();
            _command = new SQLiteCommand();

            try {
                _connection = new SQLiteConnection("Data Source=database.db;Version=3;");
                _connection.Open();
                _command.Connection = _connection;
            } catch (SQLiteException ex) {
                // ignored
            }
        }

        /// Возвращает LoginEntity в соответствии с именем пользовател и паролем. 
        /// Если пользователя с такими данными нет в базе данных, будет вернут null
        public LoginEntity GetUser(string login, string password) {
            var dTable = new DataTable();

            if (_connection.State != ConnectionState.Open) {
                return null;
            }

            try {
                var pass = GeneratePassword(password);
                var sqlQuery = $"SELECT 'user'.id, 'user'.'role', student.id as student_id, student.user_id, student.name, student.student_group FROM User as 'user' LEFT JOIN Student as student ON 'user'.id = student.user_id WHERE ('user'.login = '{login}' and 'user'.password = '{pass}')";
                var adapter = new SQLiteDataAdapter(sqlQuery, _connection);
                adapter.Fill(dTable);

                if (dTable.Rows.Count == 0) {
                    return null;
                }
                var id = Convert.ToInt32(dTable.Rows[0].ItemArray[0]);
                int role = dTable.Rows[0].ItemArray[1] is DBNull ? 0 : Convert.ToInt32(dTable.Rows[0].ItemArray[1]);
                StudentEntity student = null;
                if (!(dTable.Rows[0].ItemArray[2] is DBNull) && !(dTable.Rows[0].ItemArray[3] is DBNull)) {
                    var studentId = Convert.ToInt32(dTable.Rows[0].ItemArray[2]);
                    var userId = Convert.ToInt32(dTable.Rows[0].ItemArray[3]);
                    var name = (string) dTable.Rows[0].ItemArray[4];
                    var desc = (string) dTable.Rows[0].ItemArray[5];
                    var user = new UserEntity(userId, login, null, -1);
                    student = new StudentEntity(studentId, user, name, desc);
                }
                return new LoginEntity(id, role == 1, student);
            } catch (SQLiteException ex) {
                return null;
            }
        }

        /// Возвращает список студентов
        public List<StudentEntity> GetStudents() {
            var dTable = new DataTable();

            if (_connection.State != ConnectionState.Open) {
                return new List<StudentEntity>();
            }

            try {
                var sqlQuery = "SELECT 'user'.id, 'user'.login, student.id as student_id, student.name, student.student_group FROM Student as student INNER JOIN User as 'user' ON 'user'.id = student.user_id";
                var adapter = new SQLiteDataAdapter(sqlQuery, _connection);
                adapter.Fill(dTable);

                var students = new List<StudentEntity>();
                for (int i = 0; i < dTable.Rows.Count; i++) {
                    var userId = Convert.ToInt32(dTable.Rows[i].ItemArray[0]);
                    var userLogin = (string) dTable.Rows[i].ItemArray[1];
                    var studentId = Convert.ToInt32(dTable.Rows[i].ItemArray[2]);
                    var name = (string)dTable.Rows[i].ItemArray[3];
                    var group = (string)dTable.Rows[i].ItemArray[4];
                    var user = new UserEntity(userId, userLogin, null, -1);
                    students.Add(new StudentEntity(studentId, user, name, group));
                }

                return students;
            } catch (SQLiteException ex) {
                return new List<StudentEntity>();
            }
        }

        /// Добавляет или перезаписывает студента. Возвращает true при успешном добавлении.
        public bool SetStudent(StudentEntity student) {
            try {
                var password = GeneratePassword(student.User.Password);
                if (student.User.Id == -1) {
                    _command.CommandText =
                        $"INSERT INTO User ('id', 'login', 'password', 'role') values (NULL, '{student.User.Login}', '{password}', NULL);" +
                        "SELECT last_insert_rowid();";
                    var id = _command.ExecuteScalar();
                    _command.CommandText =
                        $"INSERT INTO Student ('id', 'user_id', 'name', 'student_group') values (NULL, {id}, '{student.Name}', '{student.Group}');";
                } else {
                    var passwordString = password == null ? "" : $"'password' = '{password}', ";
                    _command.CommandText =
                        $"UPDATE User SET 'login' = '{student.User.Login}', {passwordString}'role' = {student.User.Role} WHERE User.'id' = {student.User.Id};" +
                        $"UPDATE Student SET 'name' = '{student.Name}', 'student_group' = '{student.Group}' WHERE Student.'id' = {student.Id};";
                }
                _command.ExecuteNonQuery();
            } catch (SQLiteException ex) {
                return false;
            }
            return true;
        }

        /// Удаляет студета
        public void DeleteStudent(StudentEntity student) {
            try {
                _command.CommandText =
                    $"DELETE FROM Student WHERE Student.'id' = {student.Id};" +
                    $"DELETE FROM User WHERE User.'id' = {student.User.Id};";
                _command.ExecuteNonQuery();
            } catch (SQLiteException ex) {
                
            }
        }

        /// Возвращает список заданий
        public List<TaskEntity> GetTasks() {
            var dTable = new DataTable();

            if (_connection.State != ConnectionState.Open) {
                return new List<TaskEntity>();
            }

            try {
                var sqlQuery = "SELECT * FROM Task";
                var adapter = new SQLiteDataAdapter(sqlQuery, _connection);
                adapter.Fill(dTable);

                var tasks = new List<TaskEntity>();
                for (int i = 0; i < dTable.Rows.Count; i++) {
                    var id = Convert.ToInt32(dTable.Rows[i].ItemArray[0]);
                    var name = (string)dTable.Rows[i].ItemArray[1];
                    var description = (string)dTable.Rows[i].ItemArray[2];

                    tasks.Add(new TaskEntity(id, name, description));
                }

                return tasks;
            } catch (SQLiteException ex) {
                return new List<TaskEntity>();
            }
        }

        /// Добавляет или перезаписывает задание
        public void SetTask(TaskEntity task) {
            try {
                var id = task.Id == -1 ? "NULL" : task.Id.ToString();
                _command.CommandText = $"INSERT OR REPLACE INTO Task ('id', 'name', 'description') values ({id}, '{task.Name}', '{task.Description}');";
                _command.ExecuteNonQuery();
            } catch (SQLiteException ex) {
                // Ignore
            }
        }

        public void DeleteTask(TaskEntity task) {
            try {
                _command.CommandText = $"DELETE FROM Task WHERE Task.'id' = {task.Id};";
                _command.ExecuteNonQuery();
            } catch (SQLiteException ex) {

            }
        }

        private string GeneratePassword(string password) {
            if (password == null) {
                return null;
            }
            var pbkdf2 = new Rfc2898DeriveBytes(password, _salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(_salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
