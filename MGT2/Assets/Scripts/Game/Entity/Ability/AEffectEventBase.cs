using System;

public abstract class AEffectEventBase
{
    public EnumAEffectEvent Type { get; private set; }
    public AssemblyRole Owner { get; private set; }
    public string StrParam { get; private set; }
    public bool IsFinish { get; private set; }
    public virtual void OnInitial(EnumAEffectEvent type, AssemblyRole owner, string strParem)
    {
        Type = type;
        Owner = owner;
        StrParam = strParem;
    }

    public virtual void Execute()
    {
        SetIsFinish(false);
    }
    public virtual void UpdateEvent()
    {

    }
    public void SetIsFinish(bool value)
    {
        IsFinish = value;
    }

}
