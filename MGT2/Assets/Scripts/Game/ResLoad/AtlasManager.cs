using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[MonoSingletonPath("AtlasManager")]
public class AtlasManager : MonoSingleton<AtlasManager>
{
    private List<string> _listAtlasNames;
    private Dictionary<string, SpriteAtlas> _mapAtlas = new Dictionary<string, SpriteAtlas>();

    public Sprite GetSprite(string strName)
    {
        string atlasName = GetAtlasNames(strName);
        if (string.IsNullOrEmpty(atlasName))
        {
            return null;
        }
        if (!_mapAtlas.ContainsKey(atlasName))
        {
            SpriteAtlas spriteAtlas = ResLoadHelper.LoadAsset<SpriteAtlas>(atlasName);
            if (spriteAtlas == null)
            {
                return null;
            }
            _mapAtlas.Add(atlasName, spriteAtlas);
        }
        return _mapAtlas[atlasName].GetSprite(strName);
    }


    private string GetAtlasNames(string strName)
    {
        if (_listAtlasNames == null)
        {
            _listAtlasNames = new List<string>();
            _listAtlasNames.Add("Archerskill");
        }
        for (int cnt = 0; cnt < _listAtlasNames.Count; cnt++)
        {
            if (strName.Contains(_listAtlasNames[cnt]))
            {
                return "Atlas/" + _listAtlasNames[cnt] + ".spriteatlas";
            }
        }
        return string.Empty;
    }

}
