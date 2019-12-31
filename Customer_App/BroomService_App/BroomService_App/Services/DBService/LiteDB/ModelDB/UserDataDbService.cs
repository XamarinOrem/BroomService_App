using BroomService_App.Models;
using LiteDB;
using LiteDB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BroomService_App.Services.DBService.LiteDB.ModelDB
{
    public class UserDataDbService
    {
        private LiteDBService liteDBService;

        public UserDataDbService()
        {
            liteDBService = LiteDBService.Instance;
        }

        public UserData CreateUserDataInDB(UserData item)
        {
            return liteDBService.CreateItem(item);
        }

        public UserData DeleteItemFromDB(BsonValue id, UserData item)
        {
            return liteDBService.DeleteItem(id, item);
        }

        public bool IsUserDbPresentInDB()
        {
            UserData model = liteDBService.ReadAllItems<UserData>().FirstOrDefault(t => t.ID != 0);
            if (model == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<UserData> ReadAllItems()
        {
            return liteDBService.ReadAllItems<UserData>();
        }

        public UserData UpdateUserDataInDb(BsonValue bsonid, UserData item)
        {
            return liteDBService.UpdateItem(bsonid, item);
        }
    }
}
