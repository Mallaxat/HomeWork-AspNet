namespace HomeWork.Model;

public interface IScopedId
{
    public Guid GetID()
    {
        return Guid.NewGuid();
    }
    
}