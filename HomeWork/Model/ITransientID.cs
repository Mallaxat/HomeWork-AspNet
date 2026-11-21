namespace HomeWork.Model;

public interface ITransientID
{
    public Guid GetID()
    {
        return Guid.NewGuid();
    }
}