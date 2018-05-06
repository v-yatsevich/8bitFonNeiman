using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Tasks.View {
    public interface ITaskFormOutput {
        void FormClosed();
        void AddFormClosed();
        void AddButtonClicked();
        void DeleteButtonClicked(TaskEntity task);
        void ChangeButtonClicked(TaskEntity task);
        void LookButtonClicked(TaskEntity task);
        void TaskSaveButtonClicked(TaskEntity task);
    }
}
