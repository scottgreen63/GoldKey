namespace GoldKeyLib.Interfaces
{
    public interface IBaseEntityList<T>
    {
        string Create(T obj);
        T Retrieve(string key);
        void Update(T obj);
        void Delete(string key);
    }

    public enum RepositorySize
    {
        /// <summary>
        /// 8 records (2^3)
        /// </summary>
        Small,

        /// <summary>
        /// 128 records (2^7)
        /// </summary>
        Medium,

        /// <summary>
        /// 1024 records (2^10)
        /// </summary>
        Large
    }
}
