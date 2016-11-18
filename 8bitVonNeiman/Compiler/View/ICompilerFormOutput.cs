namespace _8bitVonNeiman.Compiler.View {
    public interface ICompilerFormOutput {
        void FormClosed(string code);
        void Compile(string code);

        void Save(string code, string path);
        void Load(string path);
    }
}
