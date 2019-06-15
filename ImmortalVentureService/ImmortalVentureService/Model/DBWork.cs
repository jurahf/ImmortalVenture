using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;


namespace ImmortalVentureService.Model
{
    public class DBWork
    {
        private const string idFieldName = "Id";
        private static HakatonModelContainer database = null;


        // синглтон
        private static HakatonModelContainer Database
        {
            get
            {
                if (database == null)
                {
                    database = new HakatonModelContainer();
                }

                return database;
            }
        }

        private int GetId<T>(T obj) where T : class
        {
            return (int)typeof(T).GetProperty(idFieldName).GetValue(obj, null);
        }


        public List<T> GetFromDatabase<T>() where T : class
        {
            try
            {
                return Database.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<T> GetFromDatabase<T>(Func<T, bool> filter) where T : class
        {
            try
            {
                return Database.Set<T>().Where(filter).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteObject<T>(T obj) where T : class
        {
            try
            {
                Database.Set<T>().Remove(obj);

                Database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteObject<T>(List<T> objList) where T : class
        {
            try
            {
                foreach (var obj in objList)
                {
                    Database.Set<T>().Remove(obj);
                }

                Database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update<T>(T obj) where T : class
        {
            try
            {
                Database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update<T>(List<T> objList) where T : class
        {
            Update(objList.FirstOrDefault());
        }

        public void Insert<T>(T obj) where T : class
        {
            try
            {
                // объект мог сохраниться с другими объектами, поэтому добавлять его не всегда надо
                if (GetFromDatabase<T>().FirstOrDefault(x => GetId(x) == GetId(obj)) == null) 
                {
                    Database.Set<T>().Add(obj);
                }

                Database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void Insert<T>(List<T> objList) where T : class
        {
            try
            {
                foreach (var obj in objList)
                {
                    // объект мог сохраниться с другими объектами, поэтому добавлять его не всегда надо
                    if (GetFromDatabase<T>().FirstOrDefault(x => GetId(x) == GetId(obj)) == null)
                    {
                        Database.Set<T>().Add(obj);
                    }
                }

                Database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }




    }
}