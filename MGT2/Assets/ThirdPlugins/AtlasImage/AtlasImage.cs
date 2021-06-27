using System.Collections;
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

    protected AtlasImage()
       : base()
    {
    }

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
        sprite = LoadEditorSprite(spriteAtlas, spriteName);
#else
        sprite = spriteAtlas ? spriteAtlas.GetSprite(spriteName) : null;
#endif
    }

#if UNITY_EDITOR
    public static Sprite LoadEditorSprite(SpriteAtlas atlas, string sprName)
    {
        if (string.IsNullOrEmpty(sprName) || atlas == null)
        {
            return null;
        }
        Sprite neSp = atlas.GetSprite(sprName);
        if (Application.isPlaying)
        {
            return neSp;
        }
        Object[] objs = UnityEditor.U2D.SpriteAtlasExtensions.GetPackables(atlas);
        for (int cnt = 0; cnt < objs.Length; cnt++)
        {
            UnityEditor.DefaultAsset asset = objs[cnt] as UnityEditor.DefaultAsset;
            if (asset != null)
            {
                string path = UnityEditor.AssetDatabase.GetAssetPath(objs[cnt]);
                string[] strs = UnityEditor.AssetDatabase.FindAssets(sprName, new string[] { path });
                for (int i = 0; i < strs.Length; i++)
                {
                    string chPath = UnityEditor.AssetDatabase.GUIDToAssetPath(strs[i]);
                    Object[] chObjs = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(chPath);
                    for (int ch = 0; ch < chObjs.Length; ch++)
                    {
                        if (chObjs[ch].name == sprName)
                        {
                            return chObjs[ch] as Sprite;
                        }
                    }
                }
            }
        }
        return null;
    }
#endif

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
