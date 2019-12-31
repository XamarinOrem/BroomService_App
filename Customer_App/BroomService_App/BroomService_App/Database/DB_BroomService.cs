using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Database
{
    public class DB_BroomService
    {
        readonly SQLiteConnection database;

        public DB_BroomService(string dbPath)
        {
            try
            {
                database = new SQLiteConnection(dbPath);
            }
            catch (Exception)
            {

            }
        }
    }
}
