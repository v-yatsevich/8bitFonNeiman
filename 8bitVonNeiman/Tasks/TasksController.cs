using System.Windows.Forms;
using _8bitVonNeiman.Database;
using _8bitVonNeiman.Database.Models;
using _8bitVonNeiman.Tasks.View;

namespace _8bitVonNeiman.Tasks {
    public class TasksController: ITasksModuleInput, ITaskFormOutput {

        private TasksForm _form;
        private AddTaskForm _addForm;
        private IDatabaseManagerInput _db = DatabaseManager.Instance;

        /// Открывает форму, если она закрыта или закрывает, если открыта
        public void ChangeFormState() {
            if (_form == null) {
                _form = new TasksForm(this);
                _form.Show();

                ShowTasks();
            } else {
                _form.Close();
            }
        }

        /// <summary>
        /// Функция, вызывающаяся при закрытии формы. Необходима для корректной работы функции ChangeFormState()
        /// </summary>
        public void FormClosed() {
            _form = null;
        }

        public void AddFormClosed() {
            _addForm = null;
        }

        public void AddButtonClicked() {
            _addForm?.Close();
            _addForm = new AddTaskForm(this);
            _addForm.Show();
        }

        public void DeleteButtonClicked(TaskEntity task) {
            _db.DeleteTask(task);
            ShowTasks();
        }

        public void ChangeButtonClicked(TaskEntity task) {
            _addForm?.Close();
            _addForm = new AddTaskForm(this, task);
            _addForm.Show();
        }

        public void LookButtonClicked(TaskEntity task) {
            _addForm?.Close();
            _addForm = new AddTaskForm(this, task, false);
            _addForm.Show();
        }

        public void TaskSaveButtonClicked(TaskEntity task) {
            if (task.Name == "") {
                MessageBox.Show("Название не может быть пустым");
                return;
            }
            _db.SetTask(task);
            ShowTasks();
            _addForm?.Close();
        }

        private void ShowTasks() {
            _form.ShowTasks(_db.GetTasks());
        }
    }
}
