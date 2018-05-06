using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Marks.View {
    public interface IMarkFormOutput {
        void FormClosed();
        void AddingFormClosed();
        void AddButtonClicked();
        void ChangeButtonClicked(MarkEntity mark);
        void DeleteButtonClicked(MarkEntity mark);
        void SaveMark(MarkEntity mark);
        void GroupValueChanged(int newIndex);
        void StudentValueChanged(int newIndex);
    }
}
