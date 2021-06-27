using UnityEngine.UI;

public class MonoPoolClickBase<T> : MonoPoolItem
{
    private System.Action<T> _callBack;
    private Button _btnEvent;
    public void SetButton(Button btn)
    {
        _btnEvent = btn;
        EventHelper.RegistEvent(btn, OnClickThis);
    }
    private void OnClickThis(Button btn)
    {
        _callBack.InvokeGracefully(this);
    }
    public void SetClickEvent(System.Action<T> callback)
    {
        _callBack = callback;
    }

}

