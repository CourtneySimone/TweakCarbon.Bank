using System.Data.Entity;

namespace TweakBank.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAllData();
        T FindById(object id);
        void InsertRecord(T objRecord);
        void Update(T objRecord);
        void DeleteRecord(object id);
        void SaveRecord();

    }
}