using System;
using System.Collections.Generic;
using ScintillaNET;
using _8bitVonNeiman.Compiler.Model;

namespace _8bitVonNeiman.Compiler.View {
    class AsmLexer {
        public const int StyleDefault = 0;
        public const int StyleKeyword = 1;
        public const int StyleIdentifier = 2;
        public const int StyleNumber = 3;
        public const int StyleComment = 4;
        public const int StyleError = 4;

        private HashSet<string> _keywords;
        private HashSet<string> _identities;

        private string _lastWord;
        private bool _isEql;
        private Scintilla _scintilla;

        public AsmLexer(Scintilla scintilla) {
            _scintilla = scintilla;
            _keywords = new HashSet<string> {
                "EQL", "NOP", "RET", "IRET", "EI", "DI", "RR", "RL", "RRC", "RLC", "HLT", "INCA", "DECA", "SWAPA", "DAA", "DSA", "IN", "OUT", "ES", "MOVASR", "MOVSRA", "DJRNZ", "JZ", "JNZ", "JC", "JNC", "JN", "JNN", "JO", "JNO", "JMP", "CALL", "INT", "NOT", "ADD", "SUB", "MUL", "DIV", "AND", "OR", "XOR", "CMP", "RD", "WP", "INC", "DEC", "POP", "PUSH", "MOV", "ADC", "SUBB", "NOT", "ADD", "SUB", "MUL", "DIV", "AND", "OR", "XOR", "CMP", "RD", "WR" , "INC", "DEC" , "ADC", "SUBB", "XCH", "CB", "SB", "SBC", "SBS", "CBI", "SBI", "NBI", "SBIC", "SBIS", "SBISC", "IN", "OUT"
            };
            _identities = new HashSet<string>();
        }

        public void Style(int startPos, int endPos) {
            var line = _scintilla.LineFromPosition(startPos);
            startPos = _scintilla.Lines[line].Position;

            var lastDivider = startPos;

            _scintilla.StartStyling(startPos);
            while (startPos < endPos) {
                int divider = FirstDivider(startPos, _scintilla.Text);
                if (divider == -1) {
                    divider = endPos;
                }
                if (divider - lastDivider == 1) {
                    startPos++;
                    lastDivider = divider;
                    continue;
                }
                string word = _scintilla.Text.Substring(lastDivider, divider - lastDivider - 1);
                if (_keywords.Contains(word.ToUpper())) {
                    if (word.ToUpper().Equals("EQL")) {
                        _isEql = true;
                    }
                    ProcessLastWord();
                    SetStyle(word.Length, StyleKeyword);
                    
                } else if (word[0] <= '9' && word[0] >= '0') {
                    try {
                        CompilerSupport.ConvertToInt(word);
                        ProcessLastWord();
                        SetStyle(word.Length, StyleNumber);
                    } catch {
                        lastDivider = _scintilla.Text.IndexOf(Environment.NewLine, divider + 1, StringComparison.Ordinal);
                        if (lastDivider == -1) {
                            startPos = endPos;
                        } else {
                            startPos = lastDivider + 1;
                        }
                        SetStyle(word.Length, StyleError);
                        SetStyle(lastDivider - divider, StyleDefault);
                        continue;
                    }
                } else if (_identities.Contains(word)) {
                    ProcessLastWord();
                    SetStyle(word.Length, StyleIdentifier);
                } else {
                    ProcessLastWord();
                    _lastWord = word;
                }
                if (divider >= _scintilla.Text.Length) {
                    break;
                }
                if (_scintilla.Text[divider] == ';') {
                    int newLine = _scintilla.Text.IndexOf(Environment.NewLine, divider + 1, StringComparison.Ordinal);
                    if (newLine == -1) {
                        newLine = endPos;
                    }
                    SetStyle(newLine - divider, StyleComment);
                    divider = newLine;
                }
                lastDivider = divider;
                startPos = lastDivider + 1;
            }
        }

        private void ProcessLastWord() {
            if (_lastWord == null) {
                _isEql = false;
                return;
            }
            if (_isEql) {
                SetStyle(_lastWord.Length, StyleIdentifier);
                _identities.Add(_lastWord);
                _isEql = false;
            } else {
                SetStyle(_lastWord.Length, StyleError);
            }
            _lastWord = null;
        }

        private int FirstDivider(int startPos, string text) {
            int divider = int.MaxValue;
            divider = Math.Min(text.IndexOf(" ", startPos, StringComparison.Ordinal), divider);
            divider = Math.Min(text.IndexOf(Environment.NewLine, startPos, StringComparison.Ordinal), divider);
            divider = Math.Min(text.IndexOf(", ", startPos, StringComparison.Ordinal), divider);
            return Math.Min(text.IndexOf(";", startPos, StringComparison.Ordinal), divider);
        }

        private void SetStyle(int length, int style) {
            _scintilla.SetStyling(length, style);
            try {
                _scintilla.SetStyling(1, StyleDefault);
            } catch {
                // ignored
            }
        }
    }
}
