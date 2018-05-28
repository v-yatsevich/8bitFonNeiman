namespace _8bitVonNeiman.Debugger.View {
    public interface IDebugFormOutput {
        void FormClosed();
        void ToggleBreakpoint(int address);
        void DeleteAllBreakpoints();
    }
}
