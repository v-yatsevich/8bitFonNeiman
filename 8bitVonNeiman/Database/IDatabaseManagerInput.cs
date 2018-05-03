using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.Database.Models;

namespace _8bitVonNeiman.Database {
    public interface IDatabaseManagerInput {
        /// Возвращает LoginEntity в соответствии с именем пользовател и паролем. 
        /// Если пользователя с такими данными нет в базе данных, будет вернут null
        LoginEntity GetUser(string login, string password);
    }
}
