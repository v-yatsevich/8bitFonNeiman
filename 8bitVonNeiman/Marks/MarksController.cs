using System.Collections.Generic;
using System.Linq;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Database;
using _8bitVonNeiman.Database.Models;
using _8bitVonNeiman.Marks.View;

namespace _8bitVonNeiman.Marks {
    public class MarksController: IMarksModuleInput, IMarkFormOutput {
        private MarksForm _form;
        private AddMarkForm _addingForm;
        private readonly IDatabaseManagerInput _db = DatabaseManager.Instance;

        private string _group;
        private int _studentId;

        private List<StudentEntity> _students;
        private List<TaskEntity> _tasks;

        private List<StudentEntity> _selectableStudents;

        /// Открывает форму, если она закрыта или закрывает, если открыта
        public void ChangeFormState() {
            if (_form == null) {
                _form = new MarksForm(this, SharedDataManager.Instance.IsAdmin);
                _form.Show();

                _studentId = SharedDataManager.Instance.StudentId;
                _group = null;
                
                _students = _db.GetStudents();
                _tasks = _db.GetTasks();

                _selectableStudents = new List<StudentEntity>(_students);
                _selectableStudents.Insert(0, new StudentEntity());
                _selectableStudents[0].Group = "";
                var groups = _selectableStudents.Select(x => x.Group).Distinct().ToList();
                _form.SetComboBoxes(_selectableStudents, groups);
                ShowMarks();
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

        public void AddButtonClicked() {
            _addingForm?.Close();
            _addingForm = new AddMarkForm(this, _students, _tasks);
            _addingForm.Show();
        }

        public void ChangeButtonClicked(MarkEntity mark) {
            _addingForm?.Close();
            _addingForm = new AddMarkForm(this, _students, _tasks, mark);
            _addingForm.Show();
        }

        public void SaveMark(MarkEntity mark) {
            _db.SetMark(mark);
            ShowMarks();
            _addingForm.Close();
        }

        public void GroupValueChanged(int newIndex) {
            if (newIndex == -1) {
                _group = "";
            } else {
                _group = _selectableStudents[newIndex].Group;
            }

            ShowMarks();
        }

        public void StudentValueChanged(int newIndex) {
            if (newIndex == -1) {
                _studentId = -1;
            } else {
                _studentId = _selectableStudents[newIndex].Id;
            }

            ShowMarks();
        }

        public void DeleteButtonClicked(MarkEntity mark) {
            _db.DeleteMark(mark);
            ShowMarks();
        }

        private void ShowMarks() {
            _form.ShowMarks(_db.GetMarks(_group, _studentId));
        }
    }
}
