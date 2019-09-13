namespace dal.queryobjects
{
    public interface IQuery<T>
    {
        T Execute();
    }
}