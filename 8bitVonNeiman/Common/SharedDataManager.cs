
namespace _8bitVonNeiman.Common {
    class SharedDataManager {
        private static SharedDataManager _instance;

        private SharedDataManager() { }

        public static SharedDataManager Instance => _instance ?? (_instance = new SharedDataManager());

        public bool IsAuthorized = false;
        public int UserId;
        public bool IsAdmin;
        public int StudentId;
    }
}
