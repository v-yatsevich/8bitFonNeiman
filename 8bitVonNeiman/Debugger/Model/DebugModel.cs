namespace _8bitVonNeiman.Debug.Model {
    public class DebugModel: IDebugModelInput {
        private readonly IDebugModelOutput _output;

        public DebugModel(IDebugModelOutput output) {
            _output = output;
        }
    }
}
