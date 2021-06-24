using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public static class EnumExtension
{
    /// <summary>
    /// 获取枚举的描述信息
    /// </summary>
    public static string GetDescriptionUIName(this Enum em)
    {
        Type type = em.GetType();
        FieldInfo fd = type.GetField(em.ToString());
        if (fd == null)
            return string.Empty;
        object[] attrs = fd.GetCustomAttributes(typeof(AttributeUIName), false);
        string name = string.Empty;
        foreach (AttributeUIName attr in attrs)
        {
            name = attr.Name;
        }
        return name;
    }

   

}

public class AttributeUIName : Attribute
{
    public string Name;
    public AttributeUIName(string v)
    {
        this.Name = v;
    }
}