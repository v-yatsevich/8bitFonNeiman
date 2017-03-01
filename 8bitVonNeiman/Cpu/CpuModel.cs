using System.Collections;
using System.Collections.Generic;
using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.Cpu {
    public class CpuModel {

        /// Аккамулятор.
        private ExtendedBitArray _acc = new ExtendedBitArray();

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
        
        /// Массив регистров общего назначения
        private List<ExtendedBitArray> _registers = new List<ExtendedBitArray>();

        /// Регистр флагов
        private ExtendedBitArray _flags = new ExtendedBitArray();

        /// Регистр временного хранения данных для получения оных из памяти, регистров или устройств
        private ExtendedBitArray _rdb = new ExtendedBitArray();

        /// Регистр, хранящий адрес данных, к которым необходимо получить доступ
        private int _rab;

        private ICpuModelOutput _output;

        /// Аллиас регистра перенома
        private bool FC {
            get { return _flags[4]; }
            set { _flags[4] = value; }
        }

        public CpuModel(ICpuModelOutput output) {
            _output = output;
            Reset();
        }

        public void Reset() {
            _cs = _defaultCs;
            _ds = _defaultDs;
            _ss = _defaultSs;
            _pcl = Constants.StartAddress;
            _acc = new ExtendedBitArray();
            _spl = 0;
            _registers = new List<ExtendedBitArray> {
                new ExtendedBitArray(), new ExtendedBitArray(), new ExtendedBitArray(), new ExtendedBitArray(),
                new ExtendedBitArray(), new ExtendedBitArray(), new ExtendedBitArray(), new ExtendedBitArray()
            };
        }
        
        /// Возвращает значение памяти по выбранному адресу. Имплементирует работу с кэшем.
        /// <param name="address">Адрес, из которого получается память.</param>
        private ExtendedBitArray GetMemory(int address) {
            return _output.GetMemory(address);
        }
        
        /// Устанавливает значение памяти по выбранному адресу. Имплементирует работу с кэшем.
        /// <param name="data">Значение устанавливаемой памяти.</param>
        /// <param name="address">Адрес, по которому записывается память.</param>
        private void SetMemory(ExtendedBitArray data, int address) {
            _output.SetMemory(data, address);
        }

        private void _y1() {
            _rdb = GetMemory(_rab);
        }

        private void _y2() {
            _rdb = new ExtendedBitArray(_registers[_rab]);
        }

        private void _y3() {
            //Not implemented
        }

        private void _y4() {
            SetMemory(_rdb, _rab);
        }

        private void _y5() {
            _registers[_rab] = new ExtendedBitArray(_rdb);
        }

        private void _y6() {
            //Not implemented
        }

        private void _y8() {
            _acc = new ExtendedBitArray(_rdb);
        }

        private void _y9() {
            _acc = new ExtendedBitArray() {
                [0] = (_cs & 1) != 0,
                [1] = (_cs & 2) != 0,
                [2] = (_ds & 1) != 0,
                [3] = (_ds & 2) != 0,
                [4] = (_ss & 1) != 0,
                [5] = (_ss & 2) != 0
            };
        }

        private void _y10() {
            //Not implemented
        }

        private void _y11() {
            FC = _acc[Constants.WordSize - 1];
            _y13();
        }

        private void _y12() {
            FC = _acc[0];
            _y14();
        }

        private void _y13() {
            var temp = _acc[Constants.WordSize - 1];
            for (int i = Constants.WordSize - 1; i > 0; i--) {
                _acc[i] = _acc[i - 1];
            }
            _acc[0] = FC;
            FC = temp;
        }

        private void _y14() {
            var temp = _acc[0];
            for (int i = 0; i < Constants.WordSize - 1; i++) {
                _acc[i] = _acc[i + 1];
            }
            _acc[Constants.WordSize - 1] = FC;
            FC = temp;
        }

        private void _y15() {
            
        }
    }
}
