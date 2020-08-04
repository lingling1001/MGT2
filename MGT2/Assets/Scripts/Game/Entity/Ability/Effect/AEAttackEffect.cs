using MFrameWork;
using System;
using UnityEngine;

public class AEAttackEffect : AEffectEventBase
{
    public string EffectName;
    public int AttType;
    public override void OnInitial(EnumAEffectEvent type, AssemblyRole owner, string param)
    {
        base.OnInitial(type, owner, param);
        string[] strParam = Utility.Xml.ParseString<string>(param, Utility.Xml.SplitComma);
        if (strParam == null)
        {
            return;
        }
        AttType = int.Parse(strParam[0]);
        EffectName = strParam[1];

    }
    public override void Execute()
    {
        AttackEffectBase data = null;
        if (AttType == 1)
        {
            data = new AttackEffectArrow();
        }
        if (data != null)
        {
            data.Initial(this);
            AttackEffectManager.Instance.AddEffect(data);
        }
       
    }

}
