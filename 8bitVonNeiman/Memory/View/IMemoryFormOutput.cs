namespace _8bitVonNeiman.Memory.View {
    public interface IMemoryFormOutput {
        void FormClosed();
        void ClearMemoryClicked();
        void MemoryChanged(int row, int collumn, string s);
    }
}
