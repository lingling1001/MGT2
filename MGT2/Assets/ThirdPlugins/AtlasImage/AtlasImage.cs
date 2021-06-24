﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


/// <summary>
/// Atlas image.
/// </summary>
public class AtlasImage : Image
{
    [SerializeField] private string m_SpriteName;
    [SerializeField] private SpriteAtlas m_SpriteAtlas;
    private string _lastSpriteName = "";


    /// <summary>Sprite Name. If there is no other sprite with the same name in the atlas, AtlasImage will display the default sprite.</summary>
    public string spriteName
    {
        get { return m_SpriteName; }
        set
        {
            if (m_SpriteName != value)
            {
                m_SpriteName = value;
                SetAllDirty();
            }
        }
    }

    /// <summary>SpriteAtlas. Get and set atlas assets created by AtlasMaker.</summary>
    public SpriteAtlas spriteAtlas
    {
        get { return m_SpriteAtlas; }
        set
        {
            if (m_SpriteAtlas != value)
            {
                m_SpriteAtlas = value;
                SetAllDirty();
            }
        }
    }

    /// <summary>
    /// Sets the material dirty.
    /// </summary>
    public override void SetMaterialDirty()
    {
        // Changing sprites from Animation.
        // If the "sprite" is changed by an animation or script, it will be reflected in the sprite name.
        if (_lastSpriteName == spriteName && sprite)
        {
            m_SpriteName = sprite.name.Replace("(Clone)", "");
        }

        if (_lastSpriteName != spriteName)
        {
            _lastSpriteName = spriteName;
            RefreshSprite();
        }

        base.SetMaterialDirty();
    }

    private void RefreshSprite()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(spriteName))
        {
            return;
        }
        Sprite spr = spriteAtlas.GetSprite(spriteName);
        Object[] objs = UnityEditor.U2D.SpriteAtlasExtensions.GetPackables(spriteAtlas);
        for (int cnt = 0; cnt < objs.Length; cnt++)
        {
            UnityEditor.DefaultAsset asset = objs[cnt] as UnityEditor.DefaultAsset;
            if (asset != null)
            {
                string path = UnityEditor.AssetDatabase.GetAssetPath(objs[cnt]);
                string realPath = string.Format("{0}/{1}.png", path, spriteName);
                sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(realPath);

            }
        }
#else
        sprite = spriteAtlas ? spriteAtlas.GetSprite(spriteName) : null;
#endif
    }


    protected AtlasImage()
        : base()
    {
    }

    /// <summary>
    /// Raises the populate mesh event.
    /// </summary>
    /// <param name="toFill">To fill.</param>
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        if (!overrideSprite)
        {
            toFill.Clear();
            return;
        }
        base.OnPopulateMesh(toFill);
    }
}