using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Students.View {
    public interface IStudentsFormOutput {
        void FormClosed();
        void AddingFormClosed();
        void AddingFormButtonDidTap();
        void AddingFromSaveDidTap(StudentEntity student);
        void ChangeDidTap(StudentEntity student);
        void DeleteDidTap(StudentEntity student);
    }
}
