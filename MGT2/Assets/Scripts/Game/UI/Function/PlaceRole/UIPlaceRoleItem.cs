using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlaceRoleItem : PrototypeItemClickBase<UIPlaceRoleItem, PrototypeRole>
{
    [SerializeField] private RectTransform modelParent;
    [SerializeField] private Image imgQual;
    [SerializeField] private RawImage rawImgRole;
    [SerializeField] private Camera modelCamera;
    [SerializeField] private TextMeshProUGUI TexName;
    private CacheGameObjectActiveMap _mapObj = new CacheGameObjectActiveMap();
    private RenderTexture _renderTexture = null;
    public AssemblyRole EntityData { get; private set; }
    public override void SetData(PrototypeRole data)
    {
        base.SetData(data);
        Color color = UIHelper.GetQualColor(Data.Quality);
        imgQual.color = new Color(color.r, color.g, color.b, 0.35f);
        GetRenderTexture();
        _mapObj.SetActiveState(false, data.Path);
        UIHelper.SetText(TexName, data.Name);
        if (_mapObj.ContainsObject(data.Path))
        {
            _mapObj.SetActive(data.Path, true);
            return;
        }
        ResLoadHelper.LoadAssetInstantiateAsync(Data.Path, EventLoadFinish, modelParent);
    }
    public void SetData(AssemblyRole entity)
    {
        EntityData = entity;
        SetData(entity.AssyPrototypeRole.Value);
    }
    private void EventLoadFinish(GameObject obj)
    {
        obj.SetLayerRecursively(LayerMask.NameToLayer(DefineLayer.UI_MODEL));
        obj.transform.rotation = Quaternion.Euler(Data.UIRotate);
        obj.transform.localPosition = Data.UIPosition;
        _mapObj.Add(Data.Path, obj);
    }

    private RenderTexture GetRenderTexture()
    {
        if (_renderTexture == null)
        {
            _renderTexture = RenderTexture.GetTemporary(512, 512, 16);
            rawImgRole.texture = _renderTexture;
            modelCamera.targetTexture = _renderTexture;
        }
        return _renderTexture;
    }
    private void OnDestroy()
    {
        if (_renderTexture != null)
        {
            RenderTexture.ReleaseTemporary(_renderTexture);
        }
    }
}
