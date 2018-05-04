namespace _8bitVonNeiman.Database.Models {
    public class UserEntity {
        public int Id;
        public string Login;
        public string Password;
        public int Role;

        public UserEntity(int id, string login, string password, int role) {
            Id = id;
            Login = login;
            Password = password;
            Role = role;
        }

        public UserEntity() {
            Id = -1;
            Role = -1;
        }
    }
}
