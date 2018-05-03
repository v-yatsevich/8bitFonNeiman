namespace _8bitVonNeiman.Database.Models {
    public class StudentEntity {
        public int Id;
        public int UserId;
        public string Name;
        public string Group;

        public StudentEntity(int id, int userId, string name, string group) {
            Id = id;
            UserId = userId;
            Name = name;
            Group = group;
        }
    }
}
