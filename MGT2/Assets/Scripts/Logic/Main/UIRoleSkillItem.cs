using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIRoleSkillItem : PrototypeItemClickBase<UIRoleSkillItem, AssemblyAbility>
{
    private void Awake()
    {
        GetBindComponents(gameObject);
        SetEventButton(m_Img_Icon.GetComponent<Button>());
    }
    public override void SetData(AssemblyAbility data)
    {
        base.SetData(data);
        m_Img_CDMask.fillAmount = 0;
        if (data == null)
        {
            UnityObjectExtension.SetActive(this.m_Img_Lock, true);
            UnityObjectExtension.SetActive(this.m_Img_Icon, false);
            return;
        }
        UnityObjectExtension.SetActive(this.m_Img_Lock, false);
        UnityObjectExtension.SetActive(this.m_Img_Icon, true);
        UIHelper.SetTexture(m_Img_Icon, data.Data.Icon);

    }

    public void ExecuteSkill()
    {
        SkillManager.CastSkill(Data);

        //if ()
        //{
        //    APreformCD cd = Data.GetAbilityLimit<APreformCD>(EnumAPreform.CD);
        //    if (cd != null)
        //    {
        //        m_Img_CDMask.fillAmount = 1;
        //        m_Img_CDMask.DOFillAmount(0, cd.Value / 1000.0f - 0.1f).SetEase(Ease.Linear);
        //    }
        //}
    }

}
