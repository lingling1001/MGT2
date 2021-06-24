using com.ootii.Messages;
using System;
using UnityEngine;

public class AssemblyRoleInfo : AssemblyBase
{
    /// <summary>
    /// 名字
    /// </summary>
    public string Name;
    /// <summary>
    /// 头像
    /// </summary>
    public string HeadIcon;
    /// <summary>
    /// 1男  2女
    /// </summary>
    public EnumGender Gender;

    public void SetData(string strName, string strHeadIcon, EnumGender gender)
    {
        Name = strName;
        Gender = gender;
        HeadIcon = strHeadIcon;
        ReadDataFinish();

    }

    public override void ReadDataFinish()
    {
        CreateRoleHelper.Addition(EnumSaveRole.Name, Name);
        CreateRoleHelper.Addition(EnumSaveRole.Icon, HeadIcon);
    }

    public string GetHeadIcon()
    {
        return AssetsName.GetHeadIcon(Gender + "/" + HeadIcon);
    }
    protected override void OnRelease()
    {
        CreateRoleHelper.Remove(EnumSaveRole.Name, Name);
        CreateRoleHelper.Remove(EnumSaveRole.Icon, HeadIcon);
        base.OnRelease();
    }

}
