using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.Cpu.View {
    public class CpuState {
        public readonly ExtendedBitArray Acc;
        public readonly ExtendedBitArray Dr;
        public readonly ExtendedBitArray Psw;
        public readonly int Ss;
        public readonly int Ds;
        public readonly int Cs;
        public readonly int Pcl;
        public readonly int Spl;
        public readonly ExtendedBitArray[] Cr;
        public readonly List<ExtendedBitArray> Registers;

        public CpuState(ExtendedBitArray acc,
            ExtendedBitArray dr,
            ExtendedBitArray psw,
            int ss,
            int ds,
            int cs,
            int pcl,
            int spl,
            ExtendedBitArray[] cr,
            List<ExtendedBitArray> registers) {
            Acc = acc;
            Dr = dr;
            Psw = psw;
            Ss = ss;
            Ds = ds;
            Cs = cs;
            Pcl = pcl;
            Spl = spl;
            Cr = cr;
            Registers = registers;
        }
    }
}