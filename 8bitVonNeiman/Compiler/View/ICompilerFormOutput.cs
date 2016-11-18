namespace _8bitVonNeiman.Compiler.View {
    public interface ICompilerFormOutput {
        void FormClosed(string code);
        void Compile(string code);
    }
}
