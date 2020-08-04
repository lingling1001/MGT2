using MFrameWork;
using SimpleFramework;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class BasePrototype
{
    //表中属性
    public int PrototypeId { get; protected set; }
    public string Name { get; protected set; }

    public void LoadData(XmlNode data)
    {
        PrototypeId = Utility.Xml.GetAttribute<int>(data, "Id");
        Name = Utility.Xml.GetAttribute<string>(data, "Name");
        OnLoadData(data);
    }

    protected abstract void OnLoadData(XmlNode data);

    public Vector3 GetAttribut_vector3(XmlNode data, string attributeName)
    {
        string vecStr = Utility.Xml.GetAttribute<string>(data, attributeName);
        if (string.IsNullOrEmpty(vecStr))
        {
            return Vector3.zero;
        }
        string[] splitStr = vecStr.Split(new char[] { ',', '，' });
        if (splitStr == null || splitStr.Length != 3)
        {
            return Vector3.zero;
        }
        Vector3 vec = new Vector3(float.Parse(splitStr[0]), float.Parse(splitStr[1]), float.Parse(splitStr[2]));
        return vec;
    }

    protected List<int> GetPrototypeList(XmlNode data, string key, char splitKey)
    {
        string str = Utility.Xml.GetAttribute<string>(data, key);
        if (string.IsNullOrEmpty(str))
        {
            return null;
        }
        string[] strList = str.Split(splitKey);
        if (strList == null)
        {
            return null;
        }
        List<int> list = new List<int>();
        for (int i = 0; i < strList.Length; i++)
        {
            list.Add(int.Parse(strList[i]));
        }
        return list;
    }




    /// <summary>
    /// 解析'|'和','号
    /// </summary>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    protected Dictionary<int, int> GetPrototypeIntDictionary(XmlNode data, string key)
    {
        Dictionary<int, int> tempDic = new Dictionary<int, int>();
        string strTmp;
        if (data.Attributes[key] != null)
        {
            strTmp = data.Attributes[key].Value;
            if (!string.IsNullOrEmpty(strTmp))
            {
                string[] arr = strTmp.Split('|');
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] a = arr[i].Split(',');
                    tempDic.Add(int.Parse(a[0]), int.Parse(a[1]));
                }
            }
        }

        return tempDic;
    }



}
