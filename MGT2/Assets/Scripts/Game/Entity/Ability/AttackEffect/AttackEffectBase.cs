public class AttackEffectBase
{
    protected AEffectEventBase Data;
    public virtual void Initial(AEffectEventBase data)
    {
        Data = data;
    }
    public virtual void Release()
    {

    }
}
