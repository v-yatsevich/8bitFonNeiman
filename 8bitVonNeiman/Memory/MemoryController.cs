using System.Collections;
using System.Collections.Generic;
using _8bitVonNeiman.Memory.View;

namespace _8bitVonNeiman.Memory {
    public class MemoryController : IMemoryControllerInput, IMemoryFormOutput {

        private MemoryForm _form;
        private Dictionary<int, BitArray> _memory;

        public MemoryController() {
            _memory = new Dictionary<int, BitArray>();
        }

        public void SetMemory(Dictionary<int, BitArray> memory) {
            _memory = memory;
            if (_form != null) {
                _form.ShowMemory(memory);
            }
        }

        /// <summary>
        /// Функция, показывающая форму, если она закрыта, и закрывающая ее, если она открыта
        /// </summary>
        public void ChangeState() {
            if (_form == null) {
                _form = new MemoryForm(this);
                _form.Show();
                _form.ShowMemory(_memory);
            } else {
                _form.Close();
            }
        }

        /// <summary>
        /// Функция, вызывающаяся при закрытии формы. Необходима для корректной работы функции ChangeState()
        /// </summary>
        public void FormClosed() {
            _form = null;
        }

        public void ClearMemoryClicked() {
            _memory = new Dictionary<int, BitArray>();
            _form.ShowMemory(_memory);
        }
    }
}
