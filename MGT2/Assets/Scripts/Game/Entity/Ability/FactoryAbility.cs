using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryAbility
{


    /// <summary>
    /// 创建技能效果列表 1:att01;2:att01
    /// </summary>
    public static List<AEffectGroup> CreateAbilityEffectsNew(string[] effects, AssemblyRole roleData)
    {
        if (effects == null || effects.Length == 0)
        {
            return null;
        }
        List<AEffectGroup> targets = new List<AEffectGroup>();
        for (int cnt = 0; cnt < effects.Length; cnt++)
        {
            AEffectGroup group = CreateAEffectGroup(effects[cnt], roleData);
            if (group != null)
            {
                targets.Add(group);
            }
        }
        return targets;
    }
    /// <summary>
    /// 效果组 1:att01;2:att01
    /// </summary>
    public static AEffectGroup CreateAEffectGroup(string strs, AssemblyRole roleData)
    {
        string[] strEffectGroups = Utility.Xml.ParseString(strs, Utility.Xml.SplitSemicolon);
        if (strEffectGroups == null || strEffectGroups.Length == 0)
        {
            return null;
        }
        List<AEffectEventBase> list = new List<AEffectEventBase>();
        for (int cnt = 0; cnt < strEffectGroups.Length; cnt++)
        {
            string[] strsEvent = Utility.Xml.ParseString(strEffectGroups[cnt], Utility.Xml.SplitColon);
            AEffectEventBase eventData = CreateAEffectEventBase(strsEvent, roleData);
            if (eventData != null)
            {
                list.Add(eventData);
            }
        }
        if (list.Count == 0)
        {
            return null;
        }
        AEffectGroup data = new AEffectGroup();
        data.Initial(list);
        return data;
    }


    /// <summary>
    /// 创建技能效果。
    /// </summary>
    public static AEffectEventBase CreateAEffectEventBase(string[] strs, AssemblyRole owner)
    {
        if (strs == null || strs.Length != 2)
        {
            return null;
        }
        AEffectEventBase ae = null;
        EnumAEffectEvent type = (EnumAEffectEvent)int.Parse(strs[0]);
        string strParem = strs[1];
        switch (type)
        {
            case EnumAEffectEvent.Delay: ae = CreateAEData<AEDelay>(); break;
            case EnumAEffectEvent.Animator: ae = CreateAEData<AEAnimator>(); break;
            case EnumAEffectEvent.AttackEffect: ae = CreateAEData<AEAttackEffect>(); break;
            default:
                Log.Error(" not support type  : " + type + " AbilityId : " + strs[0]);
                break;
        }
        if (ae != null)
        {
            ae.OnInitial(type, owner, strParem);
        }
        return ae;
    }


    /// <summary>
    /// 创建技能效果列表 1:att01,0
    /// </summary>
    public static List<APreformBase> CreateAbilityLimits(string[] effects, AssemblyRole roleData)
    {
        if (effects == null || effects.Length == 0)
        {
            return null;
        }
        List<APreformBase> targets = new List<APreformBase>();
        for (int cnt = 0; cnt < effects.Length; cnt++)
        {
            string[] strs = Utility.Xml.ParseString(effects[cnt], Utility.Xml.SplitColon);
            APreformBase data = CreateAbilityLimit(strs, roleData);
            if (data == null)
            {
                continue;
            }
            targets.Add(data);
        }
        return targets;
    }



    /// <summary>
    /// 创建技能效果。
    /// </summary>
    public static APreformBase CreateAbilityLimit(string[] strs, AssemblyRole owner)
    {
        if (strs == null || strs.Length != 2)
        {
            return null;
        }
        APreformBase ae = null;
        EnumAPreform type = (EnumAPreform)int.Parse(strs[0]);
        string strParem = strs[1];
        switch (type)
        {
            case EnumAPreform.CD: ae = CreateALData<APreformCD>(); break;
            case EnumAPreform.Distance: ae = CreateALData<APreformDistance>(); break;
            default:
                Log.Error(" not support type  : " + type + " AbilityId : " + strs[0]);
                break;
        }
        if (ae != null)
        {
            ae.OnInitial(type, owner, strParem);
        }
        return ae;
    }





    public static T CreateALData<T>() where T : APreformBase, new()
    {
        return new T();
    }

    public static T CreateAEData<T>() where T : AEffectEventBase, new()
    {
        return new T();
    }



}
