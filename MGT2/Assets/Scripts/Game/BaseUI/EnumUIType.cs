using System;
using System.Reflection;

public enum EnumUIType : int
{
    [AttributeUIName("UI/UIMain/UIEnterGame.prefab")]
    UIEnterGame = 100,
    [AttributeUIName("UI/UIMain/UIMain.prefab")]
    UIMain = 101,




    [AttributeUIName("UI/UIPreLoad/UILoading.prefab")]
    UILoading = 103,


    [AttributeUIName("UI/UIPlaceRole/UIPlaceRole.prefab")]
    UIPlaceRole = 300,
    [AttributeUIName("UI/UIPlaceRole/UIPlaceRoleItem.prefab")]
    UIPlaceRoleItem = 301,


    [AttributeUIName("UI/UIHead/UIHead.prefab")]
    UIHead = 400,
    [AttributeUIName("UI/UIHead/UIHeadItem.prefab")]
    UIHeadItem = 401,


    [AttributeUIName(AssetsName.UI_MIN_MAP)]
    UIMinMap = 500,

    /// <summary>
    /// 选择角色面板
    /// </summary>
    [AttributeUIName(AssetsName.UI_SELECT_ROLE)]
    UISelectRole = 600,

    /// <summary>
    /// 角色控制面板
    /// </summary>
    [AttributeUIName(AssetsName.UI_ROLE_CONTROL)]
    UIRoleControl = 700,
    /// <summary>
    /// 角色技能预设
    /// </summary>
    [AttributeUIName(AssetsName.UI_ROLE_SKILL_ITEM)]
    UIRoleSkillItem = 701,

    /// <summary>
    /// 遮罩
    /// </summary>
    [AttributeUIName(AssetsName.UI_EVENT_MASK)]
    MaskImage = 10000,


}
