using UnityEngine;

public class APreformDistance : APreformBase
{

    public float Distance { get; private set; }
    public override void OnInitial(EnumAPreform type, AssemblyRole owner, string param)
    {
        base.OnInitial(type, owner, param);
        int dis;
        if (int.TryParse(Param, out dis))
        {
            Distance = dis / 100.0f;
        }
        else
        {
            Distance = Owner.AssyAttribute.GetValue(DefineAttributeId.ATTACK_RANGE) / 100.0f;
        }
    }
    public override bool OnCheckPreform()
    {
        if (Target != null)
        {
            float dis = Vector3.Distance(Owner.Position, Target.Position);
            return dis <= Distance;
        }
        return true;

    }

}