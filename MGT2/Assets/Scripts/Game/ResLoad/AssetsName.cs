using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AssetsName
{

    public const string UI_CITY_INFO = "UI/UICityInfo.prefab";
    public const string UI_JOYSTICK = "UI/UIJoystick.prefab";

    public const string EFFECT_SELECT_GREED = "QuestZoneGreen";
    public const string EFFECT_SELECT_RED = "QuestZoneRed";

    public const string BUILD_TOWER = "tower";



    /// <summary>
    /// 世界相机
    /// </summary>
    public const string WORLD_CM = "Other/CMObjNode.prefab";

    public const string WORLD_MIN_MAP = "WorldMinMap";

    public const string MAP_ASTAR = "Config/Astar.txt";






    public const string UI_EVENT_MASK = "UI/UIPreLoad/EventMaskObj.prefab";

    /// <summary>
    /// 选择角色
    /// </summary>
    public const string UI_SELECT_ROLE = "UI/UISelectRole/UISelectRole.prefab";
    /// <summary>
    /// 选择角色Item
    /// </summary>
    public const string UI_SELECT_ROLE_ITEM = "UI/UISelectRole/UISelectRoleItem.prefab";


    public const string UI_ROLE_CONTROL = "UI/UIMain/UIRoleControl.prefab";

    public const string UI_ROLE_SKILL_ITEM = "UI/UIMain/UIRoleSkillItem.prefab";


    public const string ANI_IDLE = "idle";
    public const string ANI_RUN = "run";





    public const string TempMod = "Model/TempMod.prefab";

    public const string DragFollowItem = "Other/DragFollowItem.prefab";

    public const string UIHeadInfoItem = "UI/Head/UIHeadInfoItem.prefab";

    public const string UIRoleControlItem = "UI/Main/UIRoleControlItem.prefab";

    public const string UIWorldOperateRole = "UI/WorldOperate/UIWorldOperateRole.prefab";

    public const string UIWorldOperateItem = "UI/WorldOperate/UIWorldOperateItem.prefab";

    

    private static Dictionary<string, string> _mapUIPaths;
    public static string GetUIPath<T>() where T : BaseUI
    {
        string strName = typeof(T).Name;
        if (_mapUIPaths == null)
        {
            _mapUIPaths = new Dictionary<string, string>();
            _mapUIPaths.Add("UIHeadInfo", "UI/Head/UIHeadInfo.prefab");
            _mapUIPaths.Add("UIMain", "UI/Main/UIMain.prefab");
            _mapUIPaths.Add("UIEnterGame", "UI/Main/UIEnterGame.prefab");
            _mapUIPaths.Add("UIJoystick", "UI/Main/UIJoystick.prefab");
            _mapUIPaths.Add("UIRoleControl", "UI/Main/UIRoleControl.prefab");
            _mapUIPaths.Add("UIWorldOperate", "UI/WorldOperate/UIWorldOperate.prefab");
        }
        if (_mapUIPaths.ContainsKey(strName))
        {
            return _mapUIPaths[strName];
        }
        Log.Error("Get {0}  path Error ", strName);
        return string.Empty;
    }

    public static string GetHeadIcon(string strName)
    {
        return string.Format("Image/{0}.bmp", strName);
    }
}
