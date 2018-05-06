namespace _8bitVonNeiman.Database.Models {
    public class StudentEntity {
        public readonly int Id;
        public UserEntity User;
        public string Name;
        public string Group;

        public StudentEntity(int id, UserEntity user, string name, string group) {
            Id = id;
            User = user;
            Name = name;
            Group = group;
        }

        public StudentEntity() {
            Id = -1;
            User = new UserEntity();
        }

        public override string ToString() {
            return Name ?? "";
        }
    }
}
