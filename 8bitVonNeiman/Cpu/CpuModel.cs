﻿using System.Collections.Generic;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Cpu.View;

namespace _8bitVonNeiman.Cpu {
    public class CpuModel: ICpuModelInput, ICpuFormOutput {

        public delegate ICpuFormInput GetView();

        private GetView _viewDelegate;

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
        private ExtendedBitArray _psw = new ExtendedBitArray();

        /// Регистр временного хранения данных для получения оных из памяти, регистров или устройств
        private ExtendedBitArray _rdb = new ExtendedBitArray();

        /// Регистр, хранящий адрес данных, к которым необходимо получить доступ
        private int _rab;

        /// ???
        private ExtendedBitArray _dr = new ExtendedBitArray();

        /// ???
        private ExtendedBitArray[] _cr = { new ExtendedBitArray(), new ExtendedBitArray() };

        private ICpuModelOutput _output;
        //TODO: Сделать через интерфейс
        private CpuForm _view;

        /// Аллиас регистра перенома
        // ReSharper disable once InconsistentNaming
        private bool FC {
            get { return _psw[4]; }
            set { _psw[4] = value; }
        }

        public CpuModel(ICpuModelOutput output, GetView viewDelegate) {
            _viewDelegate = viewDelegate;
            _output = output;
            Reset();
        }

        public void ResetButtonTapped() {
            Reset();
            _view?.ShowState(MakeState());
        }

        public void FormClosed() {
            _view = null;
        }

        public void Tick() {
            _y43();
            _y1();
            _y26();
            _y31();
            _y43();
            _y1();
            _y27();
            _y31();
            RunCommand();
            _view?.ShowState(MakeState());
        }

