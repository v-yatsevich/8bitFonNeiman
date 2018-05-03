namespace _8bitVonNeiman.Database.Models {
    public class LoginEntity {
        public int Id;
        public bool IsAdmin;
        public StudentEntity Student;

        public LoginEntity(int id, bool isAdmin, StudentEntity student) {
            Id = id;
            IsAdmin = isAdmin;
            Student = student;
        }
    }
}
