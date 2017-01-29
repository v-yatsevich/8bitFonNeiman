using System.Collections;
using System.Collections.Generic;
using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.Cpu {
    public class CpuModel {

        /// Аккамулятор.
        private BitArray _acc = new BitArray(Constants.WordSize);

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
        private List<BitArray> _registers = new List<BitArray>();

        /// Регистр флагов
        private BitArray _flags = new BitArray(Constants.WordSize);

        /// Регистр временного хранения данных для получения оных из памяти, регистров или устройств
        private BitArray _rdb = new BitArray(Constants.WordSize);

        /// Регистр, хранящий адрес данных, к которым необходимо получить доступ
        private int _rab;

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
        
        /// Возвращает значение памяти по выбранному адресу. Имплементирует работу с кэшем.
        /// <param name="address">Адрес, из которого получается память.</param>
        private BitArray GetMemory(int address) {
            return _output.GetMemory(address);
        }
        
        /// Устанавливает значение памяти по выбранному адресу. Имплементирует работу с кэшем.
        /// <param name="data">Значение устанавливаемой памяти.</param>
        /// <param name="address">Адрес, по которому записывается память.</param>
        private void SetMemory(BitArray data, int address) {
            _output.SetMemory(data, address);
        }

        //Возвращает значение флага переноса
        private bool _fc() {
            return _flags[4];
        }

        private void _y1() {
            _rdb = GetMemory(_rab);
        }

        private void _y2() {
            _rdb = new BitArray(_registers[_rab]);
        }

        private void _y3() {
            //Not implemented
        }

        private void _y4() {
            SetMemory(_rdb, _rab);
        }

        private void _y5() {
            _registers[_rab] = new BitArray(_rdb);
        }

        private void _y6() {
            //Not implemented
        }

        private void _y8() {
            _acc = new BitArray(_rdb);
        }

        private void _y9() {
            _acc = new BitArray(Constants.WordSize) {
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
            bool temp = _acc[_acc.Count - 1];
            for (int i = _acc.Count - 1; i > 0; i--) {
                _acc[i] = _acc[i - 1];
            }
            _acc[0] = temp;
            //TODO: узнать про FC
        }

        private void _y12() {
            bool temp = _acc[0];
            for (int i = 0; i < _acc.Count - 1; i++) {
                _acc[i] = _acc[i + 1];
            }
            _acc[_acc.Count - 1] = temp;
            //TODO: узнать про FC
        }

        private void _y13() {
            for (int i = _acc.Count - 1; i > 0; i--) {
                _acc[i] = _acc[i - 1];
            }
            _acc[0] = _fc();
            //TODO: узнать про FC
        }

        private void _y14() {
            for (int i = 0; i < _acc.Count - 1; i++) {
                _acc[i] = _acc[i + 1];
            }
            _acc[_acc.Count - 1] = _fc();
        }
    }
}
