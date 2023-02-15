using System.Collections.Generic;
using System.Data.Entity;
using TweakBank.Data;

namespace TweakBank.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private TweakBankContext db = null;
        protected IDbSet<T> dbEntity = null;

        public GenericRepository()
        {
            this.db = new TweakBankContext();
            dbEntity = db.Set<T>();
        }

        public GenericRepository(TweakBankContext _db)
        {
            this.db = _db;
            dbEntity = db.Set<T>();
        }

        public IEnumerable<T> GetAllData()
        {
            return dbEntity.ToList();
        }

        public T FindById(object id)
        {
            return dbEntity.Find(id);
        }

        public void InsertRecord(T objRecord)
        {
            dbEntity.Add(objRecord);
            SaveRecord();
        }

        public void Update(T objRecord)
        {
            dbEntity.Attach(objRecord);
            db.Entry(objRecord).State = EntityState.Modified;
            SaveRecord();
        }

        public void DeleteRecord(object id)
        {
            T currentRecord = dbEntity.Find(id);
            dbEntity.Remove(currentRecord);
            SaveRecord();
        }

        public void SaveRecord()
        {
            db.SaveChanges();
        }
    }
}