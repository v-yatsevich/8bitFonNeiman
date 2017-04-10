namespace _8bitVonNeiman.Compiler.View {
    public interface ICompilerFormOutput {
        void FormClosed(string code);
        void Compile(string code);

        void SaveButtonClicked(string code);
        void SaveAsButtonClicked(string code);
        void SaveDialogEnded(string path);
        void Load(string path);
    }
}
