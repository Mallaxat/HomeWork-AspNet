namespace HomeWork.Model;

public class ScopedID:IScopedId
{
    private Guid _id = Guid.NewGuid();

    public Guid GetID()
    {
        return _id;
    }
}