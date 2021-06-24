using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class CreateRoleHelper
{

    public static StringBuilder _cacheSb = new StringBuilder();

    public static EnumGender GetRandomGender()
    {
        Random ran = RandomHelper.GetRandom();
        return (EnumGender)ran.Next(1, 3);
    }

    /// <summary>
    /// �����ȡIcon
    /// </summary>
    public static string GetRandomHeadIcon(EnumGender type)
    {
        int gender = (int)type;
        List<PrototypeHeadIcon> list = PrototypeManager<PrototypeHeadIcon>.Instance.GetTableList();
        int index;
        while (list.Count > 0)
        {
            index = RandomHelper.GetRandom().Next(0, list.Count);
            PrototypeHeadIcon data = list[index];
            if (data.Type != gender)
            {
                list.Remove(data);
                continue;
            }
            if (Contains(EnumSaveRole.Icon, data.Name))
            {
                list.Remove(data);
                continue;
            }
            return data.Name;
        }
        return string.Empty;
    }

    /// <summary>
    /// 1�� 2Ů
    /// </summary>
    public static string GetRandomName(EnumGender type)
    {
        _cacheSb.Clear();
        Random ran = RandomHelper.GetRandom();
        _cacheSb.Append(LastNameItems[ran.Next(0, LastNameItems.Length)]);
        int nameCount = ran.Next(1, 3);

        string[] strFirstName = type == EnumGender.Male ? MaleNameItems : FemaleNameItems;

        for (int cnt = 0; cnt < nameCount; cnt++)
        {
            _cacheSb.Append(strFirstName[ran.Next(0, strFirstName.Length)]);
        }
        return _cacheSb.ToString();
    }

    /// <summary>
    /// ����
    /// </summary>
    public static string[] LastNameItems = new[]{ "��", "Ǯ", "��", "��", "��", "��", "֣", "��", "��", "��", "��", "��",
          "��","��", "��", "��", "��", "��", "��", "��", "��", "��", "ʩ", "��", "��", "��", "��", "��", "��", "κ",
          "��", "��", "��", "л", "��", "��", "��", "ˮ", "�", "��", "��", "��", "��", "��", "��", "��", "��",
          "��", "³", "Τ", "��", "��", "��", "��", "��", "��", "��", "Ԭ", "��", "��", "ʷ", "��", "��", "Ѧ",
          "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "Ԫ", "��", "��",
          "ƽ", "��", "��", "��", "��", "Ҧ", "��", "տ", "��", "��", "ë", "��", "��", "��", "��", "��", "̸",
          "��", "é", "��", "��", "��", "��", "��", "��", "ף", "��", "��", "��", "��", "��", "��", "��", "��",
          "·", "¦", "��", "ͯ", "��", "��", "÷", "ʢ", "��", "��", "��", "��", "��", "��", "��", "��", "��",
          "��", "��", "��", "��", "��", "��", "֧", "��", "��", "¬", "Ī", "��", "��", "��", "��", "��", "Ӧ",
          "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "ʯ", "��", "��", "��", "��", "��", "��",
          "��", "½", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��",
          "��", "��", "��", "ղ", "��", "Ҷ", "��", "˾", "��", "��", "ӡ", "��", "��", "ۢ", "��", "��", "��",
          "׿", "��", "��", "��", "��", "��", "ݷ", "��", "��", "̷", "��", "��", "��", "��", "��", "��", "��",
          "Ƚ", "��", "Ӻ", "ɣ", "��", "ͨ", "��", "��", "��", "ũ", "��", "��", "ׯ", "��", "��", "��", "��",
          "��", "ϰ", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��",
          "��", "��", "»", "��", "��", "ŷ", "��", "ʦ", "��", "��", "��", "��", "˾��", "�Ϲ�", "ŷ��", "�ĺ�",
          "���", "����", "����", "����", "�ʸ�", "ξ��", "����", "�̨", "��ұ", "����", "���", "����", "����",
          "̫��", "����", "����", "����", "��ԯ", "���", "����", "����", "����", "Ľ��", "˾ͽ", "˾��" };
    /// <summary>
    /// ��
    /// </summary>
    public static string[] MaleNameItems = new[] { "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��",
        "��", "��", "׳", "��", "Ⱥ", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��",
        "��", "ԣ", "��", "��", "��", "��", "��", "��", "��", "ǫ", "��", "��", "��", "֮", "��", "��", "��", "��",
        "��", "��", "��", "��", "��", "��", "��", "��", "ά", "��", "��", "��", "��", "��", "��", "��", "��", "��",
        "ʿ", "��", "��", "��", "��", "��", "��", "��", "��", "ʱ", "̩", "ʢ", "��", "�", "��", "��", "��", "��",
        "ΰ", "��", "��", "��", "��", "��", "ǿ", "��", "ƽ", "��", "��", "��", "��", "��", "��", "��", "��", "��",
        "��", "־", "��", "��", "��", "��", "ɽ", "��", "��", "��", "��", "��", "��", "��", "Ԫ", "ȫ", "��", "ʤ",
        "ѧ", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "ï", "��", "��", "��", "��",
        "��", "��", "��", "˳", "��", "��", "��", "�", "��", "��", "��", "��" };

    /// <summary>
    /// Ů
    /// </summary>
    public static string[] FemaleNameItems = new[] { "��", "��", "��", "�", "Ҷ", "�", "�", "�", "��", "��", "��",
        "��","��", "ɺ", "ɯ", "��", "��", "��", "ٻ", "��", "�", "��", "�", "�", "ӱ", "¶", "��", "��", "�", "��",
        "��", "��", "��", "��", "��", "��", "ü", "��", "��", "��", "ޱ", "ݼ", "��", "�", "Է", "�", "ܰ", "�",
        "��", "��", "��", "԰", "��", "ӽ", "��", "��", "��", "��", "ع", "��", "��", "��", "ˬ", "��", "��", "��",
        "ϣ", "��", "��", "Ʈ", "��", "��", "�", "��", "��", "��", "��", "��", "��", "��", "��", "��", "ܿ", "��",
        "��", "��", "��", "��", "��", "��", "��", "Ӱ", "��", "֦", "˼", "��", "��", "��", "Ӣ", "��", "��", "��",
        "��", "��", "��", "��", "��", "��", "��", "��", "֥", "��", "Ƽ", "��", "��", "��", "��", "��", "��", "��",
        "��", "��", "��", "��", "��", "��", "��", "��", "��", "÷", "��", "��", "��", "��", "��", "��", "ѩ", "��",
        "��", "��", "ϼ", "��", "��", "ݺ", "��", "��", "��", "��", "��","��" };


    private static Dictionary<int, List<string>> _mapRoleInfos = new Dictionary<int, List<string>>();

    public static bool Addition(EnumSaveRole type, string strName)
    {
        int key = (int)type;
        if (!_mapRoleInfos.ContainsKey(key))
        {
            _mapRoleInfos.Add(key, new List<string>());
        }
        if (_mapRoleInfos[key].Contains(strName))
        {
            return false;
        }
        _mapRoleInfos[key].Add(strName);
        return true;
    }
    public static void Remove(EnumSaveRole type, string strName)
    {
        int key = (int)type;
        if (_mapRoleInfos.ContainsKey(key))
        {
            _mapRoleInfos[key].Remove(strName);
        }
    }
    public static bool Contains(EnumSaveRole type, string strName)
    {
        int key = (int)type;
        if (_mapRoleInfos.ContainsKey(key))
        {
            return _mapRoleInfos[key].Contains(strName);
        }
        return false;
    }

    public static void ClearRoleInfos()
    {
        _mapRoleInfos.Clear();
    }
    public static void ClearRoleInfos(EnumSaveRole type)
    {
        int key = (int)type;
        if (_mapRoleInfos.ContainsKey(key))
        {
            _mapRoleInfos[key].Clear();
        }
    }
}
public enum EnumGender : byte
{
    None = 0,
    /// <summary>
    /// ��
    /// </summary>
    Male = 1,
    /// <summary>
    /// Ů
    /// </summary>
    Female = 2,
}
public enum EnumSaveRole : byte
{
    Name = 1,
    Icon = 2,
}