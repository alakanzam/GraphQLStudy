namespace GraphQlStudy.Interfaces
{
    public interface ICacheService<TKey, TValue>
    {
        TValue Get(TKey key);

        void Set(TKey key, TValue value);

        bool HasKey(TKey key);

        void Delete(TKey key);
    }
}