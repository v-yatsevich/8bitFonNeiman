using System.Collections;
using System.Collections.Generic;
using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.Cpu {
    public class CpuModel {

        /// Аккамулятор.
        private BitArray _acc;

        /// Счетчик команд.
        private int _pcl;

        /// Указатель стека.
        private int _spl;

        /// Номер сегмента кода.
        private int _cs;

        /// Значение сегмента кода, которое будет установлено после сброса.
        private int _defaultCs = 0;

        /// Номер сегмента данных.
        private int _ds;

        /// Значение сегмента данных, которое будет установлено после сброса.
        private int _defaultDs = 2;

        /// Номер сегмента стека.
        private int _ss;

        /// Значение сегмента стека, которое будет установлено после сброса.
        private int _defaultSs = 3;

        private List<BitArray> _registers = new List<BitArray>();

        private ICpuModelOutput _output;

        public CpuModel(ICpuModelOutput output) {
            _output = output;
            Reset();
        }

        public void Reset() {
            _cs = _defaultCs;
            _ds = _defaultDs;
            _ss = _defaultSs;
            _pcl = Constants.StartAddress;
            _acc = new BitArray(8);
            _spl = 0;
            _registers = new List<BitArray> {
                new BitArray(8), new BitArray(8), new BitArray(8), new BitArray(8),
                new BitArray(8), new BitArray(8), new BitArray(8), new BitArray(8)
            };
        }
    }
}
