using MFrameWork;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveEntityManager : ManagerBase
{
    public static string SaveKey = "MGT";
    public static string SaveKeyEntity = "MGTE";

    private StringBuilder _tempSb = new StringBuilder();
    private string _splitVerticalBar = "|";
    private string _splitSemicolon = ";";

    public bool HasSaveGame()
    {
        return ES3.KeyExists(SaveKey);
    }


    public void SaveAllEntity()
    {
        List<EntityAssembly> list = new List<EntityAssembly>();
        Dictionary<int, EntityAssembly> map = GameManager<EntityManager>.QGetOrAddMgr().GetAllDatas();
        foreach (var item in map)
        {
            list.Add(item.Value);
        }
        SaveAllEntity(list);
    }

    public void SaveAllEntity(List<EntityAssembly> list)
    {
        try
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Converters.Add(new Vec3Conv());
            settings.Converters.Add(new Vec2Conv());

            for (int cnt = 0; cnt < list.Count; cnt++)
            {
                _tempSb.Clear();
                //第一位为Entity
                EntityAssembly entity = list[cnt];
                _tempSb.Append(JsonConvert.SerializeObject(entity));
                _tempSb.Append(_splitVerticalBar);
                //其余为组件
                for (int i = 0; i < entity.ListDatas.Count; i++)
                {
                    _tempSb.Append(entity.ListDatas[i].GetType().ToString());
                    _tempSb.Append(_splitSemicolon);
                    _tempSb.Append(JsonConvert.SerializeObject(entity.ListDatas[i], settings));
                    _tempSb.Append(_splitVerticalBar);
                }
                _tempSb.Remove(_tempSb.Length - 1, 1);
                string strValue = _tempSb.ToString();
                Log.Info("save {0} ", strValue);
                ES3.Save<string>(SaveKeyEntity + cnt, strValue);
            }
            ES3.Save<int>(SaveKey, 1);
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }

    }

    public bool LoadAllEntity()
    {
        if (!HasSaveGame())
        {
            return false;
        }
        try
        {
            int index = 0;
            while (true)
            {
                string strEntityKey = SaveKeyEntity + index;
                if (!ES3.KeyExists(strEntityKey))
                {
                    break;
                }
                string strValue = ES3.Load<string>(strEntityKey);
                EntityAssembly entity = ConvertToEntity(strValue);
                if (entity != null)
                {
                    GameManager<EntityManager>.QGetOrAddMgr().AdditionKey(entity);
                }
                Log.Info(strValue);
                index++;
            }
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
        return false;
    }

    private static EntityAssembly ConvertToEntity(string strValue)
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        string[] strInfos = Utility.Xml.ParseString<string>(strValue, Utility.Xml.SplitVerticalBar);
        if (strInfos == null || strInfos.Length == 0)
        {
            return null;
        }
        EntityAssembly entity = JsonConvert.DeserializeObject<EntityAssembly>(strInfos[0]);
        for (int cnt = 1; cnt < strInfos.Length; cnt++)
        {
            string[] strAssys = Utility.Xml.ParseString<string>(strInfos[cnt], Utility.Xml.SplitSemicolon);
            if (strAssys == null || strAssys.Length != 2)
            {
                continue;
            }
            Type assyType = assembly.GetType(strAssys[0]);
            AssemblyBase assy = JsonConvert.DeserializeObject(strAssys[1], assyType) as AssemblyBase;
            if (assy != null)
            {
                EntityFactory.AssemblyAddBase(entity, assy);
                assy.ReadDataFinish();
            }
        }

        return entity;
    }

}
