namespace _8bitVonNeiman.Debug {
    public class DebugController : IDebugModuleInput {
        private readonly IDebugModuleOutput _output;

        public DebugController(IDebugModuleOutput output) {
            _output = output;
        }
    }
}
