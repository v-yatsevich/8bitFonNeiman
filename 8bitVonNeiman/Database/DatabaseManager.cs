using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Database {
    public class DatabaseManager: IDatabaseManagerInput {
        private static DatabaseManager _instance;

        public static IDatabaseManagerInput Instance => _instance ?? (_instance = new DatabaseManager());

        private readonly SQLiteConnection _connection;
        private readonly SQLiteCommand _command;
        private byte[] salt = { 20, 214, 235, 141, 250, 249, 57, 96, 75, 115, 83, 16, 106, 245, 156, 119 };

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
            DataTable dTable = new DataTable();
            String sqlQuery;

            if (_connection.State != ConnectionState.Open) {
                return null;
            }

            try {
                var pass = GeneratePassword(password);
                sqlQuery =
                    $"SELECT 'user'.id, 'user'.'role', student.id as student_id, student.user_id, student.name, student.student_group FROM User as 'user' LEFT JOIN Student as student ON 'user'.id = student.user_id WHERE ('user'.login = '{login}' and 'user'.password = '{pass}')";
                var adapter = new SQLiteDataAdapter(sqlQuery, _connection);
                adapter.Fill(dTable);

                if (dTable.Rows.Count == 0) {
                    return null;
                }
                var id = Convert.ToInt32(dTable.Rows[0].ItemArray[0]);
                var role = Convert.ToInt32(dTable.Rows[0].ItemArray[1]);
                StudentEntity student = null;
                if (!(dTable.Rows[0].ItemArray[2] is DBNull) && !(dTable.Rows[0].ItemArray[3] is DBNull)) {
                    var studentId = Convert.ToInt32(dTable.Rows[0].ItemArray[2]);
                    var userId = Convert.ToInt32(dTable.Rows[0].ItemArray[3]);
                    var name = (string) dTable.Rows[0].ItemArray[4];
                    var desc = (string) dTable.Rows[0].ItemArray[5];
                    student = new StudentEntity(studentId, userId, name, desc);
                }
                return new LoginEntity(id, role == 1, student);

                //for (int i = 0; i < dTable.Rows.Count; i++)
                //dgvViewer.Rows.Add(dTable.Rows[i].ItemArray);
            } catch (SQLiteException ex) {
                return null;
            }
        }

        private void set() {
            try {
                /*_command.CommandText = "INSERT INTO Catalog ('author', 'book') values ('" +
                                        addData.Author + "' , '" +
                                        addData.Book + "')";

                _command.ExecuteNonQuery();*/
            } catch (SQLiteException ex) {
                
            }
        }

        private string GeneratePassword(string password) {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
