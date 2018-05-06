namespace _8bitVonNeiman.Database.Models {
    public class TaskEntity {
        public int Id;
        public string Name;
        public string Description;

        public TaskEntity() {
            Id = -1;
            Name = "";
            Description = "";
        }

        public TaskEntity(int id, string name, string description) {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
