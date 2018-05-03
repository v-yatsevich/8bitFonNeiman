using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _8bitVonNeiman.Auth.View;
using _8bitVonNeiman.Common;
using _8bitVonNeiman.Database;

namespace _8bitVonNeiman.Auth {
    public class AuthController: IAuthModelInput, IAuthFormOutput {
        private readonly IAuthModelOutput _output;
        private AuthForm _form;
        private readonly IDatabaseManagerInput _db = DatabaseManager.Instance;

        public AuthController(IAuthModelOutput output) {
            _output = output;
        }

        /// Открывает форму, если она закрыта или закрывает, если открыта
        public void ChangeFormState() {
            if (_form == null) {
                _form = new AuthForm(this);
                _form.Show();
            } else {
                _form.Close();
            }
        }

        /// <summary>
        /// Функция, вызывающаяся при закрытии формы. Необходима для корректной работы функции ChangeFormState()
        /// </summary>
        public void FormClosed() {
            _form = null;
            _output.AuthFormClosed();
        }

        public void LoginButtonClicked(string login, string password) {
            var loginEntity = _db.GetUser(login, password);
            if (loginEntity == null) {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }
            SharedDataManager.Instance.IsAuthorized = true;
            SharedDataManager.Instance.UserId = loginEntity.Id;
            SharedDataManager.Instance.IsAdmin = loginEntity.IsAdmin;
            MessageBox.Show("Вы успешно авторизовались");
            _output.AuthCompleted();
        }
    }
}
