using MFrameWork;

public interface IManagerable : IInit
{

}
#if UNITY_EDITOR
public class ManagerBase : UnityEngine.MonoBehaviour, IManagerable
#else
public class ManagerBase : IManagerable
#endif
{
    public void OnInit()
    {
        On_Init();
    }
    public void OnRelease()
    {
#if UNITY_EDITOR
        NGUITools.DestroyObject(gameObject);
#endif
        On_Release();
    }

    public virtual void On_Init()
    {

    }
    public virtual void On_Release()
    {

    }
}