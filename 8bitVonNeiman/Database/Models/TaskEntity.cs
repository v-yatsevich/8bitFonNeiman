namespace _8bitVonNeiman.Database.Models {
    public class TaskEntity {
        public readonly int Id;
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

        public override string ToString() {
            return Name ?? "";
        }
    }
}
