using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIRoleControl : BaseUI
{
    private List<UIRoleSkillItem> _listSkillItems = new List<UIRoleSkillItem>();
    private ETCJoystick _joystick;
    private Transform[] _skillParent = new Transform[4];
    public override void OnInit()
    {
        base.OnInit();
        GetBindComponents(this.ObjUI);
        _joystick = this.Find<ETCJoystick>("Joystick");
        for (int cnt = 0; cnt < _skillParent.Length; cnt++)
        {
            _skillParent[cnt] = this.Find<Transform>("Trans_Skill/Node" + cnt);
        }

        RefreshRoleSkills();

        FactoryAssembly.AddJoystick(RoleManager.Instance.CurControlRole.Owner, _joystick);

    }


    private void RefreshRoleSkills()
    {
        AssemblyRole role = RoleManager.Instance.CurControlRole;
        List<AssemblyAbility> list = role.Owner.GetDatas<AssemblyAbility>(EnumAssemblyType.Ability);

        List<AssemblyAbility> aList = new List<AssemblyAbility>();
        for (int cnt = 0; cnt < 4; cnt++)
        {
            if (cnt < list.Count)
            {
                aList.Add(list[cnt]);
            }
            else
            {
                aList.Add(null);
            }
        }
        UIHelper.SetListDataIndex(_listSkillItems, aList, EventGetSkillItem, EventSetSkillData);

    }

    private void EventSetSkillData(UIRoleSkillItem arg1, AssemblyAbility arg2, int idx)
    {
        arg1.gameObject.transform.SetParent(_skillParent[idx]);
        arg1.gameObject.transform.localPosition = Vector3.zero;
        arg1.SetData(arg2);
        arg1.SetClickCallback(EventClickItem);

    }
    /// <summary>
    /// 点击Item事件，生成角色
    /// </summary>
    private void EventClickItem(UIRoleSkillItem obj)
    {
        obj.ExecuteSkill();
    }
    private GameObject _prefabItem;
    private UIRoleSkillItem EventGetSkillItem()
    {
        if (_prefabItem == null)
        {
            _prefabItem = ResLoadHelper.LoadAsset<GameObject>(UIHelper.GetUIPath(EnumUIType.UIRoleSkillItem));
        }
        if (_prefabItem != null)
        {
            GameObject obj = NGUITools.AddChild(ObjUI, _prefabItem);
            return obj.GetOrAddComponent<UIRoleSkillItem>();
        }
        return null;
    }

    public override void OnRelease()
    {
        base.OnRelease();
    }
}