        public void ChangeFormState() {
            if (_view == null) {
                _view = new CpuForm();
                _view.Output = this;
                _view.Show();
                _view?.ShowState(MakeState());
            } else {
                _view.Close();
                _view = null;
            }
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
                new ExtendedBitArray("00110011"), new ExtendedBitArray(), new ExtendedBitArray(), new ExtendedBitArray()
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

        private CpuState MakeState() {
            return new CpuState(_acc, _dr, _psw, _ss, _ds, _cs, _pcl, _spl, _cr, _registers);
        }

        private void RunCommand() {
            var highBin = _cr[1].ToBinString();
            var highHex = _cr[1].ToHexString();
            var lowBin = _cr[0].ToBinString();
            var lowHex = _cr[0].ToHexString();
            //Регистровые
            if (highHex[0] == '5' || highHex == "F0" || highHex == "F1") { 
                ProcessRegisterCommand(highBin, highHex, lowBin, lowHex);
            }

            //ОЗУ
            if (highBin.StartsWith("011")) {
                ProcessRamCommand(highBin, highHex, lowBin, lowHex);
            }
        }

        private void ProcessRegisterCommand(string highBin, string highHex, string lowBin, string lowHex) {
            //MOV
            if (highHex[1] == 'F') {
                _y46();
                _y2();
                _y47();
                _y5();
                return;
            }
            //POP
            if (highHex[1] == 'D') {
                _y45();
                _y2();
                _y34();
                _y47();
                _y5();
                //TODO: Установить флаги
                ModifyRegister(lowHex);
                return;
            }
            //WP
            if (highHex[1] == 'A') {
                _y49();
                _y47();
                _y5();
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }

            LoadRegister(lowHex);
            //NOT
            if (highHex[1] == '0') {
                _y52();
                UnloadRegister(lowHex);
                ModifyRegister(lowHex);
                return;
            }
            //ADD
            if (highHex[1] == '1') {
                bool overflow = _acc.Add(_rdb);
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //SUB
            if (highHex[1] == '2') {
                bool overflow = _acc.Sub(_rdb);
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //MUL
            if (highHex[1] == '3') {
                bool overflow = _acc.Mul(_rdb);
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //DIV
            if (highHex[1] == '4') {
                _acc.Div(_rdb);
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //AND
            if (highHex[1] == '5') {
                _acc.And(_rdb);
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //OR
            if (highHex[1] == '6') {
                _acc.Or(_rdb);
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //XOR
            if (highHex[1] == '7') {
                _acc.Xor(_rdb);
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //CMP
            if (highHex[1] == '8') {
                var temp = new ExtendedBitArray(_rdb);
                bool overflow = temp.Sub(_acc);
                //TODO: установить регистры
                UnloadRegister(lowHex);
                ModifyRegister(lowHex);
                return;
            }
            //RD
            if (highHex[1] == '9') {
                _acc = new ExtendedBitArray(_rdb);
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //INC
            if (highHex[1] == 'B') {
                _rdb.Inc();
                //TODO: Установить флаги
                UnloadRegister(lowHex);
                ModifyRegister(lowHex);
                return;
            }
            //DEC
            if (highHex[1] == 'C') {
                _rdb.Dec();
                //TODO: Установить флаги
                UnloadRegister(lowHex);
                ModifyRegister(lowHex);
                return;
            }
            //PUSH
            if (highHex[1] == 'E') {
                _y35();
                _y45();
                _y4();
                //TODO: Установить флаги
                ModifyRegister(lowHex);
                return;
            }
            //ADC
            if (highHex == "F0") {
                var overflow = _acc.Add(_rdb);
                if (FC) {
                    overflow |= _acc.Inc();
                }
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
            //SUBB
            if (highHex == "F1") {
                var overflow = _acc.Sub(_rdb);
                if (FC) {
                    overflow |= _acc.Dec();
                }
                //TODO: установить регистры
                ModifyRegister(lowHex);
                return;
            }
        }

        private void ProcessRamCommand(string highBin, string highHex, string lowBin, string lowHex) {
            //WR
            if (highHex[1] == 'A') {
                _y44();
                _y49();
                _y4();
                //TODO: установить флаги
                return;
            }

            if (highBin[4] == '0') {
                // Константы
                _y61();
            } else {
                //прямая
                _y44();
                _y1();
            }

            //NOT
            if (highHex[1] == '0') {
                _y52();
                _y4();
                //TODO: установить флаги
                return;
            }
            //ADD
            if (highHex[1] == '1') {
                var overflow = _acc.Add(_rdb);
                //TODO: установить флаги
                return;
            }
            //SUB
            if (highHex[1] == '2') {
                var overflow = _acc.Sub(_rdb);
                //TODO: установить флаги
                return;
            }
            //MUL
            if (highHex[1] == '3') {
                var overflow = _acc.Mul(_rdb);
                //TODO: установить флаги
                return;
            }
            //DIV
            if (highHex[1] == '4') {
                _acc.Div(_rdb);
                //TODO: установить флаги
                return;
            }
            //AND
            if (highHex[1] == '5') {
                _acc.And(_rdb);
                //TODO: установить флаги
                return;
            }
            //OR
            if (highHex[1] == '6') {
                _acc.Or(_rdb);
                //TODO: установить флаги
                return;
            }
            //XOR
            if (highHex[1] == '7') {
                _acc.Xor(_rdb);
                //TODO: установить флаги
                return;
            }
            //CMP
            if (highHex[1] == '8') {
                var temp = new ExtendedBitArray(_rdb);
                bool overflow = temp.Sub(_acc);
                //TODO: установить флаги
                return;
            }
            //RD
            if (highHex[1] == '9') {
                _acc = new ExtendedBitArray(_rdb);
                //TODO: установить флаги
                return;
            }
            //INC
            if (highHex[1] == 'B') {
                _y50();
                _y4();
                //TODO: установить флаги
                return;
            }
            //DEC A
            if (highHex[1] == 'C') {
                _y51();
                _y4();
                //TODO: установить флаги
                return;
            }
            //ADC A
            if (highHex[1] == 'D') {
                var overflow = _acc.Add(_rdb);
                if (FC) {
                    overflow |= _acc.Inc();
                }
                //TODO: установить флаги
                return;
            }
            //SUBB A
            if (highHex[1] == 'D') {
                var overflow = _acc.Add(_rdb);
                if (FC) {
                    overflow |= _acc.Inc();
                }
                //TODO: установить флаги
                return;
            }
            //XCH A
            if (highHex[1] == 'D') {
                var temp = new ExtendedBitArray(_acc);
                _acc = new ExtendedBitArray(_rdb);
                _rdb = temp;
                _y4();
                //TODO: установить флаги
                return;
            }
        }

        private void LoadRegister(string lowHex) {
            _y47();
            _y2();
            if (lowHex[0] != '0') {
                //+@R    - 101
                if (lowHex[0] == '5') {
                    _y50();
                    _y5();
                } 
                else
                //-@R    - 111
                if (lowHex[0] == '7') {
                    _y51();
                    _y5();
                }
                _y60();
                _y1();
            }
        }

        private void UnloadRegister(string lowHex) {
            if (lowHex[0] == '0') {
                _y5();
            } else {
                _y4();
            }
        }
        
        private void ModifyRegister(string lowHex) {
            //@R+ - 001
            if (lowHex[0] == '1') {
                _y47();
                _y2();
                _y50();
                _y5();
                return;
            }
            //@R- - 011
            if (lowHex[0] == '3') {
                _y47();
                _y2();
                _y51();
                _y5();
            }
        }

        #region Микрокоманды

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
            try {
                _acc.Inc();
            } catch {
                //TODO: узнать про флаги
            }
        }

        private void _y16() {
            try {
                _acc.Dec();
            } catch {
                //TODO: узнать про флаги
            }
        }

        private void _y17() {
            _acc.Invert();
        }

        private void _y18() {
            var temp = new ExtendedBitArray();
            for (int i = 0; i < Constants.WordSize; i++) {
                temp[i] = _acc[(i + Constants.WordSize / 2) % Constants.WordSize];
            }
            _acc = temp;
        }

        private void _y19() {
            _acc.Add(6);
        }

        private void _y20() {
            _acc.Add(96);
        }

        private void _y21() {
            _acc.Add(-6);
        }

        private void _y22() {
            _acc.Add(-96);
        }

        private void _y23() {
            var temp = _acc;
            _acc = _rdb;
            _rdb = _acc;
        }

        private void _y24() {
            _dr = new ExtendedBitArray(_rdb);
        }

        private void _y25() {
            _dr = new ExtendedBitArray(_cr[0]);
        }

        private void _y26() {
            _cr[0] = new ExtendedBitArray(_rdb);
        }

        private void _y27() {
            _cr[1] = new ExtendedBitArray(_rdb);
        }

        private void _y28() {
            _cs = _acc[0] ? 1 : 0;
            _cs += _acc[1] ? 2 : 0;

            _ds = _acc[2] ? 1 : 0;
            _ds += _acc[3] ? 2 : 0;

            _ss = _acc[4] ? 1 : 0;
            _ss += _acc[5] ? 2 : 0;
        }

        private void _y29() {
            //TODO: ??????
        }

        private void _y30() {
            //TODO: ??????
        }

        private void _y31() {
            _pcl++;
            if (_pcl > (2 ^ (Constants.WordSize + 2))) {
                //TODO: Действие по переполнению PCL?
            }
        }

        private void _y32() {
            _pcl = _cr[0].NumValue();
        }

        private void _y33() {
            _pcl = _rdb.NumValue();
        }

        private void _y34() {
            _spl++;
            if (_spl > (2 ^ Constants.WordSize)) {
                //TODO: Действие по переполнению стека?
            }
        }

        private void _y35() {
            _spl--;
            if (_spl < 0) {
                //TODO: Действие по переполнению стека?
            }
        }

        private void _y36() {
            _spl = _acc.NumValue();
        }

        private void _y37() {
            for (int i = 0; i < 6; i++) {
                _psw[i] = _rdb[i + 2];
            }
        }

        private void _y38() {
            _psw[5] = false;
        }

        private void _y39() {
            _psw[5] = true;
        }

        private void _y40() {
            _psw[5] = false;
        }

        private void _y41() {
            _psw[5] = true;
        }

        private void _y42() {
            _rab = _acc.NumValue();
        }

        private void _y43() {
            _rab = _pcl + (_cs << Constants.WordSize);
        }

        private void _y44() {
            _rab = _cr[0].NumValue() + (_ds << Constants.WordSize);
        }

        private void _y45() {
            _rab = _spl + (_ss << Constants.WordSize);
        }

        private void _y46() {
            _rab = 0;
            for (int i = 4; i < 8; i++) {
                _rab += _cr[0][i] ? 1 << i - 4 : 0;
            }
        }

        private void _y47() {
            _rab = 0;
            for (int i = 0; i < 4; i++) {
                _rab += _cr[0][i] ? 1 << i : 0;
            }
        }

        private void _y48() {
            _rab = 0;
            for (int i = 0; i < 6; i++) {
                _rab += _cr[0][i] ? 1 << i - 4 : 0;
            }
        }

        private void _y49() {
            _rdb = new ExtendedBitArray(_acc);
        }

        private void _y50() {
            try {
                _rdb.Inc();
            } catch {
                //TODO: Переполнение RDB
            }
        }

        private void _y51() {
            try {
                _rdb.Dec();
            } catch {
                //TODO: Переполнение RDB
            }
        }

        private void _y52() {
            _rdb.Invert();
        }

        private void _y53() {
            var temp = new ExtendedBitArray();
            for (int i = 0; i < Constants.WordSize; i++) {
                temp[i] = _rdb[(i + Constants.WordSize / 2) % Constants.WordSize];
            }
            _rdb = temp;
        }

        private void _y54() {
            var bitNumber = _cr[1][0] ? 1 : 0;
            bitNumber += _cr[1][1] ? 2 : 0;
            _rdb[bitNumber] = true;
        }

        private void _y55() {
            var bitNumber = _cr[1][0] ? 1 : 0;
            bitNumber += _cr[1][1] ? 2 : 0;
            _rdb[bitNumber] = false;
        }

        private void _y56() {
            var bitNumber = _cr[1][0] ? 1 : 0;
            bitNumber += _cr[1][1] ? 2 : 0;
            _rdb[bitNumber] = true;
        }

        private void _y57() {
            _rdb[0] = (_cs & 1) != 0;
            _rdb[1] = (_cs & 2) != 0;
        }

        private void _y58() {
            for (int i = 0; i < 6; i++) {
                _rdb[i + 2] = _psw[i];
            }
        }

        private void _y59() {
            //TODO: Работа с регистром вывода
        }

        private void _y60() {
            _rab = _rdb.NumValue() + (_ds << Constants.WordSize);
        }

        private void _y61() {
            _rdb = new ExtendedBitArray(_cr[0]);
        }

        #endregion
    }
}
