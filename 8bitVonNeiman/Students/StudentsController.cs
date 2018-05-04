using System.Collections.Generic;
using System.Windows.Forms;
using _8bitVonNeiman.Database;
using _8bitVonNeiman.Database.Models;
using _8bitVonNeiman.Students.View;

namespace _8bitVonNeiman.Students {
    class StudentsController: IStudentsFormOutput, IStudentsModuleInput {

        private StudentsForm _form;
        private AddStudentForm _addingForm;
        private readonly IDatabaseManagerInput _db = DatabaseManager.Instance;

        /// Открывает форму, если она закрыта или закрывает, если открыта
        public void ChangeFormState() {
            if (_form == null) {
                _form = new StudentsForm(this);
                _form.Show();

                ShowStudents();
            } else {
                _form.Close();
                _addingForm?.Close();
            }
        }

        public void FormClosed() {
            _form = null;
        }

        public void AddingFormClosed() {
            _addingForm = null;
        }

        public void AddingFormButtonDidTap() {
            _addingForm?.Close();
            _addingForm = new AddStudentForm(this);
            _addingForm.Show();
        }

        public void AddingFromSaveDidTap(StudentEntity student) {
            if (student.Id == -1 && student.User.Password == null || student.User.Password == "") {
                MessageBox.Show("Пароль не может быть пустым");
                return;
            }
            if (student.User.Login == "") {
                MessageBox.Show("Логин не может быть пустым");
                return;
            }
            if (student.Name == "") {
                MessageBox.Show("Имя не может быть пустым");
                return;
            }
            if (student.Group == "") {
                MessageBox.Show("Группа не может быть пустой");
                return;
            }
            if (!_db.SetStudent(student)) {
                MessageBox.Show("Такой логин уже существует");
                return;
            }
            if (student.Id == -1) {
                MessageBox.Show("Студент успешно добавлен");
            } else {
                MessageBox.Show("Студент успешно изменен");
            }
            
            _addingForm.Close();
            _addingForm = null;

            ShowStudents();
        }

        public void ChangeDidTap(StudentEntity student) {
            _addingForm?.Close();
            _addingForm = new AddStudentForm(this, student);
            _addingForm.Show();
        }

        public void DeleteDidTap(StudentEntity student) {
            _db.DeleteStudent(student);
            ShowStudents();
        }

        private void ShowStudents() {
            var students = _db.GetStudents();
            _form.ShowStudents(students);
        }
    }
}
