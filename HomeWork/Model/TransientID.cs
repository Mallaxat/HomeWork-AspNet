namespace HomeWork.Model;

public class TransientID:ITransientID
{
    private Guid _id = Guid.NewGuid();

    public Guid GetID()
    {
        return _id;
    }
}