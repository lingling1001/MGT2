using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    /// <summary>
    /// 技能释放
    /// </summary>
    public static bool CastSkill(AssemblyAbility abilityData)
    {
        if (abilityData == null)
        {
            return false;
        }
        AssemblyAbilityCast cast = null;
        if (abilityData.Owner.ContainsKey(EnumAssemblyType.AbilityCast))
        {
            cast = abilityData.Owner.GetData<AssemblyAbilityCast>(EnumAssemblyType.AbilityCast);
        }
        else
        {
            cast = FactoryAssembly.AddAbilityCast(abilityData.Owner);
        }
        cast.SetAbility(abilityData);
        return true;
    }





    public static bool AttackNormal(AssemblyRole attacker, AssemblyRole def)
    {
        AssemblyAttribute attackerAtt = attacker.AssyAttribute;
        AssemblyAttribute defAtt = def.AssyAttribute;
        if (def == null || defAtt == null)
        {
            Log.Info("  target is miss or die " + def + "  : " + defAtt);
            return false;
        }
        //AbilityData ability = attacker.AssyAbility.GetAbility(1);
        //ability.SetCaster(attacker, new AssemblyRole[] { def });
        //ability.Execute();
        return true;
    }


}
