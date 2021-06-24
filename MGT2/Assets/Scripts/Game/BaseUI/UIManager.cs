using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using com.ootii.Messages;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// 当前打开的界面
    /// </summary>
    private List<BaseUI> _listOpens = new List<BaseUI>();
    public List<BaseUI> ListOpens { get { return _listOpens; } }
    private Dictionary<string, BaseUI> _mapTempOpening = new Dictionary<string, BaseUI>();

    public static void QOpenUI<T>(params object[] param) where T : BaseUI
    {
        Instance.OpenUI<T>(param);
    }
    public static void QCloseUI<T>() where T : BaseUI
    {
        Instance.CloseUI<T>();
    }

    public void OpenUI<T>(params object[] param) where T : BaseUI
    {
        string strPath = AssetsName.GetUIPath<T>();

        BaseUI openUI = GetOpenUI(strPath);
        if (openUI != null)//已经打开了界面
        {
            return;
        }
        if (_mapTempOpening.ContainsKey(strPath))//正在加载中。
        {
            return;
        }
        _mapTempOpening.Add(strPath, null);
        AddOpenUI<T>(strPath, param);
    }

    private void AddOpenUI<T>(string path, params object[] param) where T : BaseUI
    {
        GameObject prefab = ResLoadHelper.LoadAsset<GameObject>(path);
        if (prefab == null)
        {
            Log.Error("  Add Open UI Is Null " + path);
            return;
        }
        GameObject uiObj = NGUITools.Instantiate(prefab, UICanvas.transform, false);
        NGUITools.SetActive(uiObj, true);
        _mapTempOpening.Remove(path);
        BaseUI bsUI = System.Activator.CreateInstance<T>();
        bsUI.SetUIType(path);
        bsUI.SetGameObject(uiObj.gameObject, param);
        bsUI.OnInit();
        _listOpens.Add(bsUI);

    }

    public bool UIIsOpen<T>() where T : BaseUI
    {
        return GetOpenUI<T>() != null;
    }
    public BaseUI GetOpenUI<T>() where T : BaseUI
    {
        return GetOpenUI(AssetsName.GetUIPath<T>());
    }
    public BaseUI GetOpenUI(string strPath)
    {
        return _listOpens.Find(item => item.UIPath == strPath);
    }

    public BaseUI GetLastOpenUI()
    {
        if (ListOpens.Count > 0)
        {
            return ListOpens[ListOpens.Count - 1];
        }
        return null;
    }
    public void CloseUI<T>() where T : BaseUI
    {
        CloseUI(AssetsName.GetUIPath<T>());
    }
    public void CloseUI(string strType)
    {
        BaseUI ui = GetOpenUI(strType);
        if (ui != null)
        {
            ui.OnRelease();
            _listOpens.Remove(ui);
            NGUITools.DestroyObject(ui.ObjUI);
            return;
        }
        if (_mapTempOpening.ContainsKey(strType))
        {
            _mapTempOpening.Remove(strType);
            Log.Warning(" 还未打开就要关闭。。。。Type " + strType);
            return;
        }
    }


    public void CloseAllUI(EnumUIKind type)
    {
        List<BaseUI> list = new List<BaseUI>();
        for (int cnt = 0; cnt < ListOpens.Count; cnt++)
        {
            if (ListOpens[cnt].UIKind == EnumUIKind.Normal)
            {
                list.Add(ListOpens[cnt]);
            }
        }
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            CloseUI(list[cnt].UIPath);
        }
    }

    private CanvasScaler _cavScaler;
    public CanvasScaler CavScaler { get { return _cavScaler; } }

    private Canvas _uiCanvas;
    /// <summary>
    /// 画布
    /// </summary>
    public Canvas UICanvas { get { return _uiCanvas; } }

    private Transform _transUIRoot;
    public Transform TransUIRoot { get { return _transUIRoot; } }

    /// <summary>
    /// 初始化UI
    /// </summary>
    public void OnInit()
    {
        GameObject obj = GameObject.Find("UIRoot");
        if (obj != null)
        {
            GameObject.DontDestroyOnLoad(obj);
            _transUIRoot = obj.transform;
            _uiCanvas = obj.GetComponentInChildren<Canvas>();
            _cavScaler = obj.GetComponentInChildren<CanvasScaler>();
            //_uiCamera = obj.GetComponentInChildren<Camera>();
        }
        else
        {
            Log.Error(" UI Root Init Fail ");
        }

    }


    public void OnRelease()
    {
        if (TransUIRoot != null)
        {
            ResLoadHelper.DestroyObject(TransUIRoot.gameObject);
            _transUIRoot = null;
        }

    }


}
