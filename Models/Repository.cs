
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
    }
}
