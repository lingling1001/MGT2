using UnityEngine;
using System.Reflection;
using MFrameWork;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //单例模式
    protected static T _instance;
    //线程安全使用
    private static readonly object _lockObj = new object();
    public static T Instance
    {
        get
        {
            if (null == _instance)
            {
                lock (_lockObj)
                {
                    if (null == _instance)
                    {
                        _instance = GameObject.FindObjectOfType<T>();
                        if (_instance == null)
                        {
                            GameObject obj = new GameObject(typeof(T).ToString());
                            _instance = obj.AddComponent<T>();
                            MemberInfo info = typeof(T);
                            var attributes = info.GetCustomAttributes(true);
                            foreach (var atribute in attributes)
                            {
                                var defineAttri = atribute as MonoSingletonPath;
                                if (defineAttri != null)
                                {
                                    obj.name = defineAttri.PathInHierarchy;
                                    break;
                                }
                            }
                            GameObject parent = GameObject.Find("[ManagerObjectNode]");
                            if (parent == null)
                            {
                                parent = new GameObject("[ManagerObjectNode]");
                                UnityEngine.GameObject.DontDestroyOnLoad(parent);
                            }
                            obj.transform.SetParent(parent.transform);
                        }
                    }
                }
            }
            return _instance;
        }
    }

    public static bool InstanceIsValid()
    {
        if (_instance == null || Equals(_instance, null))
        {
            return false;
        }
        return true;
    }

    public static void ReleaseInstance()
    {
        if (InstanceIsValid())
        {
            GameObject.DestroyImmediate(_instance.gameObject);
        }

    }

}

