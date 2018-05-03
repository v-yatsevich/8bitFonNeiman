using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Debugger {
    class DebuggerModel: IDebuggerModelInput {
        private IDebuggerModelOutput _output;

        public DebuggerModel(IDebuggerModelOutput output) {
            _output = output;
        }
    }
}
