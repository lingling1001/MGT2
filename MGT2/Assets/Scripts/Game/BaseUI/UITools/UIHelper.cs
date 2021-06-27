using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UIHelper
{

    public static Vector3 WorldToCanvasPosition(this Canvas canvas, Vector3 worldPosition, Camera camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        var viewportPosition = camera.WorldToViewportPoint(worldPosition);
        return canvas.ViewportToCanvasPosition(viewportPosition);
    }

    public static Vector3 ScreenToCanvasPosition(this Canvas canvas, Vector3 screenPosition)
    {
        var viewportPosition = new Vector3(screenPosition.x / Screen.width,
                                           screenPosition.y / Screen.height,
                                           0);
        return canvas.ViewportToCanvasPosition(viewportPosition);
    }

    public static void SetRawImage(RawImage image, string headIcon)
    {
        if (image == null || string.IsNullOrEmpty(headIcon))
        {
            return;
        }
        Texture2D obj = ResLoadHelper.LoadAsset<Texture2D>(headIcon);
        image.texture = obj;
    }
    public static void SetAtlasImage(AtlasImage image, string headIcon)
    {
        SetAtlasImage(image, headIcon, false);
    }
    public static void SetAtlasImage(AtlasImage image, string headIcon, bool nativeSize)
    {
        if (image == null || string.IsNullOrEmpty(headIcon))
        {
            return;
        }
        image.spriteName = headIcon;
        if (nativeSize)
        {
            image.SetNativeSize();
        }
    }

    public static Vector3 ViewportToCanvasPosition(this Canvas canvas, Vector3 viewportPosition)
    {
        var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
        var canvasRect = canvas.GetComponent<RectTransform>();
        var scale = canvasRect.sizeDelta;
        return Vector3.Scale(centerBasedViewPortPosition, scale);
    }


    public static Vector2 WorldToUI(Vector3 pos)
    {
        return WorldToCanvasPosition(UIManager.Instance.UICanvas, pos);
    }
    /// <summary>
    /// 世界转UI坐标
    /// </summary>
    public static Vector2 WorldToUI(RectTransform canvas, Camera camera, Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPos, camera, out movePos);
        //Convert the local point to world point
        return canvas.transform.TransformPoint(movePos);
    }


    public static Vector2 WorldToUIPoint(Vector3 worldPos)
    {
        return WorldToUIPoint(GameManager.QGetOrAddMgr<CameraManager>().MainCamera, worldPos);
    }
    public static Vector2 WorldToUIPoint(Camera camera, Vector3 worldPos)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.UICanvas.transform as RectTransform,
             camera.WorldToScreenPoint(worldPos), UIManager.Instance.UICanvas.worldCamera, out pos);
        return pos;
    }



    private static Dictionary<int, Color> MapQualColor;
    public static Color GetQualColor(int type)
    {
        if (MapQualColor == null)
        {
            MapQualColor = new Dictionary<int, Color>();
            MapQualColor.Add(1, new Color(189f / 255f, 189f / 255f, 189f / 255f, 255f / 255f));
            MapQualColor.Add(2, new Color(76f / 255f, 234f / 255f, 189f / 255f, 255f / 255f));
            MapQualColor.Add(3, new Color(34f / 255f, 140f / 255f, 0f / 255f, 255f / 255f));
            MapQualColor.Add(4, new Color(255f / 255f, 0f, 237f / 255f, 255f / 255f));
            MapQualColor.Add(5, new Color(255f / 255f, 126f / 255f, 9f / 255f, 255f / 255f));
            MapQualColor.Add(6, new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f));
        }
        if (MapQualColor.ContainsKey(type))
        {
            return MapQualColor[type];
        }
        return MapQualColor[1];
    }


    public static void SetText(TextMeshProUGUI texName, string name)
    {
        if (texName != null)
        {
            texName.text = name;
        }
    }
    private static Dictionary<EnumCountStrType, string> _mapStrCount;


    /// <summary>
    /// 拼接数量 。
    /// </summary>
    public static string GetStrCount(object count, EnumCountStrType type = EnumCountStrType.C)
    {
        if (_mapStrCount == null)
        {
            _mapStrCount = new Dictionary<EnumCountStrType, string>();
            _mapStrCount.Add(EnumCountStrType.C, " : {0}");
            _mapStrCount.Add(EnumCountStrType.X, " X {0}");
        }
        if (_mapStrCount.ContainsKey(type))
        {
            return string.Format(_mapStrCount[type], count);
        }
        return string.Empty;
    }


    /// <summary>
    /// 简单 Item重复利用
    /// </summary>
    /// <typeparam name="TItem">物品</typeparam>
    /// <typeparam name="TData">数据</typeparam>
    /// <param name="listItem">物品集合</param>
    /// <param name="datas">数据集合</param>
    /// <param name="getItem">创建物品</param>
    /// <param name="call">设置物品数据</param>
    public static void SetItemsList<TItem, TData>(IList<TItem> listItem, IList<TData> datas, System.Func<TItem> getItem,
                                                 System.Action<TItem, TData> setItemData) where TItem : Component
    {
        if (setItemData != null)
        {
            SetListDataIndex(listItem, datas, getItem, ((arg1, arg2, arg3) =>
            {
                setItemData(arg1, arg2);
            }));
        }
        else
        {
            SetListDataIndex(listItem, datas, getItem, null);
        }

    }

    public static void SetListDataIndex<TItem, TData>(IList<TItem> listItem, IList<TData> datas, System.Func<TItem> getItem,
                                                      System.Action<TItem, TData, int> setItemData) where TItem : Component
    {
        if (listItem == null)
        {
            Debug.LogError(" List Item " + listItem);
            return;
        }
        if (datas == null)
        {
            for (int cnt = 0; cnt < listItem.Count; cnt++)
            {
                listItem[cnt].gameObject.SetActive(false);
            }
            return;
        }
        int itemCount = listItem.Count;
        int sub = itemCount - datas.Count;
        //add item
        if (sub < 0)
        {
            for (int cnt = 0; cnt < System.Math.Abs(sub); cnt++)
            {
                TItem tItem = getItem();
                if (tItem == null)
                {
                    Debug.LogError("SetListData Get Item Is Null");
                    return;
                }
                listItem.Add(tItem);
            }
        }
        else if (sub > 0)
        {
            for (int cnt = datas.Count; cnt < listItem.Count; cnt++)
            {
                listItem[cnt].gameObject.SetActive(false);
            }
        }
        for (int cnt = 0; cnt < datas.Count; cnt++)
        {
            listItem[cnt].gameObject.SetActive(true);
            if (setItemData != null)
            {
                setItemData(listItem[cnt], datas[cnt], cnt);
            }
        }
    }
    public static void SetItemsDictionary<TK, TItem, TData>(Dictionary<TK, TItem> listItem, Dictionary<TK, TData> datas, System.Func<TItem> getItem, System.Action<TK, TItem, TData> setItemData, System.Func<TK> getTempKey) where TItem : Component
    {
        if (listItem == null || datas == null)
        {
            Log.Error(" List Item " + listItem + "   ListData " + datas);
            return;
        }
        //加入临时列表
        List<TItem> tempItems = new List<TItem>();
        foreach (var item in listItem)
        {
            tempItems.Add(item.Value);
        }
        //重新设置物品信息
        listItem.Clear();
        foreach (var item in datas)
        {
            TItem tItem = null;
            if (tempItems.Count > 0)
            {
                tItem = tempItems[0];
                tempItems.RemoveAt(0);
            }
            else
            {
                tItem = getItem();
            }
            if (tItem == null)
            {
                Debug.LogError("tItem Is Null  ");
                continue;
            }
            //设置数据
            NGUITools.SetActive(tItem.gameObject, true);
            setItemData(item.Key, tItem, item.Value);
            listItem.Add(item.Key, tItem);
        }
        if (getTempKey == null)
        {

            Log.Error("getTempKey Is Null  ");
            return;
        }
        //隐藏剩余的。
        for (int cnt = 0; cnt < tempItems.Count; cnt++)
        {
            TK key = getTempKey();
            if (!listItem.ContainsKey(key))
            {
                listItem.Add(key, tempItems[cnt]);
            }
            else
            {
                Log.Error(" Key Error  " + key);
            }
            NGUITools.SetActive(tempItems[cnt], false);
        }
    }


    public static RenderTexture GetTemporaryRT(RenderTexture rt, int width, int height, int depthBuffer)
    {
        if (rt == null)
        {
            rt = RenderTexture.GetTemporary(width, height, depthBuffer);
            //if (rt == null)
            //{
            //    rt = new RenderTexture(width, height, depthBuffer);
            //}
        }
        return rt;
    }

    public static void ReleaseTemporary(RenderTexture rt)
    {
        RenderTexture.ReleaseTemporary(rt);
    }
}

public enum EnumCountStrType
{
    /// <summary>
    /// x
    /// </summary>
    x,
    X,
    /// <summary>
    /// ：
    /// </summary>
    C,
    /// <summary>
    /// +
    /// </summary>
    ADD,
    None,
    /// <summary>
    /// *星号
    /// </summary>
    Asterisk,
}