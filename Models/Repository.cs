
using SQLite;

namespace ITopiaMAUI.Models
{
    public class Repository
    {
        SQLiteConnection database;
        public Repository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<DBSaveModel>();
            database.CreateTable<DBNovellaScenario>();
        }
        public IEnumerable<DBSaveModel> GetSaves() =>
            database.Table<DBSaveModel>().ToList();
        public DBSaveModel GetSave(int id) =>
            database.Get<DBSaveModel>(id);
        public int DeleteSave(int id) =>
            database.Delete<DBSaveModel>(id);
        public int SaveSave(DBSaveModel item)
        {
            if (item.ID != 0)
            {
                database.Update(item);
                return item.ID;
            }
            return database.Insert(item);
        }
        public void DeleteAllSaves() =>
            database.DeleteAll<DBSaveModel>();
        public IEnumerable<DBNovellaScenario> GetNovellaScenarios() =>
            database.Table<DBNovellaScenario>().ToList();
        public DBNovellaScenario GetNovellaScenario(int id) =>
            database.Get<DBNovellaScenario>(id);
        public int DeleteNovellaScenario(int id) =>
            database.Delete<DBNovellaScenario>(id);
        public int SaveNovellaScenario(DBNovellaScenario item)
        {
            if (item.ID != 0)
            {
                database.Update(item);
                return item.ID;
            }
            return database.Insert(item);
        }
        public void DeleteAllNovellaScenarios() => 
            database.DeleteAll<DBNovellaScenario>();
    }
}
