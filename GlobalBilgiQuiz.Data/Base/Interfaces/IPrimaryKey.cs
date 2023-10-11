namespace GlobalBilgiQuiz.Data.Base.Interfaces
{
    // GENERIC
    public interface IPrimaryKey<T> //Type'ın T'si
    {
        public T Id { get; set; }
    }
}
