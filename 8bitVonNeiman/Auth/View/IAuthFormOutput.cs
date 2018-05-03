using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Auth.View {
    public interface IAuthFormOutput {
        /// <summary>
        /// Функция, вызывающаяся при закрытии формы. Необходима для корректной работы функции ChangeFormState()
        /// </summary>
        void FormClosed();

        void LoginButtonClicked(string login, string password);
    }
}
