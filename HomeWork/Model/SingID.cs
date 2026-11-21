namespace HomeWork.Model;

public class SingID:ISingID
{
    private Guid _id = Guid.NewGuid();

    public Guid GetID()
    {
        return _id;
    }
}