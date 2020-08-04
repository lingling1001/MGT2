using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PrototypeHelper
{
    public static void LoadAllData()
    {
        //主界面
        LoadData<PrototypeMainUI>(GetConfig("MainUI"));
        //主界面样式
        LoadData<PrototypeMainUIStyle>(GetConfig("MainUIStyle"));
        //能力
        LoadData<PrototypeAbility>(GetConfig("Ability"));
        //角色
        LoadData<PrototypeRole>(GetConfig("Role"));
        //游戏配置
        LoadData<PrototypeGameConfig>(GetConfig("GameConfig"));
        //地图信息
        LoadData<PrototypeMap>(GetConfig("Map"));
        //属性
        LoadData<PrototypeAttribute>(GetConfig("Attribute"));
        //武器挂载信息
        LoadData<PrototypeLoadWeapon>(GetConfig("LoadWeapon"));
        //武器信息
        LoadData<PrototypeWeapon>(GetConfig("Weapon"));



    }

    /// <summary>
    /// 读取xml
    /// </summary>
    /// <param name="name">Name.</param>
    public static void LoadData<T>(string content) where T : BasePrototype
    {
        try
        {
            //从文件读取到xml
            XmlDocument xmlDoc = new XmlDocument();
            string xmlString = content;
            if (xmlString == null || xmlString.Length == 0)
            {
                Log.Warning("是不是版本表配置错误，没找到此表或文件格式不正确！");
                return;
            }
            xmlDoc.LoadXml(xmlString);
            Type refType = null;
            XmlNode node = null;
            //取出表依赖的类
            node = xmlDoc.FirstChild;
            node = node.NextSibling;
            string type = node.Attributes["type"].Value;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            refType = assembly.GetType(type);
            if (refType == null)
            {
                Log.Warning("name = " + type + " 此表可能为新加表，请加入解析类！");
                return;
            }
            //把同一张表所有数据记录
            Dictionary<int, T> dicTempList = new Dictionary<int, T>();
            //解析单条数据
            XmlNodeList nodeList = node.ChildNodes;
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode childNode = nodeList[i];
                T basePrototype = System.Activator.CreateInstance(refType) as T;
                basePrototype.LoadData(childNode);
                if (dicTempList.ContainsKey(basePrototype.PrototypeId))
                {
                    dicTempList[basePrototype.PrototypeId] = basePrototype;
                }
                else
                {
                    dicTempList.Add(basePrototype.PrototypeId, basePrototype);
                }
            }
            PrototypeManager<T>.Instance.Initial(refType, dicTempList);
        }
        catch (Exception ex)
        {
            Log.Error("配置表文件格式错啦！！！！！  name = " + typeof(T).ToString() + " " + ex.ToString());
        }
    }


    private static string GetConfig(string strValue)
    {
        string path = "Config/" + strValue + ".xml";
        UnityEngine.Object objData = PreLoadResHelper.Instance.LoadAsset(path);
        if (objData == null)
        {
            Log.Error(" Get Config Error " + strValue);
            return string.Empty;
        }
        return objData.ToString();
    }
}
