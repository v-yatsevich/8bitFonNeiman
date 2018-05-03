using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8bitVonNeiman.Auth {
    public interface IAuthModelInput {
        /// <summary>
        /// Функция, показывающая форму, если она закрыта, и закрывающая ее, если она открыта
        /// </summary>
        void ChangeFormState();
    }
}
