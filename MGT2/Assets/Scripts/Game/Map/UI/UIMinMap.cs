using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIMinMap : BaseUI
{
    private RenderTexture _renderTexture;
    private GameObject _objCamera;
    private Camera _cameraMin;
    public override void OnInit()
    {
        base.OnInit();
        GetBindComponents(ObjUI);

        GameObject obj = ResLoadHelper.LoadAsset<GameObject>(AssetsName.WORLD_MIN_MAP);
        _objCamera = NGUITools.AddChild(CameraManager.Instance.gameObject, obj);
        _cameraMin = _objCamera.GetComponentInChildren<Camera>();
        _renderTexture = UIHelper.GetTemporaryRT(_renderTexture, 512, 512, 16);
        _cameraMin.targetTexture = _renderTexture;

        m_RImg_Map.texture = _renderTexture;

    }


}
