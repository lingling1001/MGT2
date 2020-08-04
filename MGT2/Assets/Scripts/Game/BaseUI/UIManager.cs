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
    private Dictionary<int, BaseUI> _mapTempOpening = new Dictionary<int, BaseUI>();
    public void OpenUI<T>(EnumUIType type, params object[] param) where T : BaseUI
    {
        BaseUI openUI = GetOpenUI(type);
        if (openUI != null)//已经打开了界面
        {
            return;
        }
        int uiType = (int)type;
        if (_mapTempOpening.ContainsKey(uiType))//正在加载中。
        {
            return;
        }
        _mapTempOpening.Add(uiType, null);
        AddOpenUI<T>(type);
    }

    private void AddOpenUI<T>(EnumUIType type) where T : BaseUI
    {
        GameObject prefab = ResLoadHelper.LoadAsset<GameObject>(UIHelper.GetUIPath(type));
        GameObject uiObj = ResLoadHelper.Instantiate(prefab);
        UnityObjectExtension.SetActive(uiObj, true);
        _mapTempOpening.Remove((int)type);
        BaseUI bsUI = System.Activator.CreateInstance<T>();
        bsUI.SetUIType(type);
        uiObj.transform.SetParent(UICanvas.transform, false);
        bsUI.SetGameObject(uiObj);
        bsUI.OnInit();
        _listOpens.Add(bsUI);

    }

    public BaseUI GetOpenUI(EnumUIType type)
    {
        BaseUI ui = _listOpens.Find(item => item.UIType == type);
        return ui;
    }
    public BaseUI GetLastOpenUI()
    {
        if (ListOpens.Count > 0)
        {
            return ListOpens[ListOpens.Count - 1];
        }
        return null;
    }

    public void CloseUI(EnumUIType type)
    {
        BaseUI ui = GetOpenUI(type);
        if (ui != null)
        {
            ui.OnRelease();
            _listOpens.Remove(ui);
            ResLoadHelper.Destory(ui.ObjUI);
            return;
        }
        int uiType = (int)type;
        if (_mapTempOpening.ContainsKey(uiType))
        {
            _mapTempOpening.Remove(uiType);
            Log.Warning(" 还未打开就要关闭。。。。Type " + type);
            return;
        }
    }

    private CanvasScaler _cavScaler;
    public CanvasScaler CavScaler { get { return _cavScaler; } }
    private Camera _uiCamera;
    public Camera UICamera { get { return _uiCamera; } }

    private Canvas _uiCanvas;
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
            _uiCamera = obj.GetComponentInChildren<Camera>();
            _cavScaler = obj.GetComponentInChildren<CanvasScaler>();
            //GameObject.DestroyImmediate(_canvas.transform.GetChild(0).gameObject);
        }

    }
    

    public void OnRelease()
    {
        if (TransUIRoot != null)
        {
            ResLoadHelper.Destory(TransUIRoot.gameObject);
            _transUIRoot = null;
        }

    }


}
