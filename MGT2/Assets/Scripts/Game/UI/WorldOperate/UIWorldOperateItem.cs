using UnityEngine.UI;

public partial class UIWorldOperateItem : MonoPoolClickBase<UIWorldOperateItem>
{
    public EnumWorldResTP OperateType;
    public AssemblyCache Data;
    private void Awake()
    {
        GetBindComponents(gameObject);
        SetButton(m_Btn_This);
    }
    public void SetData(EnumWorldResTP type, AssemblyCache assemblyCache)
    {
        OperateType = type;
        Data = assemblyCache;

        string[] strs = GetNameAndIcon(type);
        if (strs.Length == 2)
        {
            UIHelper.SetText(m_Txt_Text, strs[0]);
            UIHelper.SetAtlasImage(m_AImg_Icon, strs[1], true);

        }
    }


    public string[] GetNameAndIcon(EnumWorldResTP type)
    {
        string[] strs = new string[2];
        switch (type)
        {
            case EnumWorldResTP.Attack:
                strs[0] = DefineText.GetText(DefineText.Attack);
                strs[1] = "btn_icon_battle";
                break;
            case EnumWorldResTP.Guard:
                strs[0] = DefineText.GetText(DefineText.Guard);
                strs[1] = "btn_icon_shield";
                break;
            case EnumWorldResTP.Infomation:
                strs[0] = DefineText.GetText(DefineText.Infomation);
                strs[1] = "btn_icon_mark_info";
                break;
        }
        return strs;
    }
}
